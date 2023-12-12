using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class Tag : EntityBase<int>
    {
        /// <summary>
        /// Mã loại tag
        /// </summary>
        public int TagTypeId { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public virtual TagType TagType { get; } = null!;
        [JsonIgnore]
        public virtual ICollection<PostTag> PostTags { get; } = new List<PostTag>();

        [JsonIgnore]
        public virtual ICollection<JobTag> JobTags { get; } = new List<JobTag>();
    }
}
