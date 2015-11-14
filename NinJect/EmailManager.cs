using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinJect
{
    public class EmailManager : IEmailManager
    {
        public string to { get; set; }
        public string message { get; set; }
        public string sendEmail()
        {
            return string.Format("Email send successfully to: {0}, message: {1}", to, message);
        }
    }
}
