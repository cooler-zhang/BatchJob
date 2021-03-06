﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Dto
{
    public class BaseDto
    {
        public int? Id { get; set; }

        public string CreatedByName { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string UpdatedByName { get; set; }

        public DateTime? UpdatedDateTime { get; set; }
    }
}
