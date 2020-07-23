using BusinessLogic.Exceptions;
using System;

namespace BusinessLogic.DTO
{
    public class Author
    {
        #region Private Attributes

        private static int USER_MAX_LENGTH = 10;
        private static int NAME_MAX_LENGTH = 15;
        private static int LASTNAME_MAX_LENGTH = 15;
        private static int MIN_AGE = 13;
        private static int MAX_AGE = 100;

        private string username;
        private string name;
        private string lastName;
        private DateTime birthdate; 

        #endregion

        #region Public Attributes

        public int Id { get; set; }

        public string Username
        {
            get { return username; }
            set { SetUsername(value); }
        }

        public string Name
        {
            get { return name; }
            set { SetName(value); }
        }

        public string LastName
        {
            get { return lastName; }
            set { SetLastName(value); }
        }

        public DateTime? Birthdate
        {
            get { return this.birthdate; }
            set { SetBirthdate(value); }
        }

        #endregion

        #region Contructors

        public Author(int id, string user, string name, string lastName, DateTime? date)
        {
            Id = id;
            Username = user;
            Name = name;
            LastName = lastName;
            Birthdate = date;
        }

        public Author(string user, string name, string lastName, DateTime? date)
        {
            Username = user;
            Name = name;
            LastName = lastName;
            Birthdate = date;
        }

        #endregion

        private void SetUsername(string user)
        {
            if (string.IsNullOrEmpty(user))
                throw new AuthorException("Username can't be empty.");

            if (user.Trim().Length > USER_MAX_LENGTH)
                throw new AuthorException($"Username length can't be larger than {USER_MAX_LENGTH}.");

            this.username = user.Trim();
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new AuthorException("Name can't be empty.");

            if (name.Trim().Length > NAME_MAX_LENGTH)
                throw new AuthorException($"Name length can't be larger than {NAME_MAX_LENGTH}.");

            this.name = name.Trim();
        }

        private void SetLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new AuthorException("Last name can't be empty.");

            if (lastName.Trim().Length > LASTNAME_MAX_LENGTH)
                throw new AuthorException($"Last name length can't be larger than {LASTNAME_MAX_LENGTH}.");

            this.lastName = lastName.Trim();
        }

        private void SetBirthdate(DateTime? date)
        {
            if(date == null)
                throw new AuthorException("Birthdate must be entered.");

            var age = DateTime.Now.Year - date.Value.Year;

            if (age < MIN_AGE)
                throw new AuthorException($"The Author age can't be less than {MIN_AGE}.");

            if (age > MAX_AGE)
                throw new AuthorException($"The Author age can't be greater than {MAX_AGE}.");

            this.birthdate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day);
        }

        public override string ToString()
        {
            return $"{Name} ({Username}) {LastName} [Age: {DateTime.Now.Year - Birthdate.Value.Year}]";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj == DBNull.Value)
                return false;

            Author author = (Author)obj;

            DateTime thisBirthdate = new DateTime(this.birthdate.Year, this.birthdate.Month, this.birthdate.Day);
            DateTime authorBirtdate = new DateTime(author.birthdate.Year, author.birthdate.Month, author.birthdate.Day);

            return this.username.ToUpper().Equals(author.username.ToUpper()) &&
                this.name.ToUpper().Equals(author.name.ToUpper()) &&
                this.lastName.ToUpper().Equals(author.lastName.ToUpper()) &&
                thisBirthdate == authorBirtdate;
        }
    }
}
