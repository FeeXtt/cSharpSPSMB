using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Files
{
  internal class Program
  {
    static void Main()
    {
        string path = "movies.csv";
        var movies = new List<Movie>();

        using (var sr = new StreamReader(path))
        {
            string header = sr.ReadLine();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                movies.Add(Movie.FromCsv(line));
            }
        }
        
        var groupedByYear = movies.GroupBy(m => m.Year);

        foreach (var group in groupedByYear)
        {
            int year = group.Key;
            var moviesInYear = group.ToList();

            var worstRated = moviesInYear.OrderBy(m => m.RottenTomatoes).First();
            var bestRated = moviesInYear.OrderByDescending(m => m.RottenTomatoes).First();
            var mostProfitable = moviesInYear.OrderByDescending(m => m.Profitability).First();
            var leastProfitable = moviesInYear.OrderBy(m => m.Profitability).First();
            var avgWorldwideGross = moviesInYear.Average(m => m.WorldwideGross);

            Console.WriteLine($"Rok {year}:");
            Console.WriteLine($" - Nejhůř hodnocený: {worstRated.Film} ({worstRated.RottenTomatoes}%)");
            Console.WriteLine($" - Nejlépe hodnocený: {bestRated.Film} ({bestRated.RottenTomatoes}%)");
            Console.WriteLine($" - Nejvýdělečnější: {mostProfitable.Film} (Profitability {mostProfitable.Profitability})");
            Console.WriteLine($" - Nejméně výdělečný: {leastProfitable.Film} (Profitability {leastProfitable.Profitability})");
            Console.WriteLine($" - Průměrný Worldwide Gross: {avgWorldwideGross:C}\n");
        }

        var years = movies.Select(m => m.Year).OrderBy(y => y).ToList();
        double medianYear;
        int count = years.Count;
        if (count % 2 == 0)
            medianYear = (years[count / 2 - 1] + years[count / 2]) / 2.0;
        else
            medianYear = years[count / 2];
        Console.WriteLine($"Medián roků: {medianYear}");
        
        AddNewMovie(path);
    }

    static void AddNewMovie(string path)
    {
        Console.WriteLine("\nPřidání nového filmu:");
        Movie newMovie = new Movie();

        Console.Write("Film: ");
        newMovie.Film = Console.ReadLine();

        Console.Write("Genre: ");
        newMovie.Genre = Console.ReadLine();

        Console.Write("Lead Studio: ");
        newMovie.LeadStudio = Console.ReadLine();

        Console.Write("Audience score %: ");
        newMovie.AudienceScore = int.Parse(Console.ReadLine());

        Console.Write("Profitability: ");
        newMovie.Profitability = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

        Console.Write("Rotten Tomatoes %: ");
        newMovie.RottenTomatoes = int.Parse(Console.ReadLine());

        Console.Write("Worldwide Gross (bez $): ");
        newMovie.WorldwideGross = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

        Console.Write("Year: ");
        newMovie.Year = int.Parse(Console.ReadLine());
        
        using (var sw = File.AppendText(path))
        {
            sw.WriteLine(newMovie.ToCsv());
        }

        Console.WriteLine("Film byl přidán do CSV.");
    }
  }
  
}