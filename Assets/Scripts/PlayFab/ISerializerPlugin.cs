using System;

namespace PlayFab
{
	public interface ISerializerPlugin : IPlayFabPlugin
	{
		T DeserializeObject<T>(string serialized);

		T DeserializeObject<T>(string serialized, object serializerStrategy);

		object DeserializeObject(string serialized);

		string SerializeObject(object obj);

		string SerializeObject(object obj, object serializerStrategy);
	}
}
