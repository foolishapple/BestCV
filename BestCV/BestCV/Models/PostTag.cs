using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public partial class PostTag: EntityBase<int>
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
        [JsonIgnore]
        public virtual Post Post { get; set; } = null!;
        [JsonIgnore]
        public virtual Tag Tag { get; set; } = null!;

    }
}
