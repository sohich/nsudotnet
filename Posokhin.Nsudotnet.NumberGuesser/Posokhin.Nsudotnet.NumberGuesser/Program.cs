using System;

namespace Posokhin.Nsudotnet.NumberGuesser
{
    class Program
    {
        static void Main(string[] args)
        {
            string mystring = "hello";

            int num = 1;

            bool result = mystring.Equals(num); // this compiles OK!
            Console.WriteLine(result);
            new Handler().StartGame();
        }
    }
}
