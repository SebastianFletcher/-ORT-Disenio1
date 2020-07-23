using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.DTO
{
    public class EntityAlarm : Alarm
    {
        #region Private Attributes

        private Entity entity;

        #endregion

        #region Public Attributes

        public Entity Entity
        {
            get { return entity; }
            set { SetEntity(value); }
        }

        #endregion

        #region Constructors 

        public EntityAlarm(int id, int postQuantity, int postCount, DateTime creationDate, int numDays, SentimentType type, Entity entity) : base()
        {
            Id = id;
            PostQuantity = postQuantity.ToString();
            PostCount = postCount;
            Time = numDays.ToString();
            Type = type;
            Entity = entity;
        }

        public EntityAlarm(string postQuantity, string quantDays, SentimentType type, Entity entity) : base()
        {
            PostQuantity = postQuantity;
            Time = quantDays;
            PostCount = 0;
            Type = type;
            Entity = entity;
        }

        #endregion   

        private void SetEntity(Entity entity)
        {
            if (entity == null)
                throw new AlarmException("You must select an Entity.");

            this.entity = entity;
        }

        #region Overrides

        public override void AnalyzePhrases(IList<Phrase> phrases)
        {
            foreach (var phrase in phrases.Where(p => InRangeOfTime(p.PostedDate)))
                if (phrase.Entity != null && phrase.Entity.Id == Entity.Id && phrase.Type == Type)
                    PostCount++;
        }

        public override bool InRangeOfTime(DateTime phraseTime)
        {
            var alarmMinDate = CreationDate.AddDays(-Int32.Parse(this.Time));

            DateTime timeWithoutSeconds = new DateTime(phraseTime.Year, phraseTime.Month, phraseTime.Day);
            DateTime minDateWithoutSeconds = new DateTime(alarmMinDate.Year, alarmMinDate.Month, alarmMinDate.Day);
            DateTime alarmDateWithoutSeconds = new DateTime(CreationDate.Year, CreationDate.Month, CreationDate.Day);

            return timeWithoutSeconds >= minDateWithoutSeconds && minDateWithoutSeconds <= alarmDateWithoutSeconds;
        }

        public override string ToString()
        {
            return $"Alarm {Type}, for Entity: {Entity}, with PostQuantity: {PostQuantity}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj == DBNull.Value)
                return false;

            EntityAlarm alarm = (EntityAlarm)obj;

            return this.Type == alarm.Type && 
                this.entity.Id == alarm.entity.Id && 
                this.PostQuantity == alarm.PostQuantity;
        }

        #endregion
    }
}
