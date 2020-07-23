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
    public class EntityAlarmRepositoryTest
    {
        #region Attributes

        private EntityAlarm ALARM;
        private Entity ENTITY;
        private Author AUTHOR;
        private EntityAlarmRepository REPOSITORY;

        private List<Entity> ENTITIES;
        private List<Sentiment> SENTIMENTS;
        private List<Phrase> PHRASES;

        #endregion

        #region SetUp

        [TestInitialize]
        public void SetUp()
        {
            AUTHOR = new Author("sada", "sadasd", "sdaasd", new DateTime(1995, 2 ,3));

            ENTITY = new Entity("Google");
            ALARM = new EntityAlarm("1", "1", SentimentType.POSITIVE, ENTITY);
            REPOSITORY = new EntityAlarmRepository();

            ENTITIES = new List<Entity> { ENTITY };
            SENTIMENTS = new List<Sentiment> { new Sentiment(SentimentType.POSITIVE, "I like") };
            PHRASES = new List<Phrase>();

            Phrase phrase = new Phrase("I like Google", DateTime.Now, AUTHOR);
            phrase.AnalyzePhrase(ENTITIES, SENTIMENTS);
            PHRASES.Add(phrase);

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
            EntityRepository entityRepository = new EntityRepository();
            entityRepository.Add(ALARM.Entity);

            REPOSITORY.Add(ALARM);

            Assert.IsNotNull(REPOSITORY.Get(ALARM.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void AddExistentAlarm()
        {
            EntityRepository entityRepository = new EntityRepository();
            entityRepository.Add(ALARM.Entity);

            REPOSITORY.Add(ALARM);
            REPOSITORY.Add(ALARM);
        }

        [TestMethod]
        public void UpdateAlarmWithOldPhrases()
        {
            EntityRepository entityRepository = new EntityRepository();
            entityRepository.Add(ALARM.Entity);

            SentimentRepository sentimentRepository = new SentimentRepository();
            sentimentRepository.Add(new Sentiment(SentimentType.POSITIVE, "i like"));

            AuthorRepository authorRepository = new AuthorRepository();
            authorRepository.Add(AUTHOR);
            
            PhraseRepository phraseRepository = new PhraseRepository();
            phraseRepository.Add(new Phrase($"i like {ALARM.Entity.Name}", DateTime.Now.AddDays(-1), AUTHOR));

            REPOSITORY.Add(ALARM);
            
            Assert.AreEqual(REPOSITORY.Get(ALARM.Id).PostCount, 1);            
        }

        #endregion

        #region Modify

        [TestMethod]
        public void ModifyAlarm()
        {
            Entity entity = new Entity("Uber");
            ALARM.Entity = entity;
            
            EntityRepository entityRepository = new EntityRepository();
            entityRepository.Add(ALARM.Entity);
            entityRepository.Add(ENTITY);

            REPOSITORY.Add(ALARM);

            ALARM.Entity = ENTITY;

            REPOSITORY.Modify(ALARM.Id, ALARM);

            Assert.AreEqual(REPOSITORY.Get(ALARM.Id).Entity, ENTITY);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void ModifyNonExitentAlarm()
        {
            EntityRepository entityRepository = new EntityRepository();
            Entity entity = new Entity("Uber");
            entityRepository.Add(entity);

            ALARM.Entity = entity;

            REPOSITORY.Modify(ALARM.Id, ALARM);
        }

        [TestMethod]
        public void ModityAlarmSentimentType()
        {
            EntityRepository entityRepository = new EntityRepository();
            entityRepository.Add(ALARM.Entity);

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
            EntityRepository entityRepository = new EntityRepository();
            entityRepository.Add(ALARM.Entity);

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
            EntityRepository entityRepository = new EntityRepository();
            entityRepository.Add(ALARM.Entity);

            REPOSITORY.Add(ALARM);

            Assert.IsFalse(REPOSITORY.IsEmpty());
        }

        #endregion

        #region GetAll

        [TestMethod]
        public void GetAllAlarms()
        {
            List<EntityAlarm> alarms = new List<EntityAlarm>();

            EntityRepository entityRepository = new EntityRepository();
            entityRepository.Add(ALARM.Entity);

            EntityAlarm newAlarm = new EntityAlarm("3", "1", SentimentType.NEGATIVE, ENTITY);
            
            alarms.Add(ALARM);
            alarms.Add(newAlarm);

            REPOSITORY.Add(ALARM);
            REPOSITORY.Add(newAlarm);

            using(SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                var repositoryAlarms = Helper.Instance.GetEntityAlarms(context.EntityAlarms);

                Assert.IsTrue(repositoryAlarms.SequenceEqual(alarms));
            }
        }

        #endregion

        #region GetId

        [TestMethod]
        public void GetById()
        {
            EntityRepository entityRepository = new EntityRepository();
            entityRepository.Add(ALARM.Entity);

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
