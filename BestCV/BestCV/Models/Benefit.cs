﻿using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    /// <summary>
    /// Quyền lợi
    /// </summary>
    public class Benefit : EntityCommon<int>
    {
        public virtual ICollection<ServicePackageBenefit> ServicePackageBenefits { get; } = new List<ServicePackageBenefit>();
    }
}
