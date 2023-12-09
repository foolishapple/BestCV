using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities
{
    public class EmailMessage<T>
    {
        public List<string> ToEmails { get; set; }
        public List<string> CcEmails { get; set; }
        public List<string> BccEmails { get; set; }
        public string Subject { get; set; }
        public string TemplatePath { get; set; }
        public T Model { get; set; }
    }
}
