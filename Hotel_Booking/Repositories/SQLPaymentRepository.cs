using Hotel_Booking.Data;
using Hotel_Booking.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking.Repositories
{
    public class SQLPaymentRepository : IPaymentRepository
    {
        private readonly HotelBookingDBContext _dBContext;

        public SQLPaymentRepository(HotelBookingDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public async Task<Payment> AddNewAsync(Payment payment)
        {
            await _dBContext.Payments.AddAsync(payment);
            await _dBContext.SaveChangesAsync();
            return payment;
        }

        public async Task<bool> ConfirmAsync(string userId)
        {
            var bookings = await _dBContext.Bookings.Where(x => x.CustomerId == userId).ToListAsync();

            if (bookings.Any())
            {
                List<Payment> payments = new List<Payment>();
                foreach (var booking in bookings)
                {
                    var room = await _dBContext.Rooms.Where(x => x.Id == booking.RoomId).FirstOrDefaultAsync();
                    if (room == null)
                    {
                        return false;
                    }

                    payments.Add(new Payment
                    {
                        Id = Guid.NewGuid(),
                        Amount = room.Price,
                        BookingId = booking.Id,
                        PaymentDate = DateTime.Now,
                        PaymentMethod = "VNPay"
                    });
                }

                _dBContext.Payments.AddRange(payments); 
                await _dBContext.SaveChangesAsync();
            }

            return false;
        }

        public async Task<Payment?> DeleteByIdAsync(Guid id)
        {
            var existingPayment = await _dBContext.Payments.FirstOrDefaultAsync(x => x.Id == id);

            if (existingPayment == null)
            {
                return null;
            }

            _dBContext.Payments.Remove(existingPayment);
            await _dBContext.SaveChangesAsync();
            return existingPayment;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _dBContext.Payments.ToListAsync();
        }

        public async Task<List<Payment>> GetAllByUserIdAsync(string userId)
        {
            return await _dBContext.Payments.ToListAsync();
        }

        public async Task<Payment?> UpdateyIdAsync(Guid id, Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
