using BusinessLogic.DTO;
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
    public class AuthorAlarmRepository : IRepository<AuthorAlarm>
    {
        public void Add(AuthorAlarm alarm)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                if (context.AuthorAlarms.Any(a => a.Id == alarm.Id))
                    throw new AlarmException("An Alarm with the same ID already exists.");

                Exists(alarm);

                alarm.AnalyzePhrases(Helper.Instance.GetPhrases(context.Phrases));
                Entities.AuthorAlarm toAdd = Helper.Instance.ToAuthorAlarmEF(alarm);
                context.AuthorAlarms.Add(toAdd);

                context.SaveChanges();
                alarm.Id = toAdd.Id;
            }
        }

        public void Modify(int? id, AuthorAlarm alarm)
        {
            if (!id.HasValue)
                throw new AlarmException("You must select an Alarm to modify.");

            AuthorAlarm oldAlarm = Get(alarm.Id);
            AlarmRepository alarmRepository = new AlarmRepository();

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                var toUpdate = context.AuthorAlarms.FirstOrDefault(a => a.Id == id);

                if (id != alarm.Id || oldAlarm.Type != alarm.Type || oldAlarm.TimeMeasure != alarm.TimeMeasure ||
                    !oldAlarm.PostQuantity.Equals(alarm.PostQuantity) || !oldAlarm.Time.Equals(alarm.Time))
                    Exists(alarm);

                alarm.ReAnalyePhrases(Helper.Instance.GetPhrases(context.Phrases));

                toUpdate.Alarm.PostQuantity = Int32.Parse(alarm.PostQuantity);
                toUpdate.Alarm.PostCount = alarm.PostCount;
                toUpdate.Alarm.Time = Int32.Parse(alarm.Time);
                toUpdate.Alarm.Type = alarm.Type;
                toUpdate.TimeMeasure = alarm.TimeMeasure;

                alarmRepository.Modify(toUpdate.Alarm.Id, toUpdate.Alarm);

                context.AuthorAlarms.AddOrUpdate(toUpdate);

                context.SaveChanges();
            }
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new AlarmException("You must select an Alarm to delete.");

            AlarmRepository alarmRepository = new AlarmRepository();

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.AuthorAlarm toRemove = context.AuthorAlarms.FirstOrDefault(a => a.Id == id);

                if (toRemove == null)
                    throw new AlarmException("Alarm not found.");

                var idAlarm = toRemove.Alarm.Id;
                context.AuthorAlarms.Remove(toRemove);

                context.SaveChanges();

                alarmRepository.Delete(idAlarm);
            }
        }

        public AuthorAlarm Get(int id)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.AuthorAlarm alarm = context.AuthorAlarms.FirstOrDefault(a => a.Id == id);

                if (alarm == null)
                    throw new AlarmException("Alarm not found.");

                return Helper.Instance.ToAuthorAlarmBL(alarm);
            }
        }

        public IEnumerable<AuthorAlarm> GetAll()
        {
            try
            {
                using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                    return Helper.Instance.GetAuthorAlarms(context.AuthorAlarms);
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

        public void Exists(AuthorAlarm toFind)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                if (GetAll().Any(a => a.Equals(toFind) && a.Time.Equals(toFind.Time)))
                    throw new AlarmException($"The Alarm {toFind} already exists.");
        }

        public bool IsEmpty()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                return !context.AuthorAlarms.Any();
        }

        public void Clear()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                context.AuthorAlarms.RemoveRange(context.AuthorAlarms);
                context.SaveChanges();
            }
        }


    }
}
