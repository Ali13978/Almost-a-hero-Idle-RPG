using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class Papyr : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.unlocked = false;
			this.timer = this.periodOpen;
		}

		public override void AahUpdate(float dt)
		{
			if (this.isOpen)
			{
				this.imageIcon.gameObject.SetActive(true);
				if (this.timer > this.periodOpen)
				{
					this.rect.sizeDelta = new Vector2(this.rect.sizeDelta.x, this.maxHeight);
					this.imageBand.color = new Color(this.imageBand.color.r, this.imageBand.color.g, this.imageBand.color.b, 0f);
					this.imageLock.color = new Color(this.imageLock.color.r, this.imageLock.color.g, this.imageLock.color.b, 0f);
				}
				else
				{
					float num = Easing.BackEaseOut(this.timer, 0f, 1f, this.periodOpen);
					if (this.timer < this.periodShort)
					{
						float num2 = Easing.Linear(this.timer, 0f, 1f, this.periodShort);
						float num3 = Easing.CubicEaseOut(this.timer, 0f, 1f, this.periodShort);
						this.imageLock.rectTransform.localScale = Vector3.one * (1f + num3);
						this.imageLock.rectTransform.localEulerAngles = Vector3.forward * (num3 * 30f);
						this.imageBand.color = new Color(this.imageBand.color.r, this.imageBand.color.g, this.imageBand.color.b, 1f - num2);
						this.imageLock.color = new Color(this.imageLock.color.r, this.imageLock.color.g, this.imageLock.color.b, 1f - num2);
					}
					else
					{
						this.imageBand.color = new Color(this.imageBand.color.r, this.imageBand.color.g, this.imageBand.color.b, 0f);
						this.imageLock.color = new Color(this.imageLock.color.r, this.imageLock.color.g, this.imageLock.color.b, 0f);
					}
					this.rect.sizeDelta = new Vector2(this.rect.sizeDelta.x, this.minHeight + (this.maxHeight - this.minHeight) * num);
					this.timer += dt;
				}
			}
			else
			{
				this.imageIcon.gameObject.SetActive(false);
				if (this.timer > this.periodClose)
				{
					this.rect.sizeDelta = new Vector2(this.rect.sizeDelta.x, this.minHeight);
					this.imageBand.color = new Color(this.imageBand.color.r, this.imageBand.color.g, this.imageBand.color.b, 1f);
					this.imageLock.color = new Color(this.imageLock.color.r, this.imageLock.color.g, this.imageLock.color.b, 1f);
					this.imageLock.rectTransform.localScale = Vector3.one;
					this.imageLock.rectTransform.localEulerAngles = Vector3.zero;
				}
				else
				{
					float a = Easing.Linear(this.timer, 0f, 1f, this.periodClose);
					float num4 = Easing.CircEaseOut(this.timer, 0f, 1f, this.periodClose);
					this.rect.sizeDelta = new Vector2(this.rect.sizeDelta.x, this.maxHeight + (this.minHeight - this.maxHeight) * num4);
					this.imageBand.color = new Color(this.imageBand.color.r, this.imageBand.color.g, this.imageBand.color.b, a);
					this.imageLock.color = new Color(this.imageLock.color.r, this.imageLock.color.g, this.imageLock.color.b, a);
					this.imageLock.rectTransform.localScale = Vector3.one * (2f - num4);
					this.imageLock.rectTransform.localEulerAngles = -Vector3.forward * (1f - num4) * 30f;
					this.timer += dt;
				}
			}
			this.timerSerial += dt;
		}

		public void SetIcon(ChallengeUpgrade worldUpgrade)
		{
			this.timerSerial = 0f;
			if (worldUpgrade == null)
			{
				return;
			}
			if (worldUpgrade is ChallengeUpgradeHealth)
			{
				this.imageIcon.sprite = this.spriteIconTypeHealthUp;
			}
			else if (worldUpgrade is ChallengeDegradeHealth)
			{
				this.imageIcon.sprite = this.spriteIconTypeHealthDown;
			}
			else if (worldUpgrade is ChallengeUpgradeDamage)
			{
				this.imageIcon.sprite = this.spriteIconTypeHeroDamage;
			}
			else if (worldUpgrade is ChallengeUpgradeSkillPoints)
			{
				this.imageIcon.sprite = this.spriteIconTypeSkillPoint;
			}
			else if (worldUpgrade is ChallengeUpgradeReduceSkillLevelReq)
			{
				this.imageIcon.sprite = this.spriteIconTypeSkill;
			}
			else if (worldUpgrade is ChallengeUpgradeDamageTotem)
			{
				this.imageIcon.sprite = this.spriteIconTypeTotemDamage;
			}
			else if (worldUpgrade is ChallengeUpgradeGold)
			{
				this.imageIcon.sprite = this.spriteIconTypeGoldUp;
			}
			else if (worldUpgrade is ChallengeDegradeGold)
			{
				this.imageIcon.sprite = this.spriteIconTypeGoldDown;
			}
			else
			{
				if (!(worldUpgrade is ChallangeChoseCharm))
				{
					throw new NotImplementedException();
				}
				this.imageIcon.sprite = null;
			}
		}

		public void OnUpgrade()
		{
			this.timer = 0f;
			this.isOpen = false;
			this.unlocked = false;
		}

		public void SetUnlocked(bool isUnlocked, ChallengeUpgrade worldUpgrade)
		{
			if (this.timerSerial <= 0.5f)
			{
				if (!this.unlocked && isUnlocked)
				{
					this.SetIcon(worldUpgrade);
					this.timer = 0f;
					this.isOpen = true;
				}
				else if (this.unlocked && !isUnlocked)
				{
					this.SetIcon(worldUpgrade);
					this.isOpen = false;
				}
				this.SetIcon(worldUpgrade);
				this.unlocked = isUnlocked;
			}
			else if (this.timer >= this.periodShort)
			{
				if (!this.unlocked && isUnlocked)
				{
					this.SetIcon(worldUpgrade);
					this.timer = 0f;
					this.isOpen = true;
				}
				else if (this.unlocked && !isUnlocked)
				{
					this.isOpen = false;
				}
				this.unlocked = isUnlocked;
			}
		}

		public float minHeight;

		public float maxHeight;

		public RectTransform rect;

		public Image imageBand;

		public Image imageLock;

		public Image imageIcon;

		private bool isOpen;

		private float timer;

		private float timerSerial;

		private float periodOpen = 1f;

		private float periodClose = 0.5f;

		private float periodShort = 0.3f;

		[SerializeField]
		private Sprite spriteIconTypeHealthUp;

		[SerializeField]
		private Sprite spriteIconTypeHealthDown;

		[SerializeField]
		private Sprite spriteIconTypeSkillPoint;

		[SerializeField]
		private Sprite spriteIconTypeSkill;

		[SerializeField]
		private Sprite spriteIconTypeGoldUp;

		[SerializeField]
		private Sprite spriteIconTypeGoldDown;

		[SerializeField]
		private Sprite spriteIconTypeHeroDamage;

		[SerializeField]
		private Sprite spriteIconTypeTotemDamage;

		private bool unlocked;
	}
}
