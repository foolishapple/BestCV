using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateWorkExperience
{
    public class CandidateWorkExperienceDTO
    {
        public long Id { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string TimePeriod { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; } = false;
        public bool IsAdded { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
    }
}
