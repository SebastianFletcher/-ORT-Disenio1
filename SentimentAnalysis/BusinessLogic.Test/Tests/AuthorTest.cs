using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.Test.Tests
{
    [TestClass]
    public class AuthorTest
    {
        #region Attributes 

        private static string USER;
        private static string NAME;
        private static string LASTNAME;
        private static DateTime? BIRTHDATE;

        #endregion

        #region SetUp 

        [TestInitialize]
        public void SetUp()
        {
            USER = "Fletcher";
            NAME = "Sebastian";
            LASTNAME = "Gonzalez";
            BIRTHDATE = new DateTime(1995, 6, 3);
        }

        [TestCleanup]
        public void Cleanup()
        {
            USER = string.Empty;
            NAME = string.Empty;
            LASTNAME = string.Empty;
            BIRTHDATE = null;
        }

        #endregion

        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void ConstructorNullUser()
        {
            Author author = new Author(null, NAME, LASTNAME, BIRTHDATE);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void ConstructorNullName()
        {
            Author author = new Author(USER, null, LASTNAME, BIRTHDATE);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void ConstructorNullLastName()
        {
            Author author = new Author(USER, NAME, null, BIRTHDATE);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void ConstructorNullBirthdate()
        {
            Author author = new Author(USER, NAME, LASTNAME, null);
        }

        [TestMethod]
        public void ConstructorWithId()
        {
            Author author = new Author(1, USER, NAME, LASTNAME, new DateTime(1995, 2, 3));

            Assert.AreEqual(author.Id, 1);
        }

        #endregion

        #region Id

        [TestMethod]
        public void SetId()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            Assert.IsNotNull(author.Id);
        }

        #endregion

        #region Username

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void UpdateNullUsername()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            author.Username = null;
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void UpdateInvalidUsername()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            string newUsername = "SebastianFletcher";

            author.Username = newUsername;
        }

        [TestMethod]
        public void UpdateValidUsername()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            string newUsername = "SFletcher";

            author.Username = newUsername;

            Assert.AreEqual(author.Username, newUsername);
        }

        [TestMethod]
        public void GetUsername()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            Assert.AreEqual(author.Username, USER);
        }

        #endregion

        #region Name

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void UpdateNullName()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            author.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void UpdateInvalidName()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            string newName = "SebastianFletcherGonzalezCaballero";

            author.Name = newName;
        }

        [TestMethod]
        public void UpdateValidName()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            string newName = "Seba";

            author.Name = newName;

            Assert.AreEqual(author.Name, newName);
        }

        [TestMethod]
        public void GetName()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            Assert.AreEqual(author.Name, NAME);
        }

        #endregion

        #region LastName

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void UpdateNullLastName()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            author.LastName = null;
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void UpdateInvalidLastName()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            string newLastName = "FletcherGonzalezCaballero";

            author.LastName = newLastName;
        }

        [TestMethod]
        public void UpdateValidLastName()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            string newLastName = "Fletcher";

            author.LastName = newLastName;

            Assert.AreEqual(author.LastName, newLastName);
        }

        [TestMethod]
        public void GetLastName()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            Assert.AreEqual(author.LastName, LASTNAME);
        }

        #endregion

        #region Birthdate

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void SetMajorDate()
        {
            Author author = new Author(USER, NAME, LASTNAME, new DateTime(1780, 1, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorException))]
        public void SetMinorDate()
        {
            Author author = new Author(USER, NAME, LASTNAME, new DateTime(2020, 1, 1));
        }

        [TestMethod]
        public void UpdateValidBirthdate()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            DateTime newBirthdate = new DateTime(1997, 2, 3);

            author.Birthdate = newBirthdate;

            Assert.AreEqual(author.Birthdate, newBirthdate);
        }

        [TestMethod]
        public void GetBirthdate()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            Assert.AreEqual(author.Birthdate, BIRTHDATE);
        }

        #endregion

        #region Methods

        [TestMethod]
        public void toString()
        {
            Author author = new Author(USER, NAME, LASTNAME, BIRTHDATE);

            string toStringMethod = "Sebastian (Fletcher) Gonzalez [Age: 25]";

            Assert.AreEqual(author.ToString(), toStringMethod);
        }

        #endregion
    }
}
