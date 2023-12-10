﻿using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateSuggestionJobCategory
{
    public class CandidateSuggestionJobCategoryDTO : EntityBase<long>
    {
        public long CandidateId { get; set; }
        public int JobCategoryId { get; set; }
    }
}
