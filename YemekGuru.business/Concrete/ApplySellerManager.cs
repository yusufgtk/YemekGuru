using System.Runtime.CompilerServices;
using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class ApplySellerManager : IApplySellerService
{
    private readonly IApplySellerRepository _applySellerRepository;
    public ApplySellerManager(IApplySellerRepository applySellerRepository)
    {
        _applySellerRepository=applySellerRepository;
    }

    public async Task<int> ConfirmWaitApplySellerCountAsync()
    {
        return await _applySellerRepository.ConfirmWaitApplySellerCountAsync();
    }

    public async Task<bool> CreateAsync(ApplySeller entity)
    {
        return await _applySellerRepository.CreateAsync(entity);
    }

    public async Task<bool> DeleteAsync(ApplySeller entity)
    {
        return await _applySellerRepository.DeleteAsync(entity);
    }

    public async Task<List<ApplySeller>> GetAllAsync()
    {
        return await _applySellerRepository.GetAllAsync();
    }

    public async Task<ApplySeller> GetApplySellerByIdAsync(int id)
    {
        return await _applySellerRepository.GetApplySellerByIdAsync(id);
    }

    public async Task<ApplySeller> GetApplySellerByUserIdAsync(string userId)
    {
        return await _applySellerRepository.GetApplySellerByUserIdAsync(userId);
    }

    public async Task<List<ApplySeller>> GetApplySellersAsync()
    {
        return await _applySellerRepository.GetApplySellersAsync();
    }

    public async Task<bool> UpdateAsync(ApplySeller entity)
    {
        return await _applySellerRepository.UpdateAsync(entity);
    }
}
