using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Simulation;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHeroEvolveSkin : AahMonoBehaviour
	{
		public RectTransform rectTransform
		{
			get
			{
				RectTransform result;
				if ((result = this.m_rectTransform) == null)
				{
					result = (this.m_rectTransform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
			for (int i = 0; i < this.skinButtonCount; i++)
			{
				SkinInventoryButton skinInventoryButton = UnityEngine.Object.Instantiate<SkinInventoryButton>(this.skinInventoryButtonPrefab, this.newSkinButtonsParent);
				this.newSkinButtons.Add(skinInventoryButton);
				skinInventoryButton.gameObject.SetActive(false);
				skinInventoryButton.frame.raycastTarget = false;
				skinInventoryButton.rectTransform.localScale = Vector3.one;
				skinInventoryButton.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
				skinInventoryButton.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
				skinInventoryButton.rectTransform.anchoredPosition = new Vector2(900f, 0f);
			}
		}

		public void InitStrings()
		{
			this.infoTextExtraHealth.text = LM.Get("UI_HERO_EVOLVE_HEALTH");
			this.infoTextExtraDamage.text = LM.Get("UI_HERO_EVOLVE_DAMAGE");
			this.warningTextHeroEvolved.text = LM.Get("UI_HERO_EVOLVED");
			this.warningTextNewSkin.text = LM.Get("UI_NEW_SKINS");
		}

		public void Update()
		{
			if (this.playStartAnim)
			{
				this.playStartAnim = false;
				this.DoPutSkinButton(this.ttt, this.uiManager.sim);
			}
			if (this.playEvolveAnim)
			{
				this.playEvolveAnim = false;
			}
		}

		public void DoEvolve(string heroName, int currentSkinIndex, int currentLevel)
		{
			this.isEvolve = true;
			this.skinRevealingCounter = 0;
			this.currentLevel = currentLevel;
			this.evolveInfosParent.localScale = Vector3.zero;
			this.warningTextHeroEvolvedParent.localScale = Vector3.zero;
			this.evolveStars.SetNumberOfStars(currentLevel, 6);
			double percentageTexts = GameMath.PowInt(1.5, currentLevel) - 1.0;
			this.SetPercentageTexts(percentageTexts);
			this.DoStart(heroName, currentSkinIndex, currentLevel + 2);
			for (int i = 0; i < this.skinButtonCount; i++)
			{
				SkinInventoryButton skinInventoryButton = this.newSkinButtons[i];
				skinInventoryButton.gameObject.SetActive(false);
			}
			this.SetSkinButtons();
			if (this.skinsToBeShowed.Count > 1)
			{
				this.warningTextNewSkin.text = LM.Get("UI_NEW_SKINS");
			}
			else
			{
				this.warningTextNewSkin.text = LM.Get("UI_NEW_SKIN");
			}
			this.state = PanelHeroEvolveSkin.State.APPEAR;
		}

		public void DoStart(string heroName, int currentSkinIndex, int targetSkinIndex)
		{
			this.spineEvolveEffect.gameObject.SetActive(false);
			this.isAnimating = true;
			this.state = PanelHeroEvolveSkin.State.APPEAR;
			this.evolveInfosParent.gameObject.SetActive(this.isEvolve);
			this.heroTurntable.transform.localScale = Vector3.zero;
			this.warningTextNewSkinParent.localScale = Vector3.zero;
			this.heroTurntableShadow.transform.localScale = new Vector3(1f, 8f, 1f);
			this.backgroundGlow.SetImageAlpha(0f);
			this.heroName = heroName;
			this.targetSkinIndex = targetSkinIndex;
			this.heroAnimation.SetHeroAnimation(heroName, currentSkinIndex, true, false, false, true);
			this.SetHeroBaseDetails(this.currentLevel);
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.heroTurntable.DOScale(1f, 0.3f).SetEase(Ease.OutCubic));
			sequence.Insert(0.1f, this.backgroundGlow.DOFade(1f, 0.5f).SetEase(Ease.OutCubic));
			sequence.Insert(0.1f, this.backgroundTile.DOFade(1f, 0.5f).SetEase(Ease.OutCubic));
			sequence.Insert(0.1f, this.heroTurntableShadow.DOScale(1f, 0.2f).SetEase(Ease.OutCubic));
			if (this.isEvolve)
			{
				sequence.Append(this.evolveInfosParent.DOScale(1f, 0.2f));
			}
			sequence.AppendCallback(delegate
			{
				this.isAnimating = false;
				this.state = PanelHeroEvolveSkin.State.WAITING_EVOLVE;
			});
			sequence.Play<Sequence>();
			this.currentTween = sequence;
		}

		public void DoSkinTransform(Simulator simulator)
		{
			this.isAnimating = true;
			this.heroMaterial = this.heroAnimation.skeletonGraphic.material;
			Spine.Animation eStartAnim = this.heroAnimation.skeletonGraphic.SkeletonData.FindAnimation(this.evolveStartAnim);
			Spine.Animation eEndAnim = this.heroAnimation.skeletonGraphic.SkeletonData.FindAnimation(this.evolveEndAnim);
			Spine.Animation idAnim = this.heroAnimation.skeletonGraphic.SkeletonData.FindAnimation(this.idleAnim);
			Spine.Animation evolveFxAnim = this.spineEvolveEffect.SkeletonData.FindAnimation("animation4");
			float num = evolveFxAnim.duration * 0.57f - eStartAnim.duration;
			float atPosition;
			float num2;
			if (num > 0f)
			{
				atPosition = 0f;
				num2 = num;
			}
			else
			{
				atPosition = -num;
				num2 = 0f;
			}
			this.state = PanelHeroEvolveSkin.State.EVOLVING;
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(num2 + eStartAnim.duration);
			sequence.InsertCallback(atPosition, delegate
			{
				this.spineEvolveEffect.AnimationState.ClearTrack(0);
				this.spineEvolveEffect.AnimationState.SetAnimation(0, evolveFxAnim, false);
				this.spineEvolveEffect.gameObject.SetActive(true);
			});
			sequence.InsertCallback(num2, delegate
			{
				this.heroAnimation.skeletonGraphic.AnimationState.SetAnimation(0, eStartAnim, false);
			});
			sequence.AppendCallback(delegate
			{
				if (this.isEvolve)
				{
					this.DoAnimatePercentage();
					this.warningTextHeroEvolvedParent.DOScale(1f, 0.3f).SetEase(Ease.OutCirc).OnComplete(delegate
					{
						this.evolveStars.SetNumberOfStarsAnimatedFancy(this.currentLevel + 1, 6, delegate
						{
							this.rectTransform.DOShakeAnchorPos(0.3f, 10f, 20, 90f, false, true);
						});
					});
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.heroEvolveMain, 1f));
				}
			});
			sequence.Insert(num2 + 0.6f, this.heroMaterial.DOColor(Color.white, "_Black", 0.3f));
			sequence.AppendCallback(delegate
			{
				this.heroAnimation.SetHeroAnimation(this.heroName, this.targetSkinIndex, true, false, false, true);
				this.heroAnimation.skeletonGraphic.AnimationState.SetAnimation(0, eEndAnim, false);
				this.heroAnimation.skeletonGraphic.AnimationState.AddAnimation(0, idAnim, true, 0f);
				this.SetHeroBaseDetails(this.currentLevel + 1);
				if (this.isEvolve)
				{
					this.PlayGreenmanVO(this.targetSkinIndex - 2);
				}
			});
			sequence.AppendInterval(0.2f);
			sequence.Append(this.heroMaterial.DOColor(Color.black, "_Black", 0.25528124f));
			sequence.AppendInterval(eEndAnim.duration - 0.2f);
			sequence.InsertCallback(evolveFxAnim.duration, delegate
			{
				this.spineEvolveEffect.gameObject.SetActive(false);
			});
			sequence.AppendCallback(delegate
			{
				this.isAnimating = false;
			});
			sequence.AppendInterval(0.4f);
			sequence.AppendCallback(delegate
			{
				this.DoStep(simulator);
			});
			sequence.Play<Sequence>();
			this.currentTween = sequence;
		}

		private void SetHeroBaseDetails(int level)
		{
			this.heroPentagram.SetSprite(level);
			this.spineAnimCharBase.AnimationState.SetAnimation(0, "animation", true);
			if (level > 0)
			{
				this.spineAnimCharBase.Skeleton.SetSkin(PanelGearScreen.evolveSkins[level - 1]);
				this.spineAnimCharBase.gameObject.SetActive(true);
			}
			else
			{
				this.spineAnimCharBase.gameObject.SetActive(false);
			}
		}

		private void DoAnimatePercentage()
		{
			double oldPercentage = GameMath.PowInt(1.5, this.currentLevel) - 1.0;
			double num = GameMath.PowInt(1.5, this.currentLevel + 1) - 1.0;
			float duration = GameMath.Clamp((float)(num - oldPercentage), 1f, 3f);
			Sequence sequence = DOTween.Sequence();
			sequence.Append(DOTween.To(() => oldPercentage, delegate(double x)
			{
				oldPercentage = x;
			}, num, duration).OnUpdate(delegate
			{
				this.SetPercentageTexts(oldPercentage);
			})).Append(this.infoTextExtraDamageAmount.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutCubic)).Join(this.infoTextExtraHealthAmount.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutCubic)).AppendInterval(0.2f).AppendInterval(0.2f).Join(this.infoTextExtraDamageAmount.transform.DOScale(1f, 0.2f)).Join(this.infoTextExtraHealthAmount.transform.DOScale(1f, 0.2f));
			sequence.Play<Sequence>();
		}

		private void SetPercentageTexts(double p)
		{
			this.infoTextExtraDamageAmount.text = "+" + GameMath.GetPercentString(p, true);
			this.infoTextExtraHealthAmount.text = "+" + GameMath.GetPercentString(p, true);
		}

		public void PlayGreenmanVO(int evolveLevel)
		{
			AudioClip[] clips = new AudioClip[0];
			if (evolveLevel == 0)
			{
				clips = SoundArchieve.inst.voGreenManEvolveCommon;
			}
			else if (evolveLevel == 1)
			{
				clips = SoundArchieve.inst.voGreenManEvolveUncommon;
			}
			else if (evolveLevel == 2)
			{
				clips = SoundArchieve.inst.voGreenManEvolveRare;
			}
			else if (evolveLevel == 3)
			{
				clips = SoundArchieve.inst.voGreenManEvolveEpic;
			}
			else if (evolveLevel == 4)
			{
				clips = SoundArchieve.inst.voGreenManEvolveLegendary;
			}
			else if (evolveLevel == 5)
			{
				clips = SoundArchieve.inst.voGreenManEvolveMythical;
			}
			UiManager.sounds.Add(new SoundEventUiVariedVoiceSimple(clips, "GREEN_MAN", 1f));
		}

		private Sequence DoPutSkinButton(int skinIndex, Simulator simulator)
		{
			SkinData skinData = this.skinsToBeShowed[skinIndex];
			SkinInventoryButton skinButton = this.newSkinButtons[skinIndex];
			skinButton.gameObject.SetActive(true);
			skinButton.rectTransform.anchoredPosition = new Vector2(900f, 0f);
			int count = this.skinsToBeShowed.Count;
			Vector2 endValue = new Vector2(this.gapBetweenSkins * (float)skinIndex * 0.5f, 0f);
			Sequence sequence = DOTween.Sequence();
			this.skinRevealAnimator.transform.SetParent(skinButton.transform);
			this.skinRevealAnimator.transform.localPosition = Vector3.zero;
			this.skinRevealAnimator.Setup(skinData.unlockType == SkinData.UnlockType.CURRENCY);
			this.skinRevealAnimator.gameObject.SetActive(true);
			sequence.Append(skinButton.rectTransform.DOAnchorPos(endValue, 0.6f, false).SetEase(Ease.OutBack, 0.7f));
			if (this.skinRevealingCounter == 0)
			{
				sequence.Append(this.warningTextNewSkinParent.DOScale(1f, 0.6f).SetEase(Ease.OutElastic, 1.1f, 0.6f));
			}
			for (int i = 0; i < skinIndex; i++)
			{
				SkinInventoryButton skinInventoryButton = this.newSkinButtons[i];
				float endValue2 = endValue.x - this.gapBetweenSkins * (float)(skinIndex - i);
				sequence.Join(skinInventoryButton.rectTransform.DOAnchorPosX(endValue2, 0.6f, false).SetEase(Ease.InOutQuart));
			}
			if (skinData.unlockType == SkinData.UnlockType.HERO_EVOLVE_LEVEL)
			{
				sequence.AppendInterval(0.2f);
				sequence.AppendCallback(delegate
				{
					skinButton.allRenderersParent.gameObject.SetActive(false);
					this.skinRevealAnimator.Play();
					this.SetSkinButton(skinButton, skinData);
				});
				sequence.AppendInterval(0.45f);
				sequence.AppendCallback(delegate
				{
					skinButton.allRenderersParent.gameObject.SetActive(true);
				});
				sequence.AppendInterval(0.45f);
				sequence.AppendCallback(delegate
				{
					this.skinRevealAnimator.transform.SetParent(this.transform);
					this.skinRevealAnimator.gameObject.SetActive(false);
				});
				sequence.AppendInterval(0.4f);
			}
			else if (skinData.unlockType == SkinData.UnlockType.CURRENCY)
			{
				skinButton.priceTagParent.gameObject.SetActive(true);
				skinButton.priceTagIcon.sprite = UiData.inst.currencySprites[(!skinData.isChristmasSkin || !simulator.IsChristmasTreeEnabled()) ? 3 : 6];
				sequence.AppendInterval(0.2f);
				sequence.AppendCallback(delegate
				{
					skinButton.allRenderersParent.gameObject.SetActive(false);
					skinButton.priceTagParent.gameObject.SetActive(false);
					this.skinRevealAnimator.Play();
					this.SetSkinButton(skinButton, skinData);
				});
				sequence.AppendInterval(0.45f);
				sequence.AppendCallback(delegate
				{
					skinButton.allRenderersParent.gameObject.SetActive(true);
				});
				sequence.AppendInterval(0.45f);
				sequence.AppendCallback(delegate
				{
					this.skinRevealAnimator.transform.SetParent(this.transform);
					this.skinRevealAnimator.gameObject.SetActive(false);
					skinButton.priceTagParent.gameObject.SetActive(true);
				});
			}
			this.currentTween = sequence;
			return sequence;
		}

		private void SetSkinButtons()
		{
			for (int i = 0; i < this.skinsToBeShowed.Count; i++)
			{
				SkinData skinData = this.skinsToBeShowed[i];
				this.SetSkinButtonLocked(this.newSkinButtons[i], skinData);
			}
		}

		private void SetSkinButton(SkinInventoryButton b, SkinData skinData)
		{
			b.skinIcon.gameObject.SetActive(true);
			b.skinIcon.sprite = this.uiManager.GetHeroAvatar(skinData);
			b.newBadge.SetActive(false);
			b.frame.sprite = UiData.inst.skinFrameActive;
			b.lockIcon.gameObject.SetActive(false);
		}

		private void SetSkinButtonLocked(SkinInventoryButton b, SkinData skinData)
		{
			b.lockIcon.gameObject.SetActive(true);
			b.skinIcon.gameObject.SetActive(false);
			b.priceTagParent.gameObject.SetActive(false);
			b.frame.sprite = UiData.inst.skinFramePassive;
			b.newBadge.SetActive(false);
		}

		public void DoStep(Simulator simulator)
		{
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.heroEvolveFinish, 1f));
			Sequence t = this.DoPutSkinButton(this.skinRevealingCounter, simulator);
			this.state = PanelHeroEvolveSkin.State.REVEALING_SKINS;
			t.OnComplete(delegate
			{
				this.skinRevealingCounter++;
				if (this.skinRevealingCounter >= this.skinsToBeShowed.Count)
				{
					this.state = PanelHeroEvolveSkin.State.END;
				}
				else
				{
					this.DoStep(simulator);
				}
			});
			this.currentTween = t;
			t.Play<Sequence>();
		}

		private RectTransform m_rectTransform;

		public Text warningTextNewSkin;

		public RectTransform warningTextNewSkinParent;

		public Text warningTextHeroEvolved;

		public RectTransform warningTextHeroEvolvedParent;

		public Text infoTextExtraHealth;

		public Text infoTextExtraHealthAmount;

		public Text infoTextExtraDamage;

		public Text infoTextExtraDamageAmount;

		public EvolveStars evolveStars;

		public RectTransform evolveInfosParent;

		public SkeletonGraphic spineAnimCharBase;

		public HeroPentagram heroPentagram;

		public HeroAnimation heroAnimation;

		public RectTransform heroTurntable;

		public RectTransform heroTurntableShadow;

		public Image backgroundGlow;

		public Image backgroundTile;

		public SkeletonGraphic spineEvolveEffect;

		public UiState stateBeforeOpening;

		public GameButton buttonClose;

		public SkinRevealAnimator skinRevealAnimator;

		private string evolveStartAnim = "evolve_start";

		private string evolveEndAnim = "evolve_end";

		private string idleAnim = "idle_1";

		private string heroName;

		private int targetSkinIndex;

		private int currentLevel;

		private bool isEvolve;

		private Material heroMaterial;

		public bool isAnimating;

		public int ttt;

		public bool playStartAnim;

		public bool playEvolveAnim;

		public PanelHeroEvolveSkin.State state;

		public bool doEvolve;

		public List<SkinData> skinsToBeShowed;

		public SkinInventoryButton skinInventoryButtonPrefab;

		public RectTransform newSkinButtonsParent;

		public List<SkinInventoryButton> newSkinButtons;

		public UiManager uiManager;

		public float gapBetweenSkins = 165f;

		private int skinButtonCount = 3;

		private Tween currentTween;

		private int skinRevealingCounter;

		public enum State
		{
			APPEAR,
			WAITING_EVOLVE,
			EVOLVING,
			WAITING_REVEAL_SKIN,
			REVEALING_SKINS,
			END
		}
	}
}
