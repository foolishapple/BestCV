using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.JobSuitable
{
    public class UpdateJobSuitableDTO : InsertJobSuitableDTO
    {
        public long Id { get; set; }
    }
}
