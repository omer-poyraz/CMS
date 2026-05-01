namespace Services.Contracts
{
    public interface IMailTemplateService
    {
        string GetPasswordResetTemplate(string userName, string resetLink);
    }
}
