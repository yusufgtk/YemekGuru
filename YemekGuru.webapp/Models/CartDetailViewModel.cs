using YemekGuru.entity;

namespace YemekGuru.webapp.Models;

public class CartDetailViewModel
{
    public CartModel? CartModel { get; set; }
    public RestaurantModel? RestaurantModel { get; set; }
    public List<CartItemModel>? CartItemModels { get; set; }
}