using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.JobReference
{
    public class InsertJobReferenceDTO
    {
        public long JobId { get; set; }
        public string JobName { get; set; }
        public string? Description { get; set; }
    }
}
