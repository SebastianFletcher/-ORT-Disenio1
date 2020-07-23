using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.DTO
{
    public class AuthorAlarm : Alarm
    {
        #region Private Attributes

        private static int MAX_CANT_POSTS = 10;

        #endregion

        #region Public Attributes

        public TimeMeasure TimeMeasure { get; set; }

        #endregion

        #region Constructors 

        public AuthorAlarm(int id, int postQuantity, int postCount, DateTime creationDate, int numDays, SentimentType type, TimeMeasure measure) : base()
        {
            Id = id;
            PostQuantity = postQuantity.ToString();
            PostCount = postCount;
            Time = numDays.ToString();
            Type = type;
            TimeMeasure = measure;
        }

        public AuthorAlarm(string postQuantity, string quantDays, SentimentType type, TimeMeasure measure) : base()
        {
            PostQuantity = postQuantity;
            Time = quantDays;
            PostCount = 0;
            Type = type;
            TimeMeasure = measure;
        }

        #endregion 

        #region Overrides

        public override void SetPostQuantity(string postQuantity)
        {
            int quantity;

            if (!Int32.TryParse(postQuantity, out quantity))
                throw new AlarmException("Quantity of Posts entered is not numeric.");

            if (quantity <= 0)
                throw new AlarmException("Quantity of Posts can't be less or equals than zero.");

            if (quantity > MAX_CANT_POSTS)
                throw new AlarmException($"Quantity of Posts can't be more than {MAX_CANT_POSTS}.");

            base.SetPostQuantity(postQuantity);
        }

        public override bool InRangeOfTime(DateTime phraseTime)
        {
            DateTime timeWithoutSeconds = phraseTime.AddSeconds(-phraseTime.Second);
            DateTime alarmMinDate = TimeMeasure == TimeMeasure.DAYS
                ? CreationDate.AddSeconds(-CreationDate.Second).AddDays(-Int32.Parse(this.Time))
                : CreationDate.AddSeconds(-CreationDate.Second).AddHours(-Int32.Parse(this.Time));
            DateTime alarmDateWithoutSeconds = CreationDate.AddSeconds(CreationDate.Second);

            return timeWithoutSeconds >= alarmMinDate && alarmMinDate <= alarmDateWithoutSeconds;
        }

        public override void AnalyzePhrases(IList<Phrase> phrases)
        {
            foreach (var phrase in phrases.Where(p => InRangeOfTime(p.PostedDate)))
                if (phrase.Type == Type)
                    PostCount++;
        }

        public override string ToString()
        {
            return $"Alarm {Type}, measured in {TimeMeasure}, with PostQuantity: {PostQuantity}.";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj == DBNull.Value)
                return false;

            AuthorAlarm alarm = (AuthorAlarm)obj;

            return this.Type == alarm.Type && 
                this.TimeMeasure == alarm.TimeMeasure && 
                this.Time == alarm.Time;
        }

        #endregion
    }
}
