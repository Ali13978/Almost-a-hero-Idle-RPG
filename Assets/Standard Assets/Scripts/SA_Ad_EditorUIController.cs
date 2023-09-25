using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class SA_Ad_EditorUIController : MonoBehaviour
{
	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<bool> OnCloseVideo;



	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnVideoLeftApplication;



	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<bool> OnCloseInterstitial;



	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnInterstitialLeftApplication;



	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		SA_EditorTesting.CheckForEventSystem();
		Canvas component = base.GetComponent<Canvas>();
		component.sortingOrder = 10001;
	}

	private void Start()
	{
	}

	public void InterstitialClick()
	{
		this.OnInterstitialLeftApplication();
	}

	public void VideoClick()
	{
		this.OnVideoLeftApplication();
	}

	public void ShowInterstitialAd()
	{
		base.gameObject.SetActive(true);
		this.InterstitialPanel.SetActive(true);
	}

	public void ShowVideoAd()
	{
		base.gameObject.SetActive(true);
		this.VideoPanel.SetActive(true);
	}

	public void CloseInterstitial()
	{
		base.gameObject.SetActive(false);
		this.InterstitialPanel.SetActive(false);
		this.OnCloseInterstitial(true);
	}

	public void CloseVideo()
	{
		base.gameObject.SetActive(false);
		this.VideoPanel.SetActive(false);
		this.OnCloseVideo(true);
	}

	public GameObject VideoPanel;

	public GameObject InterstitialPanel;

	public Image[] AppIcons;

	public Text[] AppNames;
}
