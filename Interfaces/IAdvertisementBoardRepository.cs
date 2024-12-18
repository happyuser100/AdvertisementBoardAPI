using api.Models;

namespace api.Interfaces
{
    public interface IAdvertisementBoardRepository
    {
        Task<List<AdvertisementItem>> GetAllAsync();
        Task<AdvertisementItem> CreateAsync(AdvertisementItem advertisementItem);
        Task<List<AdvertisementItem>> GetByPlaceAsync(string place);
        Task<List<AdvertisementItem>> GetByPlacePropAsync(string place, string prop);
        Task<List<AdvertisementItem>> GetByOnlyProp(string prop);
        Task<AdvertisementItem?> GetByIdAsync(string id);
        Task<AdvertisementItem?> DeleteAsync(string id);
        Task<AdvertisementItem?> UpdateAsync(string id, AdvertisementItem advertisementItem);
    }
}
