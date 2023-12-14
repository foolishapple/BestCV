using BestCV.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public partial class RolePermission : EntityBase<int>
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; } = null!;
        [JsonIgnore]
        public virtual Permission Permission { get; set; } = null!;
    }
}