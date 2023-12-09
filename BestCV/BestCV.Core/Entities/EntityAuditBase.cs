using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobi.Core.Entities.Interfaces;

namespace BestCV.Core.Entities
{
    public class EntityAuditBase<T> : EntityBase<T>, IAuditable
    {
        public DateTime LastModifiedTime { get; set; }
    }
}
