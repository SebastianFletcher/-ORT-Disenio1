using BusinessLogic.Enums;
using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Test.Tests
{
    [TestClass]
    public class EntityAlarmTest
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
            EntityAlarm alarm = new EntityAlarm("0", "1", SentimentType.NEGATIVE, ENTITY);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorPostQuantityNegative()
        {
            EntityAlarm alarm = new EntityAlarm("-2", "1", SentimentType.NEGATIVE, ENTITY);
        }
        
        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorPostQuantityAlphabetic()
        {
            EntityAlarm alarm = new EntityAlarm("dos", "1", SentimentType.NEGATIVE, ENTITY);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorNegativeDays()
        {
            EntityAlarm alarm = new EntityAlarm("2", "-1", SentimentType.NEGATIVE, ENTITY);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorNullEntity()
        {
            EntityAlarm alarm = new EntityAlarm("2", "-1", SentimentType.NEGATIVE, null);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorNeutralSentimentType()
        {
            EntityAlarm alarm = new EntityAlarm("2", "-1", SentimentType.NEUTRAL, ENTITY);
        }

        [TestMethod]
        public void ConstructorWithId()
        {
            EntityAlarm alarm = new EntityAlarm(23, 2, 3, DateTime.Now, 3, SentimentType.POSITIVE, ENTITY);

            Assert.AreEqual(alarm.Id, 23);
        }

        #endregion

        #region Id

        [TestMethod]
        public void SetId()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.IsNotNull(alarm.Id);
        }

        #endregion

        #region PostQuantity
        
        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateInvalidPostQuantity()
        {
            EntityAlarm alarm = new EntityAlarm("2", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.PostQuantity = "-1";
        }

        [TestMethod]
        public void UpdateValidPostQuantity()
        {
            EntityAlarm alarm = new EntityAlarm("2", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.PostQuantity = "61";

            Assert.AreEqual(alarm.PostQuantity, "61");
        }

        [TestMethod]
        public void GetPostQuantity()
        {
            EntityAlarm alarm = new EntityAlarm("2", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.AreEqual(alarm.PostQuantity, "2");
        }

        #endregion

        #region Post Count
        
        [TestMethod]
        public void GetPostCount()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.AreEqual(alarm.PostCount, 0);
        }

        #endregion

        #region Number of Days

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateInvalidNumberDays()
        {
            EntityAlarm alarm = new EntityAlarm("2", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.Time = "-3";
        }


        [TestMethod]
        public void UpdateValidNumberDays()
        {
            EntityAlarm alarm = new EntityAlarm("2", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.Time = "5";

            Assert.AreEqual(alarm.Time, "5");
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateAlphanumericNumberDays()
        {
            EntityAlarm alarm = new EntityAlarm("2", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.Time = "asd";
        }

        [TestMethod]
        public void GetNumberDays()
        {
            EntityAlarm alarm = new EntityAlarm("2", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.AreEqual(alarm.Time, "1");
        }

        #endregion

        #region Sentiment Type

        [TestMethod]
        public void GetSentimentType()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.AreEqual(alarm.Type, SentimentType.NEGATIVE);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateNeutralSentimentType()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.Type = SentimentType.NEUTRAL;
        }

        [TestMethod]
        public void UpdateValidSentimentType()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.Type = SentimentType.POSITIVE;

            Assert.AreEqual(alarm.Type, SentimentType.POSITIVE);
        }

        #endregion

        #region Entity

        [TestMethod]
        public void GetEntity()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.AreEqual(alarm.Entity, ENTITY);
        }


        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateNullEntity()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, ENTITY);

            alarm.Entity = null;
        }

        [TestMethod]
        public void UpdateValidEntity()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, ENTITY);

            Entity uber = new Entity("Uber");

            alarm.Entity = uber;

            Assert.AreEqual(alarm.Entity, uber);
        }


        #endregion

        #region Analysis

        [TestMethod]
        public void AnalyzeUpdatePostCount()
        {
            EntityAlarm alarm = new EntityAlarm("2", "2", SentimentType.POSITIVE, ENTITY);

            Phrase phrase = new Phrase("I like google", YESTERDAY, AUTHOR);

            phrase.AnalyzePhrase(new List<Entity> { ENTITY }, new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });

            alarm.AnalyzePhrases(new List<Phrase> { phrase });

            Assert.AreEqual(alarm.PostCount, 1);
        }

        [TestMethod]
        public void AnalyzeNoUpdatePostCount()
        {
            EntityAlarm alarm = new EntityAlarm("2", "1", SentimentType.NEGATIVE, ENTITY);

            Phrase phrase = new Phrase("I like google", YESTERDAY.AddDays(-25), AUTHOR);

            phrase.AnalyzePhrase(new List<Entity> { ENTITY }, new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });

            alarm.AnalyzePhrases(new List<Phrase> { phrase });

            Assert.AreEqual(alarm.PostCount, 0);
        }

        [TestMethod]
        public void AnalyzePhraseWithoutEntity()
        {
            EntityAlarm alarm = new EntityAlarm("2", "2", SentimentType.NEGATIVE, ENTITY);

            Phrase phrase = new Phrase("I like UberEats", YESTERDAY.AddDays(-1), AUTHOR);

            try
            {
                phrase.AnalyzePhrase(new List<Entity> { ENTITY }, new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });
            }
            catch (AnalysisException) { }

            alarm.AnalyzePhrases(new List<Phrase> { phrase });

            Assert.AreEqual(alarm.PostCount, 0);
        }

        [TestMethod]
        public void ReAnalyze()
        {
            EntityAlarm alarm = new EntityAlarm("2", "2", SentimentType.POSITIVE, ENTITY);

            Phrase phrase = new Phrase("I like google", YESTERDAY, AUTHOR);

            try
            {
                phrase.AnalyzePhrase(new List<Entity> { ENTITY }, new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });
            }
            catch (AnalysisException) { }

            alarm.AnalyzePhrases(new List<Phrase> { phrase });
            alarm.ReAnalyePhrases(new List<Phrase> { phrase });

            Assert.AreEqual(alarm.PostCount, 1);
        }

        #endregion

        #region Methdods

        [TestMethod]
        public void IsEnabled()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, ENTITY);

            Assert.IsFalse(alarm.IsEnabled());
        }

        [TestMethod]
        public void IsNotEnabled()
        {
            EntityAlarm alarm = new EntityAlarm("2", "1", SentimentType.POSITIVE, ENTITY);

            Phrase phrase = new Phrase("I like google", YESTERDAY, AUTHOR);

            phrase.AnalyzePhrase(new List<Entity> { ENTITY }, new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });

            alarm.AnalyzePhrases(new List<Phrase> { phrase });

            Assert.IsFalse(alarm.IsEnabled());
        }


        [TestMethod]
        public void GetToString()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, ENTITY);
            
            string str = "Alarm POSITIVE, for Entity: Google, with PostQuantity: 1";

            Assert.AreEqual(alarm.ToString(), str);
        }

        [TestMethod]
        public void GetAlarmStatus()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, ENTITY);

            string str = $"In 1 Days/Hours there where 0 {SentimentType.POSITIVE} posts";

            Assert.AreEqual(alarm.GetStatus(), str);
        }

        [TestMethod]
        public void EqualsNull()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, ENTITY);

            Assert.IsFalse(alarm.Equals(null));
        }

        [TestMethod]
        public void EqualsDbNull()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, ENTITY);

            Assert.IsFalse(alarm.Equals(DBNull.Value));
        }

        [TestMethod]
        public void AreEquals()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, ENTITY);
            EntityAlarm otherAlarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, ENTITY);

            Assert.IsTrue(alarm.Equals(otherAlarm));
        }

        [TestMethod]
        public void NotEquals()
        {
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, ENTITY);
            EntityAlarm otherAlarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, ENTITY);

            Assert.IsFalse(alarm.Equals(otherAlarm));
        }
        #endregion 
    }
}
