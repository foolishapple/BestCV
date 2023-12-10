using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.PostCategory
{
    public class UpdatePostCategoryDTO : InsertPostCategoryDTO
    {
        public int Id { get; set; }
    }
}
