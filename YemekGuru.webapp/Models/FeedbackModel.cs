namespace YemekGuru.webapp.Models;

public class FeedbackModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Status { get; set; }

}