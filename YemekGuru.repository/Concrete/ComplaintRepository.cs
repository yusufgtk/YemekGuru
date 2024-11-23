using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class ComplaintRepository : Repository<Complaint, DataContext>, IComplaintRepository
{
    public async Task<List<Complaint>> ActiveComplaints30Async()
    {
        using(var context = new  DataContext())
        {
            return await context.Complaints.Include(o=>o.Order).Where(i=>i.Status==false).OrderBy(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<Complaint> GetComplaintByIdAsync(int id)
    {
        using(var context = new  DataContext())
        {
            return await context.Complaints.Include(o=>o.Order).FirstOrDefaultAsync(i=>i.Id==id);
        }
    }

    public async Task<List<Complaint>> GetComplaintsByUserNameAsync(string userName)
    {
        using(var context = new  DataContext())
        {
            return await context.Complaints.Include(o=>o.Order).Where(i=>i.UserName==userName).OrderByDescending(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<bool> IsThereComplaintByOrderIdAsync(int orderId)
    {
        using(var context = new  DataContext())
        {
            var result = await context.Complaints.FirstOrDefaultAsync(i=>i.OrderId==orderId);
            if(result!=null)
            {
                return true;
            }
            return false;
        }
    }

    public async Task<int> TotalActiveComplaintsCountAsync()
    {
        using(var context = new  DataContext())
        {
            return await context.Complaints.Where(i=>i.Status==false).CountAsync();
        }
    }

    public async Task<int> TotalCompletedComplaintsCountAsync()
    {
        using(var context = new  DataContext())
        {
            return await context.Complaints.Where(i=>i.Status==true).CountAsync();
        }
    }
}
