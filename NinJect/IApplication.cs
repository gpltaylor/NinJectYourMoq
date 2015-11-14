namespace NinJect
{
    public interface IApplication
    {
        Applicant App1 { get; set; }

        IEmailManager emailManager { get; set; }

        string sendEmail();
    }
}