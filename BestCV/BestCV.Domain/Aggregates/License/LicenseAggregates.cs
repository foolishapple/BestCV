using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.License
{
    public class LicenseAggregates : EntityBase<long>
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int LicenseTypeId { get; set; }
        public string LicenseTypeName { get; set; }
        public string Path { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
