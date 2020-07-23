using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using System;

namespace BusinessLogic.DTO
{
    public class Sentiment
    {
        #region Private Attributes

        private string word;
        private SentimentType type;

        #endregion

        #region Public Attributes

        public int Id { get; set; }

        public SentimentType Type
        {
            get { return type; }
            set { SetSentimentType(value); }
        }

        public string Word
        {
            get { return word; }
            set { SetWord(value); }
        }

        #endregion

        #region Constructors 

        public Sentiment(int id, SentimentType sentiment, string word) : this (sentiment, word)
        {
            Id = id;
        }

        public Sentiment(SentimentType sentiment, string word)
        {
            Type = sentiment;
            Word = word;
        }

        #endregion 

        private void SetWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new SentimentException("Word can't be empty.");

            this.word = word.Trim();
        }

        private void SetSentimentType(SentimentType type)
        {
            if (type == SentimentType.NEUTRAL)
                throw new SentimentException("Sentiment Type can't be neutral.");

            this.type = type;
        }

        public override string ToString()
        {
            return $"{Word} - [{Type}]";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj == DBNull.Value)
                return false;

            Sentiment sentiment = (Sentiment)obj;

            return this.word.ToUpper().Equals(sentiment.word.ToUpper()) && 
                this.type == sentiment.type;
        }
    }
}
