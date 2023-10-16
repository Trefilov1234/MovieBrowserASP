using System.Text.Json.Serialization;

namespace Movie.Models
{
    public class MovieApiResponse
    {
        [JsonPropertyName("Search")]
        public Cinema[] Cinemas { get; set; }
        [JsonPropertyName("totalResults")]
        public string TotalResults_string { get; set; }
        public int TotalResults { get=>int.Parse(TotalResults_string); }
        public string Response { get; set; }
        public string Error { get; set; }
    }
}


