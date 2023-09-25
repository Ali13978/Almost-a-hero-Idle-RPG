using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using DynamicLoading;
using Render;
using Simulation;
using Spine.Unity;
using Static;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelShareScreenshot : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.shareButton.onClick = new GameButton.VoidFunc(this.ShareScreenshot);
		}

		public void InitStrings()
		{
			this.shareButton.text.text = LM.Get("UI_SHARE");
		}

		public void OnClosed()
		{
			DynamicLoadManager.RemovePermanentReferenceToBundle(this.backgroundBundleName);
			IEnumerator enumerator = this.background.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public void UpdateScreenshot(Simulator simulator, UiManager uiManager, RenderManager.Background background)
		{
			this.flashCurtain.gameObject.SetActive(true);
			this.flashCurtain.SetAlpha(1f);
			this.flashCurtain.DOFade(0f, 0.5f).SetEase(Ease.InCubic).OnComplete(delegate
			{
				this.flashCurtain.gameObject.SetActive(false);
			});
			this.screenshotParent.SetScale(1.6f);
			this.screenshotParent.rotation = Quaternion.Euler(0f, 0f, 15f);
			Sequence s = DOTween.Sequence();
			s.Append(this.screenshotParent.DORotate(new Vector3(0f, 0f, 0f), 0.4f, RotateMode.Fast)).Join(this.screenshotParent.DOScale(1f, 0.4f).SetEase(Ease.InCubic)).Append(this.screenshotParent.DOShakeAnchorPos(0.3f, 20f, 20, 90f, false, true)).Play<Sequence>();
			this.backgroundBundleName = RenderManager.BackgroundBundleNames[background.bundleIndex];
			for (int i = 0; i < background.animations.backgroundAnimations.Length; i++)
			{
				SkeletonGraphic skeletonGraphic = new GameObject().AddComponent<SkeletonGraphic>();
				skeletonGraphic.transform.SetParent(this.background.transform, false);
				Vector2 vector = background.animations.backgroundAnimations[i].skeletonAnimation.transform.localPosition;
				Vector2 anchoredPosition = new Vector2(GameMath.Lerp(-512f, 512f, Mathf.InverseLerp(-1f, 1f, vector.x)), GameMath.Lerp(-512f, 512f, Mathf.InverseLerp(-1.33f, 1.33f, vector.y)));
				skeletonGraphic.rectTransform.anchoredPosition = anchoredPosition;
				skeletonGraphic.skeletonDataAsset = background.animations.backgroundAnimations[i].skeletonAnimation.SkeletonDataAsset;
				skeletonGraphic.material = this.spineMaterial;
				skeletonGraphic.startingAnimation = background.animations.animationName;
				skeletonGraphic.startingLoop = true;
				skeletonGraphic.initialSkinName = background.animations.backgroundAnimations[i].skeletonAnimation.SkeletonDataAsset.GetSkeletonData(true).Skins.Items[background.skinIndex].Name;
				skeletonGraphic.Initialize(true);
				skeletonGraphic.AnimationState.TimeScale = 0f;
			}
			this.background.sprite = background.image.sprite;
			List<Hero> list = simulator.GetActiveWorld().heroes;
			int count = list.Count;
			List<Vector2> list2 = new List<Vector2>();
			float num = 180f;
			float num2 = 6.28318548f / (float)count;
			float num3 = 0f;
			if (count == 4)
			{
				num3 = 0.7853982f;
				num = 180f;
			}
			else if (count == 2)
			{
				num3 = 1.57079637f;
				num = 130f;
			}
			else if (count == 3)
			{
				num3 = 1.04719651f;
				num = 150f;
			}
			if (count == 1)
			{
				list2.Add(new Vector2(0f, -300f));
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					list2.Add(new Vector2(Mathf.Sin(num3 + num2 * (float)j) * num, Mathf.Cos(num3 + num2 * (float)j) * num - 280f));
				}
				list2.Shuffle<Vector2>();
			}
			List<HeroAnimation> list3 = this.heroes.ToList<HeroAnimation>();
			List<Vector2> list4 = list2.ToList<Vector2>();
			for (int k = 1; k < count; k++)
			{
				for (int l = 0; l < count - k; l++)
				{
					if (list4[l].y < list4[l + 1].y)
					{
						Vector2 value = list4[l + 1];
						list4[l + 1] = list4[l];
						list4[l] = value;
						HeroAnimation value2 = list3[l + 1];
						list3[l + 1] = list3[l];
						list3[l] = value2;
					}
				}
			}
			for (int m = 0; m < count; m++)
			{
				list3[m].transform.SetAsLastSibling();
			}
			int num4 = UnityEngine.Random.Range(0, count);
			for (int n = 0; n < this.heroes.Length; n++)
			{
				HeroAnimation heroAnimation = this.heroes[n];
				if (n < count)
				{
					Hero hero = list[n];
					if (num4 == n)
					{
						heroAnimation.PoseHeroAnimation(hero.GetId(), hero.GetEquippedSkinIndex(), "victory_1", false);
					}
					else
					{
						heroAnimation.PoseHeroAnimation(hero.GetId(), hero.GetEquippedSkinIndex(), "victory_1", GameMath.GetProbabilityOutcome(0.6f, GameMath.RandType.NoSeed));
					}
					heroAnimation.gameObject.SetActive(true);
					RectTransform component = heroAnimation.GetComponent<RectTransform>();
					component.anchoredPosition = list2[n];
					component.SetScale(1.1f);
				}
				else
				{
					heroAnimation.gameObject.SetActive(false);
				}
			}
			this.noHeroesImage.SetActive(count == 0);
			World activeWorld = simulator.GetActiveWorld();
			this.gameMode = activeWorld.gameMode;
			GameMode gameMode = this.gameMode;
			if (gameMode != GameMode.STANDARD)
			{
				if (gameMode != GameMode.CRUSADE)
				{
					if (gameMode == GameMode.RIFT)
					{
						int num5 = activeWorld.GetActiveChallengeIndex() + 1;
						bool flag = activeWorld.IsActiveChallengeCursed();
						this.cursesParent.SetActive(flag);
						if (flag)
						{
							List<CurseEffectData> activeCurseEffects = (activeWorld.activeChallenge as ChallengeRift).activeCurseEffects;
							int count2 = activeCurseEffects.Count;
							if (count2 != 1)
							{
								if (count2 != 2)
								{
									this.oneCurse.enabled = false;
									this.twoCursesSetUp.SetActive(false);
									this.severalCursesSetUp.SetActive(true);
									for (int num6 = 0; num6 < this.severalCursesSetUpImages.Length; num6++)
									{
										if (num6 < count2)
										{
											this.severalCursesSetUpImages[num6].enabled = true;
											this.severalCursesSetUpImages[num6].sprite = uiManager.spritesCurseEffectIcon[activeCurseEffects[num6].baseData.id];
											this.severalCursesSetUpImages[num6].color = ((activeCurseEffects[num6].baseData.id != 1019) ? this.normalCurseColor : this.chaoticCurseColor);
										}
										else
										{
											this.severalCursesSetUpImages[num6].enabled = false;
										}
									}
								}
								else
								{
									this.oneCurse.enabled = false;
									this.twoCursesSetUp.SetActive(true);
									this.severalCursesSetUp.SetActive(false);
									this.twoCursesSetUpImages[0].sprite = uiManager.spritesCurseEffectIcon[activeCurseEffects[0].baseData.id];
									this.twoCursesSetUpImages[1].sprite = uiManager.spritesCurseEffectIcon[activeCurseEffects[1].baseData.id];
									this.twoCursesSetUpImages[0].color = ((activeCurseEffects[0].baseData.id != 1019) ? this.normalCurseColor : this.chaoticCurseColor);
									this.twoCursesSetUpImages[1].color = ((activeCurseEffects[1].baseData.id != 1019) ? this.normalCurseColor : this.chaoticCurseColor);
								}
							}
							else
							{
								this.oneCurse.enabled = true;
								this.twoCursesSetUp.SetActive(false);
								this.severalCursesSetUp.SetActive(false);
								this.oneCurse.sprite = uiManager.spritesCurseEffectIcon[activeCurseEffects[0].baseData.id];
								this.oneCurse.color = ((activeCurseEffects[0].baseData.id != 1019) ? this.normalCurseColor : this.chaoticCurseColor);
							}
						}
						ChallengeWithTime challengeWithTime = activeWorld.activeChallenge as ChallengeWithTime;
						TimeSpan time = new TimeSpan((long)challengeWithTime.timeCounter * 10000000L);
						string arg = AM.cs(num5.ToString(), this.statsColor);
						string arg2 = AM.cs(GameMath.GetLocalizedTimeString(time), this.statsColor);
						string arg3 = AM.cs(activeWorld.activeChallenge.GetWaveProgress(), this.statsColor);
						StringBuilder stringBuilder = StringExtension.StringBuilder;
						stringBuilder.AppendFormat(LM.Get((!flag) ? "UI_SHARING_GOG" : "UI_SHARING_CURSED"), arg).Append("\n").AppendFormat(LM.Get("UI_SHARING_WAVE"), arg3).Append("\n").AppendFormat(LM.Get("UI_SHARING_TIME"), arg2);
						this.stats.text = stringBuilder.ToString();
						if (challengeWithTime.state == Challenge.State.WON)
						{
							this.shareScreenshotText = string.Format(LM.Get("SHARE_SCREENSHOT_END"), LM.Get((!activeWorld.IsActiveChallengeCursed()) ? "UI_GATE" : "UI_CURSED_GATE"), num5, GameMath.GetTimeDatailedShortenedString(new TimeSpan((long)(challengeWithTime.dur - challengeWithTime.timeCounter) * 10000000L)));
						}
						else
						{
							this.shareScreenshotText = string.Format(LM.Get("SHARE_SCREENSHOT_MID_TIME_GATE"), LM.Get((!activeWorld.IsActiveChallengeCursed()) ? "UI_GATE" : "UI_CURSED_GATE"), num5);
						}
					}
				}
				else
				{
					this.cursesParent.SetActive(false);
					ChallengeWithTime challengeWithTime2 = activeWorld.activeChallenge as ChallengeWithTime;
					int num7 = activeWorld.GetActiveChallengeIndex() + 1;
					TimeSpan time2 = new TimeSpan((long)challengeWithTime2.timeCounter * 10000000L);
					string arg4 = AM.cs(num7.ToString(), this.statsColor);
					string arg5 = AM.cs(activeWorld.activeChallenge.GetWaveProgress(), this.statsColor);
					string arg6 = AM.cs(GameMath.GetLocalizedTimeString(time2), this.statsColor);
					StringBuilder stringBuilder2 = StringExtension.StringBuilder;
					stringBuilder2.AppendFormat(LM.Get("UI_TIME_CHALLENGE_STAGE"), arg4).Append("\n").AppendFormat(LM.Get("UI_SHARING_WAVE"), arg5).Append("\n").AppendFormat(LM.Get("UI_SHARING_TIME"), arg6);
					this.stats.text = stringBuilder2.ToString();
					if (challengeWithTime2.state == Challenge.State.WON)
					{
						this.shareScreenshotText = string.Format(LM.Get("SHARE_SCREENSHOT_END"), LM.Get("UI_CHALLENGE"), num7, GameMath.GetTimeDatailedShortenedString(new TimeSpan((long)(challengeWithTime2.dur - challengeWithTime2.timeCounter) * 10000000L)));
					}
					else
					{
						this.shareScreenshotText = string.Format(LM.Get("SHARE_SCREENSHOT_MID_TIME_GATE"), LM.Get("UI_CHALLENGE"), num7);
					}
				}
			}
			else
			{
				this.cursesParent.SetActive(false);
				TimeSpan time3 = new TimeSpan((long)simulator.prestigeRunTimer * 10000000L);
				string arg7 = AM.cs(activeWorld.GetStageNumber().ToString(), this.statsColor);
				string arg8 = AM.cs(GameMath.GetLocalizedTimeString(time3), this.statsColor);
				this.stats.text = StringExtension.Concat(LM.Get("UI_ADVENTURE"), "\n", string.Format(LM.Get("UI_SHARING_STAGE"), arg7), "\n", string.Format(LM.Get("UI_SHARING_TIME"), arg8));
				this.shareScreenshotText = string.Format(LM.Get("SHARE_SCREENSHOT_MID_ADVENTURE"), activeWorld.GetStageNumber());
			}
			if (activeWorld.totem != null)
			{
				this.ring.sprite = uiManager.GetSpriteTotemSmall(activeWorld.totem.id);
				this.ring.enabled = true;
			}
			else
			{
				this.ring.enabled = false;
			}
			UiManager.AddUiSound(SoundArchieve.inst.screenshot);
		}

		private void ShareScreenshot()
		{
			this.backButton.gameObject.SetActive(false);
			Main.instance.TakeGameScreenshot(this.screenshotParent, delegate(Texture2D screenshot)
			{
				this.backButton.gameObject.SetActive(true);
				string text = Path.Combine(Application.temporaryCachePath, "shared img.png");
				File.WriteAllBytes(text, screenshot.EncodeToPNG());
				new NativeShare().AddFile(text, null).SetSubject(LM.Get("SHARE_SCREENSHOT_SUBJECT")).SetText(this.shareScreenshotText).Share();
				PlayerStats.ScreenshotShared(this.gameMode);
			});
		}

		public GameButton backButton;

		public RectTransform birthdayDecorations;

		[NonSerialized]
		public bool backToModePanel;

		[SerializeField]
		private RectTransform screenshotParent;

		[SerializeField]
		private GameButton shareButton;

		[SerializeField]
		private Image background;

		[SerializeField]
		private Text stats;

		[SerializeField]
		private Image ring;

		[SerializeField]
		private HeroAnimation[] heroes;

		[SerializeField]
		private Material spineMaterial;

		[SerializeField]
		private GameObject cursesParent;

		[SerializeField]
		private Image oneCurse;

		[SerializeField]
		private Image flashCurtain;

		[SerializeField]
		private GameObject twoCursesSetUp;

		[SerializeField]
		private Image[] twoCursesSetUpImages;

		[SerializeField]
		private GameObject severalCursesSetUp;

		[SerializeField]
		private Image[] severalCursesSetUpImages;

		[SerializeField]
		private Color statsColor;

		[SerializeField]
		private GameObject noHeroesImage;

		[SerializeField]
		private Color normalCurseColor;

		[SerializeField]
		private Color chaoticCurseColor;

		private string shareScreenshotText;

		private string backgroundBundleName;

		private GameMode gameMode;
	}
}
