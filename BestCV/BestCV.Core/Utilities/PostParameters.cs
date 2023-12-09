using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Utilities
{
    public class PostParameters
    {
        public string Keywords { get; set; } = "";
        public string OrderCriteria { get; set; } = "Id";
        public bool OrderAscendingDirection { get; set; }
        public string Search { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public int PostTypeId { get; set; }
        public int AuthorId { get; set; }
        public string PostType { get; set; } = "";
        public string ListTag { get; set; } = "";
        public int TagId { get; set; }  
        public string PostCategory { get; set; } = "";
        public int PostCategoryId { get; set; }
    }
}
