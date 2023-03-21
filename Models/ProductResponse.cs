using System.Text.Json.Serialization;

namespace ContentPlatformInterview.Models
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ProductType { get; set; } = string.Empty;

        public float Price { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; } = string.Empty;

        public double FinalPrice { get; set; }
    }
}
