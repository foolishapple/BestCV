﻿using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public partial class PaymentMethod :  EntityCommon<int>
    {
        public ICollection<CandidateOrders> CandidateOrderses { get; } = new List<CandidateOrders>();
        public ICollection<EmployerOrder> EmployerOrders { get; } = new List<EmployerOrder>();
    }
}
