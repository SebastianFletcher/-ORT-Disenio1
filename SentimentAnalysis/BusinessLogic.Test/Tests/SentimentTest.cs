using BusinessLogic.Enums;
using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.Test.Tests
{
    [TestClass]
    public class SentimentTest
    {
        #region Attributes

        private static string WORD;

        #endregion

        #region SetUp

        [TestInitialize]
        public void SetUp()
        {
            WORD = "Me gusta";
        }

        [TestCleanup]
        public void Cleanup()
        {
            WORD = string.Empty;
        }

        #endregion

        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(SentimentException))]
        public void ConstructorNeutralSentimentType()
        {
            Sentiment sentiment = new Sentiment(SentimentType.NEUTRAL, WORD);
        }

        [TestMethod]
        [ExpectedException(typeof(SentimentException))]
        public void ConstructorWordNull()
        {
            Sentiment sentiment = new Sentiment(SentimentType.NEGATIVE, null);
        }

        [TestMethod]
        public void ConstructorWithId()
        {
            Sentiment sentiment = new Sentiment(32, SentimentType.NEGATIVE, "Phrase test");

            Assert.AreEqual(sentiment.Id, 32);
        }

        #endregion

        #region Id

        [TestMethod]
        public void SetId()
        {
            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, WORD);

            Assert.IsNotNull(sentiment.Id);
        }

        #endregion

        #region SentimentType

        [TestMethod]
        public void GetCorrectSentimentType()
        {
            Sentiment sentiment = new Sentiment(SentimentType.NEGATIVE, WORD);

            Assert.AreEqual(sentiment.Type, SentimentType.NEGATIVE);
        }

        #endregion

        #region Word

        [TestMethod]
        public void GetCorrectWord()
        {
            Sentiment sentiment = new Sentiment(SentimentType.NEGATIVE, WORD);

            Assert.AreEqual(sentiment.Word, WORD);
        }

        #endregion

        #region Methods

        [TestMethod]
        public void ToStringTest()
        {
            string str = string.Format("{0} - [{1}]", WORD, SentimentType.POSITIVE.ToString());
            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, WORD);

            Assert.AreEqual(sentiment.ToString(), str);
        }

        [TestMethod]
        public void EqualsNull()
        {
            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, WORD);

            Assert.IsFalse(sentiment.Equals(null));
        }

        [TestMethod]
        public void EqualsDbNull()
        {
            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, WORD);

            Assert.IsFalse(sentiment.Equals(DBNull.Value));
        }

        [TestMethod]
        public void AreEquals()
        {
            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, WORD);
            Sentiment sameSentiment = new Sentiment(SentimentType.POSITIVE, WORD);

            Assert.IsTrue(sentiment.Equals(sameSentiment));
        }

        [TestMethod]
        public void NotEquals()
        {
            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, WORD);
            Sentiment sameSentiment = new Sentiment(SentimentType.NEGATIVE, "Other phrase");

            Assert.IsFalse(sentiment.Equals(sameSentiment));
        }

        #endregion
    }
}
