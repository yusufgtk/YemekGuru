namespace YemekGuru.entity;

public class District
{
    public int Id { get; set; }
    public int? CityId  { get; set; }
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
}