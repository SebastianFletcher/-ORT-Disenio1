using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class ContextConfiguration : DbConfiguration
    {
        public ContextConfiguration()
        {
            this.SetDatabaseInitializer(new CreateDatabaseIfNotExists<SentimentAnalysisContext>());
            this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }
}