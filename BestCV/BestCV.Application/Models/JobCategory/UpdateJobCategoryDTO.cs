using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.JobCategory
{
    public class UpdateJobCategoryDTO : InsertJobCategoryDTO
    {
        public int Id { get; set; }
    }
}