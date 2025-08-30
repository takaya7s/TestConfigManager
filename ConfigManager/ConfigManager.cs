using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ConfigManager
{
	public class ConfigManagerWithXML
	{
		/* フィールド */
		private Type systemType;

		/* プロパティ */
		private string configDir;
		/// <summary>
		/// 設定ファイルの保存先のパス
		/// </summary>
		public string ConfigDir
		{
			set
			{
				configDir = value;
			}
			get
			{
				return configDir;
			}
		}

		/* コンストラクタ */
		/// <summary>
		/// インスタンスの生成
		/// </summary>
		/// <param name="systemType"></param>
		public ConfigManagerWithXML(Type systemType)
		{
			this.systemType = systemType;
			this.configDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
		}

		/// <summary>
		/// インスタンスの生成
		/// </summary>
		/// <param name="systemType"></param>
		/// <param name="configDir"></param>
		public ConfigManagerWithXML(Type systemType, string configDir)
		{
			this.systemType = systemType;
			this.configDir = configDir;
		}

		public bool IsExists()
        {
			return File.Exists(GetFilePath());
        }

		/* メソッド */
		/// <summary>
		/// ファイル名の生成
		/// </summary>
		/// <returns></returns>
		public string GetFilePath()
		{
			return Path.Combine(configDir, systemType.Name + ".xml");
		}

		/// <summary>
		/// 設定の読み込み
		/// </summary>
		/// <returns></returns>
		public object LoadConfig()
		{
			// ファイルの読み込みとXML逆シリアル化処理
			XmlSerializer serializer = new XmlSerializer(systemType);
			StreamReader sr = new StreamReader(GetFilePath(), new UTF8Encoding(false));
			object config = serializer.Deserialize(sr);
			sr.Close();

			return config;
		}

		/// <summary>
		/// 設定の書き込み
		/// </summary>
		/// <param name="config"></param>
		public void SaveConfig(object config)
		{
            // フォルダが無ければ生成
            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);
			// XMLシリアル化とファイルの書き込み処理
			XmlSerializer serializer = new XmlSerializer(systemType);
			StreamWriter sw = new StreamWriter(GetFilePath(), false, new UTF8Encoding(false));
			serializer.Serialize(sw, config);
			sw.Close();
		}
	}
}
