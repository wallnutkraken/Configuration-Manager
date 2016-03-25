using System.Collections.Generic;
using System.IO;

namespace ConfigManager
{
    public class ConfigFile : IConfigFile
    {
        public ConfigFile(string path) : this(File.ReadAllLines(path))
        {

        }

        /// <summary>
        /// Used to create an empty config file. Useful for creating a config file from code
        /// </summary>
        public ConfigFile()
        {
            
        }

        public ConfigFile(IEnumerable<string> fileLines)
        {
            ParsedLines = new List<IConfigLine>();
            foreach (string line in fileLines)
            {
                ParsedLines.Add(new ConfigLine(line));
            }
        }

        public List<IConfigLine> ParsedLines { get; }
        public void WriteToFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (IConfigLine line in ParsedLines)
                {
                    writer.WriteLine(line.Regenerate());
                }
                writer.Close();
            }
        }

        public IConfigLine FindConfigLine(string config)
        {
            bool found = false;
            IConfigLine foundLine = null;
            for (int i = 0; i < ParsedLines.Count && found == false; i++)
            {
                if (ParsedLines[i].Setting != null)
                {
                    if (ParsedLines[i].Setting == config)
                    {
                        found = true;
                        foundLine = ParsedLines[i];
                    }
                }
            }

            if (found == false)
            {
                throw new KeyNotFoundException();
            }
            return foundLine;
        }

        public void SetSettingValue(string setting, string value)
        {
            bool found = false;
            for (int i = 0; i < ParsedLines.Count && found == false; i++)
            {
                if (ParsedLines[i].Setting == setting)
                {
                    ParsedLines[i].Value = value;
                    found = true;
                }
            }
            if (found == false)
            {
                throw new KeyNotFoundException();
            }
        }

        public void SetSettingConfig(string setting, string newSetting)
        {
            bool found = false;
            for (int i = 0; i < ParsedLines.Count && found == false; i++)
            {
                if (ParsedLines[i].Setting == setting)
                {
                    ParsedLines[i].Setting = newSetting;
                    found = true;
                }
            }
            if (found == false)
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
