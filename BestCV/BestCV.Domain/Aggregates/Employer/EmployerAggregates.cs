﻿using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Employer
{
    public class EmployerAggregates
    {
        public long Id { get; set; }
        public bool IsActivated { get; set; }
        public int EmployerStatusId { get; set; }
        public string EmployerStatusName { get; set; }
        public string EmployerStatusColor { get; set; }
        public int EmployerServicePackageId { get; set; }
        public string EmployerServicePackageName { get; set; }
        public DateTime? EmployerServicePackageEfficiencyExpiry { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? Photo { get; set; }
        public int Gender { get; set; }
        public string? Phone { get; set; }
        public string? SkypeAccount { get; set; }
        public string Search { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
