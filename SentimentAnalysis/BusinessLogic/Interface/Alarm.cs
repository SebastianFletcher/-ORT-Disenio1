using BusinessLogic.DTO;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public abstract class Alarm
    {
        #region Private Attributes

        private int postQuantity;
        private SentimentType type;
        private int time;

        #endregion

        #region Public Attributes

        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public int PostCount { get; set; }

        public SentimentType Type
        {
            get { return type; }
            set { SetSentimentType(value); }
        }

        public string PostQuantity
        {
            get { return postQuantity.ToString(); }
            set { SetPostQuantity(value); }
        }

        public string Time
        {
            get { return time.ToString(); }
            set { SetTime(value); }
        }

        #endregion

        public Alarm()
        {
            CreationDate = DateTime.Now;
        }

        public bool IsEnabled()
        {
            return this.postQuantity == this.PostCount;
        }

        private void SetSentimentType(SentimentType type)
        {
            if (type == SentimentType.NEUTRAL)
                throw new AlarmException("Sentiment Type can't be neutral.");

            this.type = type;
        }

        private void SetTime(string timeMeasure)
        {
            int days;

            if (!Int32.TryParse(timeMeasure, out days))
                throw new AlarmException("Time entered is not numeric.");

            if (days < 0)
                throw new AlarmException("Time entered is less than zero.");

            time = days;
        }

        public void ReAnalyePhrases(IList<Phrase> phrases)
        {
            this.PostCount = 0;

            AnalyzePhrases(phrases);
        }

        #region Virtuals 

        public virtual void SetPostQuantity(string postQuantity)
        {
            int quantity;

            if (!Int32.TryParse(postQuantity, out quantity))
                throw new AlarmException("Quantity of Posts entered is not numeric.");

            if (quantity <= 0)
                throw new AlarmException("Quantity of Posts can't be less or equals than zero.");

            this.postQuantity = quantity;
        }

        public virtual bool InRangeOfTime(DateTime phraseTime)
        {
            return true;
        }

        public virtual void AnalyzePhrases(IList<Phrase> phrases)
        {
            throw new NotImplementedException("You must implement AnalyzePhrase for this type of Alarm.");
        }

        #endregion

        public string GetStatus()
        {
            return $"In {Time} Days/Hours there where {PostCount} {Type} posts";
        }

    }
}
