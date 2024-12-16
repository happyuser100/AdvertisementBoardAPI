using api.Interfaces;
using api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace api.Repository
{
    public class AdvertisementBoardRepository: IAdvertisementBoardRepository
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private static readonly JsonSerializerOptions _options = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        public AdvertisementBoardRepository(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<AdvertisementItem>> GetAllAsync()
        {
            var fullPath = GetFullPath();

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

        public async Task<List<AdvertisementItem>> GetByPlaceAsync(string place)
        {
            var fullPath = GetFullPath();

            using StreamReader reader = new(fullPath);
            var json = await reader.ReadToEndAsync();

            var jarray = JArray.Parse(json);
            List<AdvertisementItem> items = new();
            foreach (var item in jarray)
            {
                AdvertisementItem? ad = item.ToObject<AdvertisementItem>();
                if (ad != null && ad.place.Contains(place))
                        items.Add(ad);
            }
            return items;
        }

        public async Task<AdvertisementItem?> GetByIdAsync(string id)
        {
            var fullPath = GetFullPath();

            using StreamReader reader = new(fullPath);
            var json = await reader.ReadToEndAsync();

            var jarray = JArray.Parse(json);
            List<AdvertisementItem> items = new();
            foreach (var item in jarray)
            {
                AdvertisementItem? ad = item.ToObject<AdvertisementItem>();
                if (ad != null && ad.id == id)
                    return ad;
            }
            return null;
        }


        public async Task<AdvertisementItem> CreateAsync(AdvertisementItem advertisementItem)
        {
            List<AdvertisementItem> items = await GetAllAsync();
            string newGuid = System.Guid.NewGuid().ToString();
            advertisementItem.id = newGuid;
            advertisementItem.postDate = DateTime.Now;
            items.Add(advertisementItem);

            WriteItems(items);
            return advertisementItem;
        }

        private string GetFullPath()
        {
            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path
            var fullPath = Path.Combine(rootPath, "Data/ad.json");
            return fullPath;
        }

        private void WriteItems(List<AdvertisementItem> items)
        {
            var fullPath = GetFullPath();

            var options = new JsonSerializerOptions(_options)
            {
                WriteIndented = true
            };
            var jsonString = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(fullPath, jsonString);
        }

        public async Task<AdvertisementItem?> DeleteAsync(string id)
        {
            AdvertisementItem? advertisementItem = null;
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
                {
                    if (ad.id != id)
                        items.Add(ad);
                    else
                        advertisementItem = ad;
                }
            }
            WriteItems(items);
            return advertisementItem;
        }

        public async Task<AdvertisementItem?> UpdateAsync(string id, AdvertisementItem advertisementItem)
        {
            AdvertisementItem? existingItem = await GetByIdAsync(id);
            if (existingItem == null)
            {
                return null;
            }

            existingItem.place = advertisementItem.place;
            existingItem.adProperty = advertisementItem.adProperty;
            existingItem.title = advertisementItem.title;
            existingItem.imageURL = advertisementItem.imageURL;
            existingItem.description = advertisementItem.description;
            existingItem.iconUrl = advertisementItem.iconUrl;
            existingItem.personName = advertisementItem.personName;
            existingItem.postDate = advertisementItem.postDate;
            existingItem.commentsNumber = advertisementItem.commentsNumber;
            existingItem.isWriteComments = advertisementItem.isWriteComments;

            return existingItem;
        }
    }
}
