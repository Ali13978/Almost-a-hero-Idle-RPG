using System;

internal interface IAdBridge
{
	void Init();

	bool IsInitialized();

	bool IsAdReady();

	void LoadAd();

	void ShowAd();

	void Update();

	void EnableCallbacks();

	void DisableCallbacks();
}
