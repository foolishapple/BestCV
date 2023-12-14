using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public partial class AccountStatus : EntityCommon<int>
    {
        public string Color { get; set; } = null!;
        public virtual ICollection<Candidate> Candidates { get; } = new List<Candidate>();
        public virtual ICollection<Employer> Employers { get; } = new List<Employer>();
    }
}
