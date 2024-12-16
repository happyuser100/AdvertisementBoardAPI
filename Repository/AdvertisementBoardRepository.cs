using api.Interfaces;
using api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace api.Repository
{
    public class AdvertisementBoardRepository: IAdvertisementBoardRepository
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdvertisementBoardRepository(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<AdvertisementItem>> GetAllAsync()
        {
            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path
            var fullPath = Path.Combine(rootPath, "Data/ad.json");

            using StreamReader reader = new(fullPath);
            var json = await reader.ReadToEndAsync();

            var jarray = JArray.Parse(json);
            List<AdvertisementItem> items = new();
            foreach (var item in jarray)
            {
                AdvertisementItem? ad = item.ToObject<AdvertisementItem>();
                if (ad != null) 
                  items.Add(ad);
            }
            return items;
        }
    }
}
