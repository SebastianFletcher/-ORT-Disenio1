using BusinessLogic.Enums;
using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Test.Tests
{
    [TestClass]
    public class AlarmEntityTest
    {
        #region Attributes 

        private static Entity ENTITY;
        private static DateTime YESTERDAY;
        private static Author AUTHOR;

        #endregion

        #region SetUp 

        [TestInitialize]
        public void SetUp()
        {
            ENTITY = new Entity("Google");
            YESTERDAY = DateTime.Now.AddDays(-1);
            AUTHOR = new Author("sada", "sadasd", "sdaasd", new DateTime(1995, 1 ,2)); 
        }

        [TestCleanup]
        public void Cleanup()
        {
            ENTITY = null;
            YESTERDAY = DateTime.Now;
        }

        #endregion

        #region Constuctor 

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorPostQuantityZero()
        {
            AlarmEntity alarm = new AlarmEntity("0", "1", SentimentType.NEGATIVE, ENTITY);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorPostQuantityNegative()
        {
            AlarmEntity alarm = new AlarmEntity("-2", "1", SentimentType.NEGATIVE, ENTITY);
        }
        
        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorPostQuantityAlphabetic()
        {
            AlarmEntity alarm = new AlarmEntity("dos", "1", SentimentType.NEGATIVE, ENTITY);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorNegativeDays()
        {
            AlarmEntity alarm = new AlarmEntity("2", "-1", SentimentType.NEGATIVE, ENTITY);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorNullEntity()
        {
            AlarmEntity alarm = new AlarmEntity("2", "-1", SentimentType.NEGATIVE, null);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorNeutralSentimentType()
        {
            AlarmEntity alarm = new AlarmEntity("2", "-1", SentimentType.NEUTRAL, ENTITY);
        }

        #endregion

        #region Id

        [TestMethod]
        public void SetId()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.IsNotNull(alarm.Id);
        }

        #endregion

        #region PostQuantity
        
        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateInvalidPostQuantity()
        {
            AlarmEntity alarm = new AlarmEntity("2", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.PostQuantity = "-1";
        }

        [TestMethod]
        public void UpdateValidPostQuantity()
        {
            AlarmEntity alarm = new AlarmEntity("2", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.PostQuantity = "61";

            Assert.AreEqual(alarm.PostQuantity, "61");
        }

        [TestMethod]
        public void GetPostQuantity()
        {
            AlarmEntity alarm = new AlarmEntity("2", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.AreEqual(alarm.PostQuantity, "2");
        }

        #endregion

        #region Post Count
        
        [TestMethod]
        public void GetPostCount()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.AreEqual(alarm.PostCount, 0);
        }

        #endregion

        #region Number of Days

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateInvalidNumberDays()
        {
            AlarmEntity alarm = new AlarmEntity("2", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.NumberDays = "-3";
        }


        [TestMethod]
        public void UpdateValidNumberDays()
        {
            AlarmEntity alarm = new AlarmEntity("2", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.NumberDays = "5";

            Assert.AreEqual(alarm.NumberDays, "5");
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateAlphanumericNumberDays()
        {
            AlarmEntity alarm = new AlarmEntity("2", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.NumberDays = "asd";
        }

        [TestMethod]
        public void GetNumberDays()
        {
            AlarmEntity alarm = new AlarmEntity("2", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.AreEqual(alarm.NumberDays, "1");
        }

        #endregion

        #region Sentiment Type

        [TestMethod]
        public void GetSentimentType()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.AreEqual(alarm.Type, SentimentType.NEGATIVE);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateNeutralSentimentType()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.Type = SentimentType.NEUTRAL;
        }

        [TestMethod]
        public void UpdateValidSentimentType()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.Type = SentimentType.POSITIVE;

            Assert.AreEqual(alarm.Type, SentimentType.POSITIVE);
        }

        #endregion

        #region Entity

        [TestMethod]
        public void GetEntity()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.AreEqual(alarm.Entity, ENTITY);
        }


        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateNullEntity()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.Entity = null;
        }

        [TestMethod]
        public void UpdateValidEntity()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.NEGATIVE, ENTITY);

            Entity uber = new Entity("Uber");

            alarm.Entity = uber;

            Assert.AreEqual(alarm.Entity, uber);
        }


        #endregion

        #region CreationDate

        [TestMethod]
        public void GetCreationDate()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.AreEqual(alarm.CreationDate, DateTime.Now);
        }

        #endregion 

        #region Analysis

        [TestMethod]
        public void AnalyzeUpdatePostCount()
        {
            AlarmEntity alarm = new AlarmEntity("2", "2", SentimentType.POSITIVE, ENTITY);

            Phrase phrase = new Phrase("I like google", YESTERDAY, AUTHOR);

            phrase.AnalyzePhrase(new List<Entity> { ENTITY }, new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });

            alarm.AnalyzePhrases(new List<Phrase> { phrase });

            Assert.AreEqual(alarm.PostCount, 1);
        }

        [TestMethod]
        public void AnalyzeNoUpdatePostCount()
        {
            AlarmEntity alarm = new AlarmEntity("2", "1", SentimentType.NEGATIVE, ENTITY);

            Phrase phrase = new Phrase("I like google", YESTERDAY.AddDays(-25), AUTHOR);

            phrase.AnalyzePhrase(new List<Entity> { ENTITY }, new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });

            alarm.AnalyzePhrases(new List<Phrase> { phrase });

            Assert.AreEqual(alarm.PostCount, 0);
        }

       [TestMethod]
        public void AnalyzePhraseWithoutEntity()
        {
            Entity entity = new Entity("Starbucks");

            AlarmEntity alarm = new AlarmEntity("1", "2", SentimentType.POSITIVE, entity);

            Phrase phrase = new Phrase("I like google", YESTERDAY, AUTHOR);

            phrase.AnalyzePhrase(new List<Entity> { ENTITY }, new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });

            alarm.AnalyzePhrases(new List<Phrase> { phrase });

            Assert.AreEqual(alarm.PostCount, 0);
        }

        [TestMethod]
        public void ReAnalyze()
        {
            AlarmEntity alarm = new AlarmEntity("2", "2", SentimentType.POSITIVE, ENTITY);

            Phrase phrase = new Phrase("I like google", YESTERDAY, AUTHOR);

            phrase.AnalyzePhrase(new List<Entity> { ENTITY }, new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });

            alarm.AnalyzePhrases(new List<Phrase> { phrase });
            alarm.ReAnalyePhrases(new List<Phrase> { phrase });

            Assert.AreEqual(alarm.PostCount, 1);
        }

        #endregion

        #region Methdods

        [TestMethod]
        public void IsEnabled()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.POSITIVE, ENTITY);

            Assert.IsFalse(alarm.IsEnabled());
        }

        [TestMethod]
        public void IsNotEnabled()
        {
            AlarmEntity alarm = new AlarmEntity("2", "1", SentimentType.POSITIVE, ENTITY);

            Phrase phrase = new Phrase("I like google", YESTERDAY, AUTHOR);

            phrase.AnalyzePhrase(new List<Entity> { ENTITY }, new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });

            alarm.AnalyzePhrases(new List<Phrase> { phrase });

            Assert.IsFalse(alarm.IsEnabled());
        }


        [TestMethod]
        public void GetToString()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.POSITIVE, ENTITY);
            
            string str = "Alarm POSITIVE, for Entity: Google, with PostQuantity: 1";

            Assert.AreEqual(alarm.ToString(), str);
        }

        [TestMethod]
        public void GetAlarmStatus()
        {
            AlarmEntity alarm = new AlarmEntity("1", "1", SentimentType.POSITIVE, ENTITY);

            string str = $"In the last 1 days there where 0 {SentimentType.POSITIVE} posts";

            Assert.AreEqual(alarm.GetStatus(), str);
        }

        #endregion 
    }
}
