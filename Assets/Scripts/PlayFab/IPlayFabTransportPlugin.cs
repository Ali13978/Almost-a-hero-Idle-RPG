using System;

namespace PlayFab
{
	public interface IPlayFabTransportPlugin : ITransportPlugin, IPlayFabPlugin
	{
		string AuthKey { get; set; }

		string EntityToken { get; set; }
	}
}
