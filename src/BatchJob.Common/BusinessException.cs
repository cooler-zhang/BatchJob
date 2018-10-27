using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Common
{
    public class BusinessException : Exception
    {
        public BusinessException(string errorMessage) :
            base(errorMessage)
        {
        }
    }
}
