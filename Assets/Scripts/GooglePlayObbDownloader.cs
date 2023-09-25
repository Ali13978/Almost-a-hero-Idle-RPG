using System;
using System.IO;
using UnityEngine;

public class GooglePlayObbDownloader : IGooglePlayObbDownloader
{
	public AndroidJavaClass unityPlayerClass
	{
		get
		{
			AndroidJavaClass result;
			if ((result = this.m_unityPlayerClass) == null)
			{
				result = (this.m_unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"));
			}
			return result;
		}
	}

	public AndroidJavaObject currentActivity
	{
		get
		{
			AndroidJavaObject result;
			if ((result = this.m_currentActivity) == null)
			{
				result = (this.m_currentActivity = this.unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity"));
			}
			return result;
		}
	}

	public string PublicKey { get; set; }

	private void ApplyPublicKey()
	{
		if (string.IsNullOrEmpty(this.PublicKey))
		{
			UnityEngine.Debug.LogError("GooglePlayObbDownloader: The public key is not set - did you forget to set it in the script?\n");
		}
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.plugin.downloader.UnityDownloaderService"))
		{
			androidJavaClass.SetStatic<string>("BASE64_PUBLIC_KEY", this.PublicKey);
			androidJavaClass.SetStatic<byte[]>("SALT", new byte[]
			{
				1,
				43,
				244,
				byte.MaxValue,
				54,
				98,
				156,
				244,
				43,
				2,
				248,
				252,
				9,
				5,
				150,
				148,
				223,
				45,
				byte.MaxValue,
				84
			});
		}
	}

	public void restartActivity()
	{
		this.currentActivity.Call("restartAcvivity", new object[0]);
	}

	public bool isObbCompleted()
	{
		return this.currentActivity.Call<bool>("isObbCompleted", new object[0]);
	}

	public float getObbProgress()
	{
		return this.currentActivity.Call<float>("getObbProgress", new object[0]);
	}

	public bool needObbPermission()
	{
		return this.currentActivity.Call<bool>("needWritePermission", new object[0]);
	}

	public string getObbProgressAmount()
	{
		long bytes = this.currentActivity.Get<long>("mOverallProgress");
		long bytes2 = this.currentActivity.Get<long>("mOverallTotal");
		return string.Format("{0:F2} MB of {1:F2} MB", this.ConvertBytesToMegabytes(bytes), this.ConvertBytesToMegabytes(bytes2));
	}

	public void fireYesNoPopup(IAndroidPopupCalbackReceiver calbackReceiver, string title, string message, string yesString, string noString, Action onPositive, Action onNegative)
	{
		calbackReceiver.onPositive = (Action)Delegate.Combine(calbackReceiver.onPositive, onPositive);
		calbackReceiver.onNegative = (Action)Delegate.Combine(calbackReceiver.onNegative, onNegative);
		this.currentActivity.Call("fireYesNoPopup", new object[]
		{
			calbackReceiver.gameObject.name,
			"OnCallbackReceive",
			title,
			message,
			yesString,
			noString
		});
	}

	public void fireNutralPopup(IAndroidPopupCalbackReceiver calbackReceiver, string title, string message, string okayString, Action onOkay)
	{
		calbackReceiver.onOkay = (Action)Delegate.Combine(calbackReceiver.onOkay, onOkay);
		this.currentActivity.Call("fireNutralPopup", new object[]
		{
			calbackReceiver.gameObject.name,
			"OnCallbackReceive",
			title,
			message,
			okayString
		});
	}

	private double ConvertBytesToMegabytes(long bytes)
	{
		return (double)((float)bytes / 1024f / 1024f);
	}

	public void FetchOBB()
	{
		this.ApplyPublicKey();
		try
		{
			this.currentActivity.Call("startObbDownload", new object[0]);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("GooglePlayObbDownloader: Exception occurred while attempting to start DownloaderActivity - is the AndroidManifest.xml incorrect?\n" + ex.Message);
		}
	}

	public string GetExpansionFilePath()
	{
		if (GooglePlayObbDownloader.EnvironmentClass.CallStatic<string>("getExternalStorageState", new object[0]) != "mounted")
		{
			this.m_ExpansionFilePath = null;
			return this.m_ExpansionFilePath;
		}
		if (string.IsNullOrEmpty(this.m_ExpansionFilePath))
		{
			using (AndroidJavaObject androidJavaObject = GooglePlayObbDownloader.EnvironmentClass.CallStatic<AndroidJavaObject>("getExternalStorageDirectory", new object[0]))
			{
				string arg = androidJavaObject.Call<string>("getPath", new object[0]);
				this.m_ExpansionFilePath = string.Format("{0}/{1}/{2}", arg, "Android/obb", GooglePlayObbDownloader.ObbPackage);
			}
		}
		return this.m_ExpansionFilePath;
	}

	public string GetMainOBBPath()
	{
		return GooglePlayObbDownloader.GetOBBPackagePath(this.GetExpansionFilePath(), "main");
	}

	public string GetPatchOBBPath()
	{
		return GooglePlayObbDownloader.GetOBBPackagePath(this.GetExpansionFilePath(), "patch");
	}

	private static string GetOBBPackagePath(string expansionFilePath, string prefix)
	{
		if (string.IsNullOrEmpty(expansionFilePath))
		{
			return null;
		}
		string text = string.Format("{0}/{1}.{2}.{3}.obb", new object[]
		{
			expansionFilePath,
			prefix,
			GooglePlayObbDownloader.ObbVersion,
			GooglePlayObbDownloader.ObbPackage
		});
		return (!File.Exists(text)) ? null : text;
	}

	private static string ObbPackage
	{
		get
		{
			if (GooglePlayObbDownloader.m_ObbPackage == null)
			{
				GooglePlayObbDownloader.PopulateOBBProperties();
			}
			return GooglePlayObbDownloader.m_ObbPackage;
		}
	}

	private static int ObbVersion
	{
		get
		{
			if (GooglePlayObbDownloader.m_ObbVersion == 0)
			{
				GooglePlayObbDownloader.PopulateOBBProperties();
			}
			return GooglePlayObbDownloader.m_ObbVersion;
		}
	}

	private static void PopulateOBBProperties()
	{
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			GooglePlayObbDownloader.m_ObbPackage = @static.Call<string>("getPackageName", new object[0]);
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getPackageManager", new object[0]).Call<AndroidJavaObject>("getPackageInfo", new object[]
			{
				GooglePlayObbDownloader.m_ObbPackage,
				0
			});
			GooglePlayObbDownloader.m_ObbVersion = androidJavaObject.Get<int>("versionCode");
		}
	}

