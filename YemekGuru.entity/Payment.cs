namespace YemekGuru.entity;

public class Payment
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string UserId { get; set; }
    public int Status { get; set; } //1:havuz 2:kasada 3:Ödendi 4:geri ödeme için bekliyor 5: geri ödendi
    public int Type { get; set; } //1:kredi kartı 2:bakiye
    public decimal? TotalPrice { get; set; }
    public int End4Number { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ApprovedPaymentDate { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}