using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.DTO
{
    public class AlarmEntity
    {
        #region Private Attributes

        private int postQuantity;
        private DateTime creationDate;
        private int numberDays;
        private SentimentType type;
        private Entity entity;

        #endregion

        #region Public Attributes

        public int Id { get; set; }

        public string PostQuantity
        {
            get { return postQuantity.ToString(); }
            set { SetPostQuantity(value); }
        }

        public int PostCount
        {
            get;
            private set;
        }

        public DateTime CreationDate
        {
            get { return this.creationDate; }
        }

        public string NumberDays
        {
            get { return numberDays.ToString(); }
            set { SetNumberDays(value); }
        }

        public SentimentType Type
        {
            get { return type; }
            set { SetSentimentType(value); }
        }

        public Entity Entity
        {
            get { return entity; }
            set { SetEntity(value); }
        }

        #endregion

        #region Constructors 

        public AlarmEntity(int id, int postQuantity, int postCount, DateTime creationDate, int numDays, SentimentType type, Entity entity)
        {
            Id = id;
            PostQuantity = postQuantity.ToString();
            PostCount = postCount;
            this.creationDate = creationDate;
            NumberDays = numDays.ToString();
            Type = type;
            Entity = entity;
        }

        public AlarmEntity(string postQuantity, string quantDays, SentimentType type, Entity entity)
        {
            PostQuantity = postQuantity;
            this.creationDate = DateTime.Now;
            NumberDays = quantDays;
            PostCount = 0;
            Type = type;
            Entity = entity;
        }

        #endregion   

        private void SetPostQuantity(string postQuantity)
        {
            int quantity;

            if (!Int32.TryParse(postQuantity, out quantity))
                throw new AlarmException("Quantity of Posts entered is not numeric.");

            if (quantity <= 0)
                throw new AlarmException("Quantity of Posts can't be less or equals than zero.");

            this.postQuantity = quantity;
        }

        private void SetNumberDays(string days)
        {
            int quantity;

            if (!Int32.TryParse(days, out quantity))
                throw new AlarmException("Number of Days entered is not numeric.");

            if (quantity < 0)
                throw new AlarmException("Number of Days entered is less than zero.");

            this.numberDays = quantity;
        }

        private void SetSentimentType(SentimentType type)
        {
            if (type == SentimentType.NEUTRAL)
                throw new AlarmException("Sentiment Type can't be neutral.");

            this.type = type;
        }

        private void SetEntity(Entity entity)
        {
            if (entity == null)
                throw new AlarmException("You must select an entity.");

            this.entity = entity;
        }

        public bool IsEnabled()
        {
            return this.postQuantity == this.PostCount;
        }

        public void AnalyzePhrases(IList<Phrase> phrases)
        {
            foreach (var phrase in phrases.Where(p => PhraseInRangeTime(p.PostedDate)))
                if (phrase.Entity.Id == Entity.Id && phrase.Type == Type)
                    this.PostCount++;
        }

        public void ReAnalyePhrases(IList<Phrase> phrases)
        {
            this.PostCount = 0;

            AnalyzePhrases(phrases);
        }

        public string GetStatus()
        {
            return $"In the last {NumberDays} days there where {PostCount} {Type} posts";
        }

        public override string ToString()
        {
            return $"Alarm {Type}, for Entity: {Entity}, with PostQuantity: {PostQuantity}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            AlarmEntity alarm = (AlarmEntity)obj;

            return this.type == alarm.type && this.entity.Id == alarm.entity.Id && this.postQuantity == alarm.postQuantity;
        }

        private bool PhraseInRangeTime(DateTime phraseTime)
        {
            DateTime timeWithoutSeconds = phraseTime.AddSeconds(-phraseTime.Second);
            DateTime alarmMinDate = this.creationDate.AddSeconds(-creationDate.Second).AddDays(-this.numberDays);
            DateTime alarmDateWithoutSeconds = creationDate.AddSeconds(creationDate.Second);

            return timeWithoutSeconds >= alarmMinDate && alarmMinDate <= alarmDateWithoutSeconds;
        }
    }
}
