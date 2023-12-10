using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Post
{
    public class UpdatePostDTO : InsertPostDTO
    {
        public int Id { get; set; }
    }
}
