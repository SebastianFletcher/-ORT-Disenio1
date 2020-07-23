using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Persistence.Repositories
{
    public class EntityRepository : IRepository<Entity>
    {
        public void Add(Entity entity)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                if (context.Entities.Any(e => e.Id == entity.Id))
                    throw new EntityException("An Entity with the same ID already exists.");

                Exists(entity);

                Entities.Entity toAdd = Helper.Instance.ToEntityEF(entity);
                context.Entities.Add(toAdd);
                context.SaveChanges();
                entity.Id = toAdd.Id;

                foreach (var phraseBD in context.Phrases.Where(p => p.Entity == null))
                {
                    PhraseRepository phraseRepository = new PhraseRepository();
                    var phrase = Helper.Instance.ToPhraseBL(phraseBD);

                    try
                    {
                        phrase.AnalyzePhrase(Helper.Instance.GetEntities(context.Entities), Helper.Instance.GetSentiments(context.Sentiments));
                    }
                    catch (AnalysisException) { }

                    phraseRepository.Modify(phrase.Id, phrase);
                }
            }
        }

        public void Modify(int? id, Entity entity)
        {
            if (!id.HasValue)
                throw new EntityException("You must select an Entity to modify.");

            PhraseRepository phraseRepository = new PhraseRepository();
            EntityAlarmRepository alarmRepository = new EntityAlarmRepository();

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                var entityFound = context.Entities.FirstOrDefault(e => e.Id == id);

                if (id != entity.Id || !entityFound.Name.Equals(entity.Name))
                    Exists(entity);

                entityFound.Name = entity.Name;

                context.SaveChanges();

                foreach (var phrase in context.Phrases.Where(p => p.Entity == null || p.Entity.Id == entityFound.Id))
                {
                    phrase.Entity = null;

                    try
                    {
                        Phrase toModify = Helper.Instance.ToPhraseBL(phrase);
                        toModify.AnalyzePhrase(Helper.Instance.GetEntities(context.Entities), Helper.Instance.GetSentiments(context.Sentiments));
                        phraseRepository.Modify(phrase.Id, toModify);
                    }
                    catch (AnalysisException) { }
                }

                foreach (var alarm in context.EntityAlarms.ToList())
                {
                    EntityAlarm toModify = Helper.Instance.ToEntityAlarmBL(alarm);
                    toModify.ReAnalyePhrases(Helper.Instance.GetPhrases(context.Phrases));
                    alarmRepository.Modify(alarm.Alarm.Id, toModify);
                }
            }

        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new EntityException("You must select an Entity to delete.");

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.Entity toRemove = context.Entities.FirstOrDefault(e => e.Id == id);

                if (toRemove == null)
                    throw new EntityException("Entity not found.");

                context.Entities.Remove(toRemove);

                foreach (var phrase in context.Phrases.Where(p => p.Entity != null && p.Entity.Id == toRemove.Id))
                    phrase.Entity = null;

                List<Entities.EntityAlarm> alarmsToDelete = new List<Entities.EntityAlarm>();
                foreach (var alarm in context.EntityAlarms.Where(a => a.Entity.Id == toRemove.Id))
                    alarmsToDelete.Add(alarm);

                foreach (var alarm in alarmsToDelete)
                    context.EntityAlarms.Remove(alarm);

                context.SaveChanges();
            }
        }

        public Entity Get(int id)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.Entity entity = context.Entities.FirstOrDefault(e => e.Id == id);

                if (entity == null)
                    throw new EntityException("Entity not found.");

                return Helper.Instance.ToEntityBL(entity);
            }
        }

        public IEnumerable<Entity> GetAll()
        {
            try
            {
                using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                    return Helper.Instance.GetEntities(context.Entities);
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

        public bool IsEmpty()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                return !context.Entities.Any();
        }

        public void Clear()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                context.Entities.RemoveRange(context.Entities);
                context.SaveChanges();
            }
        }

        public void Exists(Entity toFind)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                if (GetAll().Any(e => e.Equals(toFind)))
                    throw new EntityException($"The entity {toFind} already exists.");
        }
    }
}
