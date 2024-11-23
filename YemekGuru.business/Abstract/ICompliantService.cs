using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface IComplaintService
{
    public Task<bool> CreateAsync(Complaint entity);
    public Task<bool> DeleteAsync(Complaint entity);
    public Task<bool> UpdateAsync(Complaint entity);
    public Task<List<Complaint>> GetAllAsync();
    public Task<Complaint> GetComplaintByIdAsync(int id);
    public Task<List<Complaint>> GetComplaintsByUserNameAsync(string userName);
    public Task<List<Complaint>> ActiveComplaints30Async();
    public Task<int> TotalActiveComplaintsCountAsync();
    public Task<int> TotalCompletedComplaintsCountAsync();
    public Task<bool> IsThereComplaintByOrderIdAsync(int orderId);
    
}