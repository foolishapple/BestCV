using BestCV.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities
{
    public class AdvancedSearchQuery<T>:SearchQuery
    {      
        public T SearchOptions { get; set; }
    }
}
