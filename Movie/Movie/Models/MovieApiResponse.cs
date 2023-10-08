namespace Movie.Models
{
    public class MovieApiResponse
    {
        public Cinema[] Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
        public string Error { get; set; }
    }
}


