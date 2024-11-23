using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class ApplySellerRepository : Repository<ApplySeller, DataContext>, IApplySellerRepository
{
    public async Task<int> ConfirmWaitApplySellerCountAsync()
    {
        using(var context = new DataContext())
        {
            return await context.ApplySellers.Where(i=>i.ApplyState==1).CountAsync();
        }
    }

    public async Task<ApplySeller> GetApplySellerByIdAsync(int id)
    {
        using(var context = new DataContext())
        {
            return await context.ApplySellers.FirstOrDefaultAsync(i=>i.Id==id);
        }
    }

    public async Task<ApplySeller> GetApplySellerByUserIdAsync(string userId)
    {
        using(var context = new DataContext())
        {
            return await context.ApplySellers.FirstOrDefaultAsync(i=>i.UserId==userId);
        }
    }

    public async Task<List<ApplySeller>> GetApplySellersAsync()
    {
        using(var context = new DataContext())
        {
            return await context.ApplySellers.Take(30).ToListAsync();
        }
    }
}
