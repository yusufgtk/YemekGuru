namespace YemekGuru.entity;

public class Restaurant
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? Country { get; set; }
    public int? CityId { get; set; }
    public int? DistrictId { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? EmailAdress { get; set; }
    public string? OpeningTime { get; set; }
    public string? ClosingTime { get; set; }
    public string? DeliveryTime { get; set; }
    public float? MinimumOrderPrice { get; set; }
    public float? Rating { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsApproved { get; set; }

}
