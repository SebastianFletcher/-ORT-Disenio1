using BusinessLogic.DTO;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace Persistence.Test.Tests
{
    [TestClass]
    public class EntityRepositoryTest
    {

        #region Attributes

        private Entity ENTITY;
        private Author AUTHOR;
        private EntityRepository REPOSITORY;

        #endregion

        #region Set Up

        [TestInitialize]
        public void SetUp()
        {
            ENTITY = new Entity("Google");
            REPOSITORY = new EntityRepository();
            AUTHOR = new Author("sada", "sadasd", "sdaasd", new DateTime(1995, 3, 5));

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
        public void AddNewEntity()
        {
            PhraseRepository phraseRepository = new PhraseRepository();
            AuthorRepository authorRepository = new AuthorRepository();

            authorRepository.Add(AUTHOR);

            Phrase phrase = new Phrase("I like Google", DateTime.Now, AUTHOR);
            try
            {
                phraseRepository.Add(phrase);
            }
            catch { }

            REPOSITORY.Add(ENTITY);

            Assert.AreEqual(ENTITY.Id, phraseRepository.Get(phrase.Id).Entity.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityException))]
        public void AddDuplicatedEntity()
        {
            REPOSITORY.Add(ENTITY);

            REPOSITORY.Add(ENTITY);
        }


        #endregion

        #region Modify

        [TestMethod]
        public void ModifyEntityName()
        {
            string newName = "ORT";

            REPOSITORY.Add(ENTITY);
            
            ENTITY.Name = newName;
            REPOSITORY.Modify(ENTITY.Id, ENTITY);

            Assert.AreEqual(REPOSITORY.Get(ENTITY.Id).Name, newName);
        }

        [TestMethod]
        public void ModifyUpdatePhrasesWithEntityNull()
        {
            Entity entity = new Entity("Microsoft");

            REPOSITORY.Add(entity);

            SentimentRepository sentimentRepository = new SentimentRepository();
            PhraseRepository phraseRepository = new PhraseRepository();
            AuthorRepository authorRepository = new AuthorRepository();

            authorRepository.Add(AUTHOR);

            try
            {
                sentimentRepository.Add(new Sentiment(SentimentType.POSITIVE, "i like"));
            }
            catch (AnalysisException) { }

            Phrase phrase = new Phrase("I like Uber", DateTime.Now, AUTHOR);

            try
            {
                phraseRepository.Add(phrase);
            }
            catch (AnalysisException) { }

            entity.Name = "Uber";

            REPOSITORY.Modify(entity.Id, entity);

            Assert.AreEqual(phraseRepository.Get(phrase.Id).Entity, entity);
        }

        [TestMethod]
        public void ModifyUpdatePhrases()
        {
            Entity entity = new Entity("Microsoft");

            List<Sentiment> sentiments = new List<Sentiment>();
            List<Entity> entities = new List<Entity>();
            List<Phrase> phrases = new List<Phrase>();
            List<EntityAlarm> alarms = new List<EntityAlarm>();

            sentiments.Add(new Sentiment(SentimentType.POSITIVE, "i like"));
            entities.Add(entity);

            Phrase phrase = new Phrase("I like Microsoft", DateTime.Now, AUTHOR);
            phrases.Add(phrase);

            REPOSITORY.Add(entity);

            entity.Name = "Uber";

            REPOSITORY.Modify(entity.Id, entity);

            Assert.IsNull(phrase.Entity);
        }

        [TestMethod]
        public void ModifyUpdateAlarm()
        {
            Entity entity = new Entity("Microsoft");

            List<Sentiment> sentiments = new List<Sentiment>();
            List<Entity> entities = new List<Entity>();
            List<Phrase> phrases = new List<Phrase>();
            List<EntityAlarm> alarms = new List<EntityAlarm>();

            sentiments.Add(new Sentiment(SentimentType.POSITIVE, "i like"));
            entities.Add(entity);

            Phrase phrase = new Phrase("I like Uber", DateTime.Now, AUTHOR);
            phrases.Add(phrase);

            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, entity);
            alarms.Add(alarm);

            REPOSITORY.Add(entity);

            entity.Name = "Uber";

            REPOSITORY.Modify(entity.Id, entity);

            Assert.AreEqual(alarm.Entity, entity);
        }

        #endregion

        #region Delete 

        [TestMethod]
        [ExpectedException(typeof(EntityException))]
        public void DeleteNull()
        {
            REPOSITORY.Delete(null);
        }

        [TestMethod]
        public void DeleteEntity()
        {
            REPOSITORY.Add(ENTITY);

            REPOSITORY.Delete(ENTITY.Id);

            Assert.IsTrue(REPOSITORY.IsEmpty());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityException))]
        public void DeleteNonExistenceEntity()
        {
            REPOSITORY.Delete(ENTITY.Id);
        }

        [TestMethod]
        public void DeleteUpdatesEntity()
        {
            Entity entity = new Entity("Microsoft");

            List<Sentiment> sentiments = new List<Sentiment>();
            List<Entity> entities = new List<Entity>();
            List<Phrase> phrases = new List<Phrase>();
            List<EntityAlarm> alarms = new List<EntityAlarm>();

            sentiments.Add(new Sentiment(SentimentType.POSITIVE, "i like"));
            entities.Add(entity);

            Phrase phrase = new Phrase("I like Microsoft", DateTime.Now, AUTHOR);
            phrases.Add(phrase);

            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, entity);
            alarms.Add(alarm);

            REPOSITORY.Add(entity);

            REPOSITORY.Delete(entity.Id);

            Assert.IsNull(phrase.Entity);
        }

        [TestMethod]
        [ExpectedException(typeof(AlarmException))]
        public void DeleteEntityDeletesAssosiatedAlarm()
        {
            EntityAlarmRepository alarmRepository = new EntityAlarmRepository();

            Entity entity = new Entity("Uber");
            REPOSITORY.Add(entity);

            EntityAlarm alarm = new EntityAlarm("1", "1", SentimentType.POSITIVE, entity);
            alarmRepository.Add(alarm);

            REPOSITORY.Delete(entity.Id);

            alarmRepository.Get(alarm.Id); 
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
            REPOSITORY.Add(ENTITY);

            Assert.IsFalse(REPOSITORY.IsEmpty());
        }

        #endregion

        #region GetAll

        [TestMethod]
        public void GetAllEntities()
        {
            IList<Entity> entities = new List<Entity>();

            Entity firstEntity = new Entity("Google");
            Entity secondEntity = new Entity("Starbucks");

            entities.Add(firstEntity);
            entities.Add(secondEntity);

            REPOSITORY.Add(firstEntity);
            REPOSITORY.Add(secondEntity);

            Assert.IsTrue(REPOSITORY.GetAll().SequenceEqual(entities));
        }

        #endregion

        #region GetId

        [TestMethod]
        public void GetById()
        {
            REPOSITORY.Add(ENTITY);

            Assert.IsNotNull(REPOSITORY.Get(ENTITY.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(EntityException))]
        public void GetNonExitentId()
        {
            REPOSITORY.Get(ENTITY.Id + 1);
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
