using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CVTemplateStatus : EntityCommon<long>
    {
        public virtual ICollection<CVTemplate> CVTemplates { get; } = new List<CVTemplate>();
    }
}
