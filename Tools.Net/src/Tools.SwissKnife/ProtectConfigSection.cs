using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Globalization;

namespace Tools.SwissKnife
{
    class ProtectConfigSection
    {
        internal static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length < 3)
            {
                
                System.Console.WriteLine(" -p|-u|-s \"Path.exe.config|Path.web.config\" \"sectionName\"");
                System.Console.WriteLine("To protect: -p \"c:\\myapp\\myapp.exe.config\" \"mySection\"");
                System.Console.WriteLine("To unprotect: -u \"c:\\myapp\\myapp.exe.config\" \"mySection\"");
                System.Console.WriteLine("To show: -s \"c:\\myapp\\myapp.exe.config\" \"mySection\"");
                return;
            }

            string command = args[0];
            string configFilePath = args[1];
            string sectionName = args[2];

            // Get the current configuration file.
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = configFilePath;
            
            System.Configuration.Configuration config =
                    ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);


            // Get the section.
            ConfigurationSection section =
                config.GetSection(sectionName.Trim('\"', ' '));

            if (section == null)
                throw new Exception(String.Format(CultureInfo.InvariantCulture,
                    "Section {0} can't be found in the file {1}", sectionName, configFilePath));

            if (command == "-p")
            {
                // Protect (encrypt)the section.
                section.SectionInformation.ProtectSection(
                    "RsaProtectedConfigurationProvider");
                SaveSection(config, section);
                return;
            }
            if (command == "-u")
            {
                // Protect (encrypt)the section.
                section.SectionInformation.UnprotectSection();
                SaveSection(config, section);
                return;
            }
            if (command == "-s")
            {

                // Display decrypted configuration 
                // section. Note, the system
                // uses the Rsa provider to decrypt
                // the section transparently.
                string sectionXml =
                    section.SectionInformation.GetRawXml();

                System.Console.WriteLine("Decrypted section:");
                System.Console.WriteLine(sectionXml);
                return;
            }
            throw new Exception("Incorrect usage!");
        }

        private static void SaveSection(System.Configuration.Configuration config, ConfigurationSection section)
        {
            // Save the section.
            section.SectionInformation.ForceSave = true;

            config.Save(ConfigurationSaveMode.Full);
        }
    }
}