	public void SetCallbackGameObject(GameObject callbackGameObject)
	{
		this.currentActivity.Call("setCallbackGameObject", new object[]
		{
			callbackGameObject.name
		});
	}

	public void AskForWritePermission()
	{
		this.currentActivity.Call("requestPermission", new object[]
		{
			GooglePlayObbDownloader.GetPermissionStr(GooglePlayObbDownloader.AndroidPermission.WRITE_EXTERNAL_STORAGE)
		});
	}

	public bool HasNetworkConnection()
	{
		return this.currentActivity.Call<bool>("hasNetworkConnection", new object[0]);
	}

	private static string GetPermissionStr(GooglePlayObbDownloader.AndroidPermission permission)
	{
		return "android.permission." + permission.ToString();
	}

	private static AndroidJavaClass EnvironmentClass = new AndroidJavaClass("android.os.Environment");

	private AndroidJavaClass m_unityPlayerClass;

	private AndroidJavaObject m_currentActivity;

	private const string Environment_MediaMounted = "mounted";

	public const string positive = "yes";

	public const string negative = "no";

	public const string okay = "okay";

	private string m_ExpansionFilePath;

	private static string m_ObbPackage;

	private static int m_ObbVersion;

	public enum AndroidPermission
	{
		ACCESS_COARSE_LOCATION,
		ACCESS_FINE_LOCATION,
		ADD_VOICEMAIL,
		BODY_SENSORS,
		CALL_PHONE,
		CAMERA,
		GET_ACCOUNTS,
		PROCESS_OUTGOING_CALLS,
		READ_CALENDAR,
		READ_CALL_LOG,
		READ_CONTACTS,
		READ_EXTERNAL_STORAGE,
		READ_PHONE_STATE,
		READ_SMS,
		RECEIVE_MMS,
		RECEIVE_SMS,
		RECEIVE_WAP_PUSH,
		RECORD_AUDIO,
		SEND_SMS,
		USE_SIP,
		WRITE_CALENDAR,
		WRITE_CALL_LOG,
		WRITE_CONTACTS,
		WRITE_EXTERNAL_STORAGE
	}
}
