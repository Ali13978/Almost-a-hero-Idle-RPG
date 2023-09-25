using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHubCharms : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.charmCards = new List<CharmCard>();
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.header.text = LM.Get("UI_CHARM_COLLECTION_HEADER");
			this.noCharmsHint.text = string.Format(LM.Get("UNLOCK_REQ_PRESTIGE_STAGE"), Main.instance.GetSim().GetWorld(GameMode.RIFT).unlockMode.GetReqInt());
			this.filterInfoText.text = LM.Get("UI_CHARMS_SORT_BY");
			for (int i = this.charmCards.Count - 1; i >= 0; i--)
			{
				this.charmCards[i].InitStrings();
			}
		}

		public void FindIndexChanges(List<CharmEffectData> charmEffects)
		{
			List<IndexChangePair> list = new List<IndexChangePair>();
			if (this.lastCharmEffects != null)
			{
				int count = charmEffects.Count;
				for (int i = 0; i < count; i++)
				{
					CharmEffectData charmEffectData = charmEffects[i];
					CharmEffectData charmEffectData2 = this.lastCharmEffects[i];
					if (charmEffectData != null && charmEffectData2 != null && charmEffectData != charmEffectData2)
					{
						int from = this.lastCharmEffects.IndexOf(charmEffectData);
						IndexChangePair item = new IndexChangePair
						{
							from = from,
							to = i
						};
						list.Add(item);
					}
				}
				if (list.Count > 0)
				{
					this.DoReorderAnimation(list);
				}
			}
			this.lastCharmEffects = charmEffects;
		}

		private void DoReorderAnimation(List<IndexChangePair> indexChanges)
		{
			int count = indexChanges.Count;
			if (this.reOrderAnim != null && this.isAnimatingSort)
			{
				this.reOrderAnim.Kill(false);
				this.SetCartPositions();
			}
			this.reOrderAnim = DOTween.Sequence();
			this.isAnimatingSort = true;
			float num = 0f;
			float num2 = 0.1f;
			float num3 = ((float)count * 0.03f <= num2) ? 0.03f : (num2 / (float)count);
			for (int i = 0; i < count; i++)
			{
				IndexChangePair indexChangePair = indexChanges[i];
				if (indexChangePair.IsFullChange())
				{
					CharmCard charmCard = this.charmCards[indexChangePair.from];
					Vector2 vector = PanelHubCharms.CalculateCardPosition(indexChangePair.to);
					Vector2 anchoredPosition = charmCard.rectTransform.anchoredPosition;
					Vector2 vector2 = vector - anchoredPosition;
					float num4 = -Mathf.Atan2(vector2.x, vector2.y) * 57.29578f;
					if (vector2.y < 0f)
					{
						num4 += 180f;
					}
					num4 = GameMath.Clamp(num4, -30f, 30f);
					float num5;
					num = (num5 = num + num3);
					this.reOrderAnim.Insert(num5, charmCard.rectTransform.DOAnchorPos(vector, 0.3f, false).SetEase(Ease.InOutQuad));
					this.reOrderAnim.Insert(num5, charmCard.rectTransform.DORotate(new Vector3(0f, 0f, num4), 0.15f, RotateMode.Fast).SetEase(Ease.OutCubic));
					this.reOrderAnim.Insert(num5 + 0.15f, charmCard.rectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.15f, RotateMode.Fast).SetEase(Ease.OutBack));
				}
			}
			this.reOrderAnim.OnComplete(delegate
			{
				this.SetCartPositions();
				this.isAnimatingSort = false;
				this.forceUpdate = true;
				UiManager.stateJustChanged = true;
			});
			this.reOrderAnim.Play<Sequence>();
		}

		public void FillCharmsScroll(Action<int> onClicked)
		{
			Vector2 a = new Vector2(105f, -130f);
			int count = Main.instance.GetSim().allCharmEffects.Count;
			Utility.FillUiElementList<CharmCard>(this.charmCardPrefab, this.scrollRect.content, count, this.charmCards);
			float y = -a.y - (float)(count / 4 + 1) * -300f;
			for (int i = 0; i < count; i++)
			{
				CharmCard charmCard = this.charmCards[i];
				int num = i % 4;
				int num2 = i / 4;
				charmCard.SetSoloCard();
				charmCard.InitStrings();
				charmCard.InitButton();
				charmCard.onClicked = onClicked;
				charmCard.rectTransform.anchoredPosition = a + new Vector2((float)num * 200f, (float)num2 * -300f);
			}
			this.scrollRect.content.SetSizeDeltaY(y);
		}

		public void SetCartPositions()
		{
			int count = this.charmCards.Count;
			for (int i = 0; i < count; i++)
			{
				CharmCard charmCard = this.charmCards[i];
				charmCard.rectTransform.anchoredPosition = PanelHubCharms.CalculateCardPosition(i);
			}
		}

		private static Vector2 CalculateCardPosition(int i)
		{
			Vector2 a = new Vector2(105f, -130f);
			int num = i % 4;
			int num2 = i / 4;
			return a + new Vector2((float)num * 200f, (float)num2 * -300f);
		}

		public void SetupCharmCard(CharmCard c)
		{
			c.button.interactable = true;
			c.levelProgresBarParent.gameObject.SetActive(true);
		}

		public void ShowFilterOptions()
		{
			if (this.isFilterShowing)
			{
				return;
			}
			if (this.filterShowAnimation != null && this.filterShowAnimation.isPlaying)
			{
				this.filterShowAnimation.Complete(true);
			}
			this.isFilterShowing = true;
			this.filterParent.gameObject.SetActive(true);
			this.filterShowAnimation = DOTween.Sequence();
			this.filterShowAnimation.Append(this.filterParent.DOScaleY(1f, 0.2f).SetEase(Ease.OutBack)).Join(this.scrollRect.viewport.DoTopDelta(213f, 0.2f, false).SetEase(Ease.OutCubic)).Play<Sequence>();
		}

		public void HideFilterOptions()
		{
			if (!this.isFilterShowing)
			{
				return;
			}
			if (this.filterShowAnimation != null && this.filterShowAnimation.isPlaying)
			{
				this.filterShowAnimation.Complete(true);
			}
			this.isFilterShowing = false;
			this.filterShowAnimation = DOTween.Sequence();
			this.filterShowAnimation.Append(this.filterParent.DOScaleY(0f, 0.2f).SetEase(Ease.OutCubic)).Join(this.scrollRect.viewport.DoTopDelta(98f, 0.2f, false).SetEase(Ease.OutCubic)).AppendCallback(delegate
			{
				this.filterParent.gameObject.SetActive(false);
			}).Play<Sequence>();
		}

		public CharmCard GetCharmReadyToUpgradeOrFirst(out int row)
		{
			if (this.charmCards.Count == 0)
			{
				row = 0;
				return null;
			}
			for (int i = this.charmCards.Count - 1; i >= 0; i--)
			{
				if (Main.instance.GetSim().allCharmEffects.ContainsKey(this.charmCards[i].charmId) && Main.instance.GetSim().allCharmEffects[this.charmCards[i].charmId].CanLevelUp())
				{
					row = i / 4;
					return this.charmCards[i];
				}
			}
			row = 0;
			return this.charmCards[0];
		}

		public static string GetLocalizedFilterName(CharmSortType sortType)
		{
			switch (sortType)
			{
			case CharmSortType.Default:
				return LM.Get("ARTIFACT_SORT_DEFAULT");
			case CharmSortType.Level:
				return LM.Get("CHARM_SORT_LVL");
			case CharmSortType.Type:
				return LM.Get("CHARM_SORT_TYPE");
			case CharmSortType.LevelupStatus:
				return LM.Get("CHARM_SORT_LVLUP");
			default:
				throw new Exception();
			}
		}

		public GameButton backButton;

		public ScrollRect scrollRect;

		public Transform mainBackground;

		public Text header;

		public Text sortBy;

		public CharmCard charmCardPrefab;

		public RectTransform charmBonusesParent;

		public UniversalBonusUnitWidget bonusDamage;

		public UniversalBonusUnitWidget bonusHealth;

		public UniversalBonusUnitWidget bonusGold;

		public bool animateBonus;

		public bool isAnimatingBonus;

		public double bonusDamageLastAmount;

		public double bonusHealthLastAmount;

		public double bonusGoldLastAmount;

		public RectTransform filterParent;

		public GameButton filterToggleButton;

		public GameButton filterButton;

		public Text filterInfoText;

		public RectTransform filterArrow;

		public RectTransform bonusTabParent;

		public RectTransform noCharmsParent;

		public Text noCharmsHint;

		public MenuShowCurrency scraps;

		public Transform headerBonusParent;

		private List<CharmEffectData> lastCharmEffects;

		[NonSerialized]
		public List<CharmCard> charmCards;

		public bool forceUpdate;

		private Sequence reOrderAnim;

		public bool isAnimatingSort;

		private bool isFilterShowing;

		private Sequence filterShowAnimation;
	}
}
