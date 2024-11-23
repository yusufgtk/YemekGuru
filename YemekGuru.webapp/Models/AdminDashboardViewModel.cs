namespace YemekGuru.webapp.Models;

public class AdminDashboardViewModel
{

    public int LiveUsersCount { get; set; }
    public int RestaurantsNumber { get; set; }
    public int UsersNumber { get; set; }
    public int OrdersNumber { get; set; }
    public int OpenRestaurantsNumber { get; set; }
    public int CloseRestaurantsNumber { get; set; }
    public int ProductsNumber { get; set; }
    public int CategoriesNumber { get; set; }
    public int HaveCartItemsUsersNumber { get; set; }
    public int HaveCuponUsersNumber { get; set; }
    public int NotHaveCuponUsersNumber { get; set; }
    public int ApplySellerNumber { get; set; }
    public int HaveCartItemsUserNumber { get; set; }
    public int ConfirmWaitApplySellerNumber { get; set; }
    public int WaitApprovedProductsCount { get; set; }
    public int WaitApprovedRestaurantsCount { get; set; }
    public int TotalCommentsCount { get; set; }
    public int WaitApprovedCommentsCount { get; set; }
    public int WaitApprovedFeedbackCount { get; set; }
    public int ActiveComplaintsCount { get; set; }
    public int TotalCompletedComplaintCount { get; set; }

}