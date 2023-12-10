using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateActivites
{
    public class CandidateActivitesDTO : EntityCommon<long>
    {
        public string TimePeriod { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsAdded { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
    }
}
