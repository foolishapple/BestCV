using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.JobReference
{
    public class UpdateJobReferenceDTO : InsertJobReferenceDTO
    {
        public long Id { get; set; }
    }
}
