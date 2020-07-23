using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace Persistence.Repositories
{
    public class PhraseRepository : IRepository<Phrase>
    {
        public void Add(Phrase phrase)
        {
            EntityAlarmRepository alarmEntityRepository = new EntityAlarmRepository();
            AuthorAlarmRepository authorAlarmRepository = new AuthorAlarmRepository();
            PhraseRepository phraseRepository = new PhraseRepository();

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                if (context.Phrases.Any(p => p.Id == phrase.Id))
                    throw new PhraseException("An Phrase with the same ID already exists.");

                Exists(phrase);

                try
                {
                    phrase.AnalyzePhrase(Helper.Instance.GetEntities(context.Entities), Helper.Instance.GetSentiments(context.Sentiments));
                }
                catch (AnalysisException aex)
                {
                    throw aex;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    Entities.Phrase toAdd = Helper.Instance.ToPhraseEF(phrase);
                    context.Phrases.Add(toAdd);

                    ObjectStateEntry authorEntry = null;
                    ObjectStateEntry entityEntry = null;

                    if (toAdd.Author != null)
                        ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.TryGetObjectStateEntry(toAdd.Author, out authorEntry);

                    if (toAdd.Entity != null)
                        ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.TryGetObjectStateEntry(toAdd.Entity, out entityEntry);

                    if (authorEntry != null)
                        ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(toAdd.Author, EntityState.Unchanged);

                    if (entityEntry != null)
                        ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(toAdd.Entity, toAdd.Entity.Id == phrase.Entity.Id ? EntityState.Modified : EntityState.Unchanged);

                    context.SaveChanges();
                    phrase.Id = toAdd.Id;

                    try
                    {
                        foreach (var alarm in Helper.Instance.GetEntityAlarms(context.EntityAlarms).Where(a => !a.IsEnabled() && a.Type == phrase.Type && a.Entity.Id == phrase.Entity.Id))
                        {
                            alarm.AnalyzePhrases(Helper.Instance.GetPhrases(context.Phrases));
                            alarmEntityRepository.Modify(alarm.Id, alarm);
                        }
                    }
                    catch (AnalysisException) { }

                    try
                    {
                        foreach (var alarm in Helper.Instance.GetAuthorAlarms(context.AuthorAlarms).Where(a => !a.IsEnabled() && a.Type == phrase.Type))
                        {
                            alarm.AnalyzePhrases(Helper.Instance.GetPhrases(context.Phrases));
                            authorAlarmRepository.Modify(alarm.Id, alarm);
                        }
                    }
                    catch (AnalysisException) { }
                }
            }
        }

        public void Modify(int? id, Phrase phrase)
        {
            if (!id.HasValue)
                throw new PhraseException("You must select an Phrase to modify.");

            EntityAlarmRepository entityAlarmRepository = new EntityAlarmRepository();
            AuthorAlarmRepository authorAlarmRepository = new AuthorAlarmRepository();

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Phrase phraseFound = Get(id.Value);

                if (!phraseFound.Word.Equals(phrase.Word))
                    Exists(phrase);

                phraseFound.Word = phrase.Word;
                phraseFound.PostedDate = phrase.PostedDate;
                phraseFound.Author = phrase.Author;
                phraseFound.Type = null;
                phraseFound.Entity = null;

                try
                {
                    phraseFound.AnalyzePhrase(Helper.Instance.GetEntities(context.Entities), Helper.Instance.GetSentiments(context.Sentiments));
                }
                catch (AnalysisException) { }

                var toUpdate = context.Phrases.FirstOrDefault(p => p.Id == id);
                toUpdate.Word = phraseFound.Word;
                toUpdate.PostedDate = phraseFound.PostedDate;
                toUpdate.Author = context.Authors.AsNoTracking().ToList().First(a => a.Id == phraseFound.Author.Id);
                toUpdate.Type = phraseFound.Type;
                toUpdate.Entity = phraseFound.Entity != null ? context.Entities.AsNoTracking().ToList().First(e => e.Id == phraseFound.Entity.Id) : null;
                toUpdate.Grade = phraseFound.Grade;

                context.Phrases.AddOrUpdate(toUpdate);

                ObjectStateEntry authorEntry = null;
                ObjectStateEntry entityEntry = null;

                if (toUpdate.Author != null)
                    ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.TryGetObjectStateEntry(toUpdate.Author, out authorEntry);

                if (toUpdate.Entity != null)
                    ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.TryGetObjectStateEntry(toUpdate.Entity, out entityEntry);

                if (authorEntry != null)
                    ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(toUpdate.Author, toUpdate.Author.Id == phraseFound.Author.Id ? EntityState.Modified : EntityState.Unchanged);

                if (entityEntry != null)
                    ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(toUpdate.Entity, (phraseFound.Entity == null || toUpdate.Entity.Id == phraseFound.Entity.Id) ? EntityState.Modified : EntityState.Unchanged);

                context.SaveChanges();

                foreach (var alarm in Helper.Instance.GetEntityAlarms(context.EntityAlarms))
                    entityAlarmRepository.Modify(alarm.Id, alarm);

                foreach (var alarm in Helper.Instance.GetAuthorAlarms(context.AuthorAlarms))
                    authorAlarmRepository.Modify(alarm.Id, alarm);
            }
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new PhraseException("You must select an Phrase to delete.");

            EntityAlarmRepository entityAlarmRepository = new EntityAlarmRepository();
            AuthorAlarmRepository authorAlarmRepository = new AuthorAlarmRepository();

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.Phrase toRemove = context.Phrases.FirstOrDefault(p => p.Id == id);

                if (toRemove == null)
                    throw new PhraseException("Phrase not found.");

                context.Phrases.Remove(toRemove);
                context.SaveChanges();

                foreach (var alarm in Helper.Instance.GetEntityAlarms(context.EntityAlarms))
                {
                    alarm.ReAnalyePhrases(Helper.Instance.GetPhrases(context.Phrases));
                    entityAlarmRepository.Modify(alarm.Id, alarm);
                }

                foreach (var alarm in Helper.Instance.GetAuthorAlarms(context.AuthorAlarms))
                {
                    alarm.ReAnalyePhrases(Helper.Instance.GetPhrases(context.Phrases));
                    authorAlarmRepository.Modify(alarm.Id, alarm);
                }
            }
        }

        public bool IsEmpty()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                return !context.Phrases.Any();
        }

        public IEnumerable<Phrase> GetAll()
        {
            try
            {
                using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                    return Helper.Instance.GetPhrases(context.Phrases);
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

        public Phrase Get(int id)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.Phrase phrase = context.Phrases.AsNoTracking().ToList().FirstOrDefault(p => p.Id == id);

                if (phrase == null)
                    throw new PhraseException("Phrase not found.");

                return Helper.Instance.ToPhraseBL(phrase);
            }
        }

        public void Clear()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                context.Phrases.RemoveRange(context.Phrases);
                context.SaveChanges();
            }
        }

        public void Exists(Phrase toFind)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                if (GetAll().Any(p => p.Equals(toFind)))
                    throw new PhraseException($"The Phrase {toFind} already exists.");
        }
    }
}
