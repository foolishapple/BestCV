﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities.Interfaces
{
    public interface IDateTracking
    {
        DateTime LastModifiedTime { get; set; }
    }
}
