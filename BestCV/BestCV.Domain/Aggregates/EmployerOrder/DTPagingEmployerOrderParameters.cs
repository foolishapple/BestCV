using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.EmployerOrder
{
    public class DTPagingEmployerOrderParameters : DTParameters
    {
        public long? EmployerId { get; set; }
    }
}
