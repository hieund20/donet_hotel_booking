using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Mvc;
using Hotel_Booking.Models.Domains;
using System.Net.Http;

namespace Hotel_Booking.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomRepository _roomRepository;

        public RoomsController(IRoomRepository roomRepository)
        {
            this._roomRepository = roomRepository;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _roomRepository.GetAllAsync();

            return View(rooms);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);

            if (room is not null)
            {
                return View(room);
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
