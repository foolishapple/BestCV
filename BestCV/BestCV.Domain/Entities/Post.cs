using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public partial class Post : EntityFullTextSearch<int>
    {
        public int PostTypeId { get; set; }
        public int PostLayoutId { get; set; }
        public int PostStatusId { get; set; }
        public int PostCategoryId { get; set; }
        public string Overview { get; set; }
        public string Photo { get; set; }
        public long AuthorId { get; set; }
        public bool IsPublish { get; set; }
        public bool IsDraft { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime PublishedTime { get; set; }
        [JsonIgnore]
        public virtual PostCategory PostCategory { get; set; } = null!;
        [JsonIgnore]
        public virtual PostLayout PostLayout { get; set; } = null!;
        [JsonIgnore]
        public virtual PostStatus PostStatus { get; set; } = null!;
        [JsonIgnore]
        public virtual PostType PostType { get; set; } = null!;
        [JsonIgnore]
        public virtual AdminAccount AdminAccount { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<PostTag> PostTags { get; } = new List<PostTag>();


    }
}
