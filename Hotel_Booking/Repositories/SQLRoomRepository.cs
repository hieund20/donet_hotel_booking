using Hotel_Booking.Data;
using Hotel_Booking.Models.Domains;
using Hotel_Booking.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking.Repositories
{
    public class SQLRoomRepository : IRoomRepository
    {
        private readonly HotelBookingDBContext _dBContext;

        public SQLRoomRepository(HotelBookingDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public async Task<Room> AddNewAsync(Room room)
        {
            await _dBContext.Rooms.AddAsync(room);
            await _dBContext.SaveChangesAsync();
            return room;
        }

        public async Task<Room?> DeleteByIdAsync(Guid id)
        {
            var existingRoom = await _dBContext.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRoom == null)
            {
                return null;
            }

            _dBContext.Rooms.Remove(existingRoom);
            await _dBContext.SaveChangesAsync();
            return existingRoom;
        }

        public async Task<List<Room>> GetAllAsync()
        {
            var rooms = await _dBContext.Rooms.ToListAsync();
            return rooms;
        }

        public async Task<Room?> GetByIdAsync(Guid id)
        {
            var room = await _dBContext.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (room == null)
            {
                return null;
            }

            return room;
        }

        public async Task<Room?> UpdateyIdAsync(Guid id, Room room)
        {
            var existingRoom = await _dBContext.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRoom == null)
            {
                return null;
            }

            existingRoom.Name = room.Name;
            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.Price = room.Price;
            existingRoom.Capacity = room.Capacity;
            existingRoom.RoomType = room.RoomType;
            existingRoom.Status = room.Status;

            await _dBContext.SaveChangesAsync();

            return existingRoom;
        }
    }
}
