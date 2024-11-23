namespace YemekGuru.entity;

public class Address
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? Title { get; set; }
    public string? Country { get; set; }
    public int? City { get; set; }
    public int? District { get; set; }
    public string? FullAddress { get; set; }
    public string? Description { get; set; }

}