using System;
using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class SA_EditorTestingSceneController : MonoBehaviour
{
	public void LoadInterstitial()
	{
		Singleton<SA_EditorAd>.Instance.LoadInterstitial();
	}

	public void ShowInterstitial()
	{
		Singleton<SA_EditorAd>.Instance.ShowInterstitial();
	}

	public void LoadVideo()
	{
		Singleton<SA_EditorAd>.Instance.LoadVideo();
	}

	public void ShowVideo()
	{
		Singleton<SA_EditorAd>.Instance.ShowVideo();
	}

	public void Show_Notifications()
	{
		SA_EditorNotifications.ShowNotification("Test", "Test Notification Body", SA_EditorNotificationType.Message);
	}

	public void Show_A_Notifications()
	{
		SA_EditorNotifications.ShowNotification("Achievement", "Test Notification Body", SA_EditorNotificationType.Achievement);
	}

	public void Show_L_Notifications()
	{
		SA_EditorNotifications.ShowNotification("Leaderboard", "Test Notification Body", SA_EditorNotificationType.Leaderboards);
	}

	public void Show_E_Notifications()
	{
		SA_EditorNotifications.ShowNotification("Error", "Test Notification Body", SA_EditorNotificationType.Error);
	}

	public void Show_InApp_Popup()
	{
		SA_EditorInApps.ShowInAppPopup("Product Title", "Product Describtion", "2.99$", null);
	}

	private void FixedUpdate()
	{
		if (Singleton<SA_EditorAd>.Instance.IsInterstitialReady)
		{
			this.ShowInterstitial_Button.interactable = true;
		}
		else
		{
			this.ShowInterstitial_Button.interactable = false;
		}
		if (Singleton<SA_EditorAd>.Instance.IsVideoReady)
		{
			this.ShowInterstitial_Video.interactable = true;
		}
		else
		{
			this.ShowInterstitial_Video.interactable = false;
		}
	}

	public Button ShowInterstitial_Button;

	public Button ShowInterstitial_Video;
}
