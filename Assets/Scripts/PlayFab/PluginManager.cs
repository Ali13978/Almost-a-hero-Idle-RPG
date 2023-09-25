using System;
using System.Collections.Generic;
using PlayFab.Internal;
using PlayFab.Json;

namespace PlayFab
{
	public class PluginManager
	{
		private PluginManager()
		{
		}

		public static T GetPlugin<T>(PluginContract contract, string instanceName = "") where T : IPlayFabPlugin
		{
			return (T)((object)PluginManager.Instance.GetPluginInternal(contract, instanceName));
		}

		public static void SetPlugin(IPlayFabPlugin plugin, PluginContract contract, string instanceName = "")
		{
			PluginManager.Instance.SetPluginInternal(plugin, contract, instanceName);
		}

		private IPlayFabPlugin GetPluginInternal(PluginContract contract, string instanceName)
		{
			KeyValuePair<PluginContract, string> key = new KeyValuePair<PluginContract, string>(contract, instanceName);
			if (!this.plugins.ContainsKey(key))
			{
				IPlayFabPlugin value;
				if (contract != PluginContract.PlayFab_Serializer)
				{
					if (contract != PluginContract.PlayFab_Transport)
					{
						throw new ArgumentException("This contract is not supported", "contract");
					}
					value = this.CreatePlayFabTransportPlugin();
				}
				else
				{
					value = this.CreatePlugin<SimpleJsonInstance>();
				}
				this.plugins[key] = value;
			}
			return this.plugins[key];
		}

		private void SetPluginInternal(IPlayFabPlugin plugin, PluginContract contract, string instanceName)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin", "Plugin instance cannot be null");
			}
			KeyValuePair<PluginContract, string> key = new KeyValuePair<PluginContract, string>(contract, instanceName);
			this.plugins[key] = plugin;
		}

		private IPlayFabPlugin CreatePlugin<T>() where T : IPlayFabPlugin, new()
		{
			return (IPlayFabPlugin)Activator.CreateInstance(typeof(T));
		}

		private ITransportPlugin CreatePlayFabTransportPlugin()
		{
			ITransportPlugin transportPlugin = null;
			if (PlayFabSettings.RequestType == WebRequestType.HttpWebRequest)
			{
				transportPlugin = new PlayFabWebRequest();
			}
			if (PlayFabSettings.RequestType == WebRequestType.UnityWww)
			{
				transportPlugin = new PlayFabWww();
			}
			if (transportPlugin == null)
			{
				transportPlugin = new PlayFabUnityHttp();
			}
			return transportPlugin;
		}

		private Dictionary<KeyValuePair<PluginContract, string>, IPlayFabPlugin> plugins = new Dictionary<KeyValuePair<PluginContract, string>, IPlayFabPlugin>();

		private static readonly PluginManager Instance = new PluginManager();
	}
}
