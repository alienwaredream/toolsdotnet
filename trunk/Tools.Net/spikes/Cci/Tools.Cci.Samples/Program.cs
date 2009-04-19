using System;
using System.IO;

using Microsoft.Cci;

namespace Tools.Net.Cci.Samples
{
    class Program
    {
        static HostEnvironment env;

        static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                Console.WriteLine("Usage: assemblyversion path searchPattern");
                Console.WriteLine("Usage: assemblyversion \"c:\\product\" *.dll");
            }
            else
            {
                env = new HostEnvironment();

                AnalyzeDir(args[0], args[1]);
            }
        }

        private static void AnalyzeDir(string path, string searchPattern)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    try
                    {
                        IAssembly assembly =
                            (IAssembly)env.LoadUnitFrom(file);

                        Console.WriteLine(
                                String.Format("{0}|{1}|{2}",
                                    assembly.AssemblyIdentity.Location,
                                    assembly.AssemblyIdentity.Name,
                                    assembly.AssemblyIdentity.Version));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            String.Format("{0}|{1}|{2}",
                                    file,
                                    Path.GetFileName(file),
                                    "N/A"));
                    }
                }
            }
        }
    }
}