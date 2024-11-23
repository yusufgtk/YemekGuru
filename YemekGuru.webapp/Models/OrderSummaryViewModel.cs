namespace YemekGuru.webapp.Models;

public class OrderSummaryViewModel
{
    public UserModel? UserModel { get; set; }
    public List<AddressModel>? AddressModels { get; set; }
    public CartModel? CartModel { get; set; }
    public List<CartItemModel>? CartItemModels { get; set; }
    public decimal TotalPrice { get; set; }
}