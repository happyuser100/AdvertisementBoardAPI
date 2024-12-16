namespace api.Models
{
    public class AdvertisementItem
    {
        public Guid Id { get; set; }
        public string Place { get; set; } = string.Empty;
        public string AdProperty { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public string PersonName { get; set; } = string.Empty;
        public DateTime PostDate { get; set; } = DateTime.Now;
        public int CommentsNumber { get; set; } = 0;
        public bool IsWriteComments { get; set; } = false;
    }
}
