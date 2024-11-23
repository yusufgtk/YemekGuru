namespace YemekGuru.webapp.EmailSErvice;

public interface IEmailService
{
    public Task SendEmail(string? userEmailAddress, string? subject, string? message);
}