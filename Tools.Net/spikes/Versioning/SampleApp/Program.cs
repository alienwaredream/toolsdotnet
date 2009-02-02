using System;
using System.Reflection;
using System.Globalization;
using System.Diagnostics;
using SampleLibrary;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleTraceListener listener = new ConsoleTraceListener();
            Trace.Listeners.Add(listener);

            SampleClass sampleClass = new SampleClass();

            DumpAssemblies();

            Trace.Write("Press any key to exit");
            Console.ReadKey();

            Trace.Listeners.Remove(listener);
        }
        private static void DumpAssemblies()
        {

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Trace.WriteLine(
                    String.Format(CultureInfo.InvariantCulture, "FullName: [{0}] \r\nCodebase: [{1}]"
                    , assembly.FullName, assembly.CodeBase));
                Trace.WriteLine("********************");
            }
        }
    }
}
