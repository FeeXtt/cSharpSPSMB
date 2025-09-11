using System;

namespace setup
{
    internal class Program
    {
        public static int BottomBorder = 1;
        public static int UpperBorder = 100;
        public static void Main(string[] args)
        {
            
            GenerateNumbers();
            
            
            // for (int i = 0; i < 67; i++)
            // {
            //     FizzBuzz(i);
            // }
            
        }
        
        public static void FizzBuzz(int num)
        {
            if (num % 3 == 0 && num % 5 == 0)
            {
                Console.WriteLine("FizzBuzz");
            }

            if (num % 3 == 0)
            {
                Console.WriteLine("Fizz");
            }

            if (num % 5 == 0)
            {
                Console.WriteLine("Buzz");
            }

            if (num % 3 != 0 && num % 5 != 0)
            {
                Console.WriteLine(num);
            }
            
            
        }
        public static int GenerateNumbers()
        {
            Random rnd = new Random();
            int ThatNumber = rnd.Next(BottomBorder, UpperBorder);
            return(ThatNumber);
            
        }
       

        public static void GuesOfNum(int num)
        {
            while (num == guess)
                int guess = Console.ReadLine();
        }
    }
    
}