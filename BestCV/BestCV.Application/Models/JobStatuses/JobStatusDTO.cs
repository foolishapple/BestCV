using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.JobStatuses
{
    public class JobStatusDTO : EntityCommon<int>
    {
        public string color { get; set; }
    }
}
