namespace api.Models
{
    public class AdvertisementItem
    {
        public string id { get; set; }
        public string place { get; set; } = string.Empty;
        public string adProperty { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string imageURL { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string iconUrl { get; set; } = string.Empty;
        public string personName { get; set; } = string.Empty;
        public DateTime postDate { get; set; } = DateTime.Now;
        public int commentsNumber { get; set; } = 0;
        public bool isWriteComments { get; set; } = false;
    }
}
