using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tapdaq
{
	public class TDCallbacks
	{
		internal TDCallbacks()
		{
		}

		public static TDCallbacks instance
		{
			get
			{
				if (TDCallbacks.reference == null)
				{
					TDCallbacks.reference = new TDCallbacks();
				}
				return TDCallbacks.reference;
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDAdEvent> AdAvailable;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDAdEvent> AdNotAvailable;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDAdEvent> AdWillDisplay;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDAdEvent> AdDidDisplay;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDAdEvent> AdDidFailToDisplay;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDAdEvent> AdClicked;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDAdEvent> AdClosed;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDAdEvent> AdError;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action TapdaqConfigLoaded;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDAdError> TapdaqConfigFailedToLoad;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDVideoReward> RewardVideoValidated;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<Dictionary<string, object>> CustomEvent;

		[Obsolete("Use events 'AdWillDisplay' and 'AdDidDisplay' instead.")]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDAdEvent> AdStarted;

		[Obsolete("Use event 'AdClosed' instead.")]
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<TDAdEvent> AdFinished;

		private static void Invoke<T>(Action<T> action, T value)
		{
			if (action != null)
			{
				action(value);
			}
		}

		private static void Invoke(Action action)
		{
			if (action != null)
			{
				action();
			}
		}

		public void OnAdAvailable(TDAdEvent adEvent)
		{
			TDCallbacks.Invoke<TDAdEvent>(TDCallbacks.AdAvailable, adEvent);
		}

		public void OnAdClicked(TDAdEvent adEvent)
		{
			TDCallbacks.Invoke<TDAdEvent>(TDCallbacks.AdClicked, adEvent);
		}

		public void OnAdError(TDAdEvent adEvent)
		{
			TDCallbacks.Invoke<TDAdEvent>(TDCallbacks.AdError, adEvent);
		}

		public void OnAdClosed(TDAdEvent adEvent)
		{
			TDCallbacks.Invoke<TDAdEvent>(TDCallbacks.AdClosed, adEvent);
		}

		public void OnAdNotAvailable(TDAdEvent adEvent)
		{
			TDCallbacks.Invoke<TDAdEvent>(TDCallbacks.AdNotAvailable, adEvent);
		}

		public void OnAdDidDisplay(TDAdEvent adEvent)
		{
			TDCallbacks.Invoke<TDAdEvent>(TDCallbacks.AdDidDisplay, adEvent);
		}

		public void OnAdWillDisplay(TDAdEvent adEvent)
		{
			TDCallbacks.Invoke<TDAdEvent>(TDCallbacks.AdWillDisplay, adEvent);
		}

		public void OnAdDidFailToDisplay(TDAdEvent adEvent)
		{
			TDCallbacks.Invoke<TDAdEvent>(TDCallbacks.AdDidFailToDisplay, adEvent);
		}

		public void OnTapdaqConfigLoaded()
		{
			TDCallbacks.Invoke(TDCallbacks.TapdaqConfigLoaded);
		}

		public void OnTapdaqConfigFailedToLoad(TDAdError error)
		{
			TDCallbacks.Invoke<TDAdError>(TDCallbacks.TapdaqConfigFailedToLoad, error);
		}

		public void OnRewardedVideoValidated(TDVideoReward reward)
		{
			TDCallbacks.Invoke<TDVideoReward>(TDCallbacks.RewardVideoValidated, reward);
		}

		public void OnCustomEvent(Dictionary<string, object> dictionary)
		{
			TDCallbacks.Invoke<Dictionary<string, object>>(TDCallbacks.CustomEvent, dictionary);
		}

		private static TDCallbacks reference;
	}
}
