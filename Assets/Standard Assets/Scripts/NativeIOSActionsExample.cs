using System;
using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.Contacts;
using SA.IOSNative.Gestures;
using SA.IOSNative.Privacy;
using SA.IOSNative.System;
using SA.IOSNative.UIKit;
using UnityEngine;

public class NativeIOSActionsExample : BaseIOSFeaturePreview
{
	private void Awake()
	{
		this.time = new DateTime(1997, 5, 11);
		UnityEngine.Debug.Log("Subscribed");
		Singleton<ISN_GestureRecognizer>.Instance.OnSwipe += delegate(ISN_SwipeDirection direction)
		{
			UnityEngine.Debug.Log("Swipe: " + direction);
		};
		Singleton<ForceTouch>.Instance.Setup(0.5f, 1f, 2.5f);
		Singleton<ForceTouch>.Instance.OnForceTouchStarted += delegate()
		{
			UnityEngine.Debug.Log("OnForceTouchStarted");
		};
		Singleton<ForceTouch>.Instance.OnForceChanged += delegate(ForceInfo info)
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"OnForceChanged: ",
				info.Force,
				" / ",
				info.MaxForce
			}));
		};
		Singleton<ForceTouch>.Instance.OnForceTouchFinished += delegate()
		{
			UnityEngine.Debug.Log("OnForceTouchFinished");
		};
		UnityEngine.Debug.Log(ForceTouch.AppOpenshortcutItem);
		Singleton<ForceTouch>.Instance.OnAppShortcutClick += delegate(string action)
		{
			UnityEngine.Debug.Log("Menu Item With action: " + action + " was clicked");
		};
	}

	private void OnGUI()
	{
		base.UpdateToStartPos();
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "System Utils", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Check if FB App exists"))
		{
			bool flag = SharedApplication.CheckUrl("fb://");
			if (flag)
			{
				IOSMessage.Create("Success", "Facebook App is installed on current device");
			}
			else
			{
				IOSMessage.Create("ERROR", "Facebook App is not installed on current device");
			}
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Open FB Profile"))
		{
			SharedApplication.OpenUrl("fb://profile");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Open App Store"))
		{
			SharedApplication.OpenUrl("itms-apps://");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Get IFA"))
		{
			IOSMessage.Create("Identifier Loaded", ISN_Device.CurrentDevice.AdvertisingIdentifier);
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Set App Bages Count"))
		{
			IOSNativeUtility.SetApplicationBagesNumber(10);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Clear Application Bages"))
		{
			IOSNativeUtility.SetApplicationBagesNumber(0);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Show Device Info"))
		{
			this.ShowDevoceInfo();
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Pick Contacts UI"))
		{
			Singleton<ContactStore>.Instance.ShowContactsPickerUI(delegate(ContactsResult result)
			{
				if (result.IsSucceeded)
				{
					UnityEngine.Debug.Log("Picked " + result.Contacts.Count + " contacts");
					IOSMessage.Create("Success", "Picked " + result.Contacts.Count + " contacts");
					foreach (Contact contact in result.Contacts)
					{
						UnityEngine.Debug.Log("contact.GivenName: " + contact.GivenName);
						if (contact.PhoneNumbers.Count > 0)
						{
							UnityEngine.Debug.Log("contact.PhoneNumber: " + contact.PhoneNumbers[0].Digits);
						}
						if (contact.Emails.Count > 0)
						{
							UnityEngine.Debug.Log("contact.Email: " + contact.Emails[0]);
						}
					}
				}
				else
				{
					IOSMessage.Create("Error", result.Error.Code + " / " + result.Error.Message);
				}
			});
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Load Contacts"))
		{
			Singleton<ContactStore>.Instance.RetrievePhoneContacts(delegate(ContactsResult result)
			{
				if (result.IsSucceeded)
				{
					UnityEngine.Debug.Log("Loaded " + result.Contacts.Count + " contacts");
					IOSMessage.Create("Success", "Loaded " + result.Contacts.Count + " contacts");
					foreach (Contact contact in result.Contacts)
					{
						if (contact.PhoneNumbers.Count > 0)
						{
							UnityEngine.Debug.Log(contact.GivenName + " / " + contact.PhoneNumbers[0].Digits);
						}
					}
				}
				else
				{
					IOSMessage.Create("Error", result.Error.Code + " / " + result.Error.Message);
				}
			});
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Date Time Picker", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Time"))
		{
			DateTimePicker.Show(DateTimePickerMode.Time, delegate(DateTime dateTime)
			{
			});
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Date"))
		{
			DateTimePicker.Show(DateTimePickerMode.Date, delegate(DateTime dateTime)
			{
			});
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Date And Time"))
		{
			DateTimePicker.Show(DateTimePickerMode.DateAndTime, delegate(DateTime dateTime)
			{
			});
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Countdown Timer"))
		{
			DateTimePicker.Show(DateTimePickerMode.CountdownTimer, delegate(DateTime dateTime)
			{
			});
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Set Date Without UI"))
		{
			DateTimePicker.Show(DateTimePickerMode.Date, this.time, delegate(DateTime dateTime)
			{
			});
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Show Calendar Picker"))
		{
			Calendar.PickDate(delegate(DateTime dateTime)
			{
				ISN_Logger.Log("OnDateChanged: " + dateTime.ToShortDateString(), LogType.Log);
			}, 1989);
			PermissionsManager.RequestPermission(Permission.NSPhotoLibrary, delegate(PermissionStatus permissionStatus)
			{
				UnityEngine.Debug.Log("PermissionsManager.RequestPermission: " + Permission.NSPhotoLibrary + permissionStatus.ToString());
			});
			PermissionsManager.RequestPermission(Permission.NSLocationWhenInUse, delegate(PermissionStatus permissionStatus)
			{
				UnityEngine.Debug.Log("PermissionsManager.RequestPermission: " + Permission.NSLocationWhenInUse + permissionStatus.ToString());
			});
			PermissionsManager.RequestPermission(Permission.NSMicrophone, delegate(PermissionStatus permissionStatus)
			{
				UnityEngine.Debug.Log("PermissionsManager.RequestPermission: " + Permission.NSMicrophone + permissionStatus.ToString());
			});
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Video", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Player Streamed video"))
		{
			Singleton<IOSVideoManager>.Instance.PlayStreamingVideo("https://dl.dropboxusercontent.com/u/83133800/Important/Doosan/GT2100-Video.mov");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Open YouTube Video"))
		{
			Singleton<IOSVideoManager>.Instance.OpenYouTubeVideo("xzCEdSKMkdU");
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Camera Roll", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)(this.buttonWidth + 10), (float)this.buttonHeight), "Save Screenshot To Camera Roll"))
		{
			IOSCamera.OnImageSaved += this.OnImageSaved;
			Singleton<IOSCamera>.Instance.SaveScreenshotToCameraRoll();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Save Texture To Camera Roll"))
		{
			IOSCamera.OnImageSaved += this.OnImageSaved;
			Singleton<IOSCamera>.Instance.SaveTextureToCameraRoll(this.hello_texture);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Get Image From Camera"))
		{
			IOSCamera.OnImagePicked += this.OnImage;
			Singleton<IOSCamera>.Instance.PickImage(ISN_ImageSource.Camera);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Get Image From Album"))
		{
			IOSCamera.OnImagePicked += this.OnImage;
			Singleton<IOSCamera>.Instance.PickImage(ISN_ImageSource.Album);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Load Multiple Images"))
		{
			ISN_FilePicker.MediaPickFinished += delegate(ISN_FilePickerResult res)
			{
				UnityEngine.Debug.Log("Picked " + res.PickedImages.Count + " images");
				if (res.PickedImages.Count == 0)
				{
					return;
				}
				UnityEngine.Object.Destroy(this.drawTexture);
				this.drawTexture = res.PickedImages[0];
			};
			Singleton<ISN_FilePicker>.Instance.PickFromCameraRoll(0);
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "PickedImage", this.style);
		this.StartY += this.YLableStep;
		if (this.drawTexture != null)
		{
			GUI.DrawTexture(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonWidth), this.drawTexture);
		}
	}

	private void ShowDevoceInfo()
	{
		ISN_Device currentDevice = ISN_Device.CurrentDevice;
		IOSMessage.Create("Device Info", string.Concat(new object[]
		{
			"Name: ",
			currentDevice.Name,
			"\nSystem Name: ",
			currentDevice.SystemName,
			"\nModel: ",
			currentDevice.Model,
			"\nLocalized Model: ",
			currentDevice.LocalizedModel,
			"\nSystem Version: ",
			currentDevice.SystemVersion,
			"\nMajor System Version: ",
			currentDevice.MajorSystemVersion,
			"\nPreferred Language Code: ",
			currentDevice.PreferredLanguageCode,
			"\nPreferred Language_ISO639_1: ",
			currentDevice.PreferredLanguage_ISO639_1,
			"\nUser Interface Idiom: ",
			currentDevice.InterfaceIdiom,
			"\nGUID in Base64: ",
			currentDevice.GUID.Base64String
		}));
		UnityEngine.Debug.Log("ISN_TimeZone.LocalTimeZone.Name: " + ISN_TimeZone.LocalTimeZone.Name);
		UnityEngine.Debug.Log("ISN_TimeZone.LocalTimeZone.SecondsFromGMT: " + ISN_TimeZone.LocalTimeZone.SecondsFromGMT);
		UnityEngine.Debug.Log("ISN_TimeZone.LocalTimeZone.Name: " + ISN_Build.Current.Version);
		UnityEngine.Debug.Log("ISN_TimeZone.LocalTimeZone.Name: " + ISN_Build.Current.Number);
	}

	private void OnDateChanged(DateTime time)
	{
		ISN_Logger.Log("OnDateChanged: " + time.ToString(), LogType.Log);
	}

	private void OnPickerClosed(DateTime time)
	{
		ISN_Logger.Log("OnPickerClosed: " + time.ToString(), LogType.Log);
	}

	private void OnImage(IOSImagePickResult result)
	{
		if (result.IsSucceeded)
		{
			UnityEngine.Object.Destroy(this.drawTexture);
			this.drawTexture = result.Image;
			IOSMessage.Create("Success", string.Concat(new object[]
			{
				"Image Successfully Loaded, Image size: ",
				result.Image.width,
				"x",
				result.Image.height
			}));
		}
		else
		{
			IOSMessage.Create("ERROR", "Image Load Failed");
		}
		IOSCamera.OnImagePicked -= this.OnImage;
	}

	private void OnImageSaved(Result result)
	{
		IOSCamera.OnImageSaved -= this.OnImageSaved;
		if (result.IsSucceeded)
		{
			IOSMessage.Create("Success", "Image Successfully saved to Camera Roll");
		}
		else
		{
			IOSMessage.Create("ERROR", "Image Save Failed");
		}
	}

	public Texture2D hello_texture;

	public Texture2D drawTexture;

	private DateTime time;
}
