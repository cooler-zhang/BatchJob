using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Dto
{
    public class ServiceDto : BaseDto
    {
        public string ServiceAddress { get; set; }

        public string MethodName { get; set; }

        public string OperationContractName { get; set; }

        public int JobId { get; set; }

        public string JobName { get; set; }

        public IList<ServiceParameterDto> Parameters { get; set; }

        public class ServiceParameterDto : BaseDto
        {
            public string TypeName { get; set; }

            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}
