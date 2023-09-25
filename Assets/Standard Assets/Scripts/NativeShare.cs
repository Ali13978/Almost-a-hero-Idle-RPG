using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NativeShare
{
	public NativeShare()
	{
		this.subject = string.Empty;
		this.text = string.Empty;
		this.title = string.Empty;
		this.targetPackage = string.Empty;
		this.targetClass = string.Empty;
		this.files = new List<string>(0);
		this.mimes = new List<string>(0);
	}

	private static AndroidJavaClass AJC
	{
		get
		{
			if (NativeShare.m_ajc == null)
			{
				NativeShare.m_ajc = new AndroidJavaClass("com.yasirkula.unity.NativeShare");
			}
			return NativeShare.m_ajc;
		}
	}

	private static AndroidJavaObject Context
	{
		get
		{
			if (NativeShare.m_context == null)
			{
				using (AndroidJavaObject androidJavaObject = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					NativeShare.m_context = androidJavaObject.GetStatic<AndroidJavaObject>("currentActivity");
				}
			}
			return NativeShare.m_context;
		}
	}

	public NativeShare SetSubject(string subject)
	{
		if (subject != null)
		{
			this.subject = subject;
		}
		return this;
	}

	public NativeShare SetText(string text)
	{
		if (text != null)
		{
			this.text = text;
		}
		return this;
	}

	public NativeShare SetTitle(string title)
	{
		if (title != null)
		{
			this.title = title;
		}
		return this;
	}

	public NativeShare SetTarget(string androidPackageName, string androidClassName = null)
	{
		if (!string.IsNullOrEmpty(androidPackageName))
		{
			this.targetPackage = androidPackageName;
			if (androidClassName != null)
			{
				this.targetClass = androidClassName;
			}
		}
		return this;
	}

	public NativeShare AddFile(string filePath, string mime = null)
	{
		if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
		{
			this.files.Add(filePath);
			this.mimes.Add(mime ?? string.Empty);
		}
		else
		{
			UnityEngine.Debug.LogError("File does not exist at path or permission denied: " + filePath);
		}
		return this;
	}

	public void Share()
	{
		if (this.files.Count == 0 && this.subject.Length == 0 && this.text.Length == 0)
		{
			UnityEngine.Debug.LogWarning("Share Error: attempting to share nothing!");
			return;
		}
		NativeShare.AJC.CallStatic("Share", new object[]
		{
			NativeShare.Context,
			this.targetPackage,
			this.targetClass,
			this.files.ToArray(),
			this.mimes.ToArray(),
			this.subject,
			this.text,
			this.title
		});
	}

	public static bool TargetExists(string androidPackageName, string androidClassName = null)
	{
		if (string.IsNullOrEmpty(androidPackageName))
		{
			return false;
		}
		if (androidClassName == null)
		{
			androidClassName = string.Empty;
		}
		return NativeShare.AJC.CallStatic<bool>("TargetExists", new object[]
		{
			NativeShare.Context,
			androidPackageName,
			androidClassName
		});
	}

	public static bool FindTarget(out string androidPackageName, out string androidClassName, string packageNameRegex, string classNameRegex = null)
	{
		androidPackageName = null;
		androidClassName = null;
		if (string.IsNullOrEmpty(packageNameRegex))
		{
			return false;
		}
		if (classNameRegex == null)
		{
			classNameRegex = string.Empty;
		}
		string text = NativeShare.AJC.CallStatic<string>("FindMatchingTarget", new object[]
		{
			NativeShare.Context,
			packageNameRegex,
			classNameRegex
		});
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		int num = text.IndexOf('>');
		if (num <= 0 || num >= text.Length - 1)
		{
			return false;
		}
		androidPackageName = text.Substring(0, num);
		androidClassName = text.Substring(num + 1);
		return true;
	}

	private static AndroidJavaClass m_ajc;

	private static AndroidJavaObject m_context;

	private string subject;

	private string text;

	private string title;

	private string targetPackage;

	private string targetClass;

	private List<string> files;

	private List<string> mimes;
}
