using BusinessLogic.DTO;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Test.Tests
{
    [TestClass]
    public class SentimentRepositoryTest
    {
        #region Attributes

        private static Sentiment SENTIMENT;
        private static SentimentRepository REPOSITORY;
        private static Author AUTHOR;

        #endregion

        #region SetUp

        [TestInitialize]
        public void SetUp()
        {
            REPOSITORY = new SentimentRepository();
            CleanRepositories();

            AUTHOR = new Author("sada", "sadasd", "sdaasd", new DateTime(1995, 2 ,3));
            SENTIMENT = new Sentiment(SentimentType.POSITIVE, "Me gusta");
        }

        [TestCleanup]
        public void Cleanup()
        {
            CleanRepositories();
        }

        #endregion

        #region Add

        [TestMethod]
        [ExpectedException(typeof(SentimentException))]
        public void AddDuplicatedSentiment()
        {
            REPOSITORY.Add(SENTIMENT);

            REPOSITORY.Add(SENTIMENT);
        }

        [TestMethod]
        public void AddSentimentUpdatesPhrases()
        {
            PhraseRepository phraseRepository = new PhraseRepository();
            phraseRepository.Clear();

            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Clear();

            authorRepository.Add(AUTHOR);

            Phrase phrase = new Phrase("i like google", DateTime.Now, AUTHOR); 
            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, "i like");
            
            try
            {
                phraseRepository.Add(phrase);
            }
            catch (AnalysisException) { }
            
            try
            {
                REPOSITORY.Add(sentiment);
            }
            catch (AnalysisException) { }

            Assert.AreEqual(sentiment.Type, phraseRepository.Get(phrase.Id).Type);
        }

        [TestMethod]
        public void AddSentimentUpdateAlarm()
        {
            EntityAlarmRepository alarmRepository = new EntityAlarmRepository();
            alarmRepository.Clear();

            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Clear();

            PhraseRepository phraseRepository = new PhraseRepository();
            phraseRepository.Clear();

            EntityRepository entityRepository = new EntityRepository();
            entityRepository.Clear();

            authorRepository.Add(AUTHOR);

            Phrase phrase = new Phrase("i like google", DateTime.Now, AUTHOR);
            Entity google = new Entity("google");
            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, "i like");

            try
            {
                entityRepository.Add(google);
            }
            catch (AnalysisException) { }

            try
            {
                phraseRepository.Add(phrase);
            }
            catch (AnalysisException) { }

            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, google);
            alarmRepository.Add(alarm);

            REPOSITORY.Add(sentiment);

            Assert.AreEqual(alarmRepository.Get(alarm.Id).PostCount, 1);
        }

        [TestMethod]
        public void AddSentimentNoUpdateDifferentSentimentTypeAlarm()
        {
            Phrase phrase = new Phrase("i like google", DateTime.Now, AUTHOR);
            Entity google = new Entity("google");

            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, "i like");
            try
            {
                phrase.AnalyzePhrase(new List<Entity> { google }, new List<Sentiment> { sentiment });
            }
            catch (AnalysisException) { }

            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, google);

            REPOSITORY.Add(sentiment);

            Assert.AreEqual(alarm.PostCount, 0);
        }

        #endregion

        #region Modify

        [TestMethod]
        public void ModifySentimentWord()
        {
            string newWord = "Me encanta";

            SentimentRepository repository = new SentimentRepository();
            repository.Add(SENTIMENT);

            SENTIMENT.Word = newWord;

            REPOSITORY.Modify(SENTIMENT.Id, SENTIMENT);

            Assert.AreEqual(repository.Get(SENTIMENT.Id).Word, newWord);
        }

        [TestMethod]
        public void ModifySentimentType()
        {
            string newWord = "Me disgusta";
            SentimentType newType = SentimentType.NEGATIVE;

            SentimentRepository repository = new SentimentRepository();
            repository.Add(SENTIMENT);

            SENTIMENT.Word = newWord;
            SENTIMENT.Type = newType;

            REPOSITORY.Modify(SENTIMENT.Id, SENTIMENT);

            Assert.AreEqual(repository.Get(SENTIMENT.Id).Type, (SentimentType)newType);
        }

        [TestMethod]
        public void ModifySentimentUpdatesPhrases()
        {
            Phrase phrase = new Phrase("i like google", DateTime.Now, AUTHOR);

            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, "i like");

            REPOSITORY.Add(sentiment);

            sentiment.Word = "i dont like";
            sentiment.Type = SentimentType.NEGATIVE;

            REPOSITORY.Modify(sentiment.Id, sentiment);

            Assert.IsNull(phrase.Type);
        }

        [TestMethod]
        public void MoidifySentimentUpdateAlarm()
        {
            PhraseRepository phraseRepository = new PhraseRepository();
            EntityRepository entityRepository = new EntityRepository();
            AuthorRepository authorRepository = new AuthorRepository();
            EntityAlarmRepository alarmRepository = new EntityAlarmRepository();

            Phrase phrase = new Phrase("i like google", DateTime.Now, AUTHOR);
            Entity google = new Entity("google");
            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, "i like");

            authorRepository.Add(AUTHOR);

            try
            {
                phraseRepository.Add(phrase);
            }
            catch (AnalysisException) { }

            try
            {
                entityRepository.Add(google);
            }
            catch (AnalysisException) { }

            try
            {
                phrase.AnalyzePhrase(new List<Entity> { google }, new List<Sentiment> { sentiment });
            }
            catch (AnalysisException) { }

            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, google);
            alarmRepository.Add(alarm);

            try
            {
                REPOSITORY.Add(sentiment);
            }
            catch (AnalysisException) { }

            SENTIMENT.Word = "i dont like";
            SENTIMENT.Type = SentimentType.NEGATIVE;

            var aaaa = alarmRepository.Get(alarm.Id);

            REPOSITORY.Modify(sentiment.Id, SENTIMENT);

            Assert.AreEqual(alarmRepository.Get(alarm.Id).PostCount, 0);
        }

        [TestMethod]
        public void ModifySentimentNoUpdateDifferentSentimentTypeAlarm()
        {
            PhraseRepository phraseRepository = new PhraseRepository();
            EntityRepository entityRepository = new EntityRepository();
            AuthorRepository authorRepository = new AuthorRepository();
            EntityAlarmRepository alarmRepository = new EntityAlarmRepository();

            Phrase phrase = new Phrase("i like google", DateTime.Now, AUTHOR);
            Entity google = new Entity("google");
            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, "i like");

            authorRepository.Add(AUTHOR);

            try
            {
                phraseRepository.Add(phrase);
            }
            catch (AnalysisException) { }

            try
            {
                entityRepository.Add(google);
            }
            catch (AnalysisException) { }

            try
            {
                phrase.AnalyzePhrase(new List<Entity> { google }, new List<Sentiment> { sentiment });
            }
            catch (AnalysisException) { }

            try
            {
                REPOSITORY.Add(sentiment);
            }
            catch (AnalysisException) { }

            SENTIMENT.Word = "i dont like";
            SENTIMENT.Type = SentimentType.NEGATIVE;

            REPOSITORY.Modify(sentiment.Id, SENTIMENT);

            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, google);
            alarmRepository.Add(alarm);

            SENTIMENT.Word = "i really like";

            REPOSITORY.Modify(sentiment.Id, SENTIMENT);

            Assert.AreEqual(alarmRepository.Get(alarm.Id).PostCount, 0);
        }

        [TestMethod]
        public void ModifySentimentNotUpdatePhraseWithoutType()
        {
            PhraseRepository phraseRepository = new PhraseRepository();
            AuthorRepository authorRepository = new AuthorRepository();

            Phrase phrase = new Phrase("i like google", DateTime.Now, AUTHOR);
            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, "i love");

            authorRepository.Add(AUTHOR);

            try
            {
                REPOSITORY.Add(sentiment);
            }
            catch (AnalysisException) { }

            try
            {
                phraseRepository.Add(phrase);
            }
            catch (AnalysisException) { }

            SENTIMENT.Word = "i dont like";
            SENTIMENT.Type = SentimentType.NEGATIVE;

            REPOSITORY.Modify(sentiment.Id, SENTIMENT);

            Assert.IsNull(phraseRepository.Get(phrase.Id).Type);
        }

        #endregion

        #region Delete

        [TestMethod]
        public void DeleteSentiment()
        {
            SentimentRepository repository = new SentimentRepository();
            repository.Add(SENTIMENT);

            repository.Delete(SENTIMENT.Id);

            Assert.IsTrue(repository.IsEmpty());
        }

        [TestMethod]
        [ExpectedException(typeof(SentimentException))]
        public void DeleteNullSentiment()
        {
            SentimentRepository repository = new SentimentRepository();

            repository.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(SentimentException))]
        public void DeleteNonExistenceSentiment()
        {
            SentimentRepository repository = new SentimentRepository();

            repository.Delete(SENTIMENT.Id);
        }

        [TestMethod]
        public void DeleteSentimentUpdatesPhrases()
        {
            Phrase phrase = new Phrase("i like google", DateTime.Now, AUTHOR);

            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, "i like");

            REPOSITORY.Add(sentiment);

            REPOSITORY.Delete(sentiment.Id);

            Assert.IsNull(phrase.Type);
        }

        [TestMethod]
        public void DeleteSentimentUpdateAlarm()
        {
            Phrase phrase = new Phrase("i like google", DateTime.Now, AUTHOR);
            Entity google = new Entity("google");

            Sentiment sentiment = new Sentiment(SentimentType.POSITIVE, "i like");
            try
            {
                phrase.AnalyzePhrase(new List<Entity> { google }, new List<Sentiment> { sentiment });
            }
            catch (AnalysisException) { }

            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, google);

            REPOSITORY.Add(sentiment);

            REPOSITORY.Delete(sentiment.Id);

            Assert.AreEqual(alarm.PostCount, 0);
        }

        [TestMethod]
        public void DeleteSentimentNoUpdatesPhrasesWithDiffType()
        {
            Phrase phrase = new Phrase("i really dont like google", DateTime.Now, AUTHOR);

            Sentiment sentiment = new Sentiment(SentimentType.NEGATIVE, "i dont like");

            REPOSITORY.Add(sentiment);

            REPOSITORY.Delete(sentiment.Id);

            Assert.IsNull(phrase.Type);
        }

        #endregion

        #region IsEmpty

        [TestMethod]
        public void IsEmpty()
        {
            SentimentRepository repository = new SentimentRepository();

            Assert.IsTrue(repository.IsEmpty());
        }

        [TestMethod]
        public void NotEmpty()
        {
            SentimentRepository repository = new SentimentRepository();
            repository.Add(SENTIMENT);

            Assert.IsFalse(repository.IsEmpty());
        }

        #endregion

        #region GetAll

        [TestMethod]
        public void GetAllSentiments()
        {
            List<Sentiment> allSentiments = new List<Sentiment>();

            Sentiment firstPossitive = new Sentiment(SentimentType.POSITIVE, "i like");
            Sentiment secondPositive = new Sentiment(SentimentType.POSITIVE, "i love");
            Sentiment negative = new Sentiment(SentimentType.NEGATIVE, "i hate");

            SentimentRepository repository = new SentimentRepository();
            repository.Add(firstPossitive);
            repository.Add(secondPositive);
            repository.Add(negative);

            allSentiments.Add(firstPossitive);
            allSentiments.Add(secondPositive);
            allSentiments.Add(negative);

            Assert.IsTrue(repository.GetAll().SequenceEqual(allSentiments));
        }

        #endregion

        #region GetBySentimentType

        [TestMethod]
        public void GetAllSentimentByType()
        {
            Sentiment firstPossitive = new Sentiment(SentimentType.POSITIVE, "i like");
            Sentiment secondPositive = new Sentiment(SentimentType.POSITIVE, "i love");
            Sentiment negative = new Sentiment(SentimentType.NEGATIVE, "i hate");

            SentimentRepository repository = new SentimentRepository();
            repository.Add(firstPossitive);
            repository.Add(secondPositive);
            repository.Add(negative);

            IList<Sentiment> positiveRepository = repository.GetBySentimentType(SentimentType.POSITIVE);

            Assert.IsTrue(positiveRepository.Contains(firstPossitive) && positiveRepository.Contains(secondPositive));
        }

        #endregion

        #region GetById

        [TestMethod]
        public void GetById()
        {
            SentimentRepository repository = new SentimentRepository();
            repository.Add(SENTIMENT);

            Assert.IsNotNull(repository.Get(SENTIMENT.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(SentimentException))]
        public void GetNonExitentId()
        {
            SentimentRepository repository = new SentimentRepository();

            repository.Get(SENTIMENT.Id + 1);
        }

        #endregion


        private void CleanRepositories()
        {
            AuthorAlarmRepository authorAlarmRepository = new AuthorAlarmRepository();
            PhraseRepository phraseRepository = new PhraseRepository();
            EntityRepository entityRepository = new EntityRepository();
            SentimentRepository sentimentRepository = new SentimentRepository();
            AuthorRepository authorRepository = new AuthorRepository();
            AlarmRepository alarmRepository = new AlarmRepository();

            authorAlarmRepository.Clear();
            phraseRepository.Clear();
            entityRepository.Clear();
            sentimentRepository.Clear();
            authorRepository.Clear();
            alarmRepository.Clear();
        }
    }
}
