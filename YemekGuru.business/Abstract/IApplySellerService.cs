using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface IApplySellerService
{
    public Task<bool> CreateAsync(ApplySeller entity);
    public Task<bool> DeleteAsync(ApplySeller entity);
    public Task<bool> UpdateAsync(ApplySeller entity);
    public Task<List<ApplySeller>> GetAllAsync();
    public Task<List<ApplySeller>> GetApplySellersAsync();
    public Task<ApplySeller> GetApplySellerByIdAsync(int id);
    public Task<ApplySeller> GetApplySellerByUserIdAsync(string userId);
    public Task<int> ConfirmWaitApplySellerCountAsync();
}