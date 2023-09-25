using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Tapdaq
{
	public class TDEventHandler : MonoBehaviour
	{
		public static TDEventHandler instance
		{
			get
			{
				if (!TDEventHandler.reference)
				{
					TDEventHandler[] array = UnityEngine.Object.FindObjectsOfType<TDEventHandler>();
					if (array.Length > 0)
					{
						TDEventHandler.reference = array[0];
					}
					else
					{
						TDEventHandler.reference = new GameObject("TapdaqV1").AddComponent<TDEventHandler>();
						TDDebugLogger.Log(":: Ad test ::Spawned Event Handler");
					}
				}
				return TDEventHandler.reference;
			}
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void Init()
		{
		}

		private void _didLoadConfig(string message)
		{
			TDDebugLogger.LogWarning("_didLoadConfig");
			TDCallbacks.instance.OnTapdaqConfigLoaded();
		}

		private void _didFailToLoadConfig(string message)
		{
			TDDebugLogger.LogWarning("_didFailToLoadConfig");
			TDAdError error = JsonConvert.DeserializeObject<TDAdError>(message);
			TDCallbacks.instance.OnTapdaqConfigFailedToLoad(error);
		}

		private void _didLoad(string jsonMessage)
		{
			TDDebugLogger.Log("_didLoad " + jsonMessage);
			TDAdEvent adEvent = JsonConvert.DeserializeObject<TDAdEvent>(jsonMessage);
			TDCallbacks.instance.OnAdAvailable(adEvent);
		}

		private void _didFailToLoad(string jsonMessage)
		{
			TDDebugLogger.Log("_didFailToLoad " + jsonMessage);
			TDAdEvent adEvent = JsonConvert.DeserializeObject<TDAdEvent>(jsonMessage);
			TDCallbacks.instance.OnAdNotAvailable(adEvent);
		}

		private void _didClose(string jsonMessage)
		{
			TDDebugLogger.Log("_didClose " + jsonMessage);
			TDAdEvent adEvent = JsonConvert.DeserializeObject<TDAdEvent>(jsonMessage);
			TDCallbacks.instance.OnAdClosed(adEvent);
		}

		private void _didClick(string jsonMessage)
		{
			TDDebugLogger.Log("_onAdClick " + jsonMessage);
			TDAdEvent adEvent = JsonConvert.DeserializeObject<TDAdEvent>(jsonMessage);
			TDCallbacks.instance.OnAdClicked(adEvent);
		}

		private void _didDisplay(string jsonMessage)
		{
			TDDebugLogger.Log("_didDisplay " + jsonMessage);
			TDAdEvent adEvent = JsonConvert.DeserializeObject<TDAdEvent>(jsonMessage);
			TDCallbacks.instance.OnAdDidDisplay(adEvent);
		}

		private void _willDisplay(string jsonMessage)
		{
			TDDebugLogger.Log("_willDisplay " + jsonMessage);
			TDAdEvent adEvent = JsonConvert.DeserializeObject<TDAdEvent>(jsonMessage);
			TDCallbacks.instance.OnAdWillDisplay(adEvent);
		}

		private void _didFailToDisplay(string jsonMessage)
		{
			TDDebugLogger.Log("_didFailToDisplay " + jsonMessage);
			TDAdEvent adEvent = JsonConvert.DeserializeObject<TDAdEvent>(jsonMessage);
			TDCallbacks.instance.OnAdDidFailToDisplay(adEvent);
		}

		private void _didComplete(string adType)
		{
			TDDebugLogger.Log("_didComplete " + adType);
		}

		private void _didEngagement(string adType)
		{
			TDDebugLogger.Log("_didEngagement " + adType);
		}

		private void _didReachLimit(string adType)
		{
			TDDebugLogger.Log("_didReachLimit " + adType);
			TDCallbacks.instance.OnAdError(new TDAdEvent(adType, "VALIDATION_EXCEEDED_QUOTA", null));
		}

		private void _onRejected(string adType)
		{
			TDDebugLogger.Log("_onRejected " + adType);
			TDCallbacks.instance.OnAdError(new TDAdEvent(adType, "VALIDATION_REJECTED", null));
		}

		private void _didFail(string jsonMessage)
		{
			TDDebugLogger.Log("_didFail " + jsonMessage);
			TDAdEvent adEvent = JsonConvert.DeserializeObject<TDAdEvent>(jsonMessage);
			TDCallbacks.instance.OnAdError(adEvent);
		}

		private void _onUserDeclined(string adType)
		{
			TDDebugLogger.Log("_onUserDeclined " + adType);
			TDCallbacks.instance.OnAdClosed(new TDAdEvent(adType, "DECLINED_TO_VIEW", null));
		}

		private void _didVerify(string message)
		{
			TDDebugLogger.Log("_didVerify " + message);
			TDVideoReward reward = JsonConvert.DeserializeObject<TDVideoReward>(message);
			TDCallbacks.instance.OnRewardedVideoValidated(reward);
		}

		private void _onValidationFailed(string jsonMessage)
		{
			TDDebugLogger.Log("_onValidationFailed " + jsonMessage);
			TDAdEvent adEvent = JsonConvert.DeserializeObject<TDAdEvent>(jsonMessage);
			TDCallbacks.instance.OnAdError(adEvent);
		}

		private void _didRefresh(string jsonMessage)
		{
			TDDebugLogger.Log("_didRefresh " + jsonMessage);
			TDAdEvent adEvent = JsonConvert.DeserializeObject<TDAdEvent>(jsonMessage);
			TDCallbacks.instance.OnAdAvailable(adEvent);
		}

		private void _didFailToFetchNative(string message)
		{
			TDDebugLogger.Log("_didFailToFetchNative " + message);
			TDCallbacks.instance.OnAdError(new TDAdEvent("NATIVE_AD", message, null));
		}

		private void _didCustomEvent(string jsonMessage)
		{
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonMessage);
			TDDebugLogger.Log("keys - " + dictionary.Keys.Count);
			TDCallbacks.instance.OnCustomEvent(dictionary);
		}

		private static TDEventHandler reference;
	}
}
