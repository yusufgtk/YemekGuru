namespace YemekGuru.entity;

public class Complaint
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public string? Subject  { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public bool Status { get; set; } //false: inceleniyor true:Çözüldü!
}