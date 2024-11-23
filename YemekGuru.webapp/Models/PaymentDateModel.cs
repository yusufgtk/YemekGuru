namespace YemekGuru.webapp.Models;

public class PaymentDateModel
{
    public int Id { get; set; }
    public int Status { get; set; } //1:havuz 2:ödendi 3:Geri ödendi
    public int Type { get; set; } //1:kredi kartı 2:bakiye
    public decimal TotalPrice { get; set; }
    public int End4Number { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ApprovedPaymentDate { get; set; }
    public int OrderId { get; set; }
    public OrderModel OrderModel { get; set; }
}