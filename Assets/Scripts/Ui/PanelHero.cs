using System;
using Spine;
using Spine.Unity;
using Static;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHero : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.callStartScaleAnim = false;
			this.bounceTimer = this.bouncePeriod;
			this.rectTransform = base.GetComponent<RectTransform>();
			this.animationSpeed = 1f;
			if (this.heroPortrait != null)
			{
				this.portraitScaleFactor = this.heroPortrait.transform.localScale.x;
			}
			if (this.spineLevelingUp != null)
			{
				this.spineLevelingUp.Initialize(true);
				this.spineLevelingUp.gameObject.SetActive(false);
				this.spineLevelingUp.AnimationState.Complete += delegate(TrackEntry trackEntry)
				{
					this.spineLevelingUp.gameObject.SetActive(false);
				};
			}
		}

		public void InitOnClickEvents(GameButton.VoidFunc upgradeCallBack, GameButton.VoidFunc heroPortraitCallBack)
		{
			GameButton gameButton = this.buttonUpgrade;
			this.buttonUpgrade.onLongPress = upgradeCallBack;
			gameButton.onClick = upgradeCallBack;
			this.buttonHeroPortrait.onClick = heroPortraitCallBack;
		}

		public void SetName(string name)
		{
			this.textName.text = name;
		}

		public void SetHeroSprite(Sprite hero, int evolveLevel)
		{
			this.heroPortrait.SetHero(hero, evolveLevel, false, -5f, false);
		}

		public void SetLevel(int level)
		{
			this.textLevel.text = StringExtension.ConcatAll(new object[]
			{
				"<color=#abe229ff>",
				LM.Get("UI_HEROES_LV"),
				"</color> ",
				level + 1
			});
		}

		public void SetLevel(int level, int xp, int xpNeedForNextLevel)
		{
			this.SetLevel(level);
			int num = Mathf.Max(1, xpNeedForNextLevel - 1);
			float num2 = 1f * (float)xp / (float)num;
			if (this.callStartScaleAnim)
			{
				this.StartScaleAnim(this.xpBar.scale, num2);
				this.callStartScaleAnim = false;
			}
			else
			{
				this.scaleGoal = num2;
			}
			if (this.spineLevelUpWaiting != null)
			{
				this.spineLevelUpWaiting.SetActive(num2 == 1f);
			}
			this.isLevelUp = (xp + 1 == xpNeedForNextLevel);
			this.textProgress.text = StringExtension.ConcatAll(new object[]
			{
				xp,
				" / ",
				num
			});
		}

		public void SetButtonUpgradeCost(double cost)
		{
			string text;
			if (cost == 0.0)
			{
				text = LM.Get("UI_SHOP_CHEST_0");
			}
			else
			{
				text = GameMath.GetDoubleString(cost);
			}
			this.buttonUpgradeAnim.textUp.text = text;
		}

		public void SetNumNotifications(int num)
		{
			this.notificationBadge.numNotifications = num;
		}

		public void SetTrinketStatus(bool hasTrinket, bool isMaxed)
		{
			if (hasTrinket)
			{
				this.imageIconTrinket.gameObject.SetActive(true);
				if (isMaxed)
				{
					this.imageIconTrinket.sprite = this.trinketMaxedSprite;
				}
				else
				{
					this.imageIconTrinket.sprite = this.trinketNormalSprite;
				}
			}
			else
			{
				this.imageIconTrinket.gameObject.SetActive(false);
			}
		}

		public void SetDamage(double damage, double damageDifUpgrade)
		{
			this.textDamageValue.text = GameMath.GetDoubleString(damage);
			this.buttonUpgradeAnim.isLevelUp = this.isLevelUp;
			this.buttonUpgradeAnim.iconDownType = ((!this.isLevelUp) ? ButtonUpgradeAnim.IconType.UPGRADE_ARROW : ButtonUpgradeAnim.IconType.NONE);
			if (this.isLevelUp)
			{
				string text = LM.Get("UI_HEROES_LEVELUP");
				this.buttonUpgradeAnim.textDown.text = text;
				this.buttonUpgradeAnim.textDown.fontSize = 31 + Mathf.RoundToInt((float)(8 - text.Length) * 1.4f);
				this.buttonUpgradeAnim.textDown.rectTransform.SetSizeDeltaX(160f);
				this.buttonUpgradeAnim.textDownContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			}
			else
			{
				double num = damageDifUpgrade;
				if (num < 1.0)
				{
					num = 1.0;
				}
				this.buttonUpgradeAnim.textDown.text = "+" + GameMath.GetDoubleString(num);
				this.buttonUpgradeAnim.textDown.rectTransform.SetSizeDeltaX(120f);
				this.buttonUpgradeAnim.textDownContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			}
		}

		public void SetHealthMax(double healthMax)
		{
			this.textHealthMaxValue.text = GameMath.GetDoubleString(healthMax);
		}

		public void SetHealthRatio(double healthRatio)
		{
			this.heroHealthBar.SetScale((float)healthRatio);
		}

		public override void AahUpdate(float dt)
		{
			if (this.animTimer > 0f)
			{
				this.animTimer += dt;
				if (this.animTimer >= this.animPeriod)
				{
					this.animTimer = 0f;
					this.xpBar.SetScale(this.scaleGoal);
				}
				else
				{
					float num = Easing.Linear(this.animTimer, this.scaleFirst, this.scaleGoal - this.scaleFirst, this.animPeriod);
					if ((!this.scalingUp && this.scaleMinMax > num) || (this.scalingUp && this.scaleMinMax < num))
					{
						this.animTimer = 0f;
						this.xpBar.SetScale(this.scaleMinMax);
					}
					else
					{
						this.xpBar.SetScale(num);
					}
				}
			}
			else if (this.xpBar != null)
			{
				this.xpBar.SetScale(this.scaleGoal);
			}
			if (this.heroPortrait != null)
			{
				if (this.bounceTimer < this.bouncePeriod)
				{
					this.bounceTimer += dt;
					if (this.bounceTimer >= this.bouncePeriod)
					{
						this.portraitScaleAnimActive = true;
					}
				}
				if (this.portraitScaleAnimActive)
				{
					this.bounceTimer += dt * this.animationSpeed;
					float num2 = (this.bounceTimer - this.bouncePeriod) / this.bounceAnimPeriod;
					if (num2 >= 1f)
					{
						this.heroPortrait.transform.localScale = Vector3.one * this.portraitScaleFactor;
						this.portraitScaleAnimActive = false;
					}
					else if (num2 < 0.5f)
					{
						this.heroPortrait.transform.localScale = Vector3.one * this.portraitScaleFactor * Easing.SineEaseOut(num2, 1f, 0.18f, 0.5f);
					}
					else
					{
						this.heroPortrait.transform.localScale = Vector3.one * this.portraitScaleFactor * Easing.SineEaseOut(1f - num2, 1f, 0.18f, 0.5f);
					}
				}
			}
		}

		private void StartScaleAnim(float scaleFirst, float scaleGoal)
		{
			if (scaleGoal + 0.5f < scaleFirst)
			{
				if (this.animTimer == 0f || this.scaleGoal + 0.5f > this.scaleFirst)
				{
					this.animTimer = 0.01f;
					this.scaleFirst = scaleFirst;
					this.scaleGoal = scaleGoal;
					this.animPeriod = 0.3f;
					this.scalingUp = (scaleGoal > scaleFirst);
					this.bounceTimer = 0f;
				}
			}
			else if (this.animTimer == 0f)
			{
				this.animTimer = 0.01f;
				this.scaleFirst = scaleFirst;
				this.scaleGoal = scaleGoal;
				this.animPeriod = 0.2f * Mathf.Abs(scaleGoal - scaleFirst);
				this.scalingUp = (scaleGoal > scaleFirst);
			}
			this.scaleMinMax = scaleGoal;
		}

		public void OnUpgraded()
		{
			this.animationSpeed = ((!this.buttonUpgradeAnim.gameButton.isDown) ? 1f : 2f);
			if (this.isLevelUp && this.spineLevelingUp.AnimationState != null)
			{
				this.spineLevelingUp.gameObject.SetActive(true);
				this.spineLevelingUp.AnimationState.SetAnimation(0, this.spineAnim, false);
				this.spineLevelingUp.timeScale = this.animationSpeed;
			}
			if (this.isLevelUp)
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiHeroLevelUp, 1f));
			}
			else
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiHeroUpgrade, 1f));
			}
			this.callStartScaleAnim = true;
		}

		public void SetUpgradeNotification(bool val)
		{
			this.upgradeNotification.gameObject.SetActive(val);
		}

		public RectTransform rectTransform;

		public Text textName;

		public Text textLevel;

		public GameButton buttonUpgrade;

		public GameButton buttonHeroPortrait;

		public Scaler xpBar;

		public Text textDamageValue;

		public Text textHealthMaxValue;

		public Text textDmg;

		public Text textHp;

		public Text textProgress;

		public ButtonUpgradeAnim buttonUpgradeAnim;

		private bool isLevelUp;

		public NotificationBadge notificationBadge;

		public HeroPortrait heroPortrait;

		public Image imageIconTotem;

		public Image imageIconTrinket;

		public Image upgradeNotification;

		public Sprite trinketNormalSprite;

		public Sprite trinketMaxedSprite;

		public Scaler heroHealthBar;

		public PanelHeroClass panelHeroClass;

		public GameObject spineLevelUpWaiting;

		public SkeletonGraphic spineLevelingUp;

		private float scaleGoal;

		private float scaleFirst;

		private float scaleMinMax;

		private bool scalingUp;

		private float animTimer;

		private float animPeriod;

		private const float AnimPeriod = 0.2f;

		private const float AnimPeriodFull = 0.3f;

		[SpineAnimation("", "", true, false)]
		private string spineAnim = "animation";

		private bool callStartScaleAnim;

		public bool portraitScaleAnimActive;

		private const float portraitMaxScale = 0.18f;

		private float bouncePeriod = 0.3f;

		private float bounceAnimPeriod = 0.4f;

		private float bounceTimer;

		private float animationSpeed;

		private float portraitScaleFactor = 1f;
	}
}
