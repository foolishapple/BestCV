using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateJobSuggestionSetting
{
    public class UpdateCandidateJobSuggetionSettingDTO
    {
        public long Id { get; set; } = 0;
        public int Gender { get; set; }
        public int JobPositionId { get; set; }
        public int JobCategoryId { get; set; }
        public int JobSkillId { get; set; }
        public int JobId { get; set; }
        public int experienceRangeId { get; set; }
        public int salaryRangeId { get; set; }
        public int workPlaceId { get; set; }
        public bool jobStatusSearch { get; set; }
    }
}
