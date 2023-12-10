using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.JobStatuses
{
    /// <summary>
    /// Author: HuyDQ
    /// Created: 27/07/2023
    /// </summary>
    public class UpdateJobStatusDTO : InsertJobStatusDTO
    {
        /// <summary>
        /// Mã trạng thái
        /// </summary>
        public int Id { get; set; }
    }
}
