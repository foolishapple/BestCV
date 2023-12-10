using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Tag
{
    public class TagDTO : EntityBase<int>
    {
        public int TagTypeId { get; set; }
        public string TagTypeName { get; set; }
        public string Name { get; set; }
    }
}
