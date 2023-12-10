using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.Tag;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Post
{
    public class PostAggregates : EntityFullTextSearch<int>
    {
        public int PostTypeId { get; set; }
        public string PostTypeName { get; set; }
        public int PostLayoutId { get; set; }
        public string PostLayoutName { get; set; }
        public int PostStatusId { get; set; }
        public string PostStatusName { get; set; }
        public string PostStatusColor { get; set; }
        public int PostCategoryId { get; set; }
        public string PostCategoryName { get; set; }
        public string Overview { get; set; }
        public string Photo { get; set; }
        public long AuthorId { get; set; }
		public string AuthorName { get; set; }
		public bool IsPublish { get; set; }
        public bool IsDraft { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime PublishedTime { get; set; }
        public ICollection<TagAggregates> ListTag { get; set; } = new List<TagAggregates>();

    }
}
