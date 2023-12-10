using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.InterviewStatsus
{
    public class UpdateInterviewStatusDTO : InsertInterviewStatusDTO
    {
        public int Id { get; set; }
    }
}
