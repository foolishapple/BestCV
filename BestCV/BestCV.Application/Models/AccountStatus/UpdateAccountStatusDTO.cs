using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.AccountStatus
{
    public class UpdateAccountStatusDTO : InsertAccountStatusDTO
    {
        public int Id { get; set; }
    }
}