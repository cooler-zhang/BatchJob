using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Domain
{
    public class DomainContext
    {
        public static string AXGIO_DB_CONTEXT = "BatchJob_DB_CONTEXT";

        public static DbContext Current
        {
            get
            {
                object context = CallContext.GetData(AXGIO_DB_CONTEXT);
                if (context != null)
                {
                    return (DbContext)context;
                }
                else
                {
                    throw new Exception("DomainContext is null");
                }
            }
        }
    }
}
