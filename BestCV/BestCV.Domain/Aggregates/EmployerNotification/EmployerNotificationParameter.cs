using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.EmployerNotification
{
    public  class EmployerNotificationParameter : DTParameters
    {
    public long? EmployerId { get; set; }
    }
}
