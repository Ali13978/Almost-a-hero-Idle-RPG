using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelUnlock : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToUpdates();
		}

		public override void AahUpdate(float dt)
		{
			if (this.collected)
			{
				this.iconBg.transform.localScale = Vector3.one * 0.8f;
				this.iconFrame.sprite = this.spriteIconFrameCollected;
				this.iconBg.sprite = this.spriteIconBgCollected;
				this.healthBar.gameObject.SetActive(false);
				this.bg.sprite = this.spriteBgCollected;
				this.bg.color = PanelUnlock.COLOR_COLLECTED_BG;
				this.textUnlockReward.color = this.colorTextRewardCollected;
				this.rect.SetSizeDeltaX(760f);
				this.textUnlockedStage.gameObject.SetActive(true);
				this.check.gameObject.SetActive(true);
			}
			else
			{
				this.iconBg.transform.localScale = Vector3.one;
				if (this.canCollect)
				{
					this.healthBar.SetScale(1f);
					this.iconFrame.sprite = this.spriteIconFrameCollected;
					this.iconBg.sprite = this.spriteIconBgCollected;
				}
				else
				{
					this.healthBar.SetScale(GameMath.GetMinFloat(1f, (float)this.progress / (float)this.progressCap));
					this.iconFrame.sprite = this.spriteIconFrameNotCollected;
					if (this.spriteIconBgNotCollectedSpecial != null)
					{
						this.iconBg.sprite = this.spriteIconBgNotCollectedSpecial;
					}
					else
					{
						this.iconBg.sprite = this.spriteIconBgNotCollected;
					}
				}
				this.rect.SetSizeDeltaX(822f);
				this.healthBar.gameObject.SetActive(true);
				this.bg.sprite = this.spriteBgNotCollected;
				this.bg.color = Color.white;
				this.textUnlockReward.color = this.colorTextRewardNotCollected;
				this.textUnlockedStage.gameObject.SetActive(false);
				this.check.gameObject.SetActive(false);
			}
		}

		public void SetDetails(ChallengeRift challengeRift, bool isReqSatisfied, bool addRewardString = false)
		{
			this.textUnlockTitle.text = challengeRift.unlock.GetReqString();
			this.textUnlockReward.text = string.Format(LM.Get("RIFT_AEON_DUSTS"), GameMath.GetDoubleString(challengeRift.riftPointReward));
			this.progressCap = challengeRift.targetTotWaveNo;
		}

		public void SetDetails(Unlock unlock, bool isReqSatisfied, bool addRewardString = false)
		{
			this.unlock = unlock;
			if (isReqSatisfied)
			{
				this.textUnlockTitle.text = unlock.GetReqSatisfiedString();
			}
			else
			{
				this.textUnlockTitle.text = unlock.GetReqString();
			}
			this.textUnlockReward.text = ((!addRewardString) ? string.Empty : (LM.Get("UI_UNLOCK_REWARD") + ": ")) + unlock.GetRewardString();
			this.progressCap = unlock.GetReqAmount();
			this.textUnlockedStage.text = unlock.GetReqSatisfiedString();
		}

		public void SetProgress(int progress)
		{
			this.progress = progress;
		}

		public void SetCanCollect(bool canCollect, bool collected)
		{
			this.collected = collected;
			this.canCollect = canCollect;
		}

		public void SetIconSprite(Sprite s, float scale = 1f, Color? color = null, Sprite background = null)
		{
			this.icon.transform.localScale = new Vector3(1f, 1f, 1f) * scale;
			this.icon.color = ((color == null) ? Color.white : color.Value);
			this.icon.sprite = s;
			this.spriteIconBgNotCollectedSpecial = null;
			this.icon.SetNativeSize();
			if (background == null)
			{
				this.iconBack.enabled = false;
			}
			else
			{
				this.iconBack.enabled = true;
				this.iconBack.sprite = background;
				this.iconBack.transform.localScale = this.icon.transform.localScale;
				this.iconBack.SetNativeSize();
			}
		}

		public void SetIconSpriteMerchant()
		{
			this.iconBack.enabled = false;
			this.icon.color = Color.white;
			this.icon.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
			this.icon.sprite = this.spriteIconMerchantMode;
			this.spriteIconBgNotCollectedSpecial = null;
			this.icon.SetNativeSize();
		}

		public void SetIconSpriteCompass()
		{
			this.iconBack.enabled = false;
			this.icon.color = Color.white;
			this.icon.transform.localScale = new Vector3(1f, 1f, 1f);
			this.icon.sprite = this.spriteIconCompass;
			this.spriteIconBgNotCollectedSpecial = this.spriteIconBgDark;
			this.icon.SetNativeSize();
		}

		public void SetIconSpriteSkillPointsAutoDistribution()
		{
			this.iconBack.enabled = false;
			this.icon.color = Color.white;
			this.icon.transform.localScale = new Vector3(1f, 1f, 1f);
			this.icon.sprite = UiData.inst.spriteUnlockRandomHeroAndSkills;
			this.spriteIconBgNotCollectedSpecial = this.spriteIconBgDark;
			this.icon.SetNativeSize();
		}

		public void SetIconSprite(GameMode mode)
		{
			this.iconBack.enabled = false;
			this.icon.color = Color.white;
			this.icon.transform.localScale = new Vector3(1f, 1f, 1f);
			this.spriteIconBgNotCollectedSpecial = null;
			this.icon.SetNativeSize();
			throw new NotImplementedException();
		}

		public void SetIconSprite(CurrencyType currency)
		{
			this.iconBack.enabled = false;
			this.icon.color = Color.white;
			this.icon.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
			if (currency == CurrencyType.GOLD)
			{
				this.icon.sprite = this.spriteIconGold;
			}
			else if (currency == CurrencyType.MYTHSTONE)
			{
				this.icon.sprite = this.spriteIconMythstones;
			}
			else if (currency == CurrencyType.SCRAP)
			{
				this.icon.sprite = this.spriteIconScraps;
			}
			else if (currency == CurrencyType.GEM)
			{
				this.icon.sprite = this.spriteIconCredits;
			}
			else if (currency == CurrencyType.TOKEN)
			{
				this.icon.sprite = this.spriteIconTokens;
			}
			else
			{
				if (currency != CurrencyType.AEON)
				{
					throw new NotImplementedException();
				}
				this.icon.sprite = this.spriteIconAeon;
			}
			this.spriteIconBgNotCollectedSpecial = null;
			this.icon.SetNativeSize();
		}

		public RectTransform rect;

		public Image bg;

		public Image iconBg;

		public Image iconFrame;

		public Image icon;

		public Image iconBack;

		public Image check;

		public Scaler healthBar;

		public Text textUnlockTitle;

		public Text textUnlockedStage;

		public Text textUnlockReward;

		[NonSerialized]
		public int progress;

		[NonSerialized]
		public int progressCap = 1;

		public bool collected;

		public bool canCollect;

		public Sprite spriteIconFrameNotCollected;

		public Sprite spriteIconFrameCollected;

		public Sprite spriteIconBgNotCollected;

		public Sprite spriteIconBgNotCollectedSpecial;

		public Sprite spriteIconBgCollected;

		public Sprite spriteIconBgDark;

		public Sprite spriteBgNotCollected;

		public Sprite spriteBgCollected;

		public Sprite spriteIconMerchantMode;

		public Sprite spriteIconCompass;

		public Sprite spriteIconGold;

		public Sprite spriteIconMythstones;

		public Sprite spriteIconScraps;

		public Sprite spriteIconCredits;

		public Sprite spriteIconTokens;

		public Sprite spriteIconAeon;

		public Color colorTextRewardCollected;

		public Color colorTextRewardNotCollected;

		public Unlock unlock;

		private static Color COLOR_COLLECTED_BG = Utility.HexColor("3E2D17");
	}
}
