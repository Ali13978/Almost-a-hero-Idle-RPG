using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelUnlocks : AahMonoBehaviour
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
		}

		public void InitStrings()
		{
			this.textHideCollected.text = LM.Get("UI_UNLOCKS_COMPLETED");
			if (this.collectedOnesAreHidden)
			{
				this.buttonHideCollectedUnlocks.text.text = LM.Get("UI_UNLOCKS_SHOW");
			}
			else
			{
				this.buttonHideCollectedUnlocks.text.text = LM.Get("UI_UNLOCKS_HIDE");
			}
			this.stageRearrrangeWarning.text = LM.Get("UI_STAGE_REARRANGE_WARNING");
		}

		public void UpdateUnlocks(Simulator sim, UiManager manager)
		{
			List<Unlock> list = sim.GetActiveWorld().unlocks.FindAll((Unlock u) => !u.isHidden);
			int count = list.Count;
			bool flag = true;
			int num = -1;
			for (int i = 0; i < count; i++)
			{
				Unlock unlock = list[i];
				if (unlock.isCollected)
				{
					flag = false;
				}
				else if ((i + 1 < count && list[i + 1].isCollected) || (i - 1 >= 0 && list[i - 1].isCollected))
				{
					num = i;
					break;
				}
			}
			if (flag && num == -1)
			{
				num = 0;
			}
			this.barHideCollected.SetActive(!flag);
			float num2 = (!flag) ? (-this.barHideCollectedYOffset) : -25f;
			if (sim.ShouldShowStagesRearrengeWarning())
			{
				this.stageRearrrangeWarning.gameObject.SetActive(true);
				this.stageRearrrangeWarning.rectTransform.SetAnchorPosY(num2);
				num2 -= this.stageRearrangeWarningOffset;
			}
			else
			{
				this.stageRearrrangeWarning.gameObject.SetActive(false);
			}
			for (int j = 0; j < count; j++)
			{
				Unlock unlock2 = list[j];
				PanelUnlock panelUnlock = this.panelUnlocks[j];
				PanelLocked panelLocked = this.panelLockeds[j];
				if ((this.collectedOnesAreHidden && unlock2.isCollected) || unlock2.isHidden)
				{
					panelUnlock.gameObject.SetActive(false);
					panelLocked.gameObject.SetActive(false);
				}
				else
				{
					bool flag2 = j == num;
					if (unlock2.isCollected || flag2)
					{
						if (flag2)
						{
							num2 -= this.panelUnlockFirstNotCollectedOffset;
						}
						panelUnlock.rect.SetAnchorPosY(num2);
						panelLocked.rectTransform.SetAnchorPosY(num2);
						num2 -= this.panelUnlockHeightCollected + this.yOffsetBetweenCollectedUnlocks;
						float num3 = this.unlockWidgetsParent.anchoredPosition.y + num2;
						if (-this.scrollViewport.rect.height > num3 + 100f || num3 > 150f)
						{
							panelUnlock.gameObject.SetActive(false);
							panelLocked.gameObject.SetActive(false);
						}
						else
						{
							panelUnlock.gameObject.SetActive(true);
							panelLocked.gameObject.SetActive(false);
							manager.UpdatePanelUnlock(panelUnlock, unlock2, false);
						}
						if (flag2)
						{
							num2 -= this.panelUnlockFirstNotCollectedOffset;
						}
					}
					else
					{
						panelUnlock.rect.SetAnchorPosY(num2);
						panelLocked.rectTransform.SetAnchorPosY(num2);
						num2 -= this.panelUnlockHeightCollected + this.yOffsetBetweenCollectedUnlocks;
						float num4 = this.unlockWidgetsParent.anchoredPosition.y + num2;
						if (-this.scrollViewport.rect.height > num4 || num4 > 150f)
						{
							panelUnlock.gameObject.SetActive(false);
							panelLocked.gameObject.SetActive(false);
						}
						else
						{
							panelLocked.gameObject.SetActive(true);
							panelUnlock.gameObject.SetActive(false);
							panelLocked.textGoalType.text = unlock2.GetReqStringLessDetail();
							panelLocked.textStage.text = unlock2.GetReqInt().ToString();
							panelLocked.icon.sprite = manager.GetRewardCategorySprite(unlock2.GetReward().rewardCategory);
						}
					}
				}
			}
			this.totalH = num2;
		}

		public void SetPanelPositions(int unlocksLength, bool shiftForStageRearrangeWarning)
		{
			bool flag = true;
			foreach (PanelUnlock panelUnlock in this.panelUnlocks)
			{
				if (panelUnlock.collected)
				{
					flag = false;
					break;
				}
			}
			this.barHideCollected.SetActive(!flag);
			float num = (!flag) ? (-this.barHideCollectedYOffset) : -25f;
			if (shiftForStageRearrangeWarning)
			{
				this.stageRearrrangeWarning.rectTransform.SetAnchorPosY(num);
				num -= this.stageRearrangeWarningOffset;
			}
			bool flag2 = true;
			int num2 = 0;
			int j = 0;
			int num3 = this.panelUnlocks.Length;
			while (j < num3)
			{
				PanelUnlock panelUnlock2 = this.panelUnlocks[j];
				PanelLocked panelLocked = this.panelLockeds[j];
				if ((!this.collectedOnesAreHidden || !panelUnlock2.collected) && num2 < unlocksLength)
				{
					if (flag2 && !panelUnlock2.collected && num2 > 0)
					{
						num -= this.panelUnlockFirstNotCollectedOffset;
					}
					Vector2 anchoredPosition = new Vector2(panelUnlock2.rect.anchoredPosition.x, num);
					panelUnlock2.rect.anchoredPosition = anchoredPosition;
					panelLocked.rectTransform.anchoredPosition = anchoredPosition;
					float num4 = this.panelUnlockHeightCollected;
					panelUnlock2.rect.sizeDelta = new Vector2(panelUnlock2.rect.sizeDelta.x, num4);
					panelLocked.rectTransform.sizeDelta = new Vector2(panelLocked.rectTransform.sizeDelta.x, num4);
					num -= num4;
					num -= this.yOffsetBetweenCollectedUnlocks;
					flag2 = panelUnlock2.collected;
					if (!panelUnlock2.collected && panelUnlock2.canCollect)
					{
						num -= 25f;
					}
					if (panelUnlock2.progress > 0 && panelUnlock2.progress < panelUnlock2.progressCap)
					{
						num -= this.panelUnlockFirstNotCollectedOffset;
					}
				}
				else if (panelUnlock2.gameObject.activeSelf)
				{
					panelUnlock2.gameObject.SetActive(false);
					panelLocked.gameObject.SetActive(false);
				}
				num2++;
				j++;
			}
			this.totalH = num;
		}

		public void HideOrShow()
		{
			this.collectedOnesAreHidden = !this.collectedOnesAreHidden;
			if (this.collectedOnesAreHidden)
			{
				this.buttonHideCollectedUnlocks.text.text = LM.Get("UI_UNLOCKS_SHOW");
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
			}
			else
			{
				this.buttonHideCollectedUnlocks.text.text = LM.Get("UI_UNLOCKS_HIDE");
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
			}
			UiManager.stateJustChanged = true;
			this.SetContentSize();
		}

		public void SetContentSize()
		{
			this.unlockWidgetsParent.SetSizeDeltaY(-this.totalH + 200f);
		}

		public bool collectedOnesAreHidden;

		public PanelUnlock[] panelUnlocks;

		public PanelLocked[] panelLockeds;

		public RectTransform scrollViewport;

		public RectTransform unlockWidgetsParent;

		public float panelUnlockFirstNotCollectedOffset;

		public float yOffsetBetweenCollectedUnlocks;

		public float panelUnlockHeightCollected;

		public GameButton buttonHideCollectedUnlocks;

		public GameObject barHideCollected;

		public Text textHideCollected;

		public float barHideCollectedYOffset;

		public Text stageRearrrangeWarning;

		public float stageRearrangeWarningOffset;

		private float totalH;

		private RectTransform m_rectTransform;

		public int totalCollectedCount;
	}
}
