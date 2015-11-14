namespace NinJect
{
    public interface IEmailManager
    {
        string message { get; set; }
        string to { get; set; }

        string sendEmail();
    }
}