using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.PaymentMethod
{
    public class UpdatePaymentMethodDTO : InsertPaymentMethodDTO
    {
        public int Id { get; set; }
    }
}
