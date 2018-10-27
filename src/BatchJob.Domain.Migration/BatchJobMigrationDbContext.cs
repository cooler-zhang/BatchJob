using BatchJob.Domain;
using BatchJob.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Migration
{
    public class BatchJobMigrationDbContext : BatchJobDbContext
    {
    }
}
