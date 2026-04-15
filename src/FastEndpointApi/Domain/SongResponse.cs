namespace FastEndpointApi.Domain
{
    public class SongResponse
    {
        public required string Author { get; set; }
        
        public required string Title { get; set; }

        public required string Genre { get; set; }

        public int Year { get; set; }
    }


}

