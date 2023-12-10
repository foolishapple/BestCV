using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.UploadFiles
{
	public class PagingUploadFileParameter : PagingBaseParameters
	{
        public int FodlderUploadId { get; set; }
        public string ContentType { get; set; } = null!;
    }
}
