using System;
using System.Diagnostics;
using SA.Common.Pattern;
using UnityEngine;

public class SA_EditorAd : Singleton<SA_EditorAd>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<bool> OnInterstitialFinished;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<bool> OnInterstitialLoadComplete;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action OnInterstitialLeftApplication;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<bool> OnVideoFinished;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<bool> OnVideoLoadComplete;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action OnVideoLeftApplication;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void SetFillRate(int fillRate)
	{
		this._FillRate = fillRate;
	}

	public void LoadInterstitial()
	{
		if (!this._IsInterstitialLoading && !this.IsInterstitialReady)
		{
			this._IsInterstitialLoading = true;
			float time = UnityEngine.Random.Range(1f, 3f);
			base.Invoke("OnInterstitialRequestComplete", time);
		}
	}

	public void ShowInterstitial()
	{
		if (this.IsInterstitialReady)
		{
		}
	}

	public void LoadVideo()
	{
		if (!this._IsVideoLoading && !this.IsVideoReady)
		{
			this._IsVideoLoading = true;
			float time = UnityEngine.Random.Range(1f, 3f);
			base.Invoke("OnVideoRequestComplete", time);
		}
	}

	public void ShowVideo()
	{
		if (this.IsVideoReady)
		{
		}
	}

	public bool IsVideoReady
	{
		get
		{
			return this._IsVideoReady;
		}
	}

	public bool IsVideoLoading
	{
		get
		{
			return this._IsVideoLoading;
		}
	}

	public bool IsInterstitialReady
	{
		get
		{
			return this._IsInterstitialReady;
		}
	}

	public bool IsInterstitialLoading
	{
		get
		{
			return this._IsInterstitialLoading;
		}
	}

	public bool HasFill
	{
		get
		{
			int num = UnityEngine.Random.Range(1, 100);
			return num <= this._FillRate;
		}
	}

	public int FillRate
	{
		get
		{
			return this._FillRate;
		}
	}

	private SA_Ad_EditorUIController EditorUI
	{
		get
		{
			return this._EditorUI;
		}
	}

	private void OnVideoRequestComplete()
	{
		this._IsVideoLoading = false;
		this._IsVideoReady = this.HasFill;
		SA_EditorAd.OnVideoLoadComplete(this._IsVideoReady);
	}

	private void OnInterstitialRequestComplete()
	{
		this._IsInterstitialLoading = false;
		this._IsInterstitialReady = this.HasFill;
		SA_EditorAd.OnInterstitialLoadComplete(this._IsInterstitialReady);
	}

	private void OnInterstitialFinished_UIEvent(bool IsRewarded)
	{
		this._IsInterstitialReady = false;
		SA_EditorAd.OnInterstitialFinished(IsRewarded);
	}

	private void OnVideoFinished_UIEvent(bool IsRewarded)
	{
		this._IsVideoReady = false;
		SA_EditorAd.OnVideoFinished(IsRewarded);
	}

	private void OnInterstitialLeftApplication_UIEvent()
	{
		SA_EditorAd.OnInterstitialLeftApplication();
	}

	private void OnVideoLeftApplication_UIEvent()
	{
		SA_EditorAd.OnVideoLeftApplication();
	}

	// Note: this type is marked as 'beforefieldinit'.
	static SA_EditorAd()
	{
		SA_EditorAd.OnInterstitialFinished = delegate(bool A_0)
		{
		};
		SA_EditorAd.OnInterstitialLoadComplete = delegate(bool A_0)
		{
		};
		SA_EditorAd.OnInterstitialLeftApplication = delegate()
		{
		};
		SA_EditorAd.OnVideoFinished = delegate(bool A_0)
		{
		};
		SA_EditorAd.OnVideoLoadComplete = delegate(bool A_0)
		{
		};
		SA_EditorAd.OnVideoLeftApplication = delegate()
		{
		};
	}

	public const float MIN_LOAD_TIME = 1f;

	public const float MAX_LOAD_TIME = 3f;

	private bool _IsInterstitialLoading;

	private bool _IsVideoLoading;

	private bool _IsInterstitialReady;

	private bool _IsVideoReady;

	private int _FillRate = 100;

	private SA_Ad_EditorUIController _EditorUI;
}
