using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities.Interfaces
{
	public class PagingBaseParameters
	{
        public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public string Keyword { get; set; } = null!;
        public int PageStart 
		{ 
			get 
			{
				return PageIndex * PageSize;
			}  
		}
    }
}
