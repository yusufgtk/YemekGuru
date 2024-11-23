using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class ComplaintManager : IComplaintService
{
    private readonly IComplaintRepository _complaintRepository;
    public ComplaintManager(IComplaintRepository complaintRepository)
    {   
        _complaintRepository=complaintRepository;
    }
    public async Task<List<Complaint>> ActiveComplaints30Async()
    {
        return await _complaintRepository.ActiveComplaints30Async();
    }

    public async Task<bool> CreateAsync(Complaint entity)
    {
        return await _complaintRepository.CreateAsync(entity);
    }

    public async Task<bool> DeleteAsync(Complaint entity)
    {
        return await _complaintRepository.DeleteAsync(entity);
    }

    public async Task<List<Complaint>> GetAllAsync()
    {
        return await _complaintRepository.GetAllAsync();
    }

    public async Task<Complaint> GetComplaintByIdAsync(int id)
    {
        return await _complaintRepository.GetComplaintByIdAsync(id);
    }

    public async Task<List<Complaint>> GetComplaintsByUserNameAsync(string userName)
    {
        return await _complaintRepository.GetComplaintsByUserNameAsync(userName);
    }

    public async Task<bool> IsThereComplaintByOrderIdAsync(int orderId)
    {
        return await _complaintRepository.IsThereComplaintByOrderIdAsync(orderId);
    }

    public async Task<int> TotalActiveComplaintsCountAsync()
    {
        return await _complaintRepository.TotalActiveComplaintsCountAsync();
    }

    public async Task<int> TotalCompletedComplaintsCountAsync()
    {
        return await _complaintRepository.TotalCompletedComplaintsCountAsync();
    }

    public async Task<bool> UpdateAsync(Complaint entity)
    {
        return await _complaintRepository.UpdateAsync(entity);
    }
}
