using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.JobType
{
    public class UpdateJobTypeDTO : InsertJobTypeDTO
    {
        public int Id { get; set; }
    }
}
