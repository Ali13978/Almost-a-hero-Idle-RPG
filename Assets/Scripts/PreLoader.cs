using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using PlayFab;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreLoader : MonoBehaviour, IAndroidPopupCalbackReceiver
{
	private void Awake()
	{
        //PlayerPrefs.DeleteAll();

        Cheats.version = Application.version;
		Cheats.versionObject = new Version(Cheats.version);
		PlayFabSettings.SetSharedSettings(this.playfabSharedSettings);
	}

	private void Start()
	{
		this.loadingBarCan.alpha = 0f;
		this.loadingBarCan.transform.localScale = Vector3.one * 1.2f;
		LM.Initialize(this.parsedLoc);
		int @int = PlayerPrefs.GetInt("CurrentLang", 0);
		if (@int != 0)
		{
			LM.SelectLanguage(SaveLoadManager.ConvertLanguage(@int));
		}
		this.maxStageReached = PlayerPrefs.GetInt("MaxStageReached", 0);
		this.hints = LoadingHints.GetHints(this.maxStageReached);
		string @string = PlayerPrefs.GetString("HintsShown", string.Empty);
		if (@string != string.Empty)
		{
			string[] array = @string.Split(new char[]
			{
				'|'
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string hint = array2[i];
				if (!string.IsNullOrEmpty(hint))
				{
					this.hints.RemoveAll((LoadingHints.Hint h) => h.locKey == hint);
				}
			}
		}
		this.SetHintText();
		this.loadingHintText.SetAlpha(0f);
		this.loadingHintText.rectTransform.SetAnchorPosX(-150f);
		this.loadingHintText.rectTransform.DOAnchorPosX(0f, 0.2f, false);
		this.loadingHintText.DOFade(1f, 0.2f);
		this.hintTimer = 6f;
		this.playfabId.gameObject.SetActive(false);
		//this.m_obbDownloader = GooglePlayObbDownloadManager.GetGooglePlayObbDownloader();
		//this.m_obbDownloader.PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqbqfnzFzT/J+d7kYMRg0iQbjjNlUaVhjrCrorLUq9a+d5vrm6J7ST5jK4cnzDAhmsLiiEatQlP3fU+wF1+h1xyxLm6yLPlhdhKro542NVpkatpEckjmg/uXb+jpjSRLBzhbJy4JNPKGOvuZpGErfoizIUAG6XNP2608YO5IcqfRq67LZvFebntH9pw0KRFi/dO7QBgNvs6l+6/yzWYNSnC6lYg/rF0kiUFILpRZCq3cvLMthkLQ3YXMGg3yJZFHtnICrxeMLcfAGSir+3iXvtXUmsjEYMTHmoVOrDa2U8o9hLHMmMerV6/0Ip1uo23BXrk6fmLQ9Vh3RdA6DEMimrQIDAQAB";
		//this.m_obbDownloader.SetCallbackGameObject(base.gameObject);
		//if (this.m_obbDownloader.GetExpansionFilePath() == null)
		//{
		//	UnityEngine.Debug.Log("External storage is not available!");
		//}
		//else
		//{
		//	string mainOBBPath = this.m_obbDownloader.GetMainOBBPath();
		//	UnityEngine.Debug.Log("Main = ..." + ((mainOBBPath != null) ? mainOBBPath : "Main Obb not found, Downloading from store"));
		//	if (mainOBBPath == null)
		//	{
		//		this.isFetchingObb = true;
		//		if (this.m_obbDownloader.needObbPermission())
		//		{
		//			UnityEngine.Debug.Log("Write permission needed, asking...");
		//			this.isWaitingForPermission = true;
		//			this.m_obbDownloader.fireYesNoPopup(this, string.Empty, LM.Get("UI_PRE_WRITE_PERMISSION"), LM.Get("UI_OKAY"), LM.Get("UI_NO"), delegate
		//			{
		//				this.m_obbDownloader.AskForWritePermission();
		//				this.onAllow = (Action)Delegate.Combine(this.onAllow, new Action(delegate()
		//				{
		//					this.isWaitingForPermission = false;
		//					UnityEngine.Debug.Log("Checking for network connection");
		//					if (!this.m_obbDownloader.HasNetworkConnection())
		//					{
		//						UnityEngine.Debug.Log("Do not have network connection warning user");
		//						this.m_obbDownloader.fireNutralPopup(this, string.Empty, LM.Get("UI_NO_INTERNET_WARNING"), LM.Get("UI_OKAY"), delegate
		//						{
		//							Application.Quit();
		//						});
		//						this.isWaitingForNetwork = true;
		//					}
		//					else
		//					{
		//						this.m_obbDownloader.FetchOBB();
		//					}
		//				}));
		//				this.onDeny = (Action)Delegate.Combine(this.onDeny, new Action(delegate()
		//				{
		//					UnityEngine.Debug.Log("User not granted permission");
		//					this.m_obbDownloader.fireNutralPopup(this, string.Empty, LM.Get("UI_NO_WRITE_PERMISSION_GRANTED"), LM.Get("UI_OKAY"), delegate
		//					{
		//						Application.Quit();
		//					});
		//				}));
		//			}, delegate
		//			{
		//				UnityEngine.Debug.Log("User not granted permission");
		//				this.m_obbDownloader.fireNutralPopup(this, string.Empty, LM.Get("UI_NO_WRITE_PERMISSION_GRANTED"), LM.Get("UI_OKAY"), delegate
		//				{
		//					Application.Quit();
		//				});
		//			});
		//		}
		//		else
		//		{
		//			UnityEngine.Debug.Log("No need for permission");
		//			UnityEngine.Debug.Log("Checking for network connection");
		//			if (!this.m_obbDownloader.HasNetworkConnection())
		//			{
		//				UnityEngine.Debug.Log("Do not have network connection warning user");
		//				this.m_obbDownloader.fireNutralPopup(this, string.Empty, LM.Get("UI_NO_INTERNET_WARNING"), LM.Get("UI_OKAY"), delegate
		//				{
		//					Application.Quit();
		//				});
		//				this.isWaitingForNetwork = true;
		//			}
		//			else
		//			{
		//				this.m_obbDownloader.FetchOBB();
		//			}
		//		}
		//	}
		//	else if (this.m_obbDownloader.needObbPermission())
		//	{
		//		UnityEngine.Debug.Log("Write permission needed, asking...");
		//		this.isWaitingForPermission = true;
		//		this.m_obbDownloader.fireYesNoPopup(this, string.Empty, LM.Get("UI_PRE_WRITE_PERMISSION"), LM.Get("UI_OKAY"), LM.Get("UI_NO"), delegate
		//		{
		//			this.m_obbDownloader.AskForWritePermission();
		//			this.onAllow = (Action)Delegate.Combine(this.onAllow, new Action(delegate()
		//			{
		//				this.isWaitingForPermission = false;
		//			}));
		//			this.onDeny = (Action)Delegate.Combine(this.onDeny, new Action(delegate()
		//			{
		//				UnityEngine.Debug.Log("User not granted permission");
		//				this.m_obbDownloader.fireNutralPopup(this, "A", LM.Get("UI_NO_WRITE_PERMISSION_GRANTED"), LM.Get("UI_OKAY"), delegate
		//				{
		//					Application.Quit();
		//				});
		//			}));
		//		}, delegate
		//		{
		//			UnityEngine.Debug.Log("User not granted permission");
		//			this.m_obbDownloader.fireNutralPopup(this, "A", LM.Get("UI_NO_WRITE_PERMISSION_GRANTED"), LM.Get("UI_OKAY"), delegate
		//			{
		//				Application.Quit();
		//			});
		//		});
		//	}
		//}
	}

	private void SetHintText()
	{
		int count = this.hints.Count;
		int randomInt;
		if (count == 0)
		{
			this.hints = LoadingHints.GetHints(this.maxStageReached);
			randomInt = GameMath.GetRandomInt(0, this.hints.Count, GameMath.RandType.NoSeed);
			PlayerPrefs.SetString("HintsShown", string.Empty);
		}
		else
		{
			randomInt = GameMath.GetRandomInt(0, count, GameMath.RandType.NoSeed);
		}
		LoadingHints.Hint hint = this.hints[randomInt];
		this.loadingHintText.text = LM.Get(hint.locKey);
		this.hints.RemoveAt(randomInt);
		LoadingHints.SetUsedHint(hint.locKey);
	}

	public void ShowPlayfabId()
	{
		if (this.playfabId.gameObject.activeSelf)
		{
			return;
		}
		string @string = PlayerPrefs.GetString("CurrentPlayfabId", string.Empty);
		if (@string != string.Empty)
		{
			this.playfabId.text = "ID: " + @string;
			this.playfabId.gameObject.SetActive(true);
		}
	}

	private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		base.StartCoroutine(this.UnloadScene());
		SceneManager.SetActiveScene(arg0);
	}

	private IEnumerator UnloadScene()
	{

		yield return new WaitWhile(() => !LoadingTransition.canUnloadPreloaderScene);
  
        this.preloaderFade.DOFade(0f, 0.6f);

        yield return new WaitForSeconds(0.7f);
  
        SceneManager.UnloadSceneAsync(this.thisScene);
		yield break;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) || UnityEngine.Input.touchCount > 0)
		{
			this.ShowPlayfabId();
		}
		this.hintTimer -= Time.deltaTime;
		this.fakeLoadingDur -= Time.deltaTime;
		if (this.hintTimer <= 0f)
		{
			this.hintTimer = 6f;
			Sequence s = DOTween.Sequence();
			s.Append(this.loadingHintText.rectTransform.DOAnchorPosX(150f, 0.2f, false)).Join(this.loadingHintText.DOFade(0f, 0.2f)).AppendCallback(delegate
			{
				this.SetHintText();
				this.loadingHintText.rectTransform.SetAnchorPosX(-150f);
				DOTween.Sequence().Append(this.loadingHintText.rectTransform.DOAnchorPosX(0f, 0.2f, false)).Join(this.loadingHintText.DOFade(1f, 0.2f)).Play<Sequence>();
			}).Play<Sequence>();
		}
		if (this.isWaitingForPermission || this.isWaitingForNetwork || this.hasNotEnoughSpace)
		{
			return;
		}
		
		
		if (this.animTimer < this.animDur)
		{
			this.animTimer += Time.deltaTime;
			float num2 = EaseManager.Evaluate(Ease.OutCirc, null, this.animTimer, this.animDur, 0f, 0f);
			if (num2 >= 1f)
			{
				num2 = 1f;
			}
			this.loadingBarCan.alpha = num2;
			this.loadingBarCan.transform.localScale = Vector3.one * (1.2f - num2 * 0.2f);
		}
		else if (!this.triggered && this.fakeLoadingDur <= 0f)
		{
			this.triggered = true;
			this.loadingBarCan.alpha = 1f;
			this.loadingBarCan.transform.localScale = Vector3.one;
			SceneManager.sceneLoaded += this.SceneManager_sceneLoaded;
			this.thisScene = SceneManager.GetActiveScene();
			this.levelLoaderOperation = SceneManager.LoadSceneAsync("main", LoadSceneMode.Additive);
		}
		if (this.levelLoaderOperation != null)
		{
			float progress = this.levelLoaderOperation.progress;
			this.loadingBar.fillAmount = this.loadCurve.Evaluate(progress);
			this.loadingText.text = LM.Get("GAME_LOADING") + " " + this.loadingBar.fillAmount.ToString("P0");
		}
	}

	private void OnAllow()
	{
		if (this.onAllow != null)
		{
			this.onAllow();
		}
		this.ResetPermissionCallbacks();
	}

	private void OnDeny()
	{
		if (this.onDeny != null)
		{
			this.onDeny();
		}
		this.ResetPermissionCallbacks();
	}

	private void OnDenyAndNeverAskAgain()
	{
	}

	private void OnObbDownloadFail(string error)
	{
	
	}

	private void ResetPermissionCallbacks()
	{
		this.onAllow = null;
		this.onDeny = null;
	}

	public Action onPositive { get; set; }

	public Action onNegative { get; set; }

	public Action onOkay { get; set; }

	public void OnCallbackReceive(string message)
	{
		if (message != null)
		{
			if (!(message == "yes"))
			{
				if (!(message == "no"))
				{
					if (message == "okay")
					{
						if (this.onOkay != null)
						{
							this.onOkay();
						}
					}
				}
				else if (this.onNegative != null)
				{
					this.onNegative();
				}
			}
			else if (this.onPositive != null)
			{
				this.onPositive();
			}
		}
	}

	
	private Action onDeny;

	private Action onAllow;


	private bool isWaitingForPermission;

	private bool isWaitingForNetwork;

	private bool hasNotEnoughSpace;

	public Image loadingBar;

	public RectTransform loadingBarParent;

	public CanvasGroup loadingBarCan;

	public Text loadingText;

	public AsyncOperation levelLoaderOperation;

	public Scene thisScene;

	public AnimationCurve loadCurve;

	public Text playfabId;

	public Text loadingHintText;


	public ParsedLoc parsedLoc;

	public float fakeLoadingDur;

	public CanvasGroup preloaderFade;

	public PlayFabSharedSettings playfabSharedSettings;

	private float animTimer;

	private float animDur = 0.3f;

	private bool triggered;

	private int maxStageReached;

	private List<LoadingHints.Hint> hints;

	private float hintTimer;

	private const float DurHintDisplay = 6f;
}
