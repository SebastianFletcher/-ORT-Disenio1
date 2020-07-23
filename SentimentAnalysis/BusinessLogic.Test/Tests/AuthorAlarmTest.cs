using BusinessLogic.DTO;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Test.Tests
{
    [TestClass]
    public class AuthorAlarmTest
    {
        #region Attributes 

        private static DateTime YESTERDAY;
        private static Author AUTHOR;

        #endregion

        #region SetUp 

        [TestInitialize]
        public void SetUp()
        {
            YESTERDAY = DateTime.Now.AddDays(-1);
            AUTHOR = new Author("sada", "sadasd", "sdaasd", new DateTime(1995, 1, 2));
        }

        [TestCleanup]
        public void Cleanup()
        {
            YESTERDAY = DateTime.Now;
        }

        #endregion

        #region Constuctor 

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorPostQuantityZero()
        {
            AuthorAlarm alarm = new AuthorAlarm("0", "1", SentimentType.NEGATIVE, TimeMeasure.HOURS);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorPostQuantityNegative()
        {
            AuthorAlarm alarm = new AuthorAlarm("-2", "1", SentimentType.NEGATIVE, TimeMeasure.HOURS);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorPostQuantityAlphabetic()
        {
            AuthorAlarm alarm = new AuthorAlarm("dos", "1", SentimentType.NEGATIVE, TimeMeasure.HOURS);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorNegativeDays()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "-1", SentimentType.NEGATIVE, TimeMeasure.HOURS);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ConstructorNeutralSentimentType()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "-1", SentimentType.NEUTRAL, TimeMeasure.DAYS);
        }

        [TestMethod]
        public void ConstructorWithId()
        {
            AuthorAlarm alarm = new AuthorAlarm(3, 2, 3, DateTime.Now, 3, SentimentType.POSITIVE, TimeMeasure.DAYS);

            Assert.AreEqual(alarm.Id, 3);
        }

        #endregion

        #region Id

        [TestMethod]
        public void SetId()
        {
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            Assert.IsNotNull(alarm.Id);
        }

        #endregion

        #region PostQuantity

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateInvalidPostQuantity()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            alarm.PostQuantity = "-1";
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateMaxPostQuantity()
        {
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            alarm.PostQuantity = "61";
        }

        [TestMethod]
        public void GetPostQuantity()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            Assert.AreEqual(alarm.PostQuantity, "2");
        }

        #endregion

        #region Post Count

        [TestMethod]
        public void GetPostCount()
        {
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            Assert.AreEqual(alarm.PostCount, 0);
        }

        #endregion

        #region Number of Days

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateInvalidNumberDays()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            alarm.Time = "-3";
        }


        [TestMethod]
        public void UpdateValidNumberDays()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            alarm.Time = "5";

            Assert.AreEqual(alarm.Time, "5");
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateAlphanumericNumberDays()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            alarm.Time = "asd";
        }

        [TestMethod]
        public void GetNumberDays()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            Assert.AreEqual(alarm.Time, "1");
        }

        #endregion

        #region Sentiment Type

        [TestMethod]
        public void GetSentimentType()
        {
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            Assert.AreEqual(alarm.Type, SentimentType.NEGATIVE);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void UpdateNeutralSentimentType()
        {
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            alarm.Type = SentimentType.NEUTRAL;
        }

        [TestMethod]
        public void UpdateValidSentimentType()
        {
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            alarm.Type = SentimentType.POSITIVE;

            Assert.AreEqual(alarm.Type, SentimentType.POSITIVE);
        }

        #endregion

        #region Analysis

        [TestMethod]
        public void AnalyzeUpdatePostCount()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "2", SentimentType.POSITIVE, TimeMeasure.HOURS);

            Phrase phrase = new Phrase("I like google", DateTime.Now.AddHours(-1), AUTHOR);

            try
            {
                phrase.AnalyzePhrase(new List<Entity>(), new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });
            }
            catch (AnalysisException) { }

            alarm.AnalyzePhrases(new List<Phrase> { phrase });

            Assert.AreEqual(alarm.PostCount, 1);
        }

        [TestMethod]
        public void AnalyzeUpdateCountDuplicateAuthors()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "2", SentimentType.POSITIVE, TimeMeasure.HOURS);

            Phrase firstPhrase = new Phrase("I like google", DateTime.Now.AddHours(-1), AUTHOR);
            Phrase secondPhrase = new Phrase("I like starbucks", DateTime.Now.AddHours(-1), AUTHOR);

            try
            {
                firstPhrase.AnalyzePhrase(new List<Entity>(), new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });
            }
            catch (AnalysisException) { }

            try
            {
                secondPhrase.AnalyzePhrase(new List<Entity>(), new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });
            }
            catch (AnalysisException) { }

            alarm.AnalyzePhrases(new List<Phrase> { firstPhrase, secondPhrase });

            Assert.AreEqual(alarm.PostCount, 2);
        }

        [TestMethod]
        public void AnalyzeNoUpdatePostCount()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            Phrase phrase = new Phrase("I like google", YESTERDAY.AddDays(-25), AUTHOR);

            try
            {
                phrase.AnalyzePhrase(new List<Entity>(), new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });
            }
            catch (AnalysisException) { }

            alarm.AnalyzePhrases(new List<Phrase> { phrase });

            Assert.AreEqual(alarm.PostCount, 0);
        }

        [TestMethod]
        public void ReAnalyzePostCount()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "2", SentimentType.POSITIVE, TimeMeasure.DAYS);

            Phrase phrase = new Phrase("I like google", YESTERDAY, AUTHOR);

            try
            {
                phrase.AnalyzePhrase(new List<Entity>(), new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });
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
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.POSITIVE, TimeMeasure.DAYS);

            Assert.IsFalse(alarm.IsEnabled());
        }

        [TestMethod]
        public void IsNotEnabled()
        {
            AuthorAlarm alarm = new AuthorAlarm("2", "1", SentimentType.POSITIVE, TimeMeasure.HOURS);

            Phrase phrase = new Phrase("I like google", YESTERDAY, AUTHOR);

            try
            {
                phrase.AnalyzePhrase(new List<Entity>(), new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") });
            }
            catch (AnalysisException) { }

            alarm.AnalyzePhrases(new List<Phrase> { phrase });

            Assert.IsFalse(alarm.IsEnabled());
        }


        [TestMethod]
        public void GetToString()
        {
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.POSITIVE, TimeMeasure.DAYS);

            string str = "Alarm POSITIVE, measured in DAYS, with PostQuantity: 1.";

            Assert.AreEqual(alarm.ToString(), str);
        }

        #endregion

        #region Equals

        [TestMethod]
        public void EqualsNull()
        {
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.POSITIVE, TimeMeasure.DAYS);

            Assert.IsFalse(alarm.Equals(null));
        }

        [TestMethod]
        public void EqualsDbNull()
        {
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.POSITIVE, TimeMeasure.DAYS);

            Assert.IsFalse(alarm.Equals(DBNull.Value));
        }

        [TestMethod]
        public void AreEquals()
        {
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.POSITIVE, TimeMeasure.DAYS);
            AuthorAlarm otherAlarm = new AuthorAlarm("1", "1", SentimentType.POSITIVE, TimeMeasure.DAYS);

            Assert.IsTrue(alarm.Equals(otherAlarm));
        }

        [TestMethod]
        public void NotEquals()
        {
            AuthorAlarm alarm = new AuthorAlarm("1", "1", SentimentType.POSITIVE, TimeMeasure.DAYS);
            AuthorAlarm otherAlarm = new AuthorAlarm("1", "1", SentimentType.NEGATIVE, TimeMeasure.DAYS);

            Assert.IsFalse(alarm.Equals(otherAlarm));
        }

        #endregion 
    }
}
