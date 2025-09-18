using System.Globalization;

namespace Files
{
    public class Movie
    {

        public string Film { get; set; }
        public string Genre { get; set; }
        public string LeadStudio { get; set; }
        public int AudienceScore { get; set; }
        public double Profitability { get; set; }
        public int RottenTomatoes { get; set; }
        public decimal WorldwideGross { get; set; }
        public int Year { get; set; }
        public static Movie FromCsv(string csvLine)
        {
            var values = csvLine.Split(',');


            return new Movie
            {
                Film = values[0].Trim(),
                Genre = values[1].Trim(),
                LeadStudio = values[2].Trim(),
                AudienceScore = int.Parse(values[3].Trim(), CultureInfo.InvariantCulture),
                Profitability = double.Parse(values[4].Trim(), CultureInfo.InvariantCulture),
                RottenTomatoes = int.Parse(values[5].Trim(), CultureInfo.InvariantCulture),
                WorldwideGross = decimal.Parse(values[6].Replace("$", "").Trim(), CultureInfo.InvariantCulture),
                Year = int.Parse(values[7].Trim(), CultureInfo.InvariantCulture)
            };
        }

        public string ToCsv()
        {
            return $"{Film},{Genre},{LeadStudio},{AudienceScore},{Profitability.ToString(CultureInfo.InvariantCulture)},{RottenTomatoes},${WorldwideGross.ToString(CultureInfo.InvariantCulture)},{Year}";
        }

    }
}