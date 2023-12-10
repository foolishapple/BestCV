using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.WorkPlace
{
    public class UpdateWorkplaceDTO : InsertWorkplaceDTO
    {
        /// <summary>
        /// ID đơn vị hành chính
        /// </summary>
        public int Id { get; set; }
    }
}
