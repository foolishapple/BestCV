using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Slides
{
    public class UpdateSlideDTO : InsertSlideDTO
    {
        /// <summary>
        /// Mã slide
        /// </summary>
        public int Id { get; set; }
    }
}
