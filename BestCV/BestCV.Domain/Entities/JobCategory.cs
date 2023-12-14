using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public partial class JobCategory : EntityCommon<int>
    {
        /// <summary>
        /// Icon của job category
        /// </summary>
        public string Icon { get; set; } = null!;
        public string? ReferenceCategory { get; set; }
        public ICollection<CandidateSuggestionJobCategory> CandidateSuggestionJobCategories { get; } = new List<CandidateSuggestionJobCategory>();
        public virtual ICollection<Job> Jobs { get; } = new List<Job>();
        public virtual ICollection<JobSecondaryJobCategory> JobSecondaryJobCategories { get; } = new List<JobSecondaryJobCategory>();
    }
}
