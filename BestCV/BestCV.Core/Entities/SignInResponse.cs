using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities
{
    public class SignInResponse
    {
        /// <summary>
        /// Authorization token
        /// </summary>
        public string AccessToken { get; set; } = null!;
    }
}
