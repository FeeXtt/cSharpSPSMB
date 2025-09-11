namespace Files
{
    public class Movie
    {
        private string Film { get; set; }
        private string Genre { get; set; }
        private string LeadStudio { get; set; }
        private int AudienceScore { get; set; }
        private decimal Profitability { get; set; }
        private int RottenTomatoes { get; set; }
        private float WorldwideGross { get; set; }
        private int Year { get; set; }
        
        
        public Movie(string film, string genre, string lead, string studio, int audienceScore, decimal profitability, int rottenTomatoes, float worldwideGross, int year)
        {
            Film = film;
            Genre = genre;
            LeadStudio = LeadStudio;
            AudienceScore = audienceScore;
            Profitability = profitability;
            RottenTomatoes = rottenTomatoes;
            WorldwideGross = worldwideGross;
            Year = year;
        }
    }
}
