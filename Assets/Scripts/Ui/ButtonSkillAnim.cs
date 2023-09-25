using System;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonSkillAnim : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		private void OnStunnedImageDisappeared()
		{
			this.imageStunned.gameObject.SetActive(false);
			this.stunnedImageAnim = null;
		}

		public override void Init()
		{
			this.imageStunned.gameObject.SetActive(false);
		}

		public override void AahUpdate(float dt)
		{
			if (this.stateOld != this.state && this.state == ButtonSkillAnim.State.ACTIVE)
			{
				this.activeSkillFadeOutTimer = 1f;
			}
			this.imageActiveSkill.gameObject.SetActive(this.activeSkillFadeOutTimer > 0f);
			if (this.activeSkillFadeOutTimer > 0f)
			{
				this.activeSkillFadeOutTimer -= dt;
				this.imageActiveSkill.color = new Color(1f, 1f, 1f, this.activeSkillFadeOutTimer / 1f);
			}
			if (this.imageToggleOffCross.gameObject.activeInHierarchy)
			{
				this.imageToggleOffCross.transform.localScale = Vector3.one;
			}
			this.stateOld = this.state;
			bool flag = this.state == ButtonSkillAnim.State.HERO_STUNNED || this.state == ButtonSkillAnim.State.HERO_SILENCED;
			if (flag && !this.imageStunned.gameObject.activeSelf)
			{
				this.imageStunned.rectTransform.localScale = Vector3.zero;
				this.imageStunned.rectTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
				this.imageStunned.gameObject.SetActive(true);
			}
			else if (!flag && this.imageStunned.gameObject.activeSelf && this.stunnedImageAnim == null)
			{
				this.stunnedImageAnim = DOTween.Sequence().Append(this.imageStunned.rectTransform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack)).AppendCallback(new TweenCallback(this.OnStunnedImageDisappeared)).Play<Sequence>();
			}
			switch (this.state)
			{
			case ButtonSkillAnim.State.AVAILABLE:
				this.imageCooldownCircle.gameObject.SetActive(false);
				this.imageSkillIcon.gameObject.SetActive(true);
				this.imageLocked.gameObject.SetActive(false);
				this.imageShadow.gameObject.SetActive(true);
				this.text.gameObject.SetActive(false);
				this.CooldownAnim(0f);
				break;
			case ButtonSkillAnim.State.COOLDOWN:
				this.imageCooldownCircle.gameObject.SetActive(true);
				this.imageCooldownCircle.sprite = this.spriteCooldown;
				this.imageSkillIcon.gameObject.SetActive(true);
				this.imageLocked.gameObject.SetActive(false);
				this.imageShadow.gameObject.SetActive(true);
				this.text.gameObject.SetActive(true);
				this.CooldownAnim(this.skillTimeLeft / this.skillTimeMax);
				this.text.text = GameMath.GetTimeString(this.skillTimeMax - this.skillTimeLeft);
				this.text.color = this.textColorCooldown;
				this.imageCooldownAnimatedCircle.color = this.textColorCooldown;
				break;
			case ButtonSkillAnim.State.ACTIVE:
				this.imageCooldownCircle.gameObject.SetActive(true);
				this.imageCooldownCircle.sprite = this.spriteActive;
				this.imageSkillIcon.gameObject.SetActive(true);
				this.imageLocked.gameObject.SetActive(false);
				this.imageShadow.gameObject.SetActive(true);
				this.text.gameObject.SetActive(false);
				this.CooldownAnim(0f);
				if (this.imageToggleOffCross.gameObject.activeInHierarchy)
				{
					this.pulseTimer += dt;
					if (!this.isToggling)
					{
						this.imageToggleOffCross.transform.localRotation = Quaternion.identity;
						this.imageToggleOffCross.transform.localScale = Vector3.one * this.pulseCurve.Evaluate(this.pulseTimer * 1.2f);
						this.wasToggling = false;
					}
					else
					{
						if (!this.wasToggling)
						{
							this.pulseTimer = 0f;
							this.wasToggling = true;
							this.lastCrossScale = this.imageToggleOffCross.transform.localScale;
						}
						float num = Easing.CircEaseOut(GameMath.Clamp(1f - this.pulseTimer * 2.2f, 0f, 1f), 0f, 1f, 1f);
						this.imageToggleOffCross.transform.localScale = this.lastCrossScale * num * 1.2f;
						if (num <= 0.01f)
						{
							this.imageSkillIcon.sprite = this.normalSkillSprite;
						}
					}
				}
				break;
			case ButtonSkillAnim.State.HERO_DEAD:
				this.imageCooldownCircle.gameObject.SetActive(false);
				this.imageSkillIcon.gameObject.SetActive(false);
				this.imageLocked.gameObject.SetActive(true);
				this.imageLocked.sprite = this.spriteHeroDead;
				this.imageShadow.gameObject.SetActive(true);
				this.text.gameObject.SetActive(true);
				this.CooldownAnim((this.skillTimeMax - this.skillTimeLeft) / this.skillTimeMax);
				this.text.text = GameMath.GetTimeString(this.skillTimeLeft);
				this.imageCooldownAnimatedCircle.color = this.textColorDead;
				this.text.color = this.textColorDead;
				break;
			case ButtonSkillAnim.State.HERO_STUNNED:
			case ButtonSkillAnim.State.HERO_SILENCED:
				this.imageLocked.gameObject.SetActive(true);
				this.imageLocked.sprite = this.spriteHeroDead;
				this.imageShadow.gameObject.SetActive(true);
				this.text.gameObject.SetActive(false);
				this.imageCooldownCircle.gameObject.SetActive(false);
				this.imageCooldownAnimatedCircle.fillAmount = 0f;
				break;
			case ButtonSkillAnim.State.LOCKED:
				this.imageCooldownCircle.gameObject.SetActive(false);
				this.imageSkillIcon.gameObject.SetActive(false);
				this.imageLocked.gameObject.SetActive(true);
				this.imageLocked.sprite = this.spriteLocked;
				this.imageShadow.gameObject.SetActive(false);
				this.text.gameObject.SetActive(false);
				if (this.spineRechargeBuff != null)
				{
					this.spineRechargeBuff.gameObject.SetActive(false);
				}
				this.CooldownAnim(0f);
				break;
			}
		}

		public void PressedButton()
		{
			if (this.spineUse.AnimationState != null)
			{
				this.spineUse.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
				this.spineUse.AnimationState.SetAnimation(0, "animation", false);
			}
		}

		private void CooldownAnim(float ratio)
		{
			this.imageCooldownAnimatedCircle.fillAmount = ratio;
		}

		public void SetState(ButtonSkillAnim.ButtonStateArguments a)
		{
			this.imageToggleOffCross.gameObject.SetActive(false);
			this.normalSkillSprite = a.spriteSkill;
			if (a.spriteSkill != null)
			{
				this.imageSkillIcon.sprite = a.spriteSkill;
			}
			if (a.isLocked)
			{
				this.state = ButtonSkillAnim.State.LOCKED;
			}
			else if (a.heroReviveTime > 0f)
			{
				this.state = ButtonSkillAnim.State.HERO_DEAD;
				this.skillTimeLeft = a.heroReviveTime;
				this.skillTimeMax = a.heroReviveMax;
			}
			else if (a.isActive)
			{
				this.state = ButtonSkillAnim.State.ACTIVE;
				this.skillTimeLeft = a.cooldownMax * (a.cooldownRatio - 1f);
				this.skillTimeMax = 1f;
				if (a.toggleSprite != null)
				{
					this.imageToggleOffCross.gameObject.SetActive(true);
					this.imageSkillIcon.sprite = a.toggleSprite;
				}
			}
			else if (a.stunnBuff)
			{
				this.state = ButtonSkillAnim.State.HERO_STUNNED;
				this.skillTimeLeft = a.cooldownMax * (1f - a.cooldownRatio);
				this.skillTimeMax = a.cooldownMax;
			}
			else if (a.silenceBuff)
			{
				this.state = ButtonSkillAnim.State.HERO_SILENCED;
				this.skillTimeLeft = a.cooldownMax * (1f - a.cooldownRatio);
				this.skillTimeMax = a.cooldownMax;
			}
			else if (a.canActivate)
			{
				this.state = ButtonSkillAnim.State.AVAILABLE;
			}
			else
			{
				this.state = ButtonSkillAnim.State.COOLDOWN;
				this.skillTimeLeft = a.cooldownMax * (1f - a.cooldownRatio);
				this.skillTimeMax = a.cooldownMax;
			}
			if (a.rechargeBuff && this.spineRechargeBuff == null)
			{
				this.spineRechargeBuff = UnityEngine.Object.Instantiate<GameObject>(this.prefabRechargeBuff, base.transform).GetComponent<SkeletonGraphic>();
				this.spineRechargeBuff.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -42f);
				this.spineRechargeBuff.timeScale = GameMath.GetRandomFloat(0.8f, 1.2f, GameMath.RandType.NoSeed);
			}
			if (this.spineRechargeBuff != null)
			{
				this.spineRechargeBuff.gameObject.SetActive(a.rechargeBuff);
			}
		}

		public ButtonSkillAnim.State state;

		public Image imageSkillIcon;

		public Image imageCooldownCircle;

		public Image imageLocked;

		public Image imageActiveSkill;

		public Image imageShadow;

		public Image imageToggleOffCross;

		public Image imageStunned;

		public Text text;

		public float skillTimeMax = 100f;

		public float skillTimeLeft = 100f;

		public Image imageCooldownAnimatedCircle;

		public Sprite spriteActive;

		public Sprite spriteCooldown;

		public Sprite spriteHeroDead;

		public Sprite spriteLocked;

		public Color textColorDead;

		public Color textColorCooldown;

		private ButtonSkillAnim.State stateOld;

		private float activeSkillFadeOutTimer;

		private const float activeSkillFadeOutPeriod = 1f;

		public Sprite spriteHalfMaskCooldown;

		public Sprite spriteHalfMaskDead;

		public RectTransform autoActive1Parent;

		public RectTransform autoActive2Parent;

		public Image spriteHalfAutoActive1;

		public Image spriteHalfAutoActive2;

		public SkeletonGraphic spineUse;

		public SkeletonGraphic spineRechargeBuff;

		public GameObject prefabRechargeBuff;

		private const float PosYRechargeBuff = -42f;

		public AnimationCurve pulseCurve;

		public float pulseTimer;

		public bool isToggling;

		public bool wasToggling;

		public Vector3 lastCrossScale;

		public float toggleDelta;

		private Sprite normalSkillSprite;

		private Sequence stunnedImageAnim;

		public enum State
		{
			AVAILABLE,
			COOLDOWN,
			ACTIVE,
			HERO_DEAD,
			HERO_STUNNED,
			HERO_SILENCED,
			LOCKED
		}

		public class ButtonStateArguments
		{
			public bool isLocked;

			public bool isActive;

			public bool canActivate;

			public bool rechargeBuff;

			public bool stunnBuff;

			public bool silenceBuff;

			public float heroReviveTime;

			public float heroReviveMax;

			public float cooldownRatio;

			public float cooldownMax;

			public Sprite spriteSkill;

			public Sprite toggleSprite;
		}
	}
}
