using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Persistence.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        public void Add(Author author)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                if (context.Authors.Any(a => a.Id == author.Id))
                    throw new AuthorException("An Author with the same ID already exists.");

                Exists(author);

                Entities.Author toAdd = Helper.Instance.ToAuthorEF(author);
                context.Authors.Add(toAdd);
                context.SaveChanges();
                author.Id = toAdd.Id;
            }
        }

        public void Modify(int? id, Author author)
        {
            if (!id.HasValue)
                throw new AuthorException("You must select an Author to modify.");

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                var authorFound = context.Authors.FirstOrDefault(a => a.Id == id);

                if (id != author.Id || !authorFound.Name.Equals(author.Name) || !authorFound.Username.Equals(author.Username) ||
                    !authorFound.LastName.Equals(author.LastName) || authorFound.Birthdate != author.Birthdate)
                    Exists(author);

                authorFound.Name = author.Name;
                authorFound.LastName = author.LastName;
                authorFound.Username = author.Username;
                authorFound.Birthdate = author.Birthdate;

                context.SaveChanges();
            }
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new AuthorException("You must select an Author to delete.");

            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.Author toRemove = context.Authors.FirstOrDefault(a => a.Id == id);

                if (toRemove == null)
                    throw new AuthorException("Author not found.");

                if(context.Phrases.Any(p => p.Author.Id == id))
                    throw new AuthorException("Cant remove an Author with Phrases.");

                context.Authors.Remove(toRemove);

                context.SaveChanges();
            }
        }

        public Author Get(int id)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                Entities.Author author = context.Authors.FirstOrDefault(a => a.Id == id);

                if (author == null)
                    throw new AuthorException("Author not found.");

                return Helper.Instance.ToAuthorBL(author);
            }
        }

        public IEnumerable<Author> GetAll()
        {
            try
            {
                using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                    return Helper.Instance.GetAuthors(context.Authors);
            }
            catch (SqlException)
            {
                throw new DatabaseException();
            }
            catch (DbException)
            {
                throw new DatabaseException();
            }
            catch (EntityException)
            {
                throw new DatabaseException();
            }
            catch (InvalidOperationException)
            {
                throw new DatabaseException();
            }
        }

        public bool IsEmpty()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                return !context.Authors.Any();
        }

        public void Clear()
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
            {
                context.Authors.RemoveRange(context.Authors);
                context.SaveChanges();
            }
        }

        public void Exists(Author toFind)
        {
            using (SentimentAnalysisContext context = new SentimentAnalysisContext())
                if (GetAll().Any(a => a.Equals(toFind)))
                    throw new AuthorException($"The Author {toFind} alrady exists.");
        }
    }
}
