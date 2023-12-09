using BestCV.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities
{
    public class SearchQuery
    {
        [Range(1, Int32.MaxValue)]
        public int PageIndex { get; set; }
        [Range(1, 200)]
        public int PageSize { get; set; }
        [MaxLength(500)]
        public string Keyword { get; set; }
        [MaxLength(4), SortTypeValidate]
        public string SortType { get; set; }
        public string OrderBy { get; set; }
    }
}
