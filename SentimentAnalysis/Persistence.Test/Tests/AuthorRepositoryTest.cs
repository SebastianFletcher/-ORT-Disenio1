using System;
using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.Repositories;

namespace Persistence.Test.Tests
{
    [TestClass]
    public class AuthorRepositoryTest
    {
       
        #region Attributes
        
        private static AuthorRepository REPOSITORY;
        private static Author AUTHOR;
        private SentimentRepository repository;

        #endregion

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            REPOSITORY = new AuthorRepository();
            CleanRepositories();

            AUTHOR = new Author("sada", "sadasd", "sdaasd", new DateTime(1990, 2, 3));
        }

        [TestCleanup]
        public void Cleanup()
        {
            CleanRepositories();
        }

        #endregion

        #region Add

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void AddDuplicatedAuthor()
        {
            REPOSITORY.Add(AUTHOR);

            REPOSITORY.Add(AUTHOR);

        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void AddAuthorUserNameCorrectLength()
        {
            AuthorRepository repository = new AuthorRepository();
            Author author = new Author("sada", "a", "wwwwwwwwwwwwwwwww", new DateTime(1990, 2, 3));

            repository.Add(author);

            Assert.IsFalse(repository.IsEmpty());
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void AddAuthorUserNameMaxLength()
        {
            AuthorRepository repository = new AuthorRepository();

            AUTHOR.Username = "UserName1234567890";
            repository.Add(AUTHOR);

            Assert.IsTrue(REPOSITORY.IsEmpty());
        }

        [TestMethod]
        public void AddAuthorFirstNameCorrectLength()
        {
            AuthorRepository repository = new AuthorRepository();
            Author author = new Author("sada", "aiii", "w", new DateTime(1990, 2, 3));

            repository.Add(author);
            Assert.IsFalse(repository.IsEmpty());
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void AddAuthorFirstNameMaxLength()
        {
            AuthorRepository repository = new AuthorRepository();
            Author author = new Author("sada", "aeeeeeeeeeeeeeeeaa", "sdaasd", new DateTime(1990, 2, 3));
           
            repository.Add(author);

            Assert.IsTrue(repository.IsEmpty());
           
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void AddAuthorLastNameCorrectLength()
        {
            AuthorRepository repository = new AuthorRepository();
            Author author = new Author("sada", "a", "wwwwwwwwwwwwwwwww", new DateTime(1990, 2, 3));

            repository.Add(AUTHOR);

            Assert.IsFalse(repository.IsEmpty());
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void AddAuthorLastNameMaxLength()
        {
            AuthorRepository repository = new AuthorRepository();

            AUTHOR.LastName = "LastName12345678900";
            repository.Add(AUTHOR);

            Assert.IsTrue(REPOSITORY.IsEmpty());
        }

        [TestMethod]
        public void AddAuthorBirthDateCorrect()
        {
            Author author = new Author("sada", "test", "sdaasd", new DateTime(1990, 2, 3));           
            REPOSITORY.Add(author);

            var birthdate = REPOSITORY.Get(author.Id).Birthdate.Value;
            var age = DateTime.Today.Year - birthdate.Year;
            if (birthdate.Date > DateTime.Today.AddYears(-age)) age--;
            
            Assert.IsTrue(age >= 3 && age <= 100);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void AddAuthorBirthDateIncorrect()
        {

            AuthorRepository repository = new AuthorRepository();
            AUTHOR.Birthdate = new DateTime(1912, 12, 28);

            repository.Add(AUTHOR);

            Assert.IsTrue(REPOSITORY.IsEmpty());
         
        }

        #endregion
         
        #region Delete 

        [TestMethod]
        public void DeleteAuthor()
        {
            AuthorRepository repository = new AuthorRepository();

            repository.Add(AUTHOR);

            repository.Delete(AUTHOR.Id);

            Assert.IsTrue(repository.IsEmpty());

        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void DeleteNullAuthor()
        {
            AuthorRepository repository = new AuthorRepository();

            repository.Delete(null);
        }

        [TestMethod]
        public void DeleteNonExistenceAuthor()
        {
            AuthorRepository repository = new AuthorRepository();
            Author author = new Author("sada", "sadasd", "sdaasd", new DateTime(1990, 2, 3));
            repository.Add(author);
            repository.Delete(author.Id);
            Assert.IsTrue(repository.IsEmpty());
        }

        #endregion

        #region IsEmpty

        [TestMethod]
        public void IsEmpty()
        {
            AuthorRepository repository = new AuthorRepository();

            Assert.IsTrue(repository.IsEmpty());
        }

        [TestMethod]
        public void NotEmpty()
        {
            AuthorRepository repository = new AuthorRepository();
            repository.Add(AUTHOR);

            Assert.IsFalse(repository.IsEmpty());
        }

        #endregion

        #region GetById

        [TestMethod]
        public void GetById()
        {
            AuthorRepository repository = new AuthorRepository();
            repository.Add(AUTHOR);

            Assert.IsNotNull(repository.Get(AUTHOR.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void GetNonExitentId()
        {
            AuthorRepository repository = new AuthorRepository();

            repository.Get(AUTHOR.Id + 1);
        }

        #endregion

        #region Modify

        [TestMethod]
        public void ModifyAuthorUserName()
        {
            string newWord = "UserTest1";

            AuthorRepository repository = new AuthorRepository();
            repository.Add(AUTHOR);

            AUTHOR.Username = newWord;

            REPOSITORY.Modify(AUTHOR.Id, AUTHOR);

            Assert.AreEqual(repository.Get(AUTHOR.Id).Username, newWord);
        }
        
        [TestMethod]
        public void ModifyAuthorFirstName()
        {
            string newWord = "New FirstName";

            AuthorRepository repository = new AuthorRepository();
            repository.Add(AUTHOR);

            AUTHOR.Name = newWord;

            REPOSITORY.Modify(AUTHOR.Id, AUTHOR);

            Assert.AreEqual(repository.Get(AUTHOR.Id).Name, newWord);
        }

        [TestMethod]
        public void ModifyAuthorLastName()
        {
            string newWord = "New LasName";

            AuthorRepository repository = new AuthorRepository();
            repository.Add(AUTHOR);

            AUTHOR.LastName = newWord;

            REPOSITORY.Modify(AUTHOR.Id, AUTHOR);

            Assert.AreEqual(repository.Get(AUTHOR.Id).LastName, newWord);
        }
        
        [TestMethod]
        public void ModifyAuthorBirthDate()
        {
            DateTime newBd = new DateTime(1995, 12, 28);

            AuthorRepository repository = new AuthorRepository();
            repository.Add(AUTHOR);

            AUTHOR.Birthdate = newBd;

            REPOSITORY.Modify(AUTHOR.Id, AUTHOR);

            Assert.AreEqual(repository.Get(AUTHOR.Id).Birthdate, newBd);
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
