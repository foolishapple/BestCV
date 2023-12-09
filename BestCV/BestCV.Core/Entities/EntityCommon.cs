using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities
{
    public abstract class EntityCommon<TKey> : EntityBase<TKey>, IEntityCommon
    {
        /// <summary>
        /// Tên 
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
    }
}
