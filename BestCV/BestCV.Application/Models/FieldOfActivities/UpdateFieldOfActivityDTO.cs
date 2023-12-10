using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.FieldOfActivities
{
	public class UpdateFieldOfActivityDTO : InsertFieldOfActivityDTO
	{
		/// <summary>
		/// Mã
		/// </summary>
        public int Id { get; set; }
    }
}
