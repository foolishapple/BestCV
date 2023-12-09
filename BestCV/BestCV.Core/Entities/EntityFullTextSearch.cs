using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities
{
    public class EntityFullTextSearch<T> : EntityCommon<T>, IFullTextSearch
    {
        public string Search { get; set; }
    }
}
