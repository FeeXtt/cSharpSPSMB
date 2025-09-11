using System.Collections.Generic;
using System.IO;

namespace Files
{
  internal class Program
  {
    public List<Movie> Movies = new List<Movie>();
    public static void Main(string[] args)
    {
      using (var reader = new StreamReader("./movies.csv"))
      {
        reader.ReadLine();
        while (!reader.EndOfStream)
        {
          var line = reader.ReadLine();
          var values = line.Split(',');
          try
          {
            Movies.Add(new Movie(values[0], values[1], values[2], values[3]));
            
          }
        }
      }
    }
  }
  
}