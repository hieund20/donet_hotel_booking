using Hotel_Booking.Models.Domains;
using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    public class RoomImagesController : Controller
    {
        private readonly IRoomImageRepository _roomImageRepository;

        public RoomImagesController(IRoomImageRepository roomImageRepository)
        {
            this._roomImageRepository = roomImageRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetAllImageByRoomId/{roomId}")]
        public async Task<IActionResult> GetAllByRoomId([FromRoute] Guid roomId)
        {
            ViewBag.RoomId = roomId;
            var images = await _roomImageRepository.GetAllByRoomIdAsync(roomId);

            return View("AdminRoomImagesView", images);
        }

        [HttpGet]
        public IActionResult Add([FromQuery] Guid roomId)
        {
            ViewBag.RoomId = roomId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoomImage request)
        {
            RoomImage image = new RoomImage();
            image.Id = Guid.NewGuid();
            image.FileName = request.FileName;
            image.FileDesciption = request.FileDesciption;
            image.RoomId = request.RoomId;
            image.File = request.File;
            image.FileExtension = Path.GetExtension(request.File.FileName);
            image.FileSizeInBytes = request.File.Length;

            var result = await _roomImageRepository.Upload(image);

            if (result is not null)
            {
                var images = await _roomImageRepository.GetAllByRoomIdAsync(request.RoomId);

                return View("AdminRoomImagesView", images);
            }

            return View();
        }
    }
}
