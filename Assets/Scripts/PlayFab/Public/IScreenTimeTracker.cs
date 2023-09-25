using System;

namespace PlayFab.Public
{
	public interface IScreenTimeTracker
	{
		void OnEnable();

		void OnDisable();

		void OnDestroy();

		void OnApplicationQuit();

		void OnApplicationFocus(bool isFocused);

		void ClientSessionStart(string entityId, string entityType, string playFabUserId);

		void Send();
	}
}
