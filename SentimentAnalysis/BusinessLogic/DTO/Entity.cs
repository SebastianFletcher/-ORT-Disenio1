using BusinessLogic.Exceptions;
using System;

namespace BusinessLogic.DTO
{
    public class Entity
    {
        #region Private Attributes
        
        private string name;

        #endregion

        #region Public Attributes

        public string Name
        {
            get { return name; }
            set { SetName(value); }
        }

        public int Id { get; set; }

        #endregion

        #region Constructors

        public Entity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Entity(string name)
        {
            Name = name;
        }

        #endregion 

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EntityException("Entity name can't be empty.");

            this.name = name.Trim();
        }

        public override string ToString()
        {
            return this.name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            
            if (obj == DBNull.Value)
                return false;

            Entity entity = (Entity)obj;

            return this.name.ToUpper().Equals(entity.name.ToUpper());
        }
    }
}
