using BestCV.Application.Models.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Post
{
    public class InsertPostDTO
    {
        public int PostTypeId { get; set; }
        public int PostLayoutId { get; set; }
        public int PostStatusId { get; set; }
        public int PostCategoryId { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public long AuthorId { get; set; }
        public bool IsPublish { get; set; }
        public bool IsDraft { get; set; }
        public bool IsApproved { get; set; }
        public string TagIds { get; set; }
        public DateTime PublishedTime { get; set; }
    }
}
