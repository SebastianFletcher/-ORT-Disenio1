using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Test.Tests
{
    [TestClass]
    public class PhraseTest
    {
        #region Attributes

        private static string WORD;
        private static Author AUTHOR;
        private static List<Sentiment> sentiments;
        private static List<Entity> entities;

        #endregion

        #region SetUp

        [TestInitialize]
        public void SetUp()
        {
            WORD = "I like Starbucks.";
            AUTHOR = new Author("sada", "sadasd", "sdaasd", new DateTime(1995, 3, 2));

            Sentiment positive = new Sentiment(SentimentType.POSITIVE, "I like");
            Sentiment negative = new Sentiment(SentimentType.NEGATIVE, "I dont like");

            sentiments = new List<Sentiment> { positive, negative };

            Entity starbucks = new Entity("Starbucks");
            Entity google = new Entity("Google");

            entities = new List<Entity> { starbucks, google };

        }

        [TestCleanup]
        public void Cleanup()
        {
            WORD = string.Empty;
            sentiments = new List<Sentiment>();
            entities = new List<Entity>();
        }

        #endregion

        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(PhraseException))]
        public void ConstructorWordNull()
        {
            Phrase phrase = new Phrase(null, DateTime.Now, AUTHOR);
        }

        [TestMethod]
        [ExpectedException(typeof(PhraseException))]
        public void ConstructorInvalidPostedDate()
        {
            Phrase phrase = new Phrase(WORD, DateTime.MinValue, AUTHOR);
        }

        [TestMethod]
        public void ConstructorWithId()
        {
            Phrase phrase = new Phrase(42, WORD, DateTime.Now, SentimentType.NEGATIVE, new Entity("Google"), null, AUTHOR);

            Assert.AreEqual(phrase.Id, 42);
        }

        #endregion

        #region Id

        [TestMethod]
        public void SetId()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            Assert.IsNotNull(phrase.Id);
        }

        #endregion

        #region Word

        [TestMethod]
        public void GetCorrectWord()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            Assert.AreEqual(phrase.Word, WORD);
        }

        [TestMethod]
        [ExpectedException(typeof(PhraseException))]
        public void UpdateNullWord()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            phrase.Word = null;
        }

        [TestMethod]
        public void UpdateCorrectWord()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            string newWord = "I love Google";

            phrase.Word = newWord;

            Assert.AreEqual(phrase.Word, newWord);
        }

        #endregion

        #region Type

        [TestMethod]
        public void UpdateCorrectSentimentType()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);
            phrase.Type = SentimentType.NEGATIVE;

            Assert.AreEqual(phrase.Type, SentimentType.NEGATIVE);
        }

        [TestMethod]
        public void UpdateNulSentimentType()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);
            phrase.Type = null;

            Assert.IsNull(phrase.Type);
        }

        [TestMethod]
        public void GetSentimentTyp()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            Assert.IsNull(phrase.Type);
        }

        #endregion

        #region Entity

        [TestMethod]
        public void UpdateCorrectEntity()
        {
            Entity uber = new Entity("Uber");
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);
            phrase.Entity = uber;

            Assert.AreEqual(phrase.Entity, uber);
        }

        [TestMethod]
        public void UpdateNullEntity()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);
            phrase.Entity = null;

            Assert.IsNull(phrase.Entity);
        }

        [TestMethod]
        public void GetEntity()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            Assert.IsNull(phrase.Entity);
        }

        #endregion 

        #region Posted Date

        [TestMethod]
        [ExpectedException(typeof(PhraseException))]
        public void UpdateInvalidPostedDate()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);
            phrase.PostedDate = DateTime.MinValue;
        }

        [TestMethod]
        public void UpdateValidPostedDate()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            DateTime newDate = new DateTime(2020, 05, 15);

            phrase.PostedDate = newDate;

            Assert.AreEqual(phrase.PostedDate, newDate);
        }

        [TestMethod]
        [ExpectedException(typeof(PhraseException))]
        public void UpdateFuturePostedDate()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            DateTime newDate = new DateTime(2020, 12, 15);

            phrase.PostedDate = newDate;
        }

        [TestMethod]
        public void GetPostedDate()
        {
            DateTime date = DateTime.Now;

            Phrase phrase = new Phrase(WORD, date, AUTHOR);


            Assert.AreEqual(phrase.PostedDate, date);
        }

        #endregion

        #region Grade

        [TestMethod]
        public void GetNullGrade()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            Assert.IsNull(phrase.Grade);
        }

        [TestMethod]
        public void GetHighGrade()
        {
            Entity entity = new Entity("Starbucks");

            Sentiment iLike = new Sentiment(SentimentType.POSITIVE, "I like");
            Sentiment iLove = new Sentiment(SentimentType.POSITIVE, "I love");
            Sentiment fascinatesMe = new Sentiment(SentimentType.POSITIVE, "Fascinates me");

            List<Sentiment> sentiments = new List<Sentiment>();
            sentiments.Add(iLike);
            sentiments.Add(iLove);
            sentiments.Add(fascinatesMe);

            string word = "I love and fascinates me Starbucks, i like it to much";

            Phrase phrase = new Phrase(word, DateTime.Now, AUTHOR);
            phrase.AnalyzePhrase(new List<Entity>() { entity }, sentiments);

            Assert.AreEqual(phrase.Grade, SentimentGrade.HIGH);
        }

        [TestMethod]
        public void GetMediumGrade()
        {
            Entity entity = new Entity("Starbucks");

            Sentiment iLike = new Sentiment(SentimentType.POSITIVE, "I like");
            Sentiment fascinatesMe = new Sentiment(SentimentType.POSITIVE, "Fascinates me");

            List<Sentiment> sentiments = new List<Sentiment>();
            sentiments.Add(iLike);
            sentiments.Add(fascinatesMe);

            string word = "I love and fascinates me Starbucks, i like it to much";

            Phrase phrase = new Phrase(word, DateTime.Now, AUTHOR);
            phrase.AnalyzePhrase(new List<Entity>() { entity }, sentiments);

            Assert.AreEqual(phrase.Grade, SentimentGrade.MEDIUM);
        }

        [TestMethod]
        public void GetLowGrade()
        {
            Entity entity = new Entity("Starbucks");

            Sentiment iLike = new Sentiment(SentimentType.NEGATIVE, "I dislike");

            List<Sentiment> sentiments = new List<Sentiment>();
            sentiments.Add(iLike);

            string word = "I love and fascinates me Starbucks, i dislike it to much";

            Phrase phrase = new Phrase(word, DateTime.Now, AUTHOR);
            phrase.AnalyzePhrase(new List<Entity>() { entity }, sentiments);

            Assert.AreEqual(phrase.Grade, SentimentGrade.LOW);
        }

        #endregion

        #region Sentiment List

        [TestMethod]
        public void GetSentimentList()
        {
            Entity entity = new Entity("Starbucks");

            Sentiment iLike = new Sentiment(SentimentType.POSITIVE, "I like");

            List<Sentiment> sentiments = new List<Sentiment>();
            sentiments.Add(iLike);

            string word = "I love and fascinates me Starbucks, i like it to much";

            Phrase phrase = new Phrase(word, DateTime.Now, AUTHOR);
            phrase.AnalyzePhrase(new List<Entity>() { entity }, sentiments);

            Assert.IsTrue(phrase.SentimentList.SequenceEqual(sentiments));
        }

        #endregion

        #region Methods

        [TestMethod]
        public void AnalyzePhraseSetEntity()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            phrase.AnalyzePhrase(entities, sentiments);

            Assert.AreEqual(phrase.Entity, entities.First(e => e.Name.Equals("Starbucks")));
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AnalyzePhraseNotFoundEntity()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            phrase.AnalyzePhrase(new List<Entity>(), sentiments);
        }

        [TestMethod]
        public void AnalyzePhraseSetSentimentType()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            phrase.AnalyzePhrase(entities, sentiments);

            Assert.AreEqual(phrase.Type, SentimentType.POSITIVE);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AnalyzePhraseNotFoundSentimentType()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            phrase.AnalyzePhrase(entities, new List<Sentiment>());
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AnalyzePhraseMultipleEntities()
        {
            string newWord = string.Concat(WORD, " and Google");

            Phrase phrase = new Phrase(newWord, DateTime.Now, AUTHOR);

            phrase.AnalyzePhrase(entities, sentiments);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AnalyzePhraseMultipleSentimentsType()
        {
            string newWord = string.Concat(WORD, ", but i dont like Microsoft");
            Phrase phrase = new Phrase(newWord, DateTime.Now, AUTHOR);

            phrase.AnalyzePhrase(entities, sentiments);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AnalyzeSentimentGradeEmptyList()
        {
            Entity entity = new Entity("Starbucks");

            List<Sentiment> sentiments = new List<Sentiment>();

            string word = "I love and fascinates me Starbucks, i like it to much";

            Phrase phrase = new Phrase(word, DateTime.Now, AUTHOR);
            phrase.AnalyzePhrase(new List<Entity>() { entity }, sentiments);
        }

        [TestMethod]
        public void ToStringTest()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);
            phrase.AnalyzePhrase(entities, sentiments);

            string str = $"{WORD} [Type: {SentimentType.POSITIVE}] [Entity: Starbucks] [Author: {AUTHOR.Username}]";

            Assert.AreEqual(phrase.ToString(), str);
        }

        [TestMethod]
        public void EqualsNull()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            Assert.IsFalse(phrase.Equals(null));
        }

        [TestMethod]
        public void EqualsDbNull()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            Assert.IsFalse(phrase.Equals(DBNull.Value));
        }

        [TestMethod]
        public void AreEquals()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);
            Phrase samePhrase = new Phrase(WORD, DateTime.Now, AUTHOR);

            Assert.IsTrue(phrase.Equals(samePhrase));
        }

        [TestMethod]
        public void NotEquals()
        {
            Phrase phrase = new Phrase(WORD, DateTime.Now, AUTHOR);
            Phrase samePhrase = new Phrase(WORD, DateTime.Now, new Author(3, "UserTest2", "Test", "Test", new DateTime(1995, 2, 5)));

            Assert.IsFalse(phrase.Equals(samePhrase));
        }

        #endregion
    }
}
