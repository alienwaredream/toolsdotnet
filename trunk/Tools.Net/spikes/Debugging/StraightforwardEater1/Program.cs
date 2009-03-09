using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace StraightforwardEater1
{
    class Program
    {
        static void Main(string[] args)
        {
            int iterations = 10000;
            int stringSize = 10000;

            var array = new List<string>();

            Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Going to allocate {0} strings with length {1}",
                iterations, stringSize));

            for (int i = 0; i < iterations; i++)
            {
                array.Add(new String('c', stringSize));
            }

            Console.WriteLine("Done. Press any key to exit");
            Console.ReadKey();
        }
    }
}
