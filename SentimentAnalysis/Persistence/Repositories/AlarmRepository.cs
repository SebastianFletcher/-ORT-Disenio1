using BusinessLogic.Exceptions;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Persistence.Repositories
{
    public class AlarmRepository : IRepository<Alarm>
    {
        public void Modify(int? id, Alarm alarm)
        {
            if (!id.HasValue)
                throw new AlarmException("You must select an Alarm to modify.");

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                var toUpdate = context.Alarms.FirstOrDefault(a => a.Id == id);

                toUpdate.PostQuantity = alarm.PostQuantity;
                toUpdate.PostCount = alarm.PostCount;
                toUpdate.Time = alarm.Time;
                toUpdate.Type = alarm.Type;

                context.Alarms.AddOrUpdate(toUpdate);

                context.SaveChanges();
            }
        }

        public void Delete(int? id)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Alarm toRemove = context.Alarms.FirstOrDefault(a => a.Id == id);

                context.Alarms.Remove(toRemove);

                context.SaveChanges();
            }
        }

        public void Clear()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                context.Alarms.RemoveRange(context.Alarms);
                context.SaveChanges();
            }
        }

        #region Not Used

        public void Add(Entities.Alarm toAdd)
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public Alarm Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Alarm> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Exists(Alarm toFind)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
