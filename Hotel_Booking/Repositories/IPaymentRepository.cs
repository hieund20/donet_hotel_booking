using Hotel_Booking.Models.Domains;

namespace Hotel_Booking.Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync();
        Task<List<Payment>> GetAllByUserIdAsync(string userId);
        Task<Payment> AddNewAsync(Payment payment);
        Task<bool> ConfirmAsync(string userId);
        Task<Payment?> UpdateyIdAsync(Guid id, Payment payment);
        Task<Payment?> DeleteByIdAsync(Guid id);
    }
}
