using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using Static;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui
{
	public class RiftOptionWidget : Selectable, IPointerClickHandler, IEventSystemHandler
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

		public void SetLocked()
		{
			this.isLocked = true;
			this.lockedRiftParent.gameObject.SetActive(true);
			this.discoveryHintParent.gameObject.SetActive(false);
			this.riftPointRewardParent.gameObject.SetActive(false);
			this.curseLockedOrnements.gameObject.SetActive(false);
			this.nextCursedCooldown.gameObject.SetActive(false);
			this.riftRecord.gameObject.SetActive(false);
			this.riftName.gameObject.SetActive(true);
			this.background.color = RiftOptionWidget.lockedBackgroundColor;
			this.riftName.color = RiftOptionWidget.lockedNameColor;
			this.lockIconImage.color = RiftOptionWidget.lockColor;
			this.lockHint.color = RiftOptionWidget.lockColor;
		}

		public void SetUnlocked()
		{
			this.isLocked = false;
			this.lockedRiftParent.gameObject.SetActive(false);
			this.discoveryHintParent.gameObject.SetActive(false);
			this.curseLockedOrnements.gameObject.SetActive(false);
			this.nextCursedCooldown.gameObject.SetActive(false);
			this.riftPointRewardParent.gameObject.SetActive(true);
			this.riftName.gameObject.SetActive(true);
			this.background.color = RiftOptionWidget.normalBackgroundColor;
			this.riftName.color = RiftOptionWidget.normalNameColor;
		}

		public void SetHiting()
		{
			this.discoveryHintText.text = LM.Get("UI_DISCOVER_HINT");
			this.discoveryHintText.color = RiftOptionWidget.lockedNameColor;
			this.discoveryHintParent.gameObject.SetActive(true);
			this.riftPointRewardParent.gameObject.SetActive(false);
			this.lockedRiftParent.gameObject.SetActive(false);
			this.riftEffectsParent.gameObject.SetActive(false);
			this.currencyRewardsParent.gameObject.SetActive(false);
			this.riftRecord.gameObject.SetActive(false);
			this.riftName.gameObject.SetActive(false);
			this.curseLockedOrnements.gameObject.SetActive(false);
			this.nextCursedCooldown.gameObject.SetActive(false);
			this.background.color = RiftOptionWidget.lockedBackgroundColor;
		}

		public void SetNonExistingCurse()
		{
			this.isLocked = true;
			this.discoveryHintParent.gameObject.SetActive(false);
			this.riftPointRewardParent.gameObject.SetActive(false);
			this.lockedRiftParent.gameObject.SetActive(false);
			this.riftEffectsParent.gameObject.SetActive(false);
			this.currencyRewardsParent.gameObject.SetActive(false);
			this.riftRecord.gameObject.SetActive(false);
			this.riftName.gameObject.SetActive(false);
			this.background.color = RiftOptionWidget.lockedBackgroundColor;
			this.curseLockedOrnements.gameObject.SetActive(true);
			this.nextCursedCooldown.gameObject.SetActive(false);
		}

		public void SetNewCurseCooldown()
		{
			this.isLocked = true;
			this.discoveryHintParent.gameObject.SetActive(false);
			this.riftPointRewardParent.gameObject.SetActive(false);
			this.lockedRiftParent.gameObject.SetActive(false);
			this.riftEffectsParent.gameObject.SetActive(false);
			this.currencyRewardsParent.gameObject.SetActive(false);
			this.riftRecord.gameObject.SetActive(false);
			this.riftName.gameObject.SetActive(false);
			this.background.color = RiftOptionWidget.lockedBackgroundColor;
			this.curseLockedOrnements.gameObject.SetActive(false);
			this.nextCursedCooldown.gameObject.SetActive(true);
		}

		public void SetSelected()
		{
			this.selectionFrame.gameObject.SetActive(true);
			this.selectionFrame.color = RiftOptionWidget.outlineColor;
			this.background.color = RiftOptionWidget.selectedBackgroundColor;
		}

		public void SetBackgroundColor()
		{
			this.background.color = RiftOptionWidget.selectedBackgroundColor;
		}

		public void SetUnlselected()
		{
			this.selectionFrame.gameObject.SetActive(false);
			this.background.color = RiftOptionWidget.normalBackgroundColor;
		}

		public void SetBestRecord(double durInSecond)
		{
			if (durInSecond == double.PositiveInfinity)
			{
				this.riftRecord.gameObject.SetActive(false);
			}
			else
			{
				this.riftRecord.gameObject.SetActive(true);
				this.riftRecord.text = LM.Get("RIFT_BEST_TIME") + " " + GameMath.GetLocalizedTimeString(TimeSpan.FromSeconds(durInSecond));
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.isLocked)
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiDefaultFailClick, 1f));
			}
			else if (this.onClicked != null)
			{
				this.onClicked(this.riftIndex);
			}
		}

		public void SetRiftEffects(ChallengeRift riftChallenge, UiManager uiManager)
		{
			int num = riftChallenge.riftEffects.Count;
			if (riftChallenge.IsCursed())
			{
				num--;
			}
			Utility.FillUiElementList<Image>(UiData.inst.imagePrafab, this.riftEffectsParent, num, this.riftEffectImages);
			for (int i = 0; i < num; i++)
			{
				Image image = this.riftEffectImages[i];
				image.sprite = uiManager.GetRiftEffectSmallSprite(riftChallenge.riftEffects[i].GetType());
				image.rectTransform.sizeDelta = new Vector2(60f, 60f);
			}
		}

		public void SetReward(UnlockReward reward, UiManager uiManager)
		{
			this.ResetElements();
			if (reward is UnlockRewardCurrency)
			{
				UnlockRewardCurrency unlockRewardCurrency = reward as UnlockRewardCurrency;
				this.currencyRewardsParent.gameObject.SetActive(true);
				this.currencyRewardAmount.gameObject.SetActive(true);
				this.currencyRewardAmount.rectTransform.SetAnchorPosY(-3.7f);
				this.currencyRewardAmount.text = GameMath.GetDoubleString(unlockRewardCurrency.GetAmount());
				this.currencyRewardIcon.sprite = UiData.inst.currencySprites[(int)unlockRewardCurrency.currencyType];
				this.currencyRewardIcon.SetNativeSize();
				this.currencyRewardIcon.gameObject.transform.localScale = Vector3.one;
			}
			else if (reward is UnlockRewardMerchantItem)
			{
				UnlockRewardMerchantItem unlockRewardMerchantItem = reward as UnlockRewardMerchantItem;
				this.currencyRewardsParent.gameObject.SetActive(true);
				this.currencyRewardAmount.gameObject.SetActive(false);
				this.currencyRewardIcon.sprite = uiManager.GetSpriteMerchantItem(unlockRewardMerchantItem.GetMerchantItemId());
				this.currencyRewardIcon.SetNativeSize();
				this.currencyRewardIcon.gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
				this.currencyRewardIcon.rectTransform.SetAnchorPosX(94f);
			}
			else if (reward is UnlockRewardAeonDust)
			{
				UnlockRewardAeonDust unlockRewardAeonDust = reward as UnlockRewardAeonDust;
				this.currencyRewardsParent.gameObject.SetActive(true);
				this.currencyRewardAmount.text = GameMath.GetDoubleString(unlockRewardAeonDust.GetAmount());
				this.currencyRewardAmount.rectTransform.SetAnchorPosY(-3.7f);
				this.currencyRewardAmount.gameObject.SetActive(true);
				this.currencyRewardIcon.sprite = UiData.inst.spriteRewardAeonDustSmall;
				this.currencyRewardIcon.gameObject.transform.localScale = Vector3.one;
				this.currencyRewardIcon.SetNativeSize();
			}
			else if (reward is UnlockRewardTrinketPack)
			{
				UnlockRewardTrinketPack unlockRewardTrinketPack = reward as UnlockRewardTrinketPack;
				this.currencyRewardsParent.gameObject.SetActive(true);
				this.currencyRewardAmount.gameObject.SetActive(unlockRewardTrinketPack.Amount > 1);
				if (unlockRewardTrinketPack.Amount > 1)
				{
					this.currencyRewardAmount.text = StringExtension.Concat("x", unlockRewardTrinketPack.Amount.ToString());
					this.currencyRewardAmount.rectTransform.SetAnchorPosY(-40f);
				}
				this.currencyRewardIcon.sprite = UiData.inst.spriteUnlockTrinketPack;
				this.currencyRewardIcon.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				this.currencyRewardIcon.rectTransform.SetAnchorPosX(94f);
				this.currencyRewardIcon.SetNativeSize();
			}
			else if (reward is UnlockRewardCharmPack)
			{
				this.currencyRewardsParent.gameObject.SetActive(true);
				this.currencyRewardAmount.gameObject.SetActive(false);
				this.currencyRewardIcon.rectTransform.SetAnchorPosX(90f);
				this.currencyRewardIcon.sprite = UiData.inst.spriteUnlockCharmPack;
				this.currencyRewardIcon.SetNativeSize();
			}
			else
			{
				if (!(reward is UnlockRewardSpecificCharm))
				{
					throw new NotImplementedException();
				}
				UnlockRewardSpecificCharm unlockRewardSpecificCharm = reward as UnlockRewardSpecificCharm;
				this.currencyRewardBackground.gameObject.SetActive(true);
				this.currencyRewardBackground.sprite = uiManager.GetCharmCardFace(unlockRewardSpecificCharm.ced.BaseData.charmType);
				this.currencyRewardBackground.SetNativeSize();
				this.currencyRewardBackground.transform.localScale = Vector3.one * 0.4f;
				this.currencyRewardBackground.rectTransform.SetAnchorPosX(75f);
				this.currencyRewardsParent.gameObject.SetActive(true);
				this.currencyRewardIcon.color = new Color32(247, 222, 160, byte.MaxValue);
				this.currencyRewardIcon.sprite = uiManager.spritesCharmEffectIcon[unlockRewardSpecificCharm.ced.BaseData.id];
				this.currencyRewardIcon.SetNativeSize();
				this.currencyRewardIcon.transform.localScale = Vector3.one * 0.4f;
				this.currencyRewardIcon.rectTransform.SetAnchorPosX(75f);
				this.currencyRewardAmount.gameObject.SetActive(true);
				this.currencyRewardAmount.rectTransform.SetAnchorPosY(-3.7f);
				this.currencyRewardAmount.text = "x" + GameMath.GetDoubleString((double)unlockRewardSpecificCharm.numDuplicates);
			}
		}

		private void ResetElements()
		{
			this.currencyRewardIcon.color = Color.white;
			this.currencyRewardIcon.transform.localScale = Vector3.one;
			this.currencyRewardBackground.transform.localScale = Vector3.one;
			this.currencyRewardBackground.gameObject.SetActive(false);
			this.currencyRewardBackground.rectTransform.SetAnchorPosX(41f);
			this.currencyRewardIcon.rectTransform.SetAnchorPosX(69.6f);
			this.currencyRewardAmount.rectTransform.SetAnchorPosX(-34.6f);
			this.currencyRewardAmount.rectTransform.SetAnchorPosY(-3.7f);
		}

		public void SetRewardAeonDust(double amount, UiManager uiManager)
		{
			this.ResetElements();
			this.currencyRewardsParent.gameObject.SetActive(true);
			this.currencyRewardAmount.gameObject.SetActive(true);
			this.currencyRewardAmount.text = GameMath.GetDoubleString(amount);
			this.currencyRewardIcon.sprite = UiData.inst.spriteRewardAeonDustSmall;
			this.currencyRewardIcon.gameObject.transform.localScale = Vector3.one;
			this.currencyRewardIcon.SetNativeSize();
		}

		public void DoBoingAnimation(bool isCurseMode)
		{
			if (this.selectAnimation != null && this.selectAnimation.isPlaying)
			{
				this.selectAnimation.Goto(0f, true);
			}
			else
			{
				this.selectAnimation = DOTween.Sequence();
				this.selectAnimation.Append(this.rectTransform.DOPunchScale(new Vector3(0.03f, -0.02f, 0f), 0.5f, 7, 1f));
				if (isCurseMode)
				{
					this.selectAnimation.Insert(0.05f, this.selectionFrame.DOColor(new Color(1f, 0.7215686f, 0.3215686f, 1f), 0.3f).SetEase(EaseFactory.Kick(Ease.OutQuint, Ease.InQuint)));
					this.selectAnimation.Insert(0.05f, this.curseOrnament1.DOColor(new Color(1f, 0.7215686f, 0.3215686f, 1f), 0.3f).SetEase(EaseFactory.Kick(Ease.OutQuint, Ease.InQuint)));
					this.selectAnimation.Insert(0.05f, this.curseOrnament2.DOColor(new Color(1f, 0.7215686f, 0.3215686f, 1f), 0.3f).SetEase(EaseFactory.Kick(Ease.OutQuint, Ease.InQuint)));
				}
				else
				{
					this.selectAnimation.Insert(0.05f, this.selectionFrame.DOColor(Color.white, 0.3f).SetEase(EaseFactory.Kick(Ease.OutQuint, Ease.InQuint)));
				}
				this.selectAnimation.Play<Sequence>();
			}
		}

		public static void SetTheme(Color normalBg, Color lockedBg, Color selectedBg, Color outline, Color lckColor)
		{
			RiftOptionWidget.lockedBackgroundColor = lockedBg;
			RiftOptionWidget.normalBackgroundColor = normalBg;
			RiftOptionWidget.selectedBackgroundColor = selectedBg;
			RiftOptionWidget.outlineColor = outline;
			RiftOptionWidget.lockColor = lckColor;
		}

		private RectTransform m_rectTransform;

		public static Color lockedBackgroundColor;

		public static Color normalBackgroundColor;

		public static Color selectedBackgroundColor;

		public static Color outlineColor;

		public static Color lockColor;

		public static Color lockedNameColor;

		public static Color normalNameColor;

		public Image background;

		public Image selectionFrame;

		public Image curseOrnament1;

		public Image curseOrnament2;

		public Image lockIconImage;

		public List<Image> riftEffectImages;

		public RectTransform riftPointRewardParent;

		public RectTransform firstTimeRewardParent;

		public RectTransform lockedRiftParent;

		public RectTransform riftEffectsParent;

		public RectTransform curseOrnements;

		public RectTransform curseLockedOrnements;

		public Text riftName;

		public Text riftDifficulty;

		public Text riftRecord;

		public Text lockHint;

		public Text nextCursedCooldown;

		public bool isSelected;

		[Header("Rewards")]
		public Text firstTimeRewardLabel;

		public RectTransform currencyRewardsParent;

		public Text currencyRewardAmount;

		public Image currencyRewardIcon;

		public Image currencyRewardBackground;

		public Text discoveryHintText;

		public RectTransform discoveryHintParent;

		public Action<int> onClicked;

		public bool isCursed;

		[NonSerialized]
		public int riftIndex;

		private bool isLocked;

		private Sequence selectAnimation;
	}
}
