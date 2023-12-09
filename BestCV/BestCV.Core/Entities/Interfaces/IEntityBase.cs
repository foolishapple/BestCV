using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities.Interfaces
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
        bool Active { get; set; }
        DateTime CreatedTime { get; set; }
    }
}
