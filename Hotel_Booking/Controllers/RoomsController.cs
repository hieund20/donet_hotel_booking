using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Mvc;
using Hotel_Booking.Models.Domains;
using Hotel_Booking.Models.DTO;

namespace Hotel_Booking.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomImageRepository _roomImageRepository;

        public RoomsController(IRoomRepository roomRepository, IRoomImageRepository roomImageRepository)
        {
            this._roomRepository = roomRepository;
            this._roomImageRepository = roomImageRepository;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _roomRepository.GetAllAsync();

            return View(rooms);
        }

        [HttpGet("GetAllByHotelId/{hotelId}")]
        public async Task<IActionResult> GetAllByHotelId([FromRoute] Guid hotelId)
        {
            var rooms = await _roomRepository.GetAllByHotelIdAsync(hotelId);

            List<RoomWithImageViewDto> result = new List<RoomWithImageViewDto>();

            foreach (var item in rooms)
            {
                var image = await _roomImageRepository.GetByRoomIdAsync(item.Id);
                if (image != null)
                {
                    result.Add(new RoomWithImageViewDto
                    {
                        Room = item,
                        ImageUrl = image.FilePath
                    });
                }
            }

            return View("ClientRoomsView", result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromRoute] Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);

            if (room is not null)
            {
                var image = await _roomImageRepository.GetByRoomIdAsync(id);

                RoomWithImageViewDto roomWithImage = new RoomWithImageViewDto();
                roomWithImage.Room = room;
                roomWithImage.ImageUrl = image != null ? image.FilePath: "";

                return View(roomWithImage);
            }

            return View(null);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Room newRoom)
        {

            var room = await _roomRepository.AddNewAsync(newRoom);

            if (room is not null)
            {
                return RedirectToAction("Index", "Rooms");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var room = await _roomRepository.GetByIdAsync(id);

            if (room is not null)
            {
                return View(room);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Room request)
        {
            var room = await _roomRepository.AddNewAsync(request);

            if (room is not null)
            {
                return RedirectToAction("Index", "Rooms");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Room request)
        {

            var room = _roomRepository.DeleteByIdAsync(request.Id);

            if (room is not null)
            {
                return RedirectToAction("Index", "Rooms");

            }

            return View("Edit");
        }
    }
}
