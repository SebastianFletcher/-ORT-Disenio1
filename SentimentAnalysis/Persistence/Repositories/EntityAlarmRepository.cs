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
    public class EntityAlarmRepository : IRepository<EntityAlarm>
    {
        public void Add(EntityAlarm alarm)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                if (context.EntityAlarms.Any(a => a.Id == alarm.Id))
                    throw new AlarmException("An Alarm with the same ID already exists.");

                Exists(alarm);

                alarm.AnalyzePhrases(Helper.Instance.GetPhrases(context.Phrases));
                Entities.EntityAlarm toAdd = Helper.Instance.ToEntityAlarmEF(alarm);
                context.EntityAlarms.Add(toAdd);

                ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(toAdd.Entity, toAdd.Entity.Id != alarm.Entity.Id ? EntityState.Modified : EntityState.Unchanged);
                
                context.SaveChanges();
                alarm.Id = toAdd.Id;
            }
        }

        public void Modify(int? id, EntityAlarm alarm)
        {
            if (!id.HasValue)
                throw new AlarmException("You must select an Alarm to modify.");

            EntityAlarm oldAlarm = Get(alarm.Id);
            AlarmRepository alarmRepository = new AlarmRepository();

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                var toUpdate = context.EntityAlarms.FirstOrDefault(a => a.Id == id);

                if (id != alarm.Id && 
                    (oldAlarm.Type != alarm.Type || oldAlarm.Entity.Id != alarm.Entity.Id || !oldAlarm.PostQuantity.Equals(alarm.PostQuantity) || !oldAlarm.Time.Equals(alarm.Time)))
                    Exists(alarm);

                alarm.ReAnalyePhrases(Helper.Instance.GetPhrases(context.Phrases));

                toUpdate.Alarm.PostQuantity = Int32.Parse(alarm.PostQuantity);
                toUpdate.Alarm.PostCount = alarm.PostCount;
                toUpdate.Alarm.Time = Int32.Parse(alarm.Time);
                toUpdate.Alarm.Type = alarm.Type;
                toUpdate.Entity = alarm.Entity != null ? context.Entities.AsNoTracking().ToList().First(e => e.Id == alarm.Entity.Id) : null;

                alarmRepository.Modify(toUpdate.Alarm.Id, toUpdate.Alarm);

                context.EntityAlarms.AddOrUpdate(toUpdate);

                ObjectStateEntry entityEntry;
                ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.TryGetObjectStateEntry(toUpdate.Entity, out entityEntry);

                if (entityEntry != null)
                    ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(toUpdate.Entity, toUpdate.Entity.Id != alarm.Entity.Id ? EntityState.Modified : EntityState.Unchanged);

                context.SaveChanges();
            }
        }

        public void Delete(int? id)
        {
            if (!id.HasValue)
                throw new AlarmException("You must select an Alarm to delete.");

            AlarmRepository alarmRepository = new AlarmRepository();

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.EntityAlarm toRemove = context.EntityAlarms.FirstOrDefault(a => a.Id == id);

                if (toRemove == null)
                    throw new AlarmException("Alarm not found.");

                var idAlarm = toRemove.Alarm.Id;
                context.EntityAlarms.Remove(toRemove);

                context.SaveChanges();

                alarmRepository.Delete(idAlarm);
            }
        }

        public EntityAlarm Get(int id)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.EntityAlarm alarm = context.EntityAlarms.FirstOrDefault(a => a.Id == id);

                if (alarm == null)
                    throw new AlarmException("Alarm not found.");

                return Helper.Instance.ToEntityAlarmBL(alarm);
            }
        }

        public IEnumerable<EntityAlarm> GetAll()
        {
            try
            {
                using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                    return Helper.Instance.GetEntityAlarms(context.EntityAlarms);
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
                return !context.EntityAlarms.Any();
        }

        public void Clear()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                context.EntityAlarms.RemoveRange(context.EntityAlarms);
                context.SaveChanges();
            }
        }

        public void Exists(EntityAlarm toFind)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                if (GetAll().Any(a => a.Equals(toFind) && a.Time.Equals(toFind.Time)))
                    throw new AlarmException($"The Alarm {toFind} already exists.");
        }
    }
}
