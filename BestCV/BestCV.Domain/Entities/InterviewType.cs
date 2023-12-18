using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class InterviewType : EntityCommon<int>
    {
        public ICollection<InterviewSchedule> InterviewSchedules { get;} = new List<InterviewSchedule>();
    } 
}
