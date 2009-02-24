using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IncBuildLibrary;

namespace IncBuildApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Foo foo = new Foo();

            Console.WriteLine("Message from the IncBuildApp.\r\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
