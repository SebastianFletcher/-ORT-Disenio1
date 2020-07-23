using BusinessLogic.DTO;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace Persistence.Repositories
{
    public class SentimentRepository : IRepository<Sentiment>
    {
        public void Add(Sentiment sentiment)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                if (context.Sentiments.Any(s => s.Id == sentiment.Id))
                    throw new SentimentException("An Sentiment with the same ID already exists.");

                Exists(sentiment);

                Entities.Sentiment toAdd = Helper.Instance.ToSentimentEF(sentiment);
                context.Sentiments.Add(toAdd);
                context.SaveChanges();
                sentiment.Id = toAdd.Id;

                PhraseRepository phraseRepository = new PhraseRepository();
                EntityAlarmRepository alarmRepository = new EntityAlarmRepository();

                foreach (var phrase in Helper.Instance.GetPhrases(context.Phrases))
                {
                    try
                    {
                        phraseRepository.Modify(phrase.Id, phrase);
                    }
                    catch (AnalysisException) { }
                }

                foreach (var alarm in Helper.Instance.GetEntityAlarms(context.EntityAlarms).Where(a => a.Type == sentiment.Type && !a.IsEnabled()))
                    alarmRepository.Modify(alarm.Id, alarm);
            }
        }

        public void Modify(int? id, Sentiment sentiment)
        {
            if (!id.HasValue)
                throw new SentimentException("You must select an Sentiment to modify.");

            PhraseRepository phraseRepository = new PhraseRepository();
            EntityAlarmRepository alarmRepository = new EntityAlarmRepository();

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                var sentimentFound = context.Sentiments.FirstOrDefault(s => s.Id == id);

                if (id != sentiment.Id || !sentimentFound.Word.Equals(sentiment.Word))
                    Exists(sentiment);

                SentimentType oldType = sentimentFound.Type;

                sentimentFound.Word = sentiment.Word;
                sentimentFound.Type = sentiment.Type;

                context.Sentiments.AddOrUpdate(sentimentFound);
                context.SaveChanges();

                foreach (var phrase in Helper.Instance.GetPhrases(context.Phrases).Where(p => p.Type == null || p.Type == oldType))
                {
                    phrase.Type = null;
                    phrase.Entity = null;

                    try
                    {
                        phrase.AnalyzePhrase(Helper.Instance.GetEntities(context.Entities), Helper.Instance.GetSentiments(context.Sentiments));
                        phraseRepository.Modify(phrase.Id, phrase);
                    }
                    catch (AnalysisException)
                    {
                        phraseRepository.Modify(phrase.Id, phrase);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }

                foreach (var alarm in Helper.Instance.GetEntityAlarms(context.EntityAlarms))
                {
                    alarm.ReAnalyePhrases(Helper.Instance.GetPhrases(context.Phrases));
                    alarmRepository.Modify(alarm.Id, alarm);
                }
            }
        }

        public void Delete(int? id)
        {
            if (!id.HasValue)
                throw new SentimentException("You must select an Sentiment to delete.");

            EntityAlarmRepository alarmRepository = new EntityAlarmRepository();

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.Sentiment toRemove = context.Sentiments.FirstOrDefault(s => s.Id == id);

                if (toRemove == null)
                    throw new SentimentException("Sentiment not found.");

                context.Sentiments.Remove(toRemove);
                context.SaveChanges();

                foreach (var phrase in context.Phrases.Where(p => p.Type != null && p.Type == toRemove.Type))
                {
                    phrase.Type = null;
                }

                foreach (var alarm in context.EntityAlarms.ToList())
                {
                    EntityAlarm toModify = Helper.Instance.ToEntityAlarmBL(alarm);

                    try
                    {
                        alarmRepository.Modify(alarm.Id, toModify);
                    }
                    catch { }
                }
            }
        }

        public bool IsEmpty()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                return !context.Sentiments.Any();
        }

        public IList<Sentiment> GetBySentimentType(SentimentType type)
        {
            try
            {
                return GetAll().Where(s => s.Type == type).ToList();
            }
            catch (SqlException)
            {
                throw new DatabaseException();
            }
            catch (DbException)
            {
                throw new DatabaseException();
            }
            catch (EntityException)
            {
                throw new DatabaseException();
            }
            catch (InvalidOperationException)
            {
                throw new DatabaseException();
            }
        }

        public Sentiment Get(int id)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.Sentiment sentiment = context.Sentiments.FirstOrDefault(s => s.Id == id);

                if (sentiment == null)
                    throw new SentimentException("Sentiment not found.");

                return Helper.Instance.ToSentimentBL(sentiment);
            }
        }

        public IEnumerable<Sentiment> GetAll()
        {
            try
            {
                using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                    return Helper.Instance.GetSentiments(context.Sentiments);
            }
            catch (SqlException)
            {
                throw new DatabaseException();
            }
            catch (DbException)
            {
                throw new DatabaseException();
            }
            catch (EntityException)
            {
                throw new DatabaseException();
            }
            catch (InvalidOperationException)
            {
                throw new DatabaseException();
            }
        }

        public void Clear()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                context.Sentiments.RemoveRange(context.Sentiments);
                context.SaveChanges();
            }
        }

        public void Exists(Sentiment toFind)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                if (GetAll().Any(p => p.Equals(toFind)))
                    throw new SentimentException($"The Sentiment {toFind} already exists.");
        }
    }
}
