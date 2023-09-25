using System;
using System.Collections;
using System.Collections.Generic;
using DConsole;
using DG.Tweening;
using DynamicLoading;
using Newtonsoft.Json;
using Render;
using SaveLoad;
using Simulation;
using Simulation.ArtifactSystem;
using SocialRewards;
using Spine;
using Static;
using Ui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
	private void FacebookInitAndActivate()
	{
	
	}

	public static bool IsAnyTitleNewsReadyToShow()
	{
		if (!TrustedTime.IsReady() || (Main.pendingNews.Count == 0 && Main.availableNewsWaitingForStageFilter.Count == 0))
		{
			return false;
		}
		int maxInt = GameMath.GetMaxInt(Main.instance.sim.GetStandardMaxStageReached(), Main.instance.sim.maxStagePrestigedAt);
		DateTime dateTime = TrustedTime.Get();
		PlayfabManager.NewsData newsData = Main.GetNextAvailableNewsIfMinStageIsReached(maxInt, dateTime);
		while (newsData != null && newsData.body.minStage > maxInt)
		{
			Main.pendingNews.Dequeue();
			Main.availableNewsWaitingForStageFilter.Add(newsData);
			newsData = Main.GetNextAvailableNewsIfMinStageIsReached(maxInt, dateTime);
		}
		if (newsData != null && dateTime < newsData.dateTime)
		{
			newsData = null;
		}
		if (newsData == null)
		{
			for (int i = Main.availableNewsWaitingForStageFilter.Count - 1; i >= 0; i--)
			{
				PlayfabManager.NewsData newsData2 = Main.availableNewsWaitingForStageFilter[i];
				if (!newsData2.CanBeShownReachingMinStage(maxInt, dateTime, Main.instance.sim.lastNewsTimestam))
				{
					Main.availableNewsWaitingForStageFilter.RemoveFastAt(i);
				}
				else if (newsData2.body.minStage < maxInt && dateTime >= newsData2.dateTime)
				{
					newsData = newsData2;
				}
			}
		}
		return newsData != null;
	}

	public static PlayfabManager.NewsData PeekNextNews()
	{
		if (Main.pendingNews.Count > 0)
		{
			return Main.pendingNews.Peek();
		}
		if (!TrustedTime.IsReady())
		{
			return null;
		}
		return Main.availableNewsWaitingForStageFilter.Find((PlayfabManager.NewsData n) => n.dateTime <= TrustedTime.Get());
	}

	public static PlayfabManager.NewsData DequeueNextNews()
	{
		if (Main.pendingNews.Count > 0)
		{
			return Main.pendingNews.Dequeue();
		}
		if (!TrustedTime.IsReady())
		{
			return null;
		}
		int index = Main.availableNewsWaitingForStageFilter.FindIndex((PlayfabManager.NewsData n) => n.dateTime <= TrustedTime.Get());
		PlayfabManager.NewsData result = Main.availableNewsWaitingForStageFilter[index];
		Main.availableNewsWaitingForStageFilter.RemoveAt(index);
		return result;
	}

	public static int GetAvailableNewsCount()
	{
		int num = Main.pendingNews.Count;
		if (TrustedTime.IsReady())
		{
			DateTime t = TrustedTime.Get();
			foreach (PlayfabManager.NewsData newsData in Main.availableNewsWaitingForStageFilter)
			{
				if (t >= newsData.dateTime)
				{
					num++;
				}
			}
		}
		return num;
	}

	private static PlayfabManager.NewsData GetNextAvailableNewsIfMinStageIsReached(int maxStageReched, DateTime now)
	{
		if (Main.pendingNews.Count == 0)
		{
			return null;
		}
		PlayfabManager.NewsData newsData = Main.pendingNews.Peek();
		while (newsData != null && !newsData.CanBeShownReachingMinStage(maxStageReched, now, Main.instance.sim.lastNewsTimestam))
		{
			Main.pendingNews.Dequeue();
			newsData = ((Main.pendingNews.Count <= 0) ? null : Main.pendingNews.Peek());
		}
		return newsData;
	}

	private IEnumerator InitPlayfab()
	{
		yield return null;
		yield return null;
		yield return null;
		yield return new WaitForSeconds(0.2f);
		if (Main.dontTryAuth)
		{
			PlayfabManager.Login(delegate
			{
			}, true);
		}
		else
		{
			StoreManager.Authenticate(true, delegate
			{
				if (StoreManager.IsAuthed())
				{
					PlayerStats.CheckAchievements();
				}
				PlayfabManager.Login(delegate
				{
				}, true);
			});
		}
		yield break;
	}

	private void CheckPlayfabData()
	{
		PlayfabManager.AskTitleData();
		PlayfabManager.AskPlayerData(delegate(bool isDataSuccess, SaveData saveData, PlayfabManager.RewardData rewardData)
		{
			if (isDataSuccess)
			{
				if (saveData != null)
				{
					GameMath.VersionComparisonResult versionComparisonResult = GameMath.CompareVersions(saveData.gameVersion, Cheats.lastVersionToClearSave);
					if (versionComparisonResult == GameMath.VersionComparisonResult.OLD)
					{
						saveData = null;
					}
					if (saveData != null)
					{
						GameMath.VersionComparisonResult versionComparisonResult2 = GameMath.CompareVersions(saveData.gameVersion, Cheats.version);
						if (versionComparisonResult2 == GameMath.VersionComparisonResult.NEW)
						{
							PlayfabManager.haveReceivedSaveDataFromFutureVersion = true;
							saveData = null;
							UnityEngine.Debug.Log("Playfab: the save data we got was from a new version. It will not be loaded. Playfab data shall not be overwritten");
						}
						else if (versionComparisonResult2 == GameMath.VersionComparisonResult.OLD)
						{
							this.ConvertSaveDataToCurrentVersion(saveData);
						}
						else if (versionComparisonResult2 == GameMath.VersionComparisonResult.EQUAL)
						{
						}
					}
					if (saveData != null)
					{
						Version v = new Version(saveData.gameVersion);
						Version v2 = new Version(Cheats.version);
						if (v < v2)
						{
							SaveLoadManager.cloudSaveMustBeBackedUp = true;
							PlayfabManager.BackupSaveData(delegate(bool isSuccess)
							{
								if (isSuccess)
								{
									SaveLoadManager.cloudSaveMustBeBackedUp = false;
								}
							});
						}
						if (Main.localSaveDataLifetimeInTicks < saveData.playerStatLifeTimeInTicks)
						{
							this.LoadSim(saveData, 0f, true, false);
							if (OldArtifactsConverter.DoesPlayerNeedsConversion(this.sim) || (TutorialManager.artifactOverhaul != TutorialManager.ArtifactOverhaul.FIN && TutorialManager.artifactOverhaul != TutorialManager.ArtifactOverhaul.BEFORE_BEGIN))
							{
								UiState uiState = (!OldArtifactsConverter.DoesPlayerNeedsConversion(this.sim)) ? UiState.HUB_ARTIFACTS : UiState.HUB;
								if (this.uiManager.IsInHubMenus())
								{
									this.uiManager.state = uiState;
								}
								else
								{
									DynamicLoadManager.CancelPendingRequests();
									this.uiManager.loadingTransition.DoTransition(uiState, 0f, 0f);
								}
							}
						}
						if (TrustedTime.IsReady())
						{
							this.EarnOffline(Main.lastOfflineEarnedTime);
							Main.hasEverHadPlayfabTime = true;
							Main.lastOfflineEarnedTime = TrustedTime.Get();
						}
						else
						{
							PlayfabManager.GetTime(delegate(bool isTimeSuccess, DateTime time)
							{
								if (isTimeSuccess)
								{
									TrustedTime.Init(time);
									this.EarnOffline(Main.lastOfflineEarnedTime);
									Main.hasEverHadPlayfabTime = true;
									Main.lastOfflineEarnedTime = TrustedTime.Get();
								}
							});
						}
					}
				}
				if (rewardData != null)
				{
					Main.rewardToClaim = rewardData;
				}
			}
		});
		PlayfabManager.AskTitleNews(delegate(List<PlayfabManager.NewsData> newsList)
		{
			newsList.Sort();
			foreach (PlayfabManager.NewsData item in newsList)
			{
				Main.pendingNews.Enqueue(item);
			}
		});
	}

	public Simulator GetSim()
	{
		return this.sim;
	}

	private void Awake()
	{
        //PlayerPrefs.DeleteAll();
		Cheats.version = Application.version;
		Cheats.versionObject = new Version(Cheats.version);
		DynamicLoadManager.Init("AaHBundles");
		DynamicLoadManager.GetPermanentReferenceToBundle("spine-shaders", delegate
		{
			DynamicLoadManager.LoadAllAssets("spine-shaders", false, null);
		}, true);
		Main.pendingNews = new Queue<PlayfabManager.NewsData>();
		Main.availableNewsWaitingForStageFilter = new List<PlayfabManager.NewsData>();
	}

	private void Start()
	{
		this.keyStroke = new KeyStroke();
		DLogConsole.Kill();
		Main.camera = Camera.main;
		Main.instance = this;
		Main.coroutineObject = this;
		LM.Initialize(this.parsedLoc);
		string @string = PlayerPrefs.GetString("titleDataString", "unsetTitleData");
		if (@string != "unsetTitleData")
		{
			PlayfabManager.TitleData titleData = JsonConvert.DeserializeObject<PlayfabManager.TitleData>(@string);
			if (titleData != null && titleData.freeCreditsAmount != 0.0)
			{
				PlayfabManager.titleData = titleData;
			}
		}
		string string2 = PlayerPrefs.GetString("titleTempString", "unsetTitleData");
		if (string2 != "unsetTitleData")
		{
			PlayfabManager.extraDamageTakenFromHeroes = Convert.ToDouble(string2);
		}
		this.FacebookInitAndActivate();
		base.StartCoroutine(this.InitPlayfab());
		if (Camera.main.aspect < 0.65f)
		{
			AspectRatioOffset.HERO_X = 0.15f;
			AspectRatioOffset.ENEMY_X = -0.15f;
		}
		Enemy.DEATHLESS_ZONE_LIMIT_X += AspectRatioOffset.ENEMY_X;
		DOTween.Init(null, null, null);
		DOTween.defaultEaseType = Ease.Linear;
		DOTween.defaultUpdateType = UpdateType.Manual;
		DOTween.defaultAutoPlay = AutoPlay.AutoPlayTweeners;
		DOTween.defaultRecyclable = false;
		IapManager.Init();
		AttachmentOffsets.Init();
		Exception ex = null;
		try
		{
			SaveData saveData = SaveLoadManager.Load(false);
            Debug.Log("saveData " + saveData);
			if (saveData != null)
			{
				Version v = new Version(saveData.gameVersion);
				Version v2 = new Version(Cheats.version);
				if (v < v2)
				{
					SaveLoadManager.MigrationBackupLocalSave();
					SaveLoadManager.cloudSaveMustBeBackedUp = true;
				}
				saveData = SaveLoadManager.MigrateSaveData(saveData);
                Debug.Log("save data " + saveData);
				this.LoadSim(saveData, 0f, false, false);
			}
			else
			{
				this.LoadSim(null, 0f, false, false);
			}
		}
		catch (Exception ex2)
		{
			if (string.IsNullOrEmpty(this.DEBUGerror))
			{
				this.DEBUGerror = "Error at Main.Start:" + System.Environment.NewLine;
				this.DEBUGerror = this.DEBUGerror + ex2.Message + System.Environment.NewLine;
				this.DEBUGerror += ex2.StackTrace;
			}
			UnityEngine.Debug.Log("!!!LOAD FAILED!!! Loading zero state");
			UnityEngine.Debug.Log(ex2.Message);
			UnityEngine.Debug.Log(ex2.StackTrace);
			ex = ex2;
			SaveLoadManager.loadingSaveFailed = true;
		}
		if (this.sim.prefers30Fps)
		{
			Application.targetFrameRate = 30;
		}
		else
		{
			Application.targetFrameRate = 60;
		}
		Main.SetAppSleep(this.sim);
		Manager.Init(this.sim, this);
		this.uiManager.keyStroke = this.keyStroke;
		this.uiManager.InitKeyboard();
		this.uiManager.Init(this.sim, this.soundManager);
		StoreManager.CancelNotifications();
		this.lastResolution = ScreenRes.GetCurrentRes();
		this.renderManager.OnScreenSizeChanged(this.lastResolution);
		this.uiManager.OnScreenSizeChanged(this.lastResolution);
		if (SaveLoadManager.HasSave())
		{
			AdjustTracker.TrackAppLaunch(false);
		}
		else
		{
			AdjustTracker.TrackAppLaunch(true);
		}
		if (ex != null)
		{
			throw ex;
		}
	}

	private void OnApplicationQuit()
	{
		if (RewardedAdManager.inst != null)
		{
			RewardedAdManager.inst.hasBeenWatchingAdWithoutIntermission = false;
		}
		if (this.sim == null)
		{
			return;
		}
		if (!this.justTriedSaving)
		{
			this.justTriedSaving = true;
			this.sim.PrepareForCloseAndSave();
			this.TrySave();
			this.SetNotifitifications();
		}
	}

	private void OnApplicationFocus(bool focus)
	{
		if (this.sim == null)
		{
			return;
		}
		if (!focus)
		{
			if (!this.justTriedSaving)
			{
				this.justTriedSaving = true;
				this.sim.PrepareForCloseAndSave();
				this.TrySave();
				this.SetNotifitifications();
				PlayerPrefs.Save();
			}
		}
		else
		{
			StoreManager.CancelNotifications();
			TrustedTime.Refresh();
		}
		Main.SetAppSleep(this.sim);
	}

	public static void SetAppSleep(Simulator sim)
	{
		if (!sim.appNeverSleep)
		{
			Screen.sleepTimeout = Main.SLEEP_TIMEOUT;
		}
		else
		{
			Screen.sleepTimeout = -1;
		}
	}

	private void SetNotifitifications()
	{
		double num = -1.0;
		double num2 = -1.0;
		double num3 = -1.0;
		double minesDuration = -1.0;
		double flashOfferDuration = -1.0;
		double secondsTillDustRestBonusFull = -1.0;
		List<StoreManager.NotificationInfo> list = new List<StoreManager.NotificationInfo>();
		if (TrustedTime.IsReady())
		{
			DateTime dateTime = TrustedTime.Get();
			if (this.sim.hasDailies)
			{
				num = DailyQuest.GetTimeBetweenQuests() - (dateTime - this.sim.dailyQuestCollectedTime).TotalSeconds;
				if (num < 5.0 || this.sim.dailyQuest != null)
				{
					num = -1.0;
				}
			}
			if (this.sim.MineScrapUnlocked())
			{
				num2 = this.sim.GetTimeToCollectMine(this.sim.mineScrap);
			}
			if (this.sim.MineTokenUnlocked())
			{
				num3 = this.sim.GetTimeToCollectMine(this.sim.mineToken);
			}
			if (num2 > 5.0 && num3 > 5.0)
			{
				minesDuration = GameMath.GetMinDouble(num2, num3);
			}
			else if (num2 > 5.0)
			{
				minesDuration = num2;
			}
			else if (num3 > 5.0)
			{
				minesDuration = num3;
			}
			if (TutorialManager.AreFlashOffersUnlocked() && this.sim.flashOfferBundle != null)
			{
				flashOfferDuration = this.sim.flashOfferBundle.GetRemainingDur();
			}
			if (TutorialManager.IsAeonDustUnlocked())
			{
				secondsTillDustRestBonusFull = (1.0 - this.sim.GetDaysPassedSinceLastTimeRiftRestBonusCollected()) * 86400.0;
			}
			if (this.sim.IsChristmasTreeEnabled())
			{
				if (new DateTime(2018, 12, 3, 23, 59, 59) >= dateTime)
				{
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2018, 12, 10, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_1"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2018, 12, 17, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_1"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2018, 12, 24, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_2"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2018, 12, 31, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_3"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2019, 1, 7, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_1"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2019, 1, 14, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_4"
					});
					if (PlayfabManager.christmasOfferConfigLoaded)
					{
						list.Add(new StoreManager.NotificationInfo
						{
							notificationTime = (PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed - dateTime).TotalSeconds,
							locKey = "EVENT_PUSH_5"
						});
					}
				}
				else if (new DateTime(2018, 12, 10, 23, 59, 59) >= dateTime)
				{
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2018, 12, 17, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_1"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2018, 12, 24, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_2"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2018, 12, 31, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_3"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2019, 1, 7, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_1"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2019, 1, 14, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_4"
					});
					if (PlayfabManager.christmasOfferConfigLoaded)
					{
						list.Add(new StoreManager.NotificationInfo
						{
							notificationTime = (PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed - dateTime).TotalSeconds,
							locKey = "EVENT_PUSH_5"
						});
					}
				}
				else if (new DateTime(2018, 12, 17, 23, 59, 59) >= dateTime)
				{
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2018, 12, 24, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_2"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2018, 12, 31, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_3"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2019, 1, 7, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_1"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2019, 1, 14, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_4"
					});
					if (PlayfabManager.christmasOfferConfigLoaded)
					{
						list.Add(new StoreManager.NotificationInfo
						{
							notificationTime = (PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed - dateTime).TotalSeconds,
							locKey = "EVENT_PUSH_5"
						});
					}
				}
				else if (new DateTime(2018, 12, 24, 23, 59, 59) >= dateTime)
				{
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2018, 12, 31, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_3"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2019, 1, 7, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_1"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2019, 1, 14, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_4"
					});
					if (PlayfabManager.christmasOfferConfigLoaded)
					{
						list.Add(new StoreManager.NotificationInfo
						{
							notificationTime = (PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed - dateTime).TotalSeconds,
							locKey = "EVENT_PUSH_5"
						});
					}
				}
				else if (new DateTime(2018, 12, 31, 23, 59, 59) >= dateTime)
				{
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2019, 1, 7, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_1"
					});
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2019, 1, 14, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_4"
					});
					if (PlayfabManager.christmasOfferConfigLoaded)
					{
						list.Add(new StoreManager.NotificationInfo
						{
							notificationTime = (PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed - dateTime).TotalSeconds,
							locKey = "EVENT_PUSH_5"
						});
					}
				}
				else if (new DateTime(2019, 1, 14, 10, 0, 0) >= dateTime)
				{
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (new DateTime(2019, 1, 14, 10, 0, 0) - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_4"
					});
					if (PlayfabManager.christmasOfferConfigLoaded)
					{
						list.Add(new StoreManager.NotificationInfo
						{
							notificationTime = (PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed - dateTime).TotalSeconds,
							locKey = "EVENT_PUSH_5"
						});
					}
				}
				else if (PlayfabManager.christmasOfferConfigLoaded && PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed >= dateTime && PlayfabManager.christmasOfferConfigLoaded)
				{
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = (PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed - dateTime).TotalSeconds,
						locKey = "EVENT_PUSH_5"
					});
				}
				if (!this.sim.candyDropAlreadyDisabled && StoreManager.christmasEventNotifications && this.sim.lastCandyAmountCapDailyReset < this.sim.lastFreeCandyTreatClaimedDate)
				{
					list.Add(new StoreManager.NotificationInfo
					{
						notificationTime = this.sim.GetDailtyCapResetTimer().TotalSeconds,
						locKey = "CANDIES_PUSH"
					});
				}
			}
			if (StoreManager.eventsNotifications && this.sim.IsSecondAnniversaryEventEnabled())
			{
				this.AddEventNotificationIfNecessary(list, "secondAnniversary", "freeTokens", "UI_PUSH_FREE_GIFT_BIRTHDAY");
				this.AddEventNotificationIfNecessary(list, "secondAnniversary", "freeScraps", "UI_PUSH_FREE_GIFT_BIRTHDAY");
				this.AddEventNotificationIfNecessary(list, "secondAnniversary", "freeGems", "UI_PUSH_FREE_GIFT_BIRTHDAY");
				this.AddEventNotificationIfNecessary(list, "secondAnniversary", "gemsOffer2", "NOTIF_SHOP_OFFER");
				this.AddEventNotificationIfNecessary(list, "secondAnniversary", "gemsOffer", "NOTIF_SHOP_OFFER");
				this.AddEventNotificationIfNecessary(list, "secondAnniversary", "currencyBundle", "NOTIF_SHOP_OFFER");
				this.AddEventNotificationIfNecessary(list, "secondAnniversary", "currencyBundle2", "NOTIF_SHOP_OFFER");
			}
		}
		StoreManager.SetNotifications(this.sim.GetTimeForNextFreeLootpack(), this.sim.GetTimeForNextShopOffer(), minesDuration, num, flashOfferDuration, secondsTillDustRestBonusFull, list);
	}

	private void AddEventNotificationIfNecessary(List<StoreManager.NotificationInfo> notifications, string eventId, string internalEventId, string locKey)
	{
		DateTime dateTime = TrustedTime.Get();
		EventConfig.InternalEvent internalEventConfig = PlayfabManager.eventsInfo.GetInternalEventConfig(eventId, internalEventId);
		if (internalEventConfig != null && internalEventConfig.startDate > dateTime)
		{
			notifications.Add(new StoreManager.NotificationInfo
			{
				notificationTime = (internalEventConfig.startDate - dateTime).TotalSeconds,
				locKey = locKey
			});
		}
	}

	private void OnApplicationPause(bool paused)
	{
		if (this.sim == null)
		{
			return;
		}
		if (RewardedAdManager.inst != null && RewardedAdManager.inst.IsWatchingAnyAd())
		{
			RewardedAdManager.inst.hasBeenWatchingAdWithoutIntermission = false;
		}
		if (paused)
		{
			if (!this.justTriedSaving)
			{
				this.justTriedSaving = true;
				this.sim.PrepareForCloseAndSave();
				this.TrySave();
				this.SetNotifitifications();
				PlayerPrefs.Save();
			}
			Manager.OnApplicationPause();
			TutorialManager.OnApplicationPaused();
		}
		else
		{
			UiManager.resetButtonStates = true;
			PlayerStats.OnSessionStart();
			this.FacebookInitAndActivate();
			base.StartCoroutine(this.InitPlayfab());
			StoreManager.CancelNotifications();
		}
		Main.SetAppSleep(this.sim);
	}

	private void TrySave()
	{
		if (this.sim == null)
		{
			return;
		}
		if (PlayfabManager.forceUpdate)
		{
			return;
		}
		try
		{
			this.softSaveTimer = 0f;
			foreach (World world in this.sim.GetAllWorlds())
			{
				world.shouldSave = false;
				world.shouldSoftSave = false;
			}
			SaveLoadManager.timeSinceLastSave = 0f;
			SaveLoadManager.Save(this.sim, Main.hasEverHadPlayfabTime, Main.lastOfflineEarnedTime);
		}
		catch (Exception ex)
		{
			MonoBehaviour.print("Game could not be saved due to error:");
			MonoBehaviour.print(ex.Message);
			MonoBehaviour.print(ex.StackTrace);
		}
	}

	private void LoadSim(SaveData saveData, float additionalOfflineSimTime, bool fromServer, bool dontLoadAssets = false)
	{
		this.UnloadInGameAssets();
		if (saveData != null)
		{
            CrashHandler.SetPlayfabId(saveData.allPlayfabIds);
			this.sim = new Simulator();
			PlayfabManager.sim = this.sim;
			PlayerStats.Initialize();
			TutorialManager.SetSimulator(this.sim);
			Simulator.LoadTutorialState(saveData);
			this.sim.InitLoad(saveData);
			Manager.Init(this.sim, this);
			this.LoadPlayerStats(saveData);
			PlayerStats.OnSessionStart();
			Main.localSaveDataLifetimeInTicks = saveData.playerStatLifeTimeInTicks;
			Main.hasEverHadPlayfabTime = saveData.hasEverHadPlayfabTime;
			Main.dontStoreAuthenticate = true;
			Main.playerOpenedTheGameFirstTime = false;
			if (!fromServer)
			{
				Main.dontTryAuth = saveData.dontTryAuth;
				Main.dontAskForAuthenticatingPopup = saveData.dontAskAuth;
			}
			try
			{
				Main.lastOfflineEarnedTime = new DateTime(saveData.lastOfflineEarnedTime);
			}
			catch
			{
				Main.lastOfflineEarnedTime = new DateTime(0L);
			}
			GameMath.seedArtifact = saveData.seedArtifact;
			GameMath.seedLootpack = saveData.seedLootpack;
			GameMath.seedTrinket = saveData.seedTrinket;
			GameMath.seedCharmpack = saveData.seedCharmpack;
			GameMath.seedCharmdraft = saveData.seedCharmdraft;
			GameMath.seedCursedGate = saveData.seedCursedGate;
			GameMath.seedNewCurses = saveData.seedNewCurses;
			StoreManager.areNotificationsAllowed = saveData.notifOn;
			StoreManager.askedToAllowNotifications = saveData.notifAsked;
			if (saveData.notifs == -2147483648)
			{
				StoreManager.mineNotifications = true;
				StoreManager.specialOffersNotifications = true;
				StoreManager.freeChestsNotifications = true;
				StoreManager.sideQuestNotifications = true;
				StoreManager.flashOffersNotifications = true;
				StoreManager.dustRestBonusFullNotifications = true;
				StoreManager.christmasEventNotifications = true;
				StoreManager.eventsNotifications = true;
			}
			else
			{
				StoreManager.mineNotifications = ((saveData.notifs & 1) != 0);
				StoreManager.specialOffersNotifications = ((saveData.notifs & 2) != 0);
				StoreManager.freeChestsNotifications = ((saveData.notifs & 4) != 0);
				StoreManager.sideQuestNotifications = ((saveData.notifs & 8) != 0);
				StoreManager.flashOffersNotifications = ((saveData.notifs & 16) != 0);
				StoreManager.dustRestBonusFullNotifications = ((saveData.notifs & 32) != 0);
				StoreManager.christmasEventNotifications = ((saveData.notifs & 64) != 0);
				StoreManager.eventsNotifications = ((saveData.notifs & 128) != 0);
			}
			if (StoreManager.areNotificationsAllowed)
			{
				StoreManager.RegisterForNotifications();
			}
			if (saveData.sleep)
			{
				Screen.sleepTimeout = Main.SLEEP_TIMEOUT;
			}
			else
			{
				Screen.sleepTimeout = -1;
			}
			this.sim.CheckTutorialState();
			if (!dontLoadAssets)
			{
				this.LoadGameAssets();
			}
			if (this.sim.IsModeUnlocked(GameMode.CRUSADE))
			{
				this.soundManager.LoadUiBundle("sounds/timechallenge-mode");
			}
			if (this.sim.IsModeUnlocked(GameMode.RIFT))
			{
				this.soundManager.LoadUiBundle("sounds/rift-mode");
			}
			this.sim.charmSortType = SaveLoadManager.ConvertCharmSortType(saveData.cst);
			this.sim.isCharmSortingDescending = saveData.icsd;
			this.sim.isCharmSortingShowing = saveData.isCharSS;
			this.sim.trinketSortType = SaveLoadManager.ConvertTrinketSortType(saveData.trinketSortingType);
			this.sim.isTrinketSortingDescending = saveData.isTrinketSortingDescending;
			this.sim.isTrinketSortingShowing = saveData.isTrinketSortingShowing;
			return;
		}
		if (this.sim != null)
		{
			return;
		}
        this.sim = new Simulator();
		PlayfabManager.sim = this.sim;
		TutorialManager.SetSimulator(this.sim);
		this.sim.InitZero();
		PlayerStats.Initialize();
		PlayerStats.OnSessionStart();
		Main.hasEverHadPlayfabTime = false;
		Main.lastOfflineEarnedTime = DateTime.MaxValue;
		Main.dontStoreAuthenticate = true;
		Main.playerOpenedTheGameFirstTime = true;
		Main.dontTryAuth = false;
		Main.dontAskForAuthenticatingPopup = false;
		StoreManager.areNotificationsAllowed = false;
		StoreManager.askedToAllowNotifications = false;
        //CookiePayload cookiePlayload = InstantUtility.GetCookiePlayload();
        //if (cookiePlayload != null)
        //{
        //    this.LoadInstantCookie(cookiePlayload);
           
        //}
        this.LoadGameAssets();
	}

	private void LoadInstantCookie(CookiePayload instantCookie)
	{
		if (instantCookie.initialzedCookie)
		{
			PlayerStats.acquiredTrhoughInstantGame = true;
		}
		this.sim.GainCurrency(CurrencyType.GOLD, instantCookie.coinAmount);
		World world = this.sim.GetWorld(GameMode.STANDARD);
		ChallengeStandard challengeStandard = world.activeChallenge as ChallengeStandard;
		TutorialManager.missionIndex = GameMath.GetMaxInt(TutorialManager.missionIndex, instantCookie.tutorialQuestIndex);
		if (TutorialManager.missionIndex > -1 && TutorialManager.missionIndex < TutorialMission.List.Length)
		{
			if (TutorialManager.missionIndex == 3)
			{
				TutorialMission.List[TutorialManager.missionIndex].SetLoadState(0f, false);
			}
			else
			{
				TutorialMission.List[TutorialManager.missionIndex].SetLoadState((float)instantCookie.tutorialQuestProgress, false);
			}
		}
		int newTotWave = 11 * instantCookie.stageIndex + instantCookie.waveIndex + 1;
		if (instantCookie.tutorialStep > 15)
		{
			TutorialManager.first = TutorialManager.First.FIN;
		}
		else
		{
			TutorialManager.first = SaveLoadManager.ConvertTutStateFirst(instantCookie.tutorialStep + 1);
		}
		if (instantCookie.tutorialStep >= 21)
		{
			TutorialManager.skillScreen = TutorialManager.SkillScreen.FIN;
		}
		if (instantCookie.upgradedSkillIndex > -2)
		{
			challengeStandard.LoadTotWave(newTotWave);
			world.LoadNewHero(this.sim.GetHeroDataBase("HORATIO"), this.sim.GetBoughtGears(), false);
			Hero hero = world.heroes[0];
			SkillTreeProgress skillTreeProgressGained = hero.GetSkillTreeProgressGained();
			int newLevel = 0;
			bool flag = false;
			if (instantCookie.upgradedSkillIndex == 0)
			{
				flag = true;
				skillTreeProgressGained.ulti++;
			}
			else if (instantCookie.upgradedSkillIndex == 1)
			{
				flag = true;
				skillTreeProgressGained.branches[0][0]++;
			}
			else if (instantCookie.upgradedSkillIndex == 2)
			{
				flag = true;
				skillTreeProgressGained.branches[1][0]++;
			}
			if (flag)
			{
				newLevel = 1;
			}
			hero.SetLevel(newLevel, instantCookie.heroXp);
			hero.SetSkillTreeProgressGained(skillTreeProgressGained);
			hero.Refresh();
		}
		world.SetTotemLevel(instantCookie.ringLevel, instantCookie.ringXp);
		PlayerStats.numTotTap = instantCookie.totalTaps;
	}

	private void LoadPlayerStats(SaveData saveData)
	{
		PlayerStats.lifeTimeInTicks = saveData.playerStatLifeTimeInTicks;
		PlayerStats.lifeTimeInTicksInCurrentSaveFile = saveData.playerStatLifeTimeInTicksInCurrentSaveFile;
		PlayerStats.numLogins = saveData.playerStatNumLogins;
		PlayerStats.spentCreditsDuringThisSaveFile = saveData.playerStatSpentCreditsDuringThisSaveFile;
		PlayerStats.spentCredits = saveData.playerStatSpentCredits;
		PlayerStats.spentCreditsFirstDay = saveData.playerStatSpentCreditsFirstDay;
		PlayerStats.spentMyth = saveData.playerStatSpentMyth;
		PlayerStats.spentScraps = saveData.playerStatSpentScraps;
		PlayerStats.spentTokens = saveData.playerStatSpentTokens;
		PlayerStats.spentAeons = saveData.playerStatSpentAeons;
		PlayerStats.numAdDragonCatch = saveData.playerStatNumAdDragonCatch;
		PlayerStats.numAdDragonMiss = saveData.playerStatNumAdDragonMiss;
		PlayerStats.numAdAccept = saveData.playerStatNumAdAccept;
		PlayerStats.numAdCancel = saveData.playerStatNumAdCancel;
		PlayerStats.numFreeCredits = saveData.playerStatNumFreeCredits;
		PlayerStats.numTotTap = saveData.playerStatNumTotTap;
		PlayerStats.acquiredTrhoughInstantGame = saveData.isInstantPlayer;
		PlayerStats.enemiesKilled = saveData.playerStatEnemiesKilled;
		PlayerStats.timeHeroesDied = saveData.playerStatTimeHeroesDied;
		PlayerStats.ultimatesUsedCount = saveData.playerStatUltimatesUsedCount;
		PlayerStats.minesCollectedCount = saveData.playerStatMinesCollectedCount;
		PlayerStats.secondaryAbilitiesCastedCount = saveData.playerStatSecondaryAbilitiesCastedCount;
		PlayerStats.goblinChestsDestroyedCount = saveData.playerStatGoblinChestsDestroyedCount;
		PlayerStats.screenshotsSharedInAdventure = saveData.playerStatScreenshotsSharedInAdventure;
		PlayerStats.screenshotsSharedInTimeChallenges = saveData.playerStatScreenshotsSharedInTimeChallenges;
		PlayerStats.screenshotsSharedInGog = saveData.playerStatScreenshotsSharedInGog;
		if (saveData.playerStatNumUsedMerchantItems != null)
		{
			PlayerStats.numUsedMerchantItems = saveData.playerStatNumUsedMerchantItems;
		}
		if (saveData.achievements != null)
		{
			foreach (KeyValuePair<int, bool> keyValuePair in saveData.achievements)
			{
				string key = SaveLoadManager.ConvertAchievementId(keyValuePair.Key);
				int i = 0;
				int count = PlayerStats.achievements.Count;
				while (i < count)
				{
					if (PlayerStats.achievements[i].ContainsKey(key))
					{
						PlayerStats.achievements[i][key] = saveData.achievements[keyValuePair.Key];
						break;
					}
					i++;
				}
			}
		}
		if (saveData.iapsMade != null)
		{
			int j = 0;
			int count2 = saveData.iapsMade.Count;
			while (j < count2)
			{
				PlayerStats.iapsMade[j] = saveData.iapsMade[j];
				j++;
			}
		}
		PlayerStats.numTrinketPacksOpened = saveData.numTrinketPacksOpened;
		PlayerStats.numTotalDailySkip = saveData.totalDailySkip;
		PlayerStats.numTotalDailyCompleted = saveData.totalDailyCompleted;
		PlayerStats.allTimeCharmPacksOpened = saveData.allTimeCharmPacksOpened;
		PlayerStats.allTimeFlashOffersPurchased = saveData.allTimeFlashOffersPurchased;
		PlayerStats.allTimeAdventureFlashOffersPurchased = saveData.allTimeAdventureFlashOffersPurchased;
		PlayerStats.allTimeHalloweenFlashOffersPurchased = saveData.allTimeHalloweenFlashOffersPurchased;
		PlayerStats.allTimeChristmasFlashOffersPurchased = saveData.allTimeChristmasFlashOffersPurchased;
		PlayerStats.OnTap();
		PlayerStats.OnArtifactCraft(this.sim.artifactsManager.TotalArtifactsLevel, this.sim.artifactsManager.GetTotalAmountOfArtifacts());
		PlayerStats.OnHeroEvolve(this.sim.GetAllHeroes());
		PlayerStats.OnPrestige(this.sim.numPrestiges);
		PlayerStats.OnChallengeWin(PlayerStats.GetNumChallengesWon(this.sim));
		PlayerStats.CheckDailtyQuestAchievement(PlayerStats.numTotalDailyCompleted);
		PlayerStats.OnRuneBought(this.sim.GetBoughtRunes().Count);
	}

	public SaveData GetSaveDataForCurrentState()
	{
		return SaveLoadManager.GenerateSaveData(this.sim, Main.hasEverHadPlayfabTime, Main.lastOfflineEarnedTime);
	}

	private void EarnOffline(DateTime _lastOfflineEarnedTime)
	{
		if (!TrustedTime.IsReady())
		{
			UnityEngine.Debug.Log("we can not trust the time");
			return;
		}
		if (!Main.hasEverHadPlayfabTime)
		{
			UnityEngine.Debug.Log("we never ever had playfab time.");
			return;
		}
		float num = (float)(TrustedTime.Get() - _lastOfflineEarnedTime).TotalSeconds;
		if (num < 0f)
		{
			return;
		}
		Main.DEBUGofflineDuration = num;
		SaveData saveData = SaveLoadManager.GenerateSaveData(this.sim, Main.hasEverHadPlayfabTime, Main.lastOfflineEarnedTime);
		if (saveData == null)
		{
			UnityEngine.Debug.Log("save data was null during offline earning.");
			return;
		}
		Simulator simulator = new Simulator();
		simulator.InitLoad(saveData);
		this.EarnOffline(this.sim, simulator, num);
	}

	private void EarnOffline(Simulator simReal, Simulator simHeadless, float dur)
	{
		float num = dur;
		float offlineEarningsFactor = 1f;
		if (3600f < num)
		{
			num = 3600f;
			offlineEarningsFactor = dur / 3600f;
		}
		List<GameMode> list = new List<GameMode>();
		foreach (World world in simReal.GetAllWorlds())
		{
			if (world.activeChallenge.doesUpdateInBg)
			{
				GameMode gameMode = world.gameMode;
				if (simReal.GetWorld(gameMode).activeChallenge.GetTotWave() < simHeadless.GetWorld(gameMode).activeChallenge.GetTotWave())
				{
					list.Add(gameMode);
					UnityEngine.Debug.Log("offline earning: there was a prestige, cancel all offline earnings");
				}
				else if (gameMode == GameMode.STANDARD && simReal.IsGameModeInAction(gameMode))
				{
					simReal.prestigeRunTimer += (double)dur;
				}
			}
		}
		simHeadless.ResetMerchantItems();
		if (simHeadless.hasCompass && simReal.compassDisabled)
		{
			simHeadless.hasCompass = false;
		}
		Dictionary<GameMode, double> dictionary;
		Dictionary<GameMode, int> dictionary2;
		simHeadless.CalculateOffline(num, out dictionary, out dictionary2);
		simReal.AddOfflineCooldowns(dur);
		if (dur > 120f)
		{
			foreach (GameMode key in list)
			{
				dictionary[key] = 0.0;
				dictionary2[key] = 0;
			}
			simReal.AddOfflineGolds(dictionary, offlineEarningsFactor);
			if (!simReal.compassDisabled)
			{
				simReal.AddOfflineTotWavesAdvances(dictionary2);
			}
		}
	}

	private void Update()
	{
		this.justTriedSaving = false;
		try
		{
			DynamicLoadManager.UpdatePendingRequests();
			PlayerStats.Update();
			Main.localSaveDataLifetimeInTicks += (long)(Time.deltaTime * 1E+07f);
			if (RewardedAdManager.inst != null)
			{
				RewardedAdManager.Advance();
				if (RewardedAdManager.inst.IsWatchingAnyAd())
				{
					AudioListener.volume = 0f;
					return;
				}
				AudioListener.volume = 1f;
			}
			float deltaTime = Time.deltaTime;
			TrustedTime.Increment((double)deltaTime);
			if (Main.lastOfflineEarnedTime != DateTime.MaxValue)
			{
				Main.lastOfflineEarnedTime = Main.lastOfflineEarnedTime.AddSeconds((double)deltaTime);
			}
			float dt = (!TutorialManager.IsPaused()) ? deltaTime : 0f;
			this.UpdateTaps();
			this.UpdateSim(dt);
			this.keyStroke.Update();
			if (Main.GoToGameAfterAllAssetsLoaded && this.AreAllGameAssetsLoaded() && --Main.FramesToWaitForTransition < 0)
			{
				this.ChangeUiState(UiState.NONE);
				Main.GoToGameAfterAllAssetsLoaded = false;
				Main.FramesToWaitForTransition = 10;
			}
			this.UpdateRender();
			DOTween.ManualUpdate(deltaTime, Time.unscaledDeltaTime);
			this.UpdateUi(deltaTime, this.sim);
			this.UpdateSound(deltaTime);
			DynamicLoadManager.UnloadUnusedBundles();
			if (Time.frameCount % 120 == 0)
			{
				Main.SetAppSleep(this.sim);
			}
			if (IapManager.inst.boughtProductIndex == IapIds.XMAS_PACK)
			{
				this.uiManager.selectedShopPack = new ShopPackXmas();
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				World activeWorld = this.sim.GetActiveWorld();
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				if (this.uiManager.panelShop.isHubMode)
				{
					this.RainCurrenciesPurchasedFromPackOnUi(activeWorld);
					this.uiManager.state = UiState.HUB_SHOP;
				}
				else
				{
					activeWorld.RainCredits(this.uiManager.selectedShopPack.credits);
					activeWorld.RainTokens(this.uiManager.selectedShopPack.tokensMax);
					activeWorld.RainScraps(this.uiManager.selectedShopPack.scrapsMax);
					this.uiManager.state = UiState.NONE;
				}
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.CURRENCY_PACK)
			{
				this.uiManager.selectedShopPack = new ShopPackCurrency();
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				World activeWorld2 = this.sim.GetActiveWorld();
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				if (this.uiManager.panelShop.isHubMode)
				{
					this.RainCurrenciesPurchasedFromPackOnUi(activeWorld2);
					this.uiManager.state = UiState.HUB_SHOP;
				}
				else
				{
					activeWorld2.RainCredits(this.uiManager.selectedShopPack.credits);
					activeWorld2.RainTokens(this.uiManager.selectedShopPack.tokensMax);
					activeWorld2.RainScraps(this.uiManager.selectedShopPack.scrapsMax);
					this.uiManager.state = UiState.NONE;
				}
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.STARTER_PACK)
			{
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				this.uiManager.selectedShopPack = new ShopPackStarter();
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				this.uiManager.BuyShopPack();
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.STAGE_100_OFFER)
			{
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				this.uiManager.selectedShopPack = new ShopPackStage100();
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				this.uiManager.BuyShopPack();
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.STAGE_300_OFFER)
			{
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				this.uiManager.selectedShopPack = new ShopPackStage300();
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				this.uiManager.BuyShopPack();
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.STAGE_800_OFFER)
			{
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				this.uiManager.selectedShopPack = new ShopPackStage800();
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				this.uiManager.BuyShopPack();
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.MID_GEM_OFFER)
			{
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				this.uiManager.selectedShopPack = new ShopPackBigGem();
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				this.uiManager.BuyShopPack();
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.MID_GEM_OFFER_TWO)
			{
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				this.uiManager.selectedShopPack = new ShopPackBigGemTwo();
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				this.uiManager.BuyShopPack();
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.RIFT_OFFER_01 || IapManager.inst.boughtProductIndex == IapIds.RIFT_OFFER_02 || IapManager.inst.boughtProductIndex == IapIds.RIFT_OFFER_03 || IapManager.inst.boughtProductIndex == IapIds.RIFT_OFFER_04)
			{
				if (IapManager.inst.boughtProductIndex == IapIds.RIFT_OFFER_01)
				{
					this.uiManager.selectedShopPack = new ShopPackRiftOffer01();
				}
				else if (IapManager.inst.boughtProductIndex == IapIds.RIFT_OFFER_02)
				{
					this.uiManager.selectedShopPack = new ShopPackRiftOffer02();
				}
				else if (IapManager.inst.boughtProductIndex == IapIds.RIFT_OFFER_03)
				{
					this.uiManager.selectedShopPack = new ShopPackRiftOffer03();
				}
				else if (IapManager.inst.boughtProductIndex == IapIds.RIFT_OFFER_04)
				{
					this.uiManager.selectedShopPack = new ShopPackRiftOffer04();
				}
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				World activeWorld3 = this.sim.GetActiveWorld();
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				if (this.uiManager.panelShop.isHubMode)
				{
					this.RainCurrenciesPurchasedFromPackOnUi(activeWorld3);
					this.uiManager.state = UiState.HUB_SHOP;
				}
				else
				{
					activeWorld3.RainCredits(this.uiManager.selectedShopPack.credits);
					activeWorld3.RainScraps(this.uiManager.selectedShopPack.scrapsMax);
					this.uiManager.state = UiState.NONE;
				}
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.REGIONAL_01)
			{
				this.uiManager.selectedShopPack = new ShopPackRegional01();
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				World activeWorld4 = this.sim.GetActiveWorld();
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				if (this.uiManager.panelShop.isHubMode)
				{
					this.RainCurrenciesPurchasedFromPackOnUi(activeWorld4);
					this.uiManager.state = UiState.HUB_SHOP;
				}
				else
				{
					activeWorld4.RainCredits(this.uiManager.selectedShopPack.credits);
					activeWorld4.RainScraps(this.uiManager.selectedShopPack.scrapsMax);
					this.uiManager.state = UiState.NONE;
				}
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.HALLOWEEN_GEMS_PACK)
			{
				this.uiManager.selectedShopPack = new ShopPackHalloweenGems();
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				World activeWorld5 = this.sim.GetActiveWorld();
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				if (this.uiManager.panelShop.isHubMode)
				{
					this.RainCurrenciesPurchasedFromPackOnUi(activeWorld5);
					this.uiManager.state = UiState.HUB_SHOP;
				}
				else
				{
					activeWorld5.RainCredits(this.uiManager.selectedShopPack.credits);
					this.uiManager.state = UiState.NONE;
				}
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.CANDY_PACK_01 || IapManager.inst.boughtProductIndex == IapIds.CANDY_PACK_02)
			{
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				double amount = 12500.0;
				DropPosition dropPos = new DropPosition
				{
					startPos = this.uiManager.panelChristmasOffer.candyTreats[2].transform.position,
					endPos = this.uiManager.panelChristmasOffer.candyTreats[2].transform.position + Vector3.down * 0.1f,
					invPos = this.uiManager.panelChristmasOffer.candies.GetCurrencyTransform().position,
					targetToScaleOnReach = this.uiManager.panelChristmasOffer.candies.GetCurrencyTransform()
				};
				this.sim.GetActiveWorld().RainCurrencyOnUi(UiState.CHRISTMAS_PANEL, CurrencyType.CANDY, amount, dropPos, 30, 0f, 0f, 1f, null, 0f);
				this.uiManager.state = UiState.CHRISTMAS_PANEL;
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.CHRISTMAS_GEMS_BIG_PACK || IapManager.inst.boughtProductIndex == IapIds.CHRISTMAS_GEMS_SMALL_PACK)
			{
				if (IapManager.inst.boughtProductIndex == IapIds.CHRISTMAS_GEMS_BIG_PACK)
				{
					this.uiManager.selectedShopPack = new ShopPackChristmasGemsBig();
				}
				else
				{
					this.uiManager.selectedShopPack = new ShopPackChristmasGemsSmall();
				}
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				World activeWorld6 = this.sim.GetActiveWorld();
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				DropPosition dropPos2 = new DropPosition
				{
					startPos = default(Vector3),
					endPos = Vector3.down * 0.1f,
					invPos = this.uiManager.panelChristmasOffer.candies.GetCurrencyTransform().position,
					targetToScaleOnReach = this.uiManager.panelChristmasOffer.candies.GetCurrencyTransform()
				};
				activeWorld6.RainCurrencyOnUi(UiState.CHRISTMAS_PANEL, CurrencyType.CANDY, this.uiManager.selectedShopPack.candies, dropPos2, 30, 0f, 0f, 1f, null, 0f);
				DropPosition dropPos3 = new DropPosition
				{
					startPos = default(Vector3),
					endPos = Vector3.down * 0.1f,
					invPos = this.uiManager.panelCurrencyOnTop[0].currencyFinalPosReference.position,
					showSideCurrency = true
				};
				activeWorld6.RainCurrencyOnUi(UiState.CHRISTMAS_PANEL, CurrencyType.GEM, this.uiManager.selectedShopPack.credits, dropPos3, 30, 0f, 0f, 1f, null, 0f);
				this.uiManager.state = UiState.CHRISTMAS_PANEL;
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.SECOND_ANNIVERSARY_GEMS || IapManager.inst.boughtProductIndex == IapIds.SECOND_ANNIVERSARY_GEMS_TWO)
			{
				if (IapManager.inst.boughtProductIndex == IapIds.SECOND_ANNIVERSARY_GEMS)
				{
					this.uiManager.selectedShopPack = new ShopPackSecondAnniversaryGems();
				}
				else
				{
					this.uiManager.selectedShopPack = new ShopPackSecondAnniversaryGemsTwo();
				}
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				World activeWorld7 = this.sim.GetActiveWorld();
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				if (this.uiManager.secondAnniversaryPopup.previousState == UiState.HUB_SHOP)
				{
					this.RainCurrenciesPurchasedFromPackOnUi(activeWorld7);
					this.uiManager.state = UiState.HUB_SHOP;
				}
				else
				{
					this.uiManager.state = UiState.NONE;
					activeWorld7.RainCredits(this.uiManager.selectedShopPack.credits);
				}
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex == IapIds.SECOND_ANNIVERSARY_CURRENCY_PACK || IapManager.inst.boughtProductIndex == IapIds.SECOND_ANNIVERSARY_CURRENCY_PACK_TWO)
			{
				if (IapManager.inst.boughtProductIndex == IapIds.SECOND_ANNIVERSARY_CURRENCY_PACK)
				{
					this.uiManager.selectedShopPack = new ShopPackSecondAnniversaryCurrencyBundle();
				}
				else
				{
					this.uiManager.selectedShopPack = new ShopPackSecondAnniversaryCurrencyBundleTwo();
				}
				this.uiManager.selectedShopPack.OnPurchaseCompleted();
				World activeWorld8 = this.sim.GetActiveWorld();
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				if (this.uiManager.secondAnniversaryPopup.previousState == UiState.HUB_SHOP)
				{
					this.RainCurrenciesPurchasedFromPackOnUi(activeWorld8);
					this.uiManager.state = UiState.SECOND_ANNIVERSARY_POPUP;
				}
				else
				{
					activeWorld8.RainCredits(this.uiManager.selectedShopPack.credits);
					activeWorld8.RainTokens(this.uiManager.selectedShopPack.tokensMax);
					activeWorld8.RainScraps(this.uiManager.selectedShopPack.scrapsMax);
					this.uiManager.state = UiState.NONE;
				}
				this.sim.ResetShopPackOffer();
			}
			else if (IapManager.inst.boughtProductIndex >= 0)
			{
				int boughtProductIndex = IapManager.inst.boughtProductIndex;
				PlayerStats.BoughtIAP(IapManager.inst.boughtProductIndex);
				IapManager.inst.boughtProductIndex = -1;
				if (this.uiManager.panelShop.isHubMode)
				{
					DropPosition dp = new DropPosition
					{
						startPos = this.uiManager.panelShop.panelBuyCredits[boughtProductIndex - 1].imageItem.transform.position,
						endPos = this.uiManager.panelShop.panelBuyCredits[boughtProductIndex - 1].imageItem.transform.position + Vector3.down * 0.1f,
						invPos = this.uiManager.panelHubShop.menuShowCurrencyCredits.GetCurrencyTransform().position,
						targetToScaleOnReach = this.uiManager.panelHubShop.menuShowCurrencyCredits.GetCurrencyTransform()
					};
					this.sim.OnIap(boughtProductIndex, true, dp);
				}
				else
				{
					this.sim.OnIap(boughtProductIndex, false, null);
					this.uiManager.state = UiState.NONE;
				}
			}
			foreach (World world in this.sim.GetAllWorlds())
			{
				if (world.shouldSave)
				{
					this.TrySave();
					break;
				}
				if (world.shouldSoftSave)
				{
					this.softSaveTimer += deltaTime;
					if (this.softSaveTimer > Main.SOFT_SAVE_COOLDOWN)
					{
						this.TrySave();
						break;
					}
				}
			}
			SaveLoadManager.timeSinceLastSave += deltaTime;
			if (SaveLoadManager.timeSinceLastSave >= 300f)
			{
				this.TrySave();
			}
            if (PlayfabManager.checkForPlayfabData)
            {
                PlayfabManager.checkForPlayfabData = false;
                this.CheckPlayfabData();
            }
            PlayfabManager.Update();
        }
		catch (Exception ex)
		{
			if (string.IsNullOrEmpty(this.DEBUGerror))
			{
				this.DEBUGerror = "Error at Main.Update:" + System.Environment.NewLine;
				this.DEBUGerror = this.DEBUGerror + ex.Message + System.Environment.NewLine;
				this.DEBUGerror += ex.StackTrace;
			}
			throw ex;
		}
		if (Time.frameCount % 2 == 0)
		{
			ScreenRes currentRes = ScreenRes.GetCurrentRes();
			if (currentRes != this.lastResolution)
			{
				this.renderManager.OnScreenSizeChanged(currentRes);
				this.uiManager.OnScreenSizeChanged(currentRes);
				this.lastResolution = currentRes;
			}
		}
	}

	public void LoadHeroMainAssets(string heroId, Action heroLoadedCallback = null)
	{

		if (this.heroBundles.ContainsKey(heroId))
		{
			return;
		}
		this.heroBundles.Add(heroId, (HeroBundle)null);
		string bundleName = HeroIds.HeroBundleByName[heroId];

		DynamicLoadManager.GetPermanentReferenceToBundle(bundleName, delegate
		{
			DynamicLoadManager.LoadAllAndGetAssetOfType<HeroBundle>(bundleName, delegate(HeroBundle assets)
			{
              
				if (assets == null)
				{
					this.heroBundles.Remove(heroId);
					return;
				}
				SkeletonData skeletonData = null;
				try
				{
					skeletonData = assets.animation.GetSkeletonData(false);
				}
				catch
				{
				}
				if (skeletonData == null)
				{
					this.heroBundles.Remove(heroId);
					return;
				}
				this.heroBundles[heroId] = assets;
				switch (heroId)
				{
				case "HORATIO":
					this.renderManager.OnHoratioAssetsLoaded(assets, skeletonData);
					this.soundManager.OnHoratioMainSoundsLoaded(assets);
					break;
				case "THOUR":
					this.renderManager.OnThourAssetsLoaded(assets, skeletonData);
					this.soundManager.OnThourMainSoundsLoaded(assets);
					break;
				case "IDA":
					this.renderManager.OnIdaAssetsLoaded(assets, skeletonData);
					this.soundManager.OnIdaMainSoundsLoaded(assets);
					break;
				case "KIND_LENNY":
					this.renderManager.OnKindLennyAssetsLoaded(assets, skeletonData);
					this.soundManager.OnKindLennyMainSoundsLoaded(assets);
					break;
				case "DEREK":
					this.renderManager.OnWendleAssetsLoaded(assets, skeletonData);
					this.soundManager.OnDerekMainSoundsLoaded(assets);
					break;
				case "SHEELA":
					this.renderManager.OnSheelaAssetsLoaded(assets, skeletonData);
					this.soundManager.OnSheelaMainSoundsLoaded(assets);
					break;
				case "BOMBERMAN":
					this.renderManager.OnBombermanAssetsLoaded(assets, skeletonData);
					this.soundManager.OnBombermanMainSoundsLoaded(assets);
					break;
				case "SAM":
					this.renderManager.OnSamAssetsLoaded(assets, skeletonData);
					this.soundManager.OnSamMainSoundsLoaded(assets);
					break;
				case "BLIND_ARCHER":
					this.renderManager.OnBlindArcherAssetsLoaded(assets, skeletonData);
					this.soundManager.OnLiaMainSoundsLoaded(assets);
					break;
				case "JIM":
					this.renderManager.OnJimAssetsLoaded(assets, skeletonData);
					this.soundManager.OnJimMainSoundsLoaded(assets);
					break;
				case "TAM":
					this.renderManager.OnTamAssetsLoaded(assets, skeletonData);
					this.soundManager.OnTamMainSoundsLoaded(assets);
					break;
				case "WARLOCK":
					this.renderManager.OnWarlockAssetsLoaded(assets, skeletonData);
					this.soundManager.OnWarlockMainSoundsLoaded(assets);
					break;
				case "GOBLIN":
					this.renderManager.OnGoblinAssetsLoaded(assets, skeletonData);
					this.soundManager.OnGoblinMainSoundsLoaded(assets);
					break;
				case "BABU":
					this.renderManager.OnBabuAssetsLoaded(assets, skeletonData);
					this.soundManager.OnBabuMainSoundsLoaded(assets);
					break;
				case "DRUID":
					this.renderManager.OnDruidAssetsLoaded(assets);
					this.soundManager.OnDruidMainSoundsLoaded(assets);
					break;
				}
				this.UpdateLoadStatus();
				if (heroLoadedCallback != null)
				{
					heroLoadedCallback();
				}
			}, true);
		}, false);
	}

	public void LoadGameAssets()
	{
		this.assetsStartedLoading = true;
		this.assetsLoaded = false;
		foreach (Hero hero in this.sim.GetActiveWorld().heroes)
		{
         
			this.LoadHeroMainAssets(hero.GetId(), (Action)null);
		}
		this.renderManager.LoadGameAssets(this.sim.GetActiveWorld());
		this.soundManager.LoadGameSounds(this.sim.GetActiveWorld());
	}

	public bool AreAllGameAssetsLoaded()
	{
		if (!this.assetsStartedLoading)
		{
			return false;
		}
		if (!this.assetsLoaded)
		{
			this.UpdateLoadStatus();
		}
		return Main.IgnorePendingAssetsLoaded || this.assetsLoaded;
	}

	private void UpdateLoadStatus()
	{
		List<Hero> heroes = this.sim.GetActiveWorld().heroes;
		foreach (Hero hero in heroes)
		{
			string id = hero.GetId();
            if (!this.heroBundles.ContainsKey(id) || this.heroBundles[id] == null)
			{
                this.assetsLoaded = false;
				return;
			}
		}
		HashSet<string> hashSet = new HashSet<string>();
		foreach (Hero hero2 in heroes)
		{
			if (!hashSet.Contains(hero2.GetId()))
			{
                Debug.Log(hero2.GetId());
				hashSet.Add(hero2.GetId());
			}
		}
		this.assetsLoaded = (this.renderManager.AreAllAssetsLoaded(this.sim.GetActiveWorld().heroes) && this.soundManager.AreAllSoundBundlesLoaded(hashSet.Count));
        if (this.assetsLoaded)
		{
			foreach (Hero hero3 in this.sim.GetActiveWorld().heroes)
			{
				if (hero3.needsInitialization)
				{
					hero3.needsInitialization = false;
				}
			}
			if (Main.IgnorePendingAssetsLoaded)
			{
				Main.IgnorePendingAssetsLoaded = false;
			}
		}
	}

	public void UnloadHeroMainAssets(string heroId)
	{
		if (!this.heroBundles.ContainsKey(heroId) || this.sim.GetActiveWorld().heroes.Find((Hero hero) => hero.GetId() == heroId) != null)
		{
			return;
		}
		DynamicLoadManager.RemovePermanentReferenceToBundle(HeroIds.HeroBundleByName[heroId]);
		this.heroBundles.Remove(heroId);
		switch (heroId)
		{
		case "HORATIO":
			this.soundManager.UnloadHoratioSounds();
			break;
		case "THOUR":
			this.soundManager.UnloadThourSounds();
			break;
		case "IDA":
			this.soundManager.UnloadIdaSounds();
			break;
		case "KIND_LENNY":
			this.soundManager.UnloadKindLennySounds();
			break;
		case "DEREK":
			this.soundManager.UnloadDerekSounds();
			break;
		case "SHEELA":
			this.soundManager.UnloadSheelaSounds();
			break;
		case "BOMBERMAN":
			this.soundManager.UnloadBombermanSounds();
			break;
		case "SAM":
			this.soundManager.UnloadSamSounds();
			break;
		case "BLIND_ARCHER":
			this.soundManager.UnloadLiaSounds();
			break;
		case "JIM":
			this.soundManager.UnloadJimSounds();
			break;
		case "TAM":
			this.soundManager.UnloadTamSounds();
			break;
		case "WARLOCK":
			this.soundManager.UnloadWarlockSounds();
			break;
		case "GOBLIN":
			this.soundManager.UnloadGoblinSounds();
			break;
		}
		DynamicLoadManager.UnloadUnusedBundles();
	}

	public void UnloadInGameAssets()
	{
		this.renderManager.UnloadGameAssets();
		this.soundManager.UnloadGameSounds();
		this.soundManager.UnloadGameModesVoices();
		foreach (string key in this.heroBundles.Keys)
		{
			DynamicLoadManager.RemovePermanentReferenceToBundle(HeroIds.HeroBundleByName[key]);
		}
		this.heroBundles.Clear();
		this.assetsStartedLoading = false;
		this.assetsLoaded = false;
		Main.IgnorePendingAssetsLoaded = false;
		DynamicLoadManager.UnloadUnusedBundles();
	}

	public void UnloadHeroesThatAreNotInGame()
	{
		if (this.uiManager.IsInHubMenus())
		{
			this.UnloadInGameAssets();
		}
		else
		{
			List<string> list = new List<string>();
			using (Dictionary<string, HeroBundle>.KeyCollection.Enumerator enumerator = this.heroBundles.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string heroId = enumerator.Current;
					if (this.sim.GetActiveWorld().heroes.Find((Hero h) => h.GetId() == heroId) == null)
					{
						list.Add(heroId);
					}
				}
			}
			foreach (string key in list)
			{
				DynamicLoadManager.RemovePermanentReferenceToBundle(HeroIds.HeroBundleByName[key]);
				this.heroBundles.Remove(key);
			}
		}
	}

	public static RenderManager.Background GetCurrentBackgroundAssets()
	{
		DynamicLoadManager.GetPermanentReferenceToBundle(RenderManager.BackgroundBundleNames[Main.instance.renderManager.worldBackground.bundleIndex], delegate
		{
		}, false);
		return Main.instance.renderManager.worldBackground;
	}

	private void RainCurrenciesPurchasedFromPackOnUi(World activeWorld)
	{
		if (this.uiManager.selectedShopPack.credits > 0.0)
		{
			DropPosition dropPos = new DropPosition
			{
				startPos = default(Vector3),
				endPos = Vector3.down * 0.1f,
				invPos = this.uiManager.panelHubShop.menuShowCurrencyCredits.GetCurrencyTransform().position,
				targetToScaleOnReach = this.uiManager.panelHubShop.menuShowCurrencyCredits.GetCurrencyTransform()
			};
			activeWorld.RainCurrencyOnUi(UiState.HUB_SHOP, CurrencyType.GEM, this.uiManager.selectedShopPack.credits, dropPos, 30, 0f, 0f, 1f, null, 0f);
		}
		int num = 0;
		if (this.uiManager.selectedShopPack.candies > 0.0)
		{
			DropPosition dropPos2 = new DropPosition
			{
				startPos = default(Vector3),
				endPos = Vector3.down * 0.1f,
				invPos = this.uiManager.panelCurrencyOnTop[num].panelCurrency.GetCurrencyTransform().position - Vector3.right * 0.425f,
				showSideCurrency = true,
				targetToScaleOnReach = this.uiManager.panelCurrencyOnTop[num].panelCurrency.GetCurrencyTransform()
			};
			activeWorld.RainCurrencyOnUi(UiState.HUB_SHOP, CurrencyType.CANDY, this.uiManager.selectedShopPack.candies, dropPos2, 30, 0f, 0f, 1f, null, 0f);
		}
		if (this.uiManager.selectedShopPack.tokensMax > 0.0)
		{
			DropPosition dropPos3 = new DropPosition
			{
				startPos = default(Vector3),
				endPos = Vector3.down * 0.1f,
				invPos = this.uiManager.panelCurrencyOnTop[num].panelCurrency.GetCurrencyTransform().position - Vector3.right * 0.425f,
				showSideCurrency = true,
				targetToScaleOnReach = this.uiManager.panelCurrencyOnTop[num].panelCurrency.GetCurrencyTransform()
			};
			activeWorld.RainCurrencyOnUi(UiState.HUB_SHOP, CurrencyType.TOKEN, this.uiManager.selectedShopPack.tokensMax, dropPos3, 30, 0f, 0f, 1f, null, 0f);
			num++;
		}
		if (this.uiManager.selectedShopPack.scrapsMax > 0.0)
		{
			DropPosition dropPos4 = new DropPosition
			{
				startPos = default(Vector3),
				endPos = Vector3.down * 0.1f,
				invPos = this.uiManager.panelCurrencyOnTop[num].panelCurrency.GetCurrencyTransform().position - Vector3.right * 0.425f,
				showSideCurrency = true,
				targetToScaleOnReach = this.uiManager.panelCurrencyOnTop[num].panelCurrency.GetCurrencyTransform()
			};
			activeWorld.RainCurrencyOnUi(UiState.HUB_SHOP, CurrencyType.SCRAP, this.uiManager.selectedShopPack.scrapsMax, dropPos4, 30, 0f, 0f, 1f, null, 0f);
		}
	}

	private void OnGUI()
	{
	}

	private bool IsPositionOnUi(Vector3 screenPosition)
	{
		if (this.graphicRaycaster == null || EventSystem.current == null)
		{
			return false;
		}
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = screenPosition;
		List<RaycastResult> list = new List<RaycastResult>();
		this.graphicRaycaster.Raycast(pointerEventData, list);
		if (list.Count > 0)
		{
			return true;
		}
		if (this.uiManager != null && this.uiManager.currentGraphicRaycasters != null)
		{
			foreach (GraphicRaycaster graphicRaycaster in this.uiManager.currentGraphicRaycasters)
			{
				if (!(graphicRaycaster == null))
				{
					graphicRaycaster.Raycast(pointerEventData, list);
					if (list.Count > 0)
					{
						return true;
					}
				}
			}
			return false;
		}
		return false;
	}

	private void UpdateTaps()
	{
		if (this.taps == null)
		{
			this.taps = new Taps();
		}
		else
		{
			this.taps.Reset();
		}
		int touchCount = UnityEngine.Input.touchCount;
		bool flag = false;
		for (int i = 0; i < touchCount; i++)
		{
			flag = true;
			Touch touch = UnityEngine.Input.GetTouch(i);
			if (touch.phase == TouchPhase.Began)
			{
				bool flag2 = this.IsPositionOnUi(touch.position);
				Taps.AddTouchOnUi(touch.fingerId, flag2);
				if (!flag2 && !GameMath.GetProbabilityOutcome(this.autoClickerDetection, GameMath.RandType.NoSeed))
				{
					Vector3 newPos = GameMath.ConvertPointerPosToGameSpace(touch.position);
					this.taps.AddNewSimPos(newPos);
					PlayerStats.OnTap();
					if (this.autoClickerDetection < 1f)
					{
						this.autoClickerDetection += 0.2f;
					}
				}
			}
			else if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && !this.IsPositionOnUi(touch.position) && !Taps.WasTouchOnUi(touch.fingerId))
			{
				Vector3 newPos2 = GameMath.ConvertPointerPosToGameSpace(touch.position);
				this.taps.AddOldSimPos(newPos2);
			}
		}
		if (!flag)
		{
			Taps.ResetTouchStartedOnUi();
		}
		if (this.taps.HasNone())
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 mousePosition = UnityEngine.Input.mousePosition;
				bool flag3 = this.IsPositionOnUi(mousePosition);
				Taps.mouseStartedOnUi = flag3;
				if (!flag3 && !GameMath.GetProbabilityOutcome(this.autoClickerDetection, GameMath.RandType.NoSeed))
				{
					Vector3 newPos3 = GameMath.ConvertPointerPosToGameSpace(mousePosition);
					this.taps.AddNewSimPos(newPos3);
					PlayerStats.OnTap();
					this.autoClickerDetection += 0.2f;
				}
			}
			else if (Input.GetMouseButton(0) && !Taps.mouseStartedOnUi)
			{
				Vector3 mousePosition2 = UnityEngine.Input.mousePosition;
				if (!this.IsPositionOnUi(mousePosition2))
				{
					Vector3 newPos4 = GameMath.ConvertPointerPosToGameSpace(mousePosition2);
					this.taps.AddOldSimPos(newPos4);
				}
			}
		}
		this.autoClickerDetection = Mathf.Max(0f, this.autoClickerDetection - 0.1f);
	}

	private void UpdateSim(float dt)
	{
		if (Main.hardReset)
		{
			Main.hardReset = false;
			PlayfabManager.SaveAsOld(SaveLoadManager.GetJsonOfCurrentState(this.sim, Main.hasEverHadPlayfabTime, Main.lastOfflineEarnedTime));
			PlayerPrefs.DeleteAll();
			PlayerStats.ResetSoft();
			Main.hasEverHadPlayfabTime = false;
			Main.lastOfflineEarnedTime = DateTime.MaxValue;
			this.sim = null;
			this.LoadSim(null, 0f, false, false);
			this.uiManager.state = UiState.NONE;
			TutorialManager.Reset();
			SaveLoadManager.Save(this.sim, Main.hasEverHadPlayfabTime, Main.lastOfflineEarnedTime);
			
		}
		UiCommand command = this.uiManager.GetCommand();
		if (command != null)
		{
			command.Apply(this);
		}
		float num = 0f;
		float num2 = 1f;
		if (dt < num)
		{
			dt = num;
		}
		if (dt > num2)
		{
			dt = num2;
		}
		this.sim.Update(dt, this.taps, command);
	}

	private void UpdateRender()
	{
		if (this.uiManager.IsUiCoveringGame())
		{
			this.renderManager._sceneRenderers.SetActive(false);
			this.renderManager.RenderWhileUiCovering(this.sim, this.sim.GetActiveWorld(), !this.uiManager.IsInHubMenus());
		}
		else
		{
			bool flag = this.uiManager.IsInHubMenus();
			bool flag2 = this.AreAllGameAssetsLoaded();
			if (!flag2 && !this.assetsStartedLoading && !flag && !this.uiManager.loadingTransition.IsPlaying())
			{
				this.LoadGameAssets();
			}
			if (flag2)
			{
				this.renderManager._sceneRenderers.SetActive(true);
				this.renderManager.Render(this.sim, this.sim.GetActiveWorld());
			}
			else
			{
				this.renderManager._sceneRenderers.SetActive(false);
				this.renderManager.RenderWhileUiCovering(this.sim, this.sim.GetActiveWorld(), !flag);
			}
		}
	}

	private void UpdateUi(float dt, Simulator sim)
	{
		this.uiManager.UpdateUi(dt, sim, this.taps);
	}

	public void ChangeUiState(UiState uiState)
	{
		this.uiManager.state = uiState;
	}

	private void UpdateSound(float dt)
	{
		if (this.sim.setSoundsMute != this.soundManager.muteSounds)
		{
			this.ToggleSound();
		}
		if (this.sim.setMusicMute != this.soundManager.muteMusic)
		{
			this.ToggleMusic();
		}
		if (this.sim.setVoicesMute != this.soundManager.muteVoices)
		{
			this.ToggleVoices();
		}
		if (!this.soundManager.muteSounds || !this.soundManager.muteVoices)
		{
			if (!this.uiManager.IsInHubMenus())
			{
				foreach (SoundEvent soundEvent in this.sim.GetActiveWorldSounds())
				{
					if (soundEvent.IsCancel || (soundEvent.IsVoice && !this.soundManager.muteVoices) || (!soundEvent.IsVoice && !this.soundManager.muteSounds))
					{
						this.soundManager.ApplyEvent(soundEvent);
					}
				}
			}
			foreach (SoundEvent soundEvent2 in UiManager.sounds)
			{
				if (soundEvent2.IsCancel || (soundEvent2.IsVoice && !this.soundManager.muteVoices) || (!soundEvent2.IsVoice && !this.soundManager.muteSounds))
				{
					this.soundManager.ApplyEvent(soundEvent2);
				}
			}
		}
		this.sim.ClearSounds();
		UiManager.sounds.Clear();
		float volumeGameplay = 1f;
		if (this.uiManager.IsAnyUiShown())
		{
			volumeGameplay = 0.1f;
		}
		this.soundManager.SetVolumeGameplay(volumeGameplay);
		float dt2 = dt * this.sim.GetActiveWorldTotalTimeSpeedFactor();
		this.soundManager.UpdateSound(dt2);
	}

	public void ToggleSound()
	{
		this.soundManager.muteSounds = !this.soundManager.muteSounds;
		if (this.soundManager.muteSounds)
		{
			this.soundManager.CancelSoundEffects();
		}
		this.sim.setSoundsMute = this.soundManager.muteSounds;
	}

	public void ToggleVoices()
	{
		this.soundManager.muteVoices = !this.soundManager.muteVoices;
		if (this.soundManager.muteVoices)
		{
			this.soundManager.CancelVoices();
		}
		this.sim.setVoicesMute = this.soundManager.muteVoices;
	}

	public void ToggleMusic()
	{
		this.soundManager.muteMusic = !this.soundManager.muteMusic;
		if (this.soundManager.muteMusic)
		{
			this.soundManager.CancelBy("music");
			this.soundManager.CancelBy("musicBoss");
		}
		else
		{
			this.soundManager.PlayMusic(this.uiManager, this.sim);
		}
		this.sim.setMusicMute = this.soundManager.muteMusic;
	}

	public void TryBuyingLowestCostHero(World world)
	{
		double num = double.MaxValue;
		Hero hero = null;
		for (int i = 0; i < world.heroes.Count; i++)
		{
			if (world.heroes[i].GetUpgradeCost(true) < num)
			{
				num = world.heroes[i].GetUpgradeCost(true);
				hero = world.heroes[i];
			}
		}
		if (hero != null)
		{
			world.TryUpgradeHero(hero);
		}
	}

	public void TryLevelingSkillOfRandomHero(World w)
	{
		for (int i = 0; i < w.heroes.Count; i++)
		{
			if (w.heroes[i].GetNumUnspentSkillPoints() > 0)
			{
				Hero hero = w.heroes[i];
				SkillTree skillTree = w.heroes[i].GetSkillTree();
				if (hero.CanUpgradeSkillUlti())
				{
					hero.TryUpgradeSkillUlti();
				}
				else
				{
					List<Vector2> list = new List<Vector2>();
					for (int j = 0; j < skillTree.branches.Length; j++)
					{
						for (int k = 0; k < skillTree.branches[j].Length; k++)
						{
							if (hero.CanUpgradeSkill(j, k))
							{
								list.Add(new Vector2((float)j, (float)k));
							}
						}
					}
					if (list.Count > 0)
					{
						Vector2 vector = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
						hero.TryUpgradeSkill((int)vector.x, (int)vector.y);
						break;
					}
				}
			}
		}
	}

	public void ConvertSaveDataToCurrentVersion(SaveData saveData)
	{
	}

	public DateTime GetLastCappedCurrencyWatchedTime(CurrencyType currencyType)
	{
		return (this.sim == null) ? DateTime.MaxValue : this.sim.GetLastCappedCurrencyWatchedTime(currencyType);
	}

	public void TakeGameScreenshot(RectTransform screenshotRect, Action<Texture2D> onComplete)
	{
		base.StartCoroutine(this.TakeScreenshot(this.renderManager.mainCam, screenshotRect, this.uiManager.canvas, false, onComplete));
	}

	private IEnumerator TakeScreenshot(Camera camera, RectTransform screenshotRect, Canvas canvas, bool disableCameraAfter, Action<Texture2D> onComplete)
	{
		yield return null;
		this.renderManager._sceneRenderers.SetActive(false);
		this.renderManager._uiSceneRenderers.SetActive(false);
		Texture2D snapShot = new Texture2D((int)(screenshotRect.sizeDelta.x * canvas.scaleFactor * 0.99f), (int)(screenshotRect.sizeDelta.y * canvas.scaleFactor * 0.99f), TextureFormat.ARGB32, false);
		RenderTexture snapShotRT = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
		RenderTexture.active = snapShotRT;
		camera.targetTexture = snapShotRT;
		if (disableCameraAfter)
		{
			camera.enabled = true;
		}
		camera.Render();
		float pos = screenshotRect.anchoredPosition.y;
		Rect lassoRectSS = new Rect((float)((int)((float)(Screen.width - snapShot.width) * 0.5f)), (float)((int)((float)(Screen.height - snapShot.height) * 0.5f + pos * canvas.scaleFactor)), (float)snapShot.width, (float)snapShot.height);
		snapShot.ReadPixels(lassoRectSS, 0, 0);
		snapShot.Apply();
		RenderTexture.active = null;
		camera.targetTexture = null;
		if (disableCameraAfter)
		{
			camera.enabled = false;
		}
		onComplete(snapShot);
		yield break;
	}

	[SerializeField]
	private UiManager uiManager;

	[SerializeField]
	private RenderManager renderManager;

	[SerializeField]
	private SoundManager soundManager;

	[SerializeField]
	private GraphicRaycaster graphicRaycaster;

	[SerializeField]
	private ParsedLoc parsedLoc;

	public static int SLEEP_TIMEOUT = 60;

	public static Main instance;

	private Simulator sim;

	private Taps taps = new Taps();

	private KeyStroke keyStroke;

	public static long localSaveDataLifetimeInTicks = 0L;

	public static bool hasEverHadPlayfabTime = false;

	public static DateTime lastOfflineEarnedTime = DateTime.MaxValue;

	public static bool forceStoreUpdate = false;

	public static bool GoToGameAfterAllAssetsLoaded = false;

	public static bool IgnorePendingAssetsLoaded = false;

	private static int FramesToWaitForTransition = 10;

	private string DEBUGerror;

	private bool justTriedSaving;

	public static MonoBehaviour coroutineObject;

	public static Camera camera;

	public static SaveData saveDataToLoad;

	public static PlayfabManager.RewardData rewardToClaim;

	private static Queue<PlayfabManager.NewsData> pendingNews;

	private static List<PlayfabManager.NewsData> availableNewsWaitingForStageFilter;

	public static float DEBUGofflineDuration = -1f;

	public static bool playerOpenedTheGameFirstTime;

	public static bool dontTryAuth;

	public static bool dontAskForAuthenticatingPopup;

	public static bool dontStoreAuthenticate;

	private ScreenRes lastResolution;

	public bool notificationOpened;

	private bool assetsStartedLoading;

	private bool assetsLoaded;

	public Dictionary<string, HeroBundle> heroBundles = new Dictionary<string, HeroBundle>();

	private GUIStyle DEBUGguiStyle;

	private float autoClickerDetection;

	private float autoPlayFightBossTimer = 180f;

	public static bool hardReset;

	private float softSaveTimer;

	public static float SOFT_SAVE_COOLDOWN = 5f;
}
