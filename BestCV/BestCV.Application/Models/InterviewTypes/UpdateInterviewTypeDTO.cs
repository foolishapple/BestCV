using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.InterviewTypes
{
    public class UpdateInterviewTypeDTO : InsertInterviewTypeDTO
    {
        /// <summary>
        /// Mã loại phỏng vấn
        /// </summary>
        public int Id { get; set; }
    }
}
