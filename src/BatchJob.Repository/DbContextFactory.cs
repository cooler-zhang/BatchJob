using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Repository
{
    public class DbContextFactory
    {
        public static BatchJobDbContext Create()
        {
            return new BatchJobDbContext();
        }
    }
}
