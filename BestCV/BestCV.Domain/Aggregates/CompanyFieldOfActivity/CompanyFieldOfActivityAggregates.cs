using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.CompanyFieldOfActivity
{
    public class CompanyFieldOfActivityAggregates : EntityBase<long>
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public int FieldOfActivityId { get; set; }

        public string FieldOfActivityName { get; set; }

    }
}
