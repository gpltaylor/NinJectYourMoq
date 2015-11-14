using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinJect
{
    public class Application : IApplication
    {
        public IEmailManager emailManager { get; set; }

        public Applicant App1 { get; set; }


        public Application(Applicant applicant)
        {
            this.App1 = applicant;
        }

        public string sendEmail()
        {
            if (emailManager == null)
                return "Email server not configured";

            emailManager.to = App1.EmailAddress;
            return emailManager.sendEmail();
        }

    }

    public class Applicant : IApplicant
    {
        public Address Address { get; set; }
        public string EmailAddress { get; set; }

        public Applicant(Address address)
        {
            this.Address = address;
        }
    }

    public class Address : IAddress
    {
        public String Address1 { get; set; }
        public String Postcode { get; set; }
    }
}
