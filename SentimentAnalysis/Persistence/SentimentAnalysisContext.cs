using Persistence.Entities;
using System.Data.Entity;

namespace Persistence
{

    [DbConfigurationType(typeof(ContextConfiguration))]
    public class SentimentAnalysisContext : DbContext
    {
        public SentimentAnalysisContext() : base("name=SAConn")
        {
        }

        //Entities 
        public DbSet<Entity> Entities { get; set; }
        
        public DbSet<Sentiment> Sentiments { get; set; }
        
        public DbSet<Phrase> Phrases { get; set; }

        public DbSet<Alarm> Alarms { get; set; }

        public DbSet<EntityAlarm> EntityAlarms { get; set; }
        
        public DbSet<AuthorAlarm> AuthorAlarms { get; set; }

        public DbSet<Author> Authors { get; set; }

    }
}
