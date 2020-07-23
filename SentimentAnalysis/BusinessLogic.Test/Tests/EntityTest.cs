using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.Test.Tests
{
    [TestClass]
    public class EntityTest
    {

        #region Attributes

        private static string NAME;

        #endregion

        #region SetUp

        [TestInitialize]
        public void SetUp()
        {
            NAME = "Google";
        }

        [TestCleanup]
        public void Cleanup()
        {
            NAME = string.Empty;
        }

        #endregion

        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(EntityException))]
        public void ConstructorNullName()
        {
            Entity entity = new Entity(null);
        }

        [TestMethod]
        public void ConstructorWithId()
        {
            Entity entity = new Entity(4, "Seba");

            Assert.AreEqual(entity.Id, 4);
        }

        #endregion

        #region Id

        [TestMethod]
        public void SetId()
        {
            Entity entity = new Entity(NAME);

            Assert.IsNotNull(entity.Id);
        }

        #endregion

        #region Name

        [TestMethod]
        [ExpectedException(typeof(EntityException))]
        public void ModifyNameIncorrectly()
        {
            Entity entity = new Entity(NAME);

            entity.Name = null;
        }

        [TestMethod]
        public void ModifyName()
        {
            string newName = "ORT";

            Entity entity = new Entity(NAME);

            entity.Name = newName;

            Assert.AreEqual(entity.Name, newName);
        }

        #endregion

        #region ToString

        [TestMethod]
        public void ToStringTest()
        {
            Entity entity = new Entity(NAME);

            Assert.AreEqual(entity.ToString(), NAME);
        }

        #endregion

        #region Equals

        [TestMethod]
        public void EqualsNull()
        {
            Entity entity = new Entity(NAME);

            Assert.IsFalse(entity.Equals(null));
        }

        [TestMethod]
        public void EqualsDbNull()
        {
            Entity entity = new Entity(NAME);

            Assert.IsFalse(entity.Equals(DBNull.Value));
        }

        [TestMethod]
        public void AreEquals()
        {
            Entity entity = new Entity(NAME);
            Entity sameEntity = new Entity(NAME);

            Assert.IsTrue(entity.Equals(sameEntity));
        }

        [TestMethod]
        public void NotEquals()
        {
            Entity entity = new Entity(NAME);
            Entity sameEntity = new Entity("Another name");

            Assert.IsFalse(entity.Equals(sameEntity));
        }


        #endregion

    }
}
