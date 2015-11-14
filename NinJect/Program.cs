using System;
using Newtonsoft.Json;
using Ninject;
using Moq;
using Ninject.MockingKernel.Moq;

namespace NinJect
{
    class Program
    {
        private static bool DEBUGING = true;

        static void Main(string[] args)
        {
            {
                // Without ninJect
                Address add = new Address() { Postcode = "PR2 2UT" };
                Applicant applicant = new Applicant(add);
                applicant.EmailAddress = "long@way.com";
                Application app = new Application(applicant);
                app.emailManager = new EmailManager();
                app.emailManager.message = "Setup using basic DI pattern";

                Console.WriteLine(app.sendEmail());
                Console.WriteLine(JsonConvert.SerializeObject(app));

                Console.ReadKey();
            }

            using (var kernel = new MoqMockingKernel())
            {
                SetupNinJect(kernel);

                // With NinJect (includes Mocking as well!
                var app = kernel.Get<IApplication>();

                Console.WriteLine(app.sendEmail());
                Console.WriteLine(JsonConvert.SerializeObject(app.App1));
                Console.ReadKey();
            }

        }

        private static void SetupNinJect(MoqMockingKernel kernel)
        {
            if (DEBUGING)
            {
                var MockEmailManager = kernel.GetMock<IEmailManager>();
                MockEmailManager.Setup(x => x.sendEmail()).Returns("Return from unit test mock");
            } else
            {
                kernel.Bind<IEmailManager>()
                    .To<EmailManager>()
                    .WithPropertyValue("message", "setup from NinJect");
            }

            kernel.Bind<IAddress>()
                .To<Address>()
                .WithPropertyValue("Postcode", "PR1 1UT");

            kernel.Bind<IApplicant>()
                .To<Applicant>()
                .WithPropertyValue("EmailAddress", "ioc@ninject.net")
                .WithConstructorArgument("address", kernel.Get<IAddress>());

            kernel.Bind<IApplication>()
                .To<Application>()
                .WithPropertyValue("emailManager", kernel.Get<IEmailManager>())
                .WithConstructorArgument("applicant", kernel.Get<IApplicant>());
        }
    }
}

