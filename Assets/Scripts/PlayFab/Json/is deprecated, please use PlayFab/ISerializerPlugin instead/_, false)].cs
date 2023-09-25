using System;

namespace PlayFab.Json
{
	[Obsolete("This interface is deprecated, please use PlayFab.ISerializerPlugin instead.", false)]
	public interface ISerializer : ISerializerPlugin, IPlayFabPlugin
	{
	}
}
