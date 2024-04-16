
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace WebAPI.Utils.Mail
{
    public class EmailService : IEmailService
    {
        // Variavel que armazena as configs de EmailSettings:
        private readonly EmailSettings emailSettings;

        // IOptions = Todos os dados inseridos dentro da classe EmailSettings são "injetados" no construtor:
        // Construtor que recebe a dependence injection de EmailSettings:
        public EmailService(IOptions<EmailSettings> options)
        {
            emailSettings = options.Value;
        }


        // Método para envio de Email:
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                // Objeto que representa o Email:
                var email = new MimeMessage();



                // Define o remetente do Email:
                email.Sender = MailboxAddress.Parse(emailSettings.Email);


                // Define o destinatário do Email:
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));       

                
                // Define o assunto do Email:
                email.Subject = mailRequest.Subject;


                // Cria o corpo do Email:
                var builder = new BodyBuilder();


                // Define o corpo do Email como HTML:
                builder.HtmlBody = mailRequest.Body;


                // Define o corpo do Email no objeto MimeMessage:
                email.Body = builder.ToMessageBody();

                // Cria um client SMTP para envio de email:
                using (var smtp = new SmtpClient())
                {
                    // Conecta-se ao servidor SMTP usandos os dados do emailSettings:
                    smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);

                    // Autentica-se no servidor SMTP:
                    smtp.Authenticate(emailSettings.Email, emailSettings.Password);

                    // Envia o Email:
                    await smtp.SendAsync(email);
                }

               
            }
            catch (Exception) {

                throw;
            }
        }
    }
}
