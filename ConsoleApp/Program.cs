using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string hello = "HELLO WORLD";
            int i = 0;

            while (i < hello.Length)
            {
                Console.WriteLine(hello.Substring(0, i));
                i++;
            }
            while (i >= 0)
            {
                Console.WriteLine(hello.Substring(0, i));
                i--;
            }

            Console.WriteLine("Shall we have another go? Press 'Q' to quit, any other key to continue");
            if (Console.ReadKey().Key != ConsoleKey.Q)
                Main(null);
        }
    }
}
