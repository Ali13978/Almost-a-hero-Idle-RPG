using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace DynamicLoading
{
	[Serializable]
	public class AssetBundleEditorSettings
	{
		public static AssetBundleEditorSettings Instance
		{
			get
			{
				if (AssetBundleEditorSettings.instance == null)
				{
					if (File.Exists(AssetBundleEditorSettings.SettingsPath))
					{
						StreamReader streamReader = File.OpenText(AssetBundleEditorSettings.SettingsPath);
						AssetBundleEditorSettings.instance = JsonConvert.DeserializeObject<AssetBundleEditorSettings>(streamReader.ReadToEnd());
						streamReader.Close();
					}
					else
					{
						AssetBundleEditorSettings.instance = new AssetBundleEditorSettings();
						StreamWriter streamWriter = File.CreateText(AssetBundleEditorSettings.SettingsPath);
						streamWriter.Write(JsonConvert.SerializeObject(AssetBundleEditorSettings.instance));
						streamWriter.Close();
						streamWriter.Dispose();
					}
				}
				return AssetBundleEditorSettings.instance;
			}
		}

		public bool SimulationModeActive
		{
			get
			{
				return this.simulationModeActive;
			}
			set
			{
				this.simulationModeActive = value;
			}
		}

		public void SaveSettings()
		{
			File.WriteAllText(AssetBundleEditorSettings.SettingsPath, JsonConvert.SerializeObject(this));
		}

		[SerializeField]
		private bool simulationModeActive;

		private static AssetBundleEditorSettings instance;

		private static readonly string SettingsPath = Path.Combine(Application.persistentDataPath, "AssetBundlesEditorSettings.dat");
	}
}
