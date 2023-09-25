using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class IOSSocialManager : Singleton<IOSSocialManager>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action OnFacebookPostStart;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action OnTwitterPostStart;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action OnInstagramPostStart;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnFacebookPostResult;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnTwitterPostResult;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnInstagramPostResult;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnMailResult;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void ShareMedia(string text, Texture2D texture = null)
	{
	}

	public void TwitterPost(string text, string url = null, Texture2D texture = null)
	{
		IOSSocialManager.OnTwitterPostStart();
	}

	public void TwitterPostGif(string text, string url)
	{
	}

	public void FacebookPost(string text, string url = null, Texture2D texture = null)
	{
		IOSSocialManager.OnFacebookPostStart();
	}

	public void InstagramPost(Texture2D texture)
	{
		this.InstagramPost(texture, string.Empty);
	}

	public void InstagramPost(Texture2D texture, string message)
	{
		IOSSocialManager.OnInstagramPostStart();
	}

	public void WhatsAppShareText(string message)
	{
	}

	public void WhatsAppShareImage(Texture2D texture)
	{
	}

	public void SendMail(string subject, string body, string recipients)
	{
		this.SendMail(subject, body, recipients, null);
	}

	public void SendMail(string subject, string body, string recipients, Texture2D texture)
	{
		if (texture == null)
		{
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<TextMessageComposeResult> OnTextMessageResult;

	public void SendTextMessage(string body, string recepient, Action<TextMessageComposeResult> callback)
	{
		this.SendTextMessage(body, new List<string>
		{
			recepient
		}, callback);
	}

	public void SendTextMessage(string body, List<string> recepients, Action<TextMessageComposeResult> callback)
	{
		IOSSocialManager.OnTextMessageResult += callback;
	}

	private void OnTextMessageComposeResult(string data)
	{
		int obj = Convert.ToInt32(data);
		IOSSocialManager.OnTextMessageResult((TextMessageComposeResult)obj);
		IOSSocialManager.OnTextMessageResult = delegate(TextMessageComposeResult A_0)
		{
		};
	}

	private void OnTwitterPostFailed()
	{
		Result obj = new Result(new Error());
		IOSSocialManager.OnTwitterPostResult(obj);
	}

	private void OnTwitterPostSuccess()
	{
		Result obj = new Result();
		IOSSocialManager.OnTwitterPostResult(obj);
	}

	private void OnFacebookPostFailed()
	{
		Result obj = new Result(new Error());
		IOSSocialManager.OnFacebookPostResult(obj);
	}

	private void OnFacebookPostSuccess()
	{
		Result obj = new Result();
		IOSSocialManager.OnFacebookPostResult(obj);
	}

	private void OnMailFailed()
	{
		Result obj = new Result(new Error());
		IOSSocialManager.OnMailResult(obj);
	}

	private void OnMailSuccess()
	{
		Result obj = new Result();
		IOSSocialManager.OnMailResult(obj);
	}

	private void OnInstaPostSuccess()
	{
		Result obj = new Result();
		IOSSocialManager.OnInstagramPostResult(obj);
	}

	private void OnInstaPostFailed(string data)
	{
		int code = Convert.ToInt32(data);
		Error error = new Error(code, "Posting Failed");
		Result obj = new Result(error);
		IOSSocialManager.OnInstagramPostResult(obj);
	}

	// Note: this type is marked as 'beforefieldinit'.
	static IOSSocialManager()
	{
		IOSSocialManager.OnFacebookPostStart = delegate()
		{
		};
		IOSSocialManager.OnTwitterPostStart = delegate()
		{
		};
		IOSSocialManager.OnInstagramPostStart = delegate()
		{
		};
		IOSSocialManager.OnFacebookPostResult = delegate(Result A_0)
		{
		};
		IOSSocialManager.OnTwitterPostResult = delegate(Result A_0)
		{
		};
		IOSSocialManager.OnInstagramPostResult = delegate(Result A_0)
		{
		};
		IOSSocialManager.OnMailResult = delegate(Result A_0)
		{
		};
		IOSSocialManager.OnTextMessageResult = delegate(TextMessageComposeResult A_0)
		{
		};
	}
}
