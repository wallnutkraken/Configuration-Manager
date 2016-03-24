using System.Collections.Generic;

namespace ConfigManager
{
    public interface IConfigFile
    {
        List<IConfigLine> ParsedLines { get; }
        void WriteToFile(string path);
        IConfigLine FindConfigLine(string config);
        void SetSettingValue(string setting, string value);
        void SetSettingConfig(string setting, string newSetting);

    }
}
