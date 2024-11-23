using System.Net;
using System.Net.Mail;

namespace YemekGuru.webapp.EmailSErvice;

public class EmailService : IEmailService
{
    private string? _host;
    private int _port;
    private bool _enableSSL;
    private string? _emailAddress;
    private string? _password;
  
    public EmailService(string? host, int port, bool enableSSL, string? emailAddress, string? password)
    {
        _host = host;
        _port = port;
        _enableSSL = enableSSL;
        _emailAddress = emailAddress;
        _password = password;
    }

    public async Task SendEmail(string? userEmailAddress, string? subject, string? message)
    {
        SmtpClient client = new SmtpClient(){
            Host=_host,
            Port=_port,
            Credentials=new NetworkCredential(_emailAddress,_password),
            EnableSsl=_enableSSL
        };
        MailMessage mailMessage = new MailMessage(_emailAddress,userEmailAddress,subject,message){
            IsBodyHtml=true
        };
        client.SendMailAsync(mailMessage);
    }
}