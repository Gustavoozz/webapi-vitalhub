namespace WebAPI.Utils.Mail
{
    public interface IEmailService
    {
        // Método assíncrono para o envio de email:
        // Recebe o objeto ( mailRequest ) da classe MailRequest:
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
