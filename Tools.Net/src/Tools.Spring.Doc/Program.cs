using System;
using System.IO;
using System.Xml.Xsl;
using System.Xml;
using System.Text.RegularExpressions;

namespace Tools.Spring.Doc
{
    class Program
    {
        static string indexPath;
        static void Main(string[] args)
        {
            string configPath = args[0];
            string outPath = args[1];

            try
            {
                indexPath = Path.Combine(outPath, "index.htm");
                File.Delete(indexPath);
                File.AppendAllText(indexPath, "<html><head><title>Index</title></head><body>");
                TraverseDirectory(configPath, outPath, "");
            }
            catch (Exception ex)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = oldColor;
            }
            finally
            {
                File.AppendAllText(indexPath, "</body></html>");
            }

            Console.Write("-----DONE!------");
            Console.ReadKey();
        }

        private static void TraverseDirectory(string sourceDir, string targetDir, string relativePart)
        {
            File.AppendAllText(indexPath, String.Format("<h2>{0}</h2>", relativePart));
            TransformFiles(sourceDir, targetDir, relativePart);
            

            var dirs = Directory.GetDirectories(sourceDir);

            foreach (string dir in dirs)
            {
                TraverseDirectory(
                sourceDir:Path.Combine(sourceDir, Path.GetFileName(dir)), 
                targetDir:Path.Combine(targetDir, Path.GetFileName(dir)),
                relativePart:relativePart + (String.IsNullOrEmpty(relativePart) ? "" : "/") + Path.GetFileName(dir));
            }
        }

        private static void TransformFiles(string sourceDir, string targetDir, string relativePart)
        {
            var files = Directory.GetFiles(sourceDir, "*.xml");

            foreach (string file in files)
            {
                string targetFilePath = String.Format(@"{0}\{1}.htm", targetDir, Path.GetFileNameWithoutExtension(file));

                File.AppendAllText(indexPath, String.Format("<a href=\"{0}/{1}.htm\" style=\"margin-left:40\">{1}</a><br/>", relativePart, Path.GetFileNameWithoutExtension(file)));

                if (!Directory.Exists(Path.GetDirectoryName(targetFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath));
                }
                
                ApplyTransform(file, targetFilePath);
            }
        }

        private static void ApplyTransform(string sourceFile, string targetFile)
        {
            var transform = new System.Xml.Xsl.XslCompiledTransform();
            var settings = new XsltSettings(enableDocumentFunction: false, enableScript: true);

            transform.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "doc.xslt"), settings, new XmlUrlResolver());
            transform.Transform(sourceFile, targetFile);

            Console.WriteLine(String.Format("***{0}->{1}", sourceFile, targetFile));
        }
        private static void RegexTest()
        {
            var rgx = new Regex("", RegexOptions.Singleline);
            //rgx.Replace(
        }
    }
}