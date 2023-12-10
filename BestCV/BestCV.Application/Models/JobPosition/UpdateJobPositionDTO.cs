using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.JobPosition
{
	public class UpdateJobPositionDTO : InsertJobPositionDTO
	{
		public int Id { get; set; }
	}
}
