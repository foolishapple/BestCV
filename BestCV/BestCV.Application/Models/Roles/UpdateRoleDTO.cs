﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Roles
{
    public class UpdateRoleDTO : InsertRoleDTO
    {
        /// <summary>
        /// Mã vài trò
        /// </summary>
        public int Id { get; set; }
    }
}
