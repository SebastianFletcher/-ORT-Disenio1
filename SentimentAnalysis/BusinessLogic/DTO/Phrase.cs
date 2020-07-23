using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.DTO
{
    public class Phrase
    {
        #region Private Attributes

        private string word;
        private SentimentType? type;
        private Entity entity;
        private Author author;
        private DateTime postedDate;
        private List<Sentiment> sentimentList;

        #endregion

        #region Public Attributes

        public int Id { get; set; }

        public List<Sentiment> SentimentList
        {
            get { return sentimentList; }
            private set { sentimentList = value; }
        }

        public string Word
        {
            get { return word; }
            set { SetWord(value); }
        }

        public SentimentType? Type
        {
            get { return type; }
            set { SetSentimentType(value); }
        }

        public Entity Entity
        {
            get { return entity; }
            set { SetEntity(value); }
        }

        public DateTime PostedDate
        {
            get { return postedDate; }
            set { SetPostedDate(value); }
        }

        public SentimentGrade? Grade { get; set; }

        public Author Author
        {
            get { return author; }
            set { SetAuthor(value); }
        }

        #endregion

        #region Constructors

        public Phrase(int id, string word, DateTime postedDate, SentimentType? type, Entity entity, SentimentGrade? grade, Author author)
        {
            Id = id;
            Word = word;
            PostedDate = postedDate;
            Entity = entity;
            Type = type;
            Grade = grade;
            Author = author;

            SentimentList = new List<Sentiment>();
        }

        public Phrase(string word, DateTime date, Author author)
        {
            Word = word;
            PostedDate = date;
            Author = author;
            Grade = null;
            SentimentList = new List<Sentiment>();
        }

        #endregion 

        private void SetWord(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new PhraseException("Word can't be empty.");

            this.word = word.Trim();
        }

        private void SetAuthor(Author author)
        {
            if (author == null)
                throw new PhraseException("Author can't be empty.");

            this.author = author;
        }

        private void SetEntity(Entity entity)
        {
            this.entity = entity;
        }

        private void SetSentimentType(SentimentType? type)
        {
            this.type = type;
        }

        private void SetPostedDate(DateTime date)
        {
            if (date == null)
                throw new PhraseException("Posted Date can't be null.");

            if (date > DateTime.Now || date == DateTime.MinValue)
                throw new PhraseException("Invalid Posted Date.");

            this.postedDate = date;
        }

        private void SetSentimentGrade(int quantity)
        {
            if (quantity >= 3)
                this.Grade = SentimentGrade.HIGH;
            else if (quantity < 2)
                this.Grade = SentimentGrade.LOW;
            else
                this.Grade = SentimentGrade.MEDIUM;
        }

        private void AnalyzeSentimentGrade()
        {
            if (sentimentList == null || !sentimentList.Any())
                return;

            SentimentType predominant;

            int cantNegative = this.sentimentList.Count(s => s.Type == SentimentType.NEGATIVE);
            int cantPositive = this.sentimentList.Count(s => s.Type == SentimentType.POSITIVE);

            if (cantNegative > cantPositive)
                predominant = SentimentType.NEGATIVE;
            else predominant = cantPositive > cantNegative ? SentimentType.POSITIVE : SentimentType.NEUTRAL;

            SetSentimentType(predominant);

            int quantity = sentimentList.Count(s => s.Type == predominant);

            if (predominant == SentimentType.NEUTRAL)
                throw new AnalysisException("Multiple sentiments found.");
            else
                SetSentimentGrade(quantity);
        }

        public void AnalyzePhrase(IList<Entity> entities, IList<Sentiment> sentiments)
        {
            foreach (var entity in entities)
            {
                if (this.Word.ToUpper().Contains(entity.Name.ToUpper()))
                {
                    if (this.Entity == null)
                        this.Entity = entity;
                    else
                    {
                        this.Type = SentimentType.NEUTRAL;
                        throw new AnalysisException("Multiple entities found.");
                    }
                }
            }

            foreach (var sentiment in sentiments)
                if (this.Word.ToUpper().Contains(sentiment.Word.ToUpper()))
                    this.sentimentList.Add(sentiment);

            AnalyzeSentimentGrade();

            if (this.Entity == null)
                throw new AnalysisException("An entity was not found.");

            if (this.Type == null)
                throw new AnalysisException("An sentiment was not found.");
        }

        public override string ToString()
        {
            return $"{Word} [Type: {Type}] [Entity: {Entity}] [Author: {Author.Username}]";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj == DBNull.Value)
                return false;

            Phrase phrase = (Phrase)obj;

            return this.word.ToUpper().Equals(phrase.word.ToUpper()) && 
                this.Author.Id == phrase.Author.Id;
        }
    }
}
