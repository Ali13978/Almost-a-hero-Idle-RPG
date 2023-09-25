using System;
using System.Diagnostics;
using SA.Common.Pattern;
using SA.IOSNative.Models;
using SA.IOSNative.UserNotifications;
using UnityEngine;

namespace SA.IOSNative.Core
{
	public class AppController : Singleton<AppController>
	{
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnApplicationDidEnterBackground;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnApplicationDidBecomeActive;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnApplicationDidReceiveMemoryWarning;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnApplicationWillResignActive;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnApplicationWillTerminate;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<LaunchUrl> OnOpenURL;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<UniversalLink> OnContinueUserActivity;

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public static void Subscribe()
		{
			Singleton<AppController>.Instance.enabled = true;
		}

		public static LaunchUrl LaunchUrl
		{
			get
			{
				return new LaunchUrl(string.Empty, string.Empty);
			}
		}

		public static UniversalLink LaunchUniversalLink
		{
			get
			{
				return new UniversalLink(string.Empty);
			}
		}

		public static NotificationRequest LaunchNotification
		{
			get
			{
				return new NotificationRequest();
			}
		}

		private void openURL(string data)
		{
			LaunchUrl obj = new LaunchUrl(data);
			AppController.OnOpenURL(obj);
		}

		private void continueUserActivity(string absoluteUrl)
		{
			UniversalLink obj = new UniversalLink(absoluteUrl);
			AppController.OnContinueUserActivity(obj);
		}

		private void applicationDidEnterBackground()
		{
			AppController.OnApplicationDidEnterBackground();
		}

		private void applicationDidBecomeActive()
		{
			AppController.OnApplicationDidBecomeActive();
		}

		private void applicationDidReceiveMemoryWarning()
		{
			AppController.OnApplicationDidReceiveMemoryWarning();
		}

		private void applicationWillResignActive()
		{
			AppController.OnApplicationWillResignActive();
		}

		private void applicationWillTerminate()
		{
			AppController.OnApplicationWillTerminate();
		}

		protected override void OnApplicationQuit()
		{
			base.OnApplicationQuit();
			AppController.OnApplicationWillTerminate();
		}

		// Note: this type is marked as 'beforefieldinit'.
		static AppController()
		{
			AppController.OnApplicationDidEnterBackground = delegate()
			{
			};
			AppController.OnApplicationDidBecomeActive = delegate()
			{
			};
			AppController.OnApplicationDidReceiveMemoryWarning = delegate()
			{
			};
			AppController.OnApplicationWillResignActive = delegate()
			{
			};
			AppController.OnApplicationWillTerminate = delegate()
			{
			};
			AppController.OnOpenURL = delegate(LaunchUrl A_0)
			{
			};
			AppController.OnContinueUserActivity = delegate(UniversalLink A_0)
			{
			};
		}
	}
}
