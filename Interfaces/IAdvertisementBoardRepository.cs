using api.Models;

namespace api.Interfaces
{
    public interface IAdvertisementBoardRepository
    {
        Task<List<AdvertisementItem>> GetAllAsync();
        Task<AdvertisementItem> CreateAsync(AdvertisementItem advertisementItem);
    }
}
