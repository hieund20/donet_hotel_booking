using Hotel_Booking.Models.DTO;
using Hotel_Booking.Repositories;
using Hotel_Booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelImageRepository _hotelImageRepository;
        private readonly ProvicesService _proviceService;

        public HomeController(ILogger<HomeController> logger, 
                        IHotelRepository hotelRepository, 
                        IHotelImageRepository hotelImageRepository, 
                        ProvicesService provicesService
            )
        {
            this._logger = logger;
            this._hotelRepository = hotelRepository;
            this._hotelImageRepository = hotelImageRepository;
            this._proviceService = provicesService;
        }

        public async Task<IActionResult> Index(string? hotelName, string? province)
        {
            //Get Province data
            List<Province> provices = _proviceService.GetProvinces();

            //Get Hotel data
            var hotels = await _hotelRepository.GetAllAsync("Name", hotelName);
            var hotelProvinceFilter = await _hotelRepository.GetAllAsync("Province", province);

            hotels = hotels.Intersect(hotelProvinceFilter).ToList();

            List<HotelWithImageViewDto> result = new List<HotelWithImageViewDto>();

            foreach (var item in hotels)
            {
                var image = await _hotelImageRepository.GetByHotelIdAsync(item.Id);
                if (image != null)
                {
                    result.Add(new HotelWithImageViewDto
                    {
                        Hotel = item,
                        ImageUrl = image.FilePath
                    });
                }
            }

            HomeViewDto homeViewDto = new HomeViewDto();
            homeViewDto.Provinces = provices;
            homeViewDto.HotelWithImageViewDtos = result;

            return View(homeViewDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
