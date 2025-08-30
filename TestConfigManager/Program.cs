using System;
using System.Collections.Generic;
using System.Text;

using ConfigManager;

using TestConfigManager.Config;

namespace TestConfigManager
{
	class Program
    {
        private static ConfigManagerWithXML configManager;
        private static MainConfig mainConfig;

        static void Main(string[] args)
		{
            InitConfig();
            LoadConfig();
            ChangeConfig();
            SaveConfig();

            Console.Write("続行するには何かキーを押してください . . .");
            Console.ReadKey();
        }

        private static void InitConfig()
        {
            configManager = new ConfigManagerWithXML(typeof(MainConfig));
        }

        private static void ChangeConfig()
        {
            mainConfig.TestCol1 += 1;
            mainConfig.TestCol2 += "X";
            mainConfig.TestCol3 = "項目３";
        }

        public static void LoadConfig()
        {
            if (configManager.IsExists())
            {
                mainConfig = (MainConfig)configManager.LoadConfig();

                Console.WriteLine(mainConfig.TestCol1);
                Console.WriteLine(mainConfig.TestCol2);
                Console.WriteLine(mainConfig.TestCol3);
            }
            else
            {
                mainConfig = new MainConfig();
                Console.WriteLine("ファイルが存在しないためデフォルト値で読み込みました。");

                Console.WriteLine(mainConfig.TestCol1);
                Console.WriteLine(mainConfig.TestCol2);
                Console.WriteLine(mainConfig.TestCol3);
            }

            Console.WriteLine();
        }

        public static void SaveConfig()
        {
            configManager.SaveConfig(mainConfig);
            Console.WriteLine("ファイルを保存しました。");

            Console.WriteLine(mainConfig.TestCol1);
            Console.WriteLine(mainConfig.TestCol2);
            Console.WriteLine(mainConfig.TestCol3);
        }
	}
}
