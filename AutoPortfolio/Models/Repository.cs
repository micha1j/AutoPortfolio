using System.Text.Json.Serialization;

namespace AutoPortfolio.Models
{
    public class Repository
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string HtmlUrl { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
