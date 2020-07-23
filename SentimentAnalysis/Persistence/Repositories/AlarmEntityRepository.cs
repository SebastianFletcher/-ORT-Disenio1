using BusinessLogic.DTO;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Persistence.Repositories
{
    public class AlarmEntityRepository : IRepository<AlarmEntity>
    {
        private Utils.Helper helper;

        public AlarmEntityRepository()
        {
            helper = new Utils.Helper();
        }

        public void Add(AlarmEntity alarm)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                if (context.EntityAlarms.Any(a => a.Id == alarm.Id))
                    throw new AlarmException("An Alarm with the same ID already exists.");

                ExistsAlarm(alarm.Entity, alarm.NumberDays, alarm.PostQuantity, alarm.Type);

                alarm.AnalyzePhrases(helper.GetPhrases(context.Phrases));
                Entities.AlarmEntity toAdd = helper.ToAlarmEntityEF(alarm);
                context.EntityAlarms.Add(toAdd);

                ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeObjectState(toAdd.Entity, toAdd.Entity.Id != alarm.Entity.Id ? EntityState.Modified : EntityState.Unchanged);

                context.SaveChanges();
                alarm.Id = toAdd.Id;
            }
        }

        public void Modify(int? id, AlarmEntity alarm)
        {
            if (!id.HasValue)
                throw new AlarmException("You must select an Phrase to modify.");

            AlarmEntity oldAlarm = Get(alarm.Id);

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                var toUpdate = context.EntityAlarms.FirstOrDefault(a => a.Id == id);

                if (id != alarm.Id ||
                    oldAlarm.Type != alarm.Type ||
                    oldAlarm.Entity.Id != alarm.Entity.Id ||
                    !oldAlarm.PostQuantity.Equals(alarm.PostQuantity) ||
                    !oldAlarm.NumberDays.Equals(alarm.NumberDays)
                    )
                    ExistsAlarm(alarm.Entity, alarm.NumberDays, alarm.PostQuantity, alarm.Type);

                alarm.ReAnalyePhrases(helper.GetPhrases(context.Phrases));

                toUpdate.PostQuantity = Int16.Parse(alarm.PostQuantity);
                toUpdate.PostCount = alarm.PostCount;
                toUpdate.NumberDays = Int16.Parse(alarm.NumberDays);
                toUpdate.Type = alarm.Type;
                toUpdate.Entity = alarm.Entity != null ? context.Entities.AsNoTracking().ToList().First(e => e.Id == alarm.Entity.Id) : null;

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
            if (id == null)
                throw new AlarmException("You must select an Alarm to delete.");

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.AlarmEntity toRemove = context.EntityAlarms.FirstOrDefault(a => a.Id == id);

                if (toRemove == null)
                    throw new AlarmException("Alarm not found.");

                context.EntityAlarms.Remove(toRemove);

                context.SaveChanges();
            }
        }

        public AlarmEntity Get(int id)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.AlarmEntity alarm = context.EntityAlarms.FirstOrDefault(a => a.Id == id);

                if (alarm == null)
                    throw new AlarmException("Alarm not found.");

                return helper.ToAlarmEntityBL(alarm);
            }
        }

        public IEnumerable<AlarmEntity> GetAll()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                IEnumerable<Entities.AlarmEntity> alarms = context.EntityAlarms.ToList();
                return alarms.Select(a => helper.ToAlarmEntityBL(a));
            }
        }

        public bool IsEmpty()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                return !context.EntityAlarms.Any();
        }

        private void ExistsAlarm(Entity entity, string numbDays, string postQuantity, SentimentType type)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                if (helper.GetEntityAlarms(context.EntityAlarms).Any(a => a.Entity.Id == entity.Id && a.NumberDays.Equals(numbDays) && a.PostQuantity.Equals(postQuantity) && a.Type == type))
                    throw new AlarmException($"Already exist an Alarm for {entity.Name}, with {numbDays} days and Sentiment {type}");
        }

        public void Clear()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                context.EntityAlarms.RemoveRange(context.EntityAlarms);
                context.SaveChanges();
            }
        }

    }
}
