using System;
using System.Collections;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class IOSSocialUseExample : MonoBehaviour
{
	private void Awake()
	{
		IOSSocialManager.OnFacebookPostResult += this.HandleOnFacebookPostResult;
		IOSSocialManager.OnTwitterPostResult += this.HandleOnTwitterPostResult;
		IOSSocialManager.OnInstagramPostResult += this.HandleOnInstagramPostResult;
		IOSSocialManager.OnMailResult += this.OnMailResult;
		this.InitStyles();
	}

	private void InitStyles()
	{
		this.style = new GUIStyle();
		this.style.normal.textColor = Color.white;
		this.style.fontSize = 16;
		this.style.fontStyle = FontStyle.BoldAndItalic;
		this.style.alignment = TextAnchor.UpperLeft;
		this.style.wordWrap = true;
		this.style2 = new GUIStyle();
		this.style2.normal.textColor = Color.white;
		this.style2.fontSize = 12;
		this.style2.fontStyle = FontStyle.Italic;
		this.style2.alignment = TextAnchor.UpperLeft;
		this.style2.wordWrap = true;
	}

	private void OnGUI()
	{
		float num = 20f;
		float num2 = 10f;
		GUI.Label(new Rect(num2, num, (float)Screen.width, 40f), "Twitter", this.style);
		num += 40f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Post"))
		{
			Singleton<IOSSocialManager>.Instance.TwitterPost("Twitter posting test", null, null);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Post Screenshot"))
		{
			base.StartCoroutine(this.PostTwitterScreenshot());
		}
		num += 80f;
		num2 = 10f;
		GUI.Label(new Rect(num2, num, (float)Screen.width, 40f), "Facebook", this.style);
		num += 40f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Post"))
		{
			Singleton<IOSSocialManager>.Instance.FacebookPost("Facebook posting test", null, null);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Post Screenshot"))
		{
			base.StartCoroutine(this.PostFBScreenshot());
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Post Image"))
		{
			Singleton<IOSSocialManager>.Instance.FacebookPost("Hello world", "https://www.assetstore.unity3d.com/en/#!/publisher/2256", this.textureForPost);
		}
		num += 80f;
		num2 = 10f;
		GUI.Label(new Rect(num2, num, (float)Screen.width, 40f), "Native Sharing", this.style);
		num += 40f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Text"))
		{
			Singleton<IOSSocialManager>.Instance.ShareMedia("Some text to share", null);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Screenshot"))
		{
			base.StartCoroutine(this.PostScreenshot());
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Send Mail"))
		{
			Singleton<IOSSocialManager>.Instance.SendMail("Mail Subject", "Mail Body  <strong> text html </strong> ", "mail1@gmail.com, mail2@gmail.com", this.textureForPost);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Send Txt Message"))
		{
			Singleton<IOSSocialManager>.Instance.SendTextMessage("Hello Google", "+18773555787", delegate(TextMessageComposeResult result)
			{
				UnityEngine.Debug.Log("Message send result: " + result);
			});
		}
		num += 80f;
		num2 = 10f;
		GUI.Label(new Rect(num2, num, (float)Screen.width, 40f), "Instagram", this.style);
		num += 40f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Post image from camera"))
		{
			IOSCamera.OnImagePicked += this.OnPostImageInstagram;
			Singleton<IOSCamera>.Instance.PickImage(ISN_ImageSource.Camera);
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Post Screenshot"))
		{
			base.StartCoroutine(this.PostScreenshotInstagram());
		}
		num += 80f;
		num2 = 10f;
		GUI.Label(new Rect(num2, num, (float)Screen.width, 40f), "WhatsApp", this.style);
		num += 40f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Share Text"))
		{
			Singleton<IOSSocialManager>.Instance.WhatsAppShareText("Some text");
		}
		num2 += 170f;
		if (GUI.Button(new Rect(num2, num, 150f, 50f), "Share Image"))
		{
			Singleton<IOSSocialManager>.Instance.WhatsAppShareImage(this.textureForPost);
		}
	}

	private void OnPostImageInstagram(IOSImagePickResult result)
	{
		if (result.IsSucceeded)
		{
			UnityEngine.Object.Destroy(this.drawTexture);
			this.drawTexture = result.Image;
		}
		else
		{
			IOSMessage.Create("ERROR", "Image Load Failed");
		}
		Singleton<IOSSocialManager>.Instance.InstagramPost(this.drawTexture, "Some text to share");
		IOSCamera.OnImagePicked -= this.OnPostImageInstagram;
	}

	private IEnumerator PostScreenshotInstagram()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		tex.Apply();
		Singleton<IOSSocialManager>.Instance.InstagramPost(tex, "Some text to share");
		UnityEngine.Object.Destroy(tex);
		yield break;
	}

	private IEnumerator PostScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		tex.Apply();
		Singleton<IOSSocialManager>.Instance.ShareMedia("Some text to share", tex);
		UnityEngine.Object.Destroy(tex);
		yield break;
	}

	private IEnumerator PostTwitterScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		tex.Apply();
		Singleton<IOSSocialManager>.Instance.TwitterPost("My app Screenshot", null, tex);
		UnityEngine.Object.Destroy(tex);
		yield break;
	}

	private IEnumerator PostFBScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		tex.Apply();
		Singleton<IOSSocialManager>.Instance.FacebookPost("My app Screenshot", null, tex);
		UnityEngine.Object.Destroy(tex);
		yield break;
	}

	private void HandleOnInstagramPostResult(Result res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Posting example", "Post Success!");
		}
		else
		{
			IOSNativePopUpManager.showMessage("Posting example", "Post Failed :(");
		}
	}

	private void HandleOnTwitterPostResult(Result res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Posting example", "Post Success!");
		}
		else
		{
			IOSNativePopUpManager.showMessage("Posting example", "Post Failed :(");
		}
	}

	private void HandleOnFacebookPostResult(Result res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Posting example", "Post Success!");
		}
		else
		{
			IOSNativePopUpManager.showMessage("Posting example", "Post Failed :(");
		}
	}

	private void OnMailResult(Result result)
	{
		if (result.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Posting example", "Mail Sent");
		}
		else
		{
			IOSNativePopUpManager.showMessage("Posting example", "Mail Failed :(");
		}
	}

	private GUIStyle style;

	private GUIStyle style2;

	public Texture2D drawTexture;

	public Texture2D textureForPost;
}
