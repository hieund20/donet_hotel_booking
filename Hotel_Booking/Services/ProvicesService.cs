using Hotel_Booking.Models.DTO;
using Newtonsoft.Json;

namespace Hotel_Booking.Services
{
    public class ProviceData
    {
        public string Code { get; set; }
    }

    public class ProvicesService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProvicesService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public List<Province> GetProvinces()
        {
            var rootPath = _hostingEnvironment.ContentRootPath;
            var fullPath = Path.Combine(rootPath, "JsonDatas/ProvicesJsonData.json");
            var jsonData = File.ReadAllText(fullPath);
            if (string.IsNullOrWhiteSpace(jsonData)) return null;

            var provicesDict = JsonConvert.DeserializeObject<Dictionary<string, ProviceData>>(jsonData);
            if (provicesDict == null) return null;

            var provices = new List<Province>();
            foreach (var kvp in provicesDict)
            {
                provices.Add(new Province
                {
                    Name = kvp.Key,
                    Code = kvp.Value.Code
                });
            }

            return provices;
        }
    }
}
