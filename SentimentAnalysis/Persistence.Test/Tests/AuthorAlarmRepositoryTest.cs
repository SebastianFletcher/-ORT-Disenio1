using BusinessLogic.DTO;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.Repositories;
using Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Test.Tests
{
    [TestClass]
    public class AuthorAlarmRepositoryTest
    {
        #region Attributes

        private AuthorAlarm ALARM;
        private Author AUTHOR;
        private AuthorAlarmRepository REPOSITORY;

        #endregion

        #region SetUp

        [TestInitialize]
        public void SetUp()
        {
            AUTHOR = new Author("sada", "sadasd", "sdaasd", new DateTime(1995, 2, 3));

            ALARM = new AuthorAlarm("1", "1", SentimentType.POSITIVE, TimeMeasure.DAYS);
            REPOSITORY = new AuthorAlarmRepository();

            CleanRepositories();
        }

        [TestCleanup]
        public void Cleanup()
        {
            CleanRepositories();
        }

        #endregion

        #region Add

        [TestMethod]
        public void AddAlarm()
        {
            REPOSITORY.Add(ALARM);

            Assert.IsNotNull(REPOSITORY.Get(ALARM.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void AddExistentAlarm()
        {
            REPOSITORY.Add(ALARM);
            REPOSITORY.Add(ALARM);
        }

        [TestMethod]
        public void UpdateAlarmWithOldPhrases()
        {
            SentimentRepository sentimentRepository = new SentimentRepository();
            sentimentRepository.Add(new Sentiment(SentimentType.POSITIVE, "i like"));

            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Add(AUTHOR);

            PhraseRepository phraseRepository = new PhraseRepository();
            
            REPOSITORY.Add(ALARM);

            Phrase phraseTest = new Phrase($"i like Google", DateTime.Now, AUTHOR);

            try
            {
                phraseRepository.Add(phraseTest);
            }
            catch (AnalysisException) { }

            Assert.AreEqual(REPOSITORY.Get(ALARM.Id).PostCount, 1);
        }

        #endregion

        #region Modify

        [TestMethod]
        public void ModifyAlarm()
        {
            REPOSITORY.Add(ALARM);

            ALARM.TimeMeasure = TimeMeasure.HOURS;

            REPOSITORY.Modify(ALARM.Id, ALARM);

            Assert.AreEqual(REPOSITORY.Get(ALARM.Id).TimeMeasure, TimeMeasure.HOURS);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ModifyNonExitentAlarm()
        {
            REPOSITORY.Modify(ALARM.Id, ALARM);
        }

        [TestMethod]
        public void ModityAlarmSentimentType()
        {
            REPOSITORY.Add(ALARM);

            ALARM.Type = SentimentType.NEGATIVE;

            REPOSITORY.Modify(ALARM.Id, ALARM);

            Assert.AreEqual(ALARM.Type, SentimentType.NEGATIVE);
        }

        #endregion

        #region Delete

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void DeleteNullAlarm()
        {
            REPOSITORY.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void DeleteAlarm()
        {
            REPOSITORY.Add(ALARM);

            REPOSITORY.Delete(ALARM.Id);

            REPOSITORY.Get(ALARM.Id);
        }

        #endregion

        #region IsEmpty

        [TestMethod]
        public void IsEmpty()
        {
            Assert.IsTrue(REPOSITORY.IsEmpty());
        }

        [TestMethod]
        public void IsNotEmpty()
        {
            REPOSITORY.Add(ALARM);

            Assert.IsFalse(REPOSITORY.IsEmpty());
        }

        #endregion

        #region GetAll

        [TestMethod]
        public void GetAllAlarms()
        {
            List<AuthorAlarm> alarms = new List<AuthorAlarm>();

            AuthorAlarm newAlarm = new AuthorAlarm("3", "1", SentimentType.NEGATIVE, TimeMeasure.HOURS);

            alarms.Add(ALARM);
            alarms.Add(newAlarm);

            REPOSITORY.Add(ALARM);
            REPOSITORY.Add(newAlarm);

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                var repositoryAlarms = Helper.Instance.GetAuthorAlarms(context.AuthorAlarms);

                Assert.IsTrue(repositoryAlarms.SequenceEqual(alarms));
            }
        }

        #endregion

        #region GetId

        [TestMethod]
        public void GetById()
        {
            REPOSITORY.Add(ALARM);

            Assert.IsNotNull(REPOSITORY.Get(ALARM.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void GetNonExitentId()
        {
            REPOSITORY.Get(ALARM.Id + 1);
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