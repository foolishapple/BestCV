using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.PostCategory
{
    public class PostCategoryDTO
    {
        public string Color { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
