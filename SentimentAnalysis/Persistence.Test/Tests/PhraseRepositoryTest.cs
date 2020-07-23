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
    public class PhraseRepositoryTest
    {
        #region Attributes

        private static Phrase PHRASE;
        private static PhraseRepository REPOSITORY;
        private static Author AUTHOR;
        private static EntityAlarm ALARM;
        private static List<Entity> ENTITITES;
        private static List<Sentiment> SENTIMENTS;
        private static List<EntityAlarm> ALARMS;

        #endregion

        #region SetUp

        [TestInitialize]
        public void SetUp()
        {
            CleanRepositories();

            AUTHOR = new Author("sada", "sadasd", "sdaasd", new DateTime(1995, 2, 3));

            Entity google = new Entity("Google");
            Entity starbucks = new Entity("Starbucks");
            ENTITITES = new List<Entity>();
            ENTITITES.Add(google);
            ENTITITES.Add(starbucks);

            Sentiment positive = new Sentiment(SentimentType.POSITIVE, "i like");
            Sentiment negative = new Sentiment(SentimentType.NEGATIVE, "i dont like");
            SENTIMENTS = new List<Sentiment>();
            SENTIMENTS.Add(positive);
            SENTIMENTS.Add(negative);

            PHRASE = new Phrase("I like Google", DateTime.Now, AUTHOR);

            ALARM = new EntityAlarm("1", "1", SentimentType.POSITIVE, google);
            ALARMS = new List<EntityAlarm>();
            ALARMS.Add(ALARM);

            REPOSITORY = new PhraseRepository();
        }

        [TestCleanup]
        public void Cleanup()
        {
            CleanRepositories();
        }

        #endregion

        #region Add

        [TestMethod]
        public void AddPhrase()
        {
            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Add(PHRASE.Author);
            try
            {
                REPOSITORY.Add(PHRASE);
            }
            catch (AnalysisException) { }

            Assert.IsNotNull(REPOSITORY.Get(PHRASE.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AddPhraseEntityNull()
        {
            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Add(PHRASE.Author);

            REPOSITORY.Add(PHRASE);
        }

        [TestMethod]
        [ExpectedException(typeof(AnalysisException))]
        public void AddPhraseSentimentNull()
        {
            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Add(PHRASE.Author);

            REPOSITORY.Add(PHRASE);
        }

        [TestMethod]
        public void AddUpdateAlarm()
        {
            EntityRepository entityRepository = new EntityRepository();
            entityRepository.Add(ENTITITES.First(e => e.Name.Equals("Starbucks")));

            EntityAlarmRepository alarmRepository = new EntityAlarmRepository();
            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.NEGATIVE, ENTITITES.First(e => e.Name.Equals("Starbucks")));
            alarmRepository.Add(alarm);

            SentimentRepository sentimentRepository = new SentimentRepository();
            sentimentRepository.Add(new Sentiment(SentimentType.NEGATIVE, "i dont like"));

            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Add(AUTHOR);

            Phrase phrase = new Phrase("I dont like Starbucks", DateTime.Now, AUTHOR);
            REPOSITORY.Add(phrase);

            Assert.AreEqual(alarmRepository.Get(alarm.Id).PostCount, 1);
        }

        #endregion

        #region Modify

        [TestMethod]
        public void ExistentModify()
        {
            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Add(AUTHOR);

            try
            {
                REPOSITORY.Add(PHRASE);
            }
            catch (AnalysisException) { }

            Author newAuthor = new Author("Seba", "Seba", "Gonzalez", new DateTime(1998, 3, 6));
            authorRepository.Add(newAuthor);

            PHRASE.Author = newAuthor;

            try
            {
                REPOSITORY.Modify(PHRASE.Id, PHRASE);
            }
            catch (AnalysisException) { }

            Assert.AreEqual(REPOSITORY.Get(PHRASE.Id).Author.Id, newAuthor.Id);
        }

        #endregion

        #region Delete

        [TestMethod]
        [ExpectedException(typeof(PhraseException))]
        public void DeleteNull()
        {
            REPOSITORY.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(PhraseException))]
        public void Delete()
        {
            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Add(AUTHOR);

            try
            {
                REPOSITORY.Add(PHRASE);
            }
            catch (AnalysisException) { }

            REPOSITORY.Delete(PHRASE.Id);

            REPOSITORY.Get(PHRASE.Id);
        }

        #endregion

        #region IsEmpty

        [TestMethod]
        public void IsEmpty()
        {
            Assert.IsTrue(REPOSITORY.IsEmpty());
        }

        [TestMethod]
        public void IsNonEmpty()
        {
            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Add(PHRASE.Author);
            try
            {
                REPOSITORY.Add(PHRASE);
            }
            catch (AnalysisException) { }

            Assert.IsFalse(REPOSITORY.IsEmpty());
        }

        #endregion

        #region GetAll

        [TestMethod]
        public void GetAllPhrases()
        {
            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Add(AUTHOR);

            IList<Phrase> phrases = new List<Phrase>();

            Phrase firstPhrase = new Phrase("i like", DateTime.Now, AUTHOR);
            Phrase secondPhrase = new Phrase("i dont like", DateTime.Now, AUTHOR);

            phrases.Add(firstPhrase);
            phrases.Add(secondPhrase);

            try
            {
                REPOSITORY.Add(firstPhrase);
            }
            catch (AnalysisException) { }

            try
            {
                REPOSITORY.Add(secondPhrase);
            }
            catch (AnalysisException) { }

            Assert.IsTrue(REPOSITORY.GetAll().SequenceEqual(phrases));
        }

        #endregion

        #region GetById

        [TestMethod]
        [ExpectedException(typeof(PhraseException))]
        public void GetNonExistentId()
        {
            REPOSITORY.Get(PHRASE.Id + 1);
        }

        [TestMethod]
        public void GetById()
        {
            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Add(PHRASE.Author);
            try
            {
                REPOSITORY.Add(PHRASE);
            }
            catch (AnalysisException) { }

            Assert.IsNotNull(REPOSITORY.Get(PHRASE.Id));
        }
        #endregion

        #region GetGrade



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
