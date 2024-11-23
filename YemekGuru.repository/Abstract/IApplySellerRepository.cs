using YemekGuru.entity;

namespace YemekGuru.repository.Abstract;

public interface IApplySellerRepository:IRepository<ApplySeller>
{
    public Task<List<ApplySeller>> GetApplySellersAsync();
    public Task<ApplySeller> GetApplySellerByIdAsync(int id);
    public Task<ApplySeller> GetApplySellerByUserIdAsync(string userId);
    public Task<int> ConfirmWaitApplySellerCountAsync();
}
