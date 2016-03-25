using ConfigManager;

namespace ConsoleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigFile conf = new ConfigFile("file.conf");
        }
    }
}
