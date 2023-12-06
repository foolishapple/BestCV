using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CompanyImage : EntityCommon<long>
    {
        public int CompanyId { get; set; }
        public string Path { get; set; }
        public int OrderSort { get; set; }
        public virtual Company Company { get; }

    }
}
