using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateSuggestionJobPosition
{
    public class UpdateCandidateSuggestionJobPositionDTO : InsertCandidateSuggestionJobPositionDTO
    {
        public long Id { get; set; }
    }
}
