using YemekGuru.entity;

namespace YemekGuru.repository.Abstract;

public interface IComplaintRepository:IRepository<Complaint>
{
    public Task<Complaint> GetComplaintByIdAsync(int id);
    public Task<List<Complaint>> GetComplaintsByUserNameAsync(string userName);
    public Task<List<Complaint>> ActiveComplaints30Async();
    public Task<int> TotalActiveComplaintsCountAsync();
    public Task<int> TotalCompletedComplaintsCountAsync();
    public Task<bool> IsThereComplaintByOrderIdAsync(int orderId);
    
}