using System;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHeroEvolve : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.text.text = LM.Get("UI_HERO_EVOLVED");
		}

		public override void AahUpdate(float dt)
		{
			if (this.state == PanelHeroEvolveState.OPENING)
			{
				this.stateTimer = Mathf.Max(0f, this.stateTimer - dt);
				base.gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, 1f - this.stateTimer / this.openingDuration);
				if (this.stateTimer == 0f)
				{
					this.ChangeState(PanelHeroEvolveState.OPENED);
				}
			}
			else if (this.state != PanelHeroEvolveState.OPENED)
			{
				if (this.state == PanelHeroEvolveState.FADING_IN)
				{
					this.stateTimer = Mathf.Max(0f, this.stateTimer - dt);
					if (this.stateTimer > this.fadeInDuration * (1f - this.fadeInStayWhitePercentage))
					{
						this.matHeroAnimation.SetFloat("_TextureFade", 1f);
					}
					else
					{
						if (!this.isEvolved)
						{
							this.isEvolved = true;
							this.heroAnimation.SetHeroAnimation(this.heroID, this.equippedSkin, true, false, false, true);
							this.spineAnimCharBase.gameObject.SetActive(true);
							if (this.spineAnimCharBase.Skeleton != null)
							{
								this.spineAnimCharBase.Skeleton.SetSkin(PanelGearScreen.evolveSkins[this.evolveLevel]);
								this.spineAnimCharBase.Skeleton.SetToSetupPose();
							}
							if (this.spineAnimCharBase.AnimationState != null)
							{
								this.spineAnimCharBase.AnimationState.SetAnimation(0, "animation", true);
							}
							this.heroPentagram.SetSprite(this.evolveLevel + 1);
							this.evolveStars.SetNumberOfStars(this.evolveLevel + 1, 6);
							UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.heroEvolveMain, 1f));
							this.PlayGreenmanVO();
						}
						this.textHealthBuffAmount.text = "+" + GameMath.GetPercentString(this.percentage, true);
						this.textDamageBuffAmount.text = "+" + GameMath.GetPercentString(this.percentage, true);
						this.matHeroAnimation.SetFloat("_TextureFade", this.stateTimer / (this.fadeInDuration * (1f - this.fadeInStayWhitePercentage)));
						this.evolvedDeco.transform.localScale = Vector3.Lerp(this.evolvedDeco.transform.localScale, Vector3.one, dt * 10f);
						this.textBuff.color = new Color(this.textBuff.color.r, this.textBuff.color.g, this.textBuff.color.b, this.evolvedDeco.transform.localScale.x);
					}
					if (this.stateTimer == 0f)
					{
						this.ChangeState(PanelHeroEvolveState.EVOLVED);
					}
				}
				else if (this.state == PanelHeroEvolveState.EVOLVED)
				{
					this.textHealthBuffAmount.text = "+" + GameMath.GetPercentString(this.percentage, true);
					this.textDamageBuffAmount.text = "+" + GameMath.GetPercentString(this.percentage, true);
				}
				else if (this.state == PanelHeroEvolveState.CLOSING)
				{
					this.stateTimer = Mathf.Max(0f, this.stateTimer - dt);
					base.gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, this.stateTimer / this.closingDuration);
					if (this.stateTimer == 0f)
					{
						this.ChangeState(PanelHeroEvolveState.CLOSED);
					}
				}
			}
			this.stateJustChanged = false;
		}

		public void ChangeState(PanelHeroEvolveState newState)
		{
			this.state = newState;
			this.stateJustChanged = true;
			double num = GameMath.PowInt(1.5, this.evolveLevel) - 1.0;
			double num2 = GameMath.PowInt(1.5, this.evolveLevel + 1) - 1.0;
			if (this.state == PanelHeroEvolveState.OPENING)
			{
				this.percentage = num;
				this.isEvolved = false;
				this.evolveStars.SetNumberOfStars(this.evolveLevel, 6);
				this.heroAnimation.SetHeroAnimation(this.heroID, this.equippedSkin, true, false, false, true);
				if (this.evolveLevel > 0)
				{
					this.spineAnimCharBase.gameObject.SetActive(true);
					if (this.spineAnimCharBase.Skeleton != null)
					{
						this.spineAnimCharBase.Skeleton.SetSkin(PanelGearScreen.evolveSkins[this.evolveLevel - 1]);
						if (this.spineAnimCharBase.AnimationState != null)
						{
							this.spineAnimCharBase.AnimationState.SetAnimation(0, "animation", true);
						}
						this.spineAnimCharBase.Skeleton.SetToSetupPose();
					}
					this.heroPentagram.SetSprite(this.evolveLevel);
				}
				else
				{
					this.spineAnimCharBase.gameObject.SetActive(false);
					this.heroPentagram.SetSprite(0);
				}
				this.gameObjectEvolveEffect.SetActive(false);
				this.textHealthBuffAmount.text = "+" + GameMath.GetPercentString(this.percentage, true);
				this.textDamageBuffAmount.text = "+" + GameMath.GetPercentString(this.percentage, true);
				this.textHealthBuffAmount.transform.localScale = Vector3.one;
				this.textDamageBuffAmount.transform.localScale = Vector3.one;
				this.stateTimer = this.openingDuration;
				this.matHeroAnimation.SetFloat("_TextureFade", 0f);
				this.evolvedDeco.SetActive(false);
				base.gameObject.transform.localScale = Vector3.zero;
				this.textBuff.color = new Color(this.textBuff.color.r, this.textBuff.color.g, this.textBuff.color.b, 0f);
			}
			else if (this.state == PanelHeroEvolveState.OPENED)
			{
				base.gameObject.transform.localScale = Vector3.one;
				this.gameObjectEvolveEffect.SetActive(false);
			}
			else if (this.state == PanelHeroEvolveState.EVOLVE_ANIM)
			{
				base.gameObject.transform.localScale = Vector3.one;
				this.gameObjectEvolveEffect.SetActive(false);
			}
			else if (this.state == PanelHeroEvolveState.FADING_IN)
			{
				this.percentage = num;
				float duration = GameMath.Clamp((float)(num2 - num) * this.pSpeed, 1f, 3f);
				this.heroAnimation.skeletonGraphic.AnimationState.SetAnimation(0, this.evolveStartAnim, false);
				this.textHealthBuffAmount.text = "+" + GameMath.GetPercentString(this.percentage, true);
				this.textDamageBuffAmount.text = "+" + GameMath.GetPercentString(this.percentage, true);
				Sequence sequence = DOTween.Sequence();
				sequence.Append(DOTween.To(() => this.percentage, delegate(double x)
				{
					this.percentage = x;
				}, num2, duration)).Append(this.textHealthBuffAmount.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutCubic)).Join(this.textDamageBuffAmount.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutCubic)).AppendInterval(0.2f).AppendInterval(0.2f).Join(this.textHealthBuffAmount.transform.DOScale(1f, 0.2f)).Join(this.textDamageBuffAmount.transform.DOScale(1f, 0.2f));
				sequence.Play<Sequence>();
				this.evolvedDeco.transform.localScale = Vector3.zero;
				this.evolvedDeco.SetActive(true);
				this.gameObjectEvolveEffect.SetActive(true);
				if (this.spineEvolveEffect.AnimationState != null)
				{
					this.spineEvolveEffect.AnimationState.SetAnimation(0, this.spineEvolveAnim, false);
				}
				this.spineEvolveEffect.Skeleton.SetToSetupPose();
				this.stateTimer = this.fadeInDuration;
				this.matHeroAnimation.SetFloat("_TextureFade", 1f);
			}
			else if (this.state == PanelHeroEvolveState.EVOLVED)
			{
				this.evolvedDeco.transform.localScale = Vector3.one;
				this.heroAnimation.SetHeroAnimation(this.heroID, this.equippedSkin, true, false, false, true);
				this.spineAnimCharBase.gameObject.SetActive(true);
				if (this.spineAnimCharBase.Skeleton != null)
				{
					this.spineAnimCharBase.Skeleton.SetSkin(PanelGearScreen.evolveSkins[this.evolveLevel]);
					if (this.spineAnimCharBase.AnimationState != null)
					{
						this.spineAnimCharBase.AnimationState.SetAnimation(0, "animation", true);
					}
					this.spineAnimCharBase.Skeleton.SetToSetupPose();
				}
				this.heroPentagram.SetSprite(this.evolveLevel + 1);
				this.evolveStars.SetNumberOfStars(this.evolveLevel + 1, 6);
				this.gameObjectEvolveEffect.SetActive(false);
				this.matHeroAnimation.SetFloat("_TextureFade", 0f);
				this.textBuff.color = new Color(this.textBuff.color.r, this.textBuff.color.g, this.textBuff.color.b, 1f);
			}
			else if (this.state == PanelHeroEvolveState.CLOSING)
			{
				this.gameObjectEvolveEffect.SetActive(false);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.heroEvolveFinish, 1f));
				this.stateTimer = this.closingDuration;
			}
			else if (this.state == PanelHeroEvolveState.CLOSED)
			{
				this.gameObjectEvolveEffect.SetActive(false);
				this.heroAnimation.SetHeroAnimation(this.heroID, this.equippedSkin, true, false, false, true);
				this.evolvedDeco.SetActive(false);
			}
		}

		public void AdvanceState()
		{
			if (this.state == PanelHeroEvolveState.OPENING)
			{
				this.ChangeState(PanelHeroEvolveState.OPENED);
			}
			else if (this.state == PanelHeroEvolveState.OPENED)
			{
				this.ChangeState(PanelHeroEvolveState.EVOLVE_ANIM);
			}
			else if (this.state == PanelHeroEvolveState.EVOLVE_ANIM)
			{
				this.ChangeState(PanelHeroEvolveState.FADING_IN);
			}
			else if (this.state == PanelHeroEvolveState.FADING_IN)
			{
				this.ChangeState(PanelHeroEvolveState.EVOLVED);
			}
			else if (this.state == PanelHeroEvolveState.EVOLVED)
			{
				this.ChangeState(PanelHeroEvolveState.CLOSING);
			}
			else if (this.state == PanelHeroEvolveState.CLOSING)
			{
				this.ChangeState(PanelHeroEvolveState.CLOSED);
			}
		}

		public void SetHeroParams(string heroID, int evolveLevel, int equippedSkin)
		{
			this.heroID = heroID;
			this.evolveLevel = evolveLevel;
			this.equippedSkin = equippedSkin;
			this.textBuff.text = string.Format(LM.Get("UI_HERO_EVOLVE_BUFF"), string.Empty);
			this.textHealthBuff.text = LM.Get("UI_HERO_EVOLVE_HEALTH");
			this.textDamageBuff.text = LM.Get("UI_HERO_EVOLVE_DAMAGE");
		}

		public void PlayGreenmanVO()
		{
			AudioClip[] clips = new AudioClip[0];
			if (this.evolveLevel == 0)
			{
				clips = SoundArchieve.inst.voGreenManEvolveCommon;
			}
			else if (this.evolveLevel == 1)
			{
				clips = SoundArchieve.inst.voGreenManEvolveUncommon;
			}
			else if (this.evolveLevel == 2)
			{
				clips = SoundArchieve.inst.voGreenManEvolveRare;
			}
			else if (this.evolveLevel == 3)
			{
				clips = SoundArchieve.inst.voGreenManEvolveEpic;
			}
			else if (this.evolveLevel == 4)
			{
				clips = SoundArchieve.inst.voGreenManEvolveLegendary;
			}
			else if (this.evolveLevel == 5)
			{
				clips = SoundArchieve.inst.voGreenManEvolveMythical;
			}
			UiManager.sounds.Add(new SoundEventUiVariedVoiceSimple(clips, "GREEN_MAN", 1f));
		}

		public float openingDuration;

		public float fadeInDuration;

		public float fadeInStayWhitePercentage;

		public float closingDuration;

		private float stateTimer;

		public HeroAnimation heroAnimation;

		public SkeletonGraphic spineAnimCharBase;

		public Material matHeroAnimation;

		public EvolveStars evolveStars;

		[SpineAnimation("", "", true, false)]
		private string spineEvolveAnim = "animation";

		public SkeletonGraphic spineEvolveEffect;

		public GameObject gameObjectEvolveEffect;

		public Text text;

		public GameButton buttonClose;

		public PanelHeroEvolveState state;

		public GameObject evolvedDeco;

		public Text textBuffPercentage;

		public Text textBuff;

		public Text textHealthBuff;

		public Text textHealthBuffAmount;

		public Text textDamageBuff;

		public Text textDamageBuffAmount;

		private string heroID;

		private int evolveLevel;

		private int equippedSkin;

		private bool isEvolved;

		public bool stateJustChanged;

		public HeroPentagram heroPentagram;

		private double percentage;

		public float pSpeed = 1f;

		private string evolveStartAnim = "evolve_start";
	}
}
