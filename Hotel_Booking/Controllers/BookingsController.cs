﻿using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    public class BookingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
