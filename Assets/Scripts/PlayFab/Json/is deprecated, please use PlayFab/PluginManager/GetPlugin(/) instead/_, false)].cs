using System;

namespace PlayFab.Json
{
	[Obsolete("This class is deprecated, please use PlayFab.PluginManager.GetPlugin(..) instead.", false)]
	public static class JsonWrapper
	{
		[Obsolete("This property is deprecated, please use PlayFab.PluginManager.GetPlugin(..) instead.", false)]
		public static ISerializerPlugin Instance
		{
			get
			{
				return PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty);
			}
		}

		public static T DeserializeObject<T>(string json)
		{
			return PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty).DeserializeObject<T>(json);
		}

		public static T DeserializeObject<T>(string json, object jsonSerializerStrategy)
		{
			return PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty).DeserializeObject<T>(json, jsonSerializerStrategy);
		}

		public static object DeserializeObject(string json)
		{
			return PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty).DeserializeObject(json);
		}

		public static string SerializeObject(object json)
		{
			return PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty).SerializeObject(json);
		}

		public static string SerializeObject(object json, object jsonSerializerStrategy)
		{
			return PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty).SerializeObject(json, jsonSerializerStrategy);
		}
	}
}
