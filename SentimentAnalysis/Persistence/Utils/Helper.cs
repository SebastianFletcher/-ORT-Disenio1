using BusinessLogic.DTO;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace Persistence.Utils
{
    public sealed class Helper
    {
        private readonly static Helper _instance = new Helper();

        private Helper()
        {

        }

        public static Helper Instance
        {
            get { return _instance; }
        }

        #region Business Logic DTO -> Entity Framework DTO

        public Entities.Entity ToEntityEF(BusinessLogic.DTO.Entity entity)
        {
            if (entity == null)
                return null;

            return new Entities.Entity()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public Entities.EntityAlarm ToEntityAlarmEF(BusinessLogic.DTO.EntityAlarm alarm)
        {
            return new Entities.EntityAlarm()
            {
                Id = alarm.Id,
                Alarm = new Entities.Alarm()
                {
                    Id = alarm.Id,
                    CreationDate = alarm.CreationDate,
                    Time = Int16.Parse(alarm.Time),
                    PostCount = alarm.PostCount,
                    PostQuantity = Int16.Parse(alarm.PostQuantity),
                    Type = alarm.Type
                },
                Entity = ToEntityEF(alarm.Entity)
            };
        }

        public Entities.AuthorAlarm ToAuthorAlarmEF(BusinessLogic.DTO.AuthorAlarm alarm)
        {
            return new Entities.AuthorAlarm()
            {
                Alarm = new Entities.Alarm()
                {
                    Id = alarm.Id,
                    CreationDate = alarm.CreationDate,
                    Time = Int16.Parse(alarm.Time),
                    PostCount = alarm.PostCount,
                    PostQuantity = Int16.Parse(alarm.PostQuantity),
                    Type = alarm.Type,
                },
                TimeMeasure = alarm.TimeMeasure
            };
        }

        public Entities.Author ToAuthorEF(BusinessLogic.DTO.Author author)
        {
            return new Entities.Author()
            {
                Id = author.Id,
                Name = author.Name,
                LastName = author.LastName,
                Username = author.Username,
                Birthdate = author.Birthdate
            };
        }

        public Entities.Phrase ToPhraseEF(BusinessLogic.DTO.Phrase phrase)
        {
            return new Entities.Phrase()
            {
                Id = phrase.Id,
                Word = phrase.Word,
                Type = phrase.Type,
                Grade = phrase.Grade,
                PostedDate = phrase.PostedDate,
                Author = ToAuthorEF(phrase.Author),
                Entity = ToEntityEF(phrase.Entity)
            };
        }

        public Entities.Sentiment ToSentimentEF(BusinessLogic.DTO.Sentiment sentiment)
        {
            return new Entities.Sentiment()
            {
                Id = sentiment.Id,
                Word = sentiment.Word,
                Type = sentiment.Type
            };
        }

        #endregion

        #region Entity Framework DTO -> Business Logic DTO

        public BusinessLogic.DTO.Entity ToEntityBL(Entities.Entity entity)
        {
            if (entity == null)
                return null;

            return new BusinessLogic.DTO.Entity(entity.Id, entity.Name);
        }

        public BusinessLogic.DTO.EntityAlarm ToEntityAlarmBL(Entities.EntityAlarm alarm)
        {
            return new BusinessLogic.DTO.EntityAlarm(alarm.Id, alarm.Alarm.PostQuantity, alarm.Alarm.PostCount, alarm.Alarm.CreationDate, alarm.Alarm.Time, alarm.Alarm.Type, ToEntityBL(alarm.Entity));
        }

        public BusinessLogic.DTO.AuthorAlarm ToAuthorAlarmBL(Entities.AuthorAlarm alarm)
        {
            return new BusinessLogic.DTO.AuthorAlarm(alarm.Id, alarm.Alarm.PostQuantity, alarm.Alarm.PostCount, alarm.Alarm.CreationDate, alarm.Alarm.Time, alarm.Alarm.Type, alarm.TimeMeasure);
        }

        public BusinessLogic.DTO.Author ToAuthorBL(Entities.Author author)
        {
            if (author == null)
                return null;

            return new BusinessLogic.DTO.Author(author.Id, author.Username, author.Name, author.LastName, author.Birthdate);
        }

        public BusinessLogic.DTO.Phrase ToPhraseBL(Entities.Phrase phrase)
        {
            return new BusinessLogic.DTO.Phrase(phrase.Id, phrase.Word, phrase.PostedDate, phrase.Type, ToEntityBL(phrase.Entity), phrase.Grade, ToAuthorBL(phrase.Author));
        }

        public BusinessLogic.DTO.Sentiment ToSentimentBL(Entities.Sentiment sentiment)
        {
            return new BusinessLogic.DTO.Sentiment(sentiment.Id, sentiment.Type, sentiment.Word);
        }

        #endregion

        #region DbSet -> List Business Logic

        public IList<BusinessLogic.DTO.Entity> GetEntities(DbSet<Entities.Entity> dbset)
        {
            return dbset.AsNoTracking().ToList().Select(e => ToEntityBL(e)).ToList();
        }

        public IList<BusinessLogic.DTO.EntityAlarm> GetEntityAlarms(DbSet<Entities.EntityAlarm> dbset)
        {
            return dbset.AsNoTracking().ToList().Select(a => ToEntityAlarmBL(a)).ToList();
        }

        public IList<BusinessLogic.DTO.AuthorAlarm> GetAuthorAlarms(DbSet<Entities.AuthorAlarm> dbset)
        {
            return dbset.AsNoTracking().ToList().Select(a => ToAuthorAlarmBL(a)).ToList();
        }

        public IList<BusinessLogic.DTO.Author> GetAuthors(DbSet<Entities.Author> dbset)
        {
            return dbset.AsNoTracking().ToList().Select(a => ToAuthorBL(a)).ToList();
        }

        public IList<BusinessLogic.DTO.Phrase> GetPhrases(DbSet<Entities.Phrase> dbset)
        {
            return dbset.AsNoTracking().ToList().Select(p => ToPhraseBL(p)).ToList();
        }

        public IList<BusinessLogic.DTO.Sentiment> GetSentiments(DbSet<Entities.Sentiment> dbset)
        {
            return dbset.AsNoTracking().ToList().Select(s => ToSentimentBL(s)).ToList();
        }

        #endregion


    }
}
