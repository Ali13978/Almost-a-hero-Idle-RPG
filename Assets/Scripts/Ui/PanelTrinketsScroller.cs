using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTrinketsScroller : AahMonoBehaviour
	{
		public static string GetLocalizedFilterName(TrinketSortType sortType)
		{
			switch (sortType)
			{
			case TrinketSortType.Default:
				return LM.Get("ARTIFACT_SORT_DEFAULT");
			case TrinketSortType.NumberOfEffects:
				return LM.Get("TRINKET_SORT_NUMBER_OF_EFFECTS");
			case TrinketSortType.Level:
				return LM.Get("TRINKET_SORT_LEVEL");
			case TrinketSortType.Color:
				return LM.Get("TRINKET_SORT_COLOR");
			default:
				throw new NotImplementedException();
			}
		}

		public void InitScrollPos(bool isShowingFilter)
		{
			this.disassembleMultipleTrinketsMenu.SetScaleY(0f);
			if (isShowingFilter)
			{
				this.filtersParent.SetScaleY(1f);
				this.scrollRectTransform.SetTopDelta(244f);
			}
			else
			{
				this.filtersParent.SetScaleY(0f);
				this.scrollRectTransform.SetTopDelta(126f);
			}
		}

		public void InitStrings()
		{
			this.trinketDescHeader.text = LM.Get("UI_TRINKET_DESC");
			this.dissassembleMultipleToggleText.text = LM.Get("MULTIPLE_TRINKETS_DISENCHANT_TOGGLE");
			this.dissassembleMultipleMenuText.text = LM.Get("MULTIPLE_TRINKETS_DISENCHANT_DESC");
			this.trinketSortByLabel.text = LM.Get("TRINKET_SORT_BY");
			this.disassembleMultipleTrinketsButton.text.text = LM.Get("TRINKET_DISASSEMBLE");
		}

		public void FocusOnTrinketIfNecessary(Trinket trinket)
		{
			int index = this.trinkets.FindIndex((ButtonSelectTrinket t) => t.trinketUi.simTrinket == trinket);
			this.trinkets[index].rectTransform.GetWorldCorners(this.focusTargetCorners);
			this.scrollRect.viewport.GetWorldCorners(this.viewportCorners);
			if (this.focusTargetCorners[0].y < this.viewportCorners[0].y)
			{
				float b = this.scrollRect.content.sizeDelta.y - this.scrollRect.viewport.rect.height;
				float minFloat = GameMath.GetMinFloat(-(this.CalculateTrinketPosition(index).y - 300f), b);
				this.SetScrollPosition(minFloat);
			}
		}

		public void InitScroll(int count, Action<int> onTrinketSelected, Action onMultipleTrinketsDisassemble)
		{
			if (count == this.trinkets.Count)
			{
				return;
			}
			Utility.FillUiElementList<ButtonSelectTrinket>(this.trinketPrefab, this.scrollRect.content, count, this.trinkets, new Utility.ElementStateChange<ButtonSelectTrinket>(this.OnButtonSpawn), new Utility.ElementStateChange<ButtonSelectTrinket>(this.OnButtonDestroy));
			this.disassembleMultipleTrinketsButton.onClick = delegate()
			{
				onMultipleTrinketsDisassemble();
			};
			this.CalculatePositions();
			this.CalculateContentSize(count);
			this.disassembleMultipleTrinketsMenu.SetScale(0f);
			int num = 0;
			using (List<ButtonSelectTrinket>.Enumerator enumerator = this.trinkets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{

                    ButtonSelectTrinket trinket = enumerator.Current;
					int ind = num;
					trinket.multipleSelectionImage.transform.SetScale(0f);
					trinket.canvasGroup.alpha = 1f;
					trinket.gameButton.onClick = delegate()
					{
						if (this.isShowingMultipleSelectionMenu)
						{
							this.OnMultipleTrinketSelectionChanged(trinket, ind);
						}
						else
						{
							onTrinketSelected(ind);
						}
					};
					trinket.gameButton.Register();
					num++;
				}
			}
		}

		public void OnTrinketUiChanged(Trinket trinket)
		{
			for (int i = this.trinkets.Count - 1; i >= 0; i--)
			{
				if (this.trinkets[i].trinketUi.simTrinket == trinket)
				{
					this.trinkets[i].trinketUi.InitVisual(false);
				}
			}
		}

		public void ClearMultipleTrinketsSelection()
		{
			this.multipleSelectedTrinkets.Clear();
			for (int i = this.trinkets.Count - 1; i >= 0; i--)
			{
				this.trinkets[i].multipleSelectionImage.transform.SetScale(0f);
				if (this.isShowingMultipleSelectionMenu)
				{
					this.trinkets[i].canvasGroup.alpha = 0.6f;
				}
				else
				{
					this.trinkets[i].canvasGroup.alpha = 1f;
				}
			}
			this.disassembleMultipleTrinketsButton.text.text = LM.Get("TRINKET_DISASSEMBLE");
			this.dissassembleMultipleRewardAmount.text = "0";
			this.disassembleMultipleTrinketsButton.interactable = false;
		}

		private void OnMultipleTrinketSelectionChanged(ButtonSelectTrinket trinket, int trinketIndex)
		{
			if (this.multipleSelectedTrinkets.Contains(this.lastTrinkets[trinketIndex]))
			{
				this.multipleSelectedTrinkets.Remove(this.lastTrinkets[trinketIndex]);
				DOTween.Sequence().Append(trinket.multipleSelectionImage.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack)).Append(trinket.canvasGroup.DOFade(0.6f, 0.1f)).Play<Sequence>();
			}
			else
			{
				this.multipleSelectedTrinkets.Add(this.lastTrinkets[trinketIndex]);
				trinket.canvasGroup.DOFade(1f, 0.1f);
				trinket.multipleSelectionImage.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
			}
			double num = 0.0;
			for (int i = this.multipleSelectedTrinkets.Count - 1; i >= 0; i--)
			{
				num += this.multipleSelectedTrinkets[i].GetDestroyReward();
			}
			this.dissassembleMultipleRewardAmount.text = GameMath.GetDoubleString(num);
			trinket.trinketUi.simTrinket.GetDestroyReward();
			bool flag = this.multipleSelectedTrinkets.Count == 0;
			this.disassembleMultipleTrinketsButton.interactable = !flag;
			this.disassembleMultipleTrinketsButton.text.text = ((!flag) ? string.Format(LM.Get("MULTIPLE_TRINKETS_DISENCHANT_BUTTON"), this.multipleSelectedTrinkets.Count) : LM.Get("TRINKET_DISASSEMBLE"));
		}

		private void OnButtonDestroy(ButtonSelectTrinket obj, RectTransform parent, int index)
		{
			UiManager.willRemoveFromToUpdates.Add(obj.GetComponent<GameButton>());
		}

		private void OnButtonSpawn(ButtonSelectTrinket obj, RectTransform parent, int index)
		{
		}

		public void SetScrollPosition(float pos)
		{
			this.scrollRect.content.SetAnchorPosY(pos);
		}

		private void CalculatePositions()
		{
			int count = this.trinkets.Count;
			for (int i = 0; i < count; i++)
			{
				ButtonSelectTrinket buttonSelectTrinket = this.trinkets[i];
				RectTransform rectTransform = buttonSelectTrinket.transform as RectTransform;
				rectTransform.anchorMax = new Vector2(0.5f, 1f);
				rectTransform.anchorMin = new Vector2(0.5f, 1f);
				rectTransform.pivot = new Vector2(0.5f, 1f);
				rectTransform.anchoredPosition = this.CalculateTrinketPosition(i);
			}
		}

		private Vector2 CalculateTrinketPosition(int index)
		{
			int num = index % 4;
			int num2 = index / 4;
			return new Vector2(180f * (float)num - 270f, -180f * (float)num2 - 10f);
		}

		private void CalculateContentSize(int count)
		{
			int num = GameMath.CeilToInt((float)count / 4f);
			float y = 180f * (float)num + 10f + 20f;
			this.scrollRect.content.SetSizeDeltaY(y);
		}

		public void UpdateTrinkets(Simulator sim, Dictionary<string, Sprite> spritesHeroPortrait)
		{
			if (UiManager.stateJustChanged && !this.isAnimatingSort && !this.isAnimatingMassDisenchant)
			{
				List<Trinket> sortedTrinkets = sim.GetSortedTrinkets();
				this.FindIndexChanges(sortedTrinkets);
				if (!this.isAnimatingSort)
				{
					int count = sortedTrinkets.Count;
					int i = 0;
					int count2 = this.trinkets.Count;
					while (i < count2)
					{
						ButtonSelectTrinket buttonSelectTrinket = this.trinkets[i];
						if (!buttonSelectTrinket.isAnimatingPop)
						{
							buttonSelectTrinket.trinketUi.transform.SetScale(1f);
						}
						if (i >= count)
						{
							buttonSelectTrinket.transform.localScale = new Vector3(1f, 1f, 1f);
							buttonSelectTrinket.gameButton.interactable = false;
							buttonSelectTrinket.trinketParent.gameObject.SetActive(false);
							buttonSelectTrinket.heroParent.SetActive(false);
							buttonSelectTrinket.notification.gameObject.SetActive(false);
							buttonSelectTrinket.pinParent.SetActive(false);
						}
						else
						{
							Trinket trinket = sortedTrinkets[i];
							buttonSelectTrinket.gameButton.interactable = true;
							buttonSelectTrinket.trinketParent.gameObject.SetActive(true);
							buttonSelectTrinket.trinketUi.gameObject.SetActive(true);
							buttonSelectTrinket.trinketUi.Init(trinket);
							bool active = trinket.IsCapped();
							buttonSelectTrinket.notification.gameObject.SetActive(active);
							HeroDataBase heroWithTrinket = sim.GetHeroWithTrinket(sortedTrinkets[i]);
							if (heroWithTrinket == null)
							{
								buttonSelectTrinket.heroParent.SetActive(false);
							}
							else
							{
								buttonSelectTrinket.heroParent.SetActive(true);
								buttonSelectTrinket.imageHero.sprite = spritesHeroPortrait[heroWithTrinket.id];
							}
							buttonSelectTrinket.pinParent.SetActive(sim.IsTrinketPinned(trinket) != -1);
						}
						i++;
					}
				}
			}
			if (sim.GetUnlock(UnlockIds.TRINKET_DISASSEMBLE).isCollected)
			{
				this.trinketDescHeader.enabled = false;
				this.dissassembleMultipleToggleText.enabled = true;
				this.dissassembleMultipleToggle.gameObject.SetActive(true);
			}
			else
			{
				this.trinketDescHeader.enabled = true;
				this.dissassembleMultipleToggleText.enabled = false;
				this.dissassembleMultipleToggle.gameObject.SetActive(false);
			}
			this.changeFilterOptionButton.text.text = PanelTrinketsScroller.GetLocalizedFilterName(sim.trinketSortType);
			if (sim.trinketSortType == TrinketSortType.Default)
			{
				this.filterArrow.gameObject.SetActive(false);
			}
			else
			{
				this.filterArrow.gameObject.SetActive(true);
				if (sim.isTrinketSortingDescending)
				{
					this.filterArrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
				}
				else
				{
					this.filterArrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				}
			}
		}

		public void UpdateLastTrinkets(List<Trinket> sortedTrinkets)
		{
			this.lastTrinkets = sortedTrinkets;
		}

		public void SetFilterOptions(bool isFilterShowing)
		{
			if (isFilterShowing)
			{
				this.filtersParent.DOScaleY(1f, 0.3f).SetEase(Ease.OutBack);
				if (this.isShowingMultipleSelectionMenu)
				{
					this.disassembleMultipleTrinketsMenu.DOScaleY(0f, 0.3f).SetEase(Ease.OutCubic);
					this.isShowingMultipleSelectionMenu = false;
					this.ClearMultipleTrinketsSelection();
					for (int i = this.trinkets.Count - 1; i >= 0; i--)
					{
						this.trinkets[i].canvasGroup.alpha = 1f;
					}
				}
				this.scrollRectTransform.DoTopDelta(244f, 0.3f, false).SetEase(Ease.OutBack);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuUp, 1f));
			}
			else
			{
				this.filtersParent.DOScaleY(0f, 0.3f).SetEase(Ease.OutCubic);
				this.scrollRectTransform.DoTopDelta(126f, 0.3f, false).SetEase(Ease.OutCubic);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuDown, 1f));
			}
		}

		public void ToggleMultipleSelectionMenu(bool isFilterShowing)
		{
			if (!this.isShowingMultipleSelectionMenu)
			{
				this.isShowingMultipleSelectionMenu = true;
				if (isFilterShowing)
				{
					this.filtersParent.DOScaleY(0f, 0.3f).SetEase(Ease.OutCubic);
				}
				this.disassembleMultipleTrinketsMenu.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
				this.scrollRectTransform.DoTopDelta(330f, 0.3f, false).SetEase(Ease.OutBack);
				for (int i = this.trinkets.Count - 1; i >= 0; i--)
				{
					this.trinkets[i].canvasGroup.alpha = 0.6f;
				}
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuUp, 1f));
			}
			else
			{
				this.ClearMultipleTrinketsSelection();
				this.isShowingMultipleSelectionMenu = false;
				for (int j = this.trinkets.Count - 1; j >= 0; j--)
				{
					this.trinkets[j].canvasGroup.alpha = 1f;
				}
				this.disassembleMultipleTrinketsMenu.DOScaleY(0f, 0.3f).SetEase(Ease.OutCubic);
				this.scrollRectTransform.DoTopDelta(126f, 0.3f, false).SetEase(Ease.OutCubic);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuDown, 1f));
			}
		}

		public void PlayTrinketSelectedFeedback(Trinket trinket)
		{
			int num = this.lastTrinkets.IndexOf(trinket);
			if (num == -1)
			{
				return;
			}
			ButtonSelectTrinket trinketButton = this.trinkets[num];
			trinketButton.trinketUi.transform.SetScale(1.3f);
			trinketButton.isAnimatingPop = true;
			DOTween.Sequence().AppendInterval(0.1f).Append(trinketButton.trinketUi.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBounce)).AppendCallback(delegate
			{
				trinketButton.isAnimatingPop = false;
			}).Play<Sequence>();
		}

		public void DoMassDisenchantAnim(List<TrinketUi> clones, Vector3 invPos)
		{
			List<ButtonSelectTrinket> list = new List<ButtonSelectTrinket>();
			foreach (Trinket trinket in this.multipleSelectedTrinkets)
			{
				foreach (ButtonSelectTrinket buttonSelectTrinket in this.trinkets)
				{
					if (trinket == buttonSelectTrinket.trinketUi.simTrinket)
					{
						list.Add(buttonSelectTrinket);
						break;
					}
				}
			}
			ButtonSelectTrinket buttonSelectTrinket2 = list[0];
			foreach (ButtonSelectTrinket buttonSelectTrinket3 in list)
			{
				if (buttonSelectTrinket3.rectTransform.anchoredPosition.y > buttonSelectTrinket2.rectTransform.anchoredPosition.y)
				{
					buttonSelectTrinket2 = buttonSelectTrinket3;
				}
			}
			float num = buttonSelectTrinket2.rectTransform.anchoredPosition.y + 10f;
			float num2 = this.scrollRect.content.sizeDelta.y - this.scrollRect.viewport.rect.height;
			if (-num > num2)
			{
				num = -num2;
			}
			this.scrollRect.content.SetAnchorPosY(-num);
			Sequence sequence = DOTween.Sequence();
			this.manager.EnableInputBlocker();
			this.isAnimatingMassDisenchant = true;
			for (int i = 0; i < clones.Count; i++)
			{
				Sequence sequence2 = DOTween.Sequence();
				ButtonSelectTrinket buttonSelectTrinket4 = list[i];
				TrinketUi tui = clones[i];
				tui.transform.SetParent(base.transform.parent);
				sequence2.Append(tui.transform.DOScale(0f, 0.4f).SetEase(Ease.InBack)).Join(tui.transform.DORotate(new Vector3(0f, 0f, -360f), 0.4f, RotateMode.LocalAxisAdd).SetEase(Ease.InQuart)).Join(buttonSelectTrinket4.multipleSelectionImage.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack)).AppendCallback(delegate
				{
					tui.transform.localScale = Vector3.one;
					tui.imageBody.transform.localScale = Vector3.zero;
					tui.imageBg.transform.localScale = Vector3.zero;
					for (int l = 0; l < tui.icons.Length; l++)
					{
						Image image2 = tui.icons[l];
						if (image2 != null && image2.gameObject.activeSelf)
						{
							image2.transform.localScale = Vector3.zero;
						}
					}
				});
				for (int j = 2; j < tui.icons.Length; j++)
				{
					Image image = tui.icons[j];
					if (image != null && image.gameObject.activeSelf)
					{
						image.transform.localScale = Vector3.zero;
						sequence2.Insert(0.41f + (float)j * 0.05f, image.transform.DOScale(1f, 0.1f).SetEase(Ease.OutBack));
					}
				}
				float num3 = sequence2.Duration(true) + 0.5f;
				for (int k = 2; k < tui.icons.Length; k++)
				{
					Image item = tui.icons[k];
					tui.transform.SetParent(base.transform.parent.parent.parent.parent);
					if (item != null && item.gameObject.activeSelf)
					{
						item.transform.localScale = Vector3.zero;
						sequence2.Insert(num3 + (float)k * 0.2f, item.transform.DOMove(invPos, 0.3f, false).SetEase(Ease.InOutQuad).OnComplete(delegate
						{
							item.transform.localScale = Vector3.zero;
							this.manager.panelHubTrinkets.BounceSmithTabButton();
						}));
					}
				}
				sequence.Insert(0.1f + 0.1f * (float)i, sequence2);
			}
			sequence.AppendCallback(delegate
			{
				this.isAnimatingMassDisenchant = false;
			});
			sequence.AppendCallback(delegate
			{
				UiManager.stateJustChanged = true;
			});
			sequence.AppendCallback(delegate
			{
				this.manager.DisableInputBlocker();
				foreach (TrinketUi trinketUi in clones)
				{
					UnityEngine.Object.Destroy(trinketUi.gameObject);
				}
				this.ClearMultipleTrinketsSelection();
			});
			sequence.Play<Sequence>();
		}

		private void FindIndexChanges(List<Trinket> sortedTrinkets)
		{
			List<IndexChangePair> list = new List<IndexChangePair>();
			if (this.lastTrinkets != null)
			{
				int num = Mathf.Max(sortedTrinkets.Count, this.lastTrinkets.Count);
				for (int i = 0; i < num; i++)
				{
					Trinket trinket = (i >= sortedTrinkets.Count) ? null : sortedTrinkets[i];
					Trinket trinket2 = (i >= this.lastTrinkets.Count) ? null : this.lastTrinkets[i];
					if (trinket != null && trinket2 != null && trinket != trinket2)
					{
						int from = this.lastTrinkets.IndexOf(trinket);
						IndexChangePair item = new IndexChangePair
						{
							from = from,
							to = i
						};
						list.Add(item);
					}
					else if (trinket2 == null)
					{
						IndexChangePair item2 = new IndexChangePair
						{
							from = -1,
							to = i
						};
						list.Add(item2);
					}
					else if (trinket == null)
					{
						IndexChangePair item3 = new IndexChangePair
						{
							from = i,
							to = -1
						};
						list.Add(item3);
					}
				}
				if (list.Count > 0)
				{
					this.DoReorderAnimation(list);
				}
			}
			this.lastTrinkets = sortedTrinkets;
		}

		private void DoReorderAnimation(List<IndexChangePair> indexChanges)
		{
			int count = indexChanges.Count;
			if (this.reOrderAnim != null && this.isAnimatingSort)
			{
				this.reOrderAnim.Kill(false);
				this.CalculatePositions();
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
					Vector2 endValue = this.CalculateTrinketPosition(indexChangePair.to);
					ButtonSelectTrinket buttonSelectTrinket = this.trinkets[indexChangePair.from];
					this.reOrderAnim.Insert(num += num3, (buttonSelectTrinket.transform as RectTransform).DOAnchorPos(endValue, 0.2f, false).SetEase(Ease.InOutQuad));
				}
			}
			this.reOrderAnim.OnComplete(delegate
			{
				this.CalculatePositions();
				this.isAnimatingSort = false;
				UiManager.stateJustChanged = true;
			});
			this.reOrderAnim.Play<Sequence>();
		}

		public RectTransform rectTransform;

		public GameButton changeFilterOptionButton;

		public GameButton filterOptionsToggle;

		public GameButton disassembleMultipleTrinketsButton;

		public Text dissassembleMultipleRewardAmount;

		public RectTransform disassembleMultipleTrinketsMenu;

		public Text dissassembleMultipleMenuText;

		public GameButton dissassembleMultipleToggle;

		public Text dissassembleMultipleToggleText;

		public UiManager manager;

		[NonSerialized]
		public List<ButtonSelectTrinket> trinkets;

		[NonSerialized]
		public bool isAnimatingSort;

		[NonSerialized]
		public List<Trinket> lastTrinkets;

		[NonSerialized]
		public List<Trinket> multipleSelectedTrinkets = new List<Trinket>();

		public bool isAnimatingMassDisenchant;

		private Vector3[] focusTargetCorners = new Vector3[4];

		private Vector3[] viewportCorners = new Vector3[4];

		[SerializeField]
		private ButtonSelectTrinket trinketPrefab;

		[SerializeField]
		private ScrollRect scrollRect;

		[SerializeField]
		private Text trinketDescHeader;

		[SerializeField]
		private Text trinketSortByLabel;

		[SerializeField]
		private RectTransform filtersParent;

		[SerializeField]
		private RectTransform scrollRectTransform;

		[SerializeField]
		private RectTransform filterArrow;

		private Sequence reOrderAnim;

		private bool isShowingMultipleSelectionMenu;

		private const int trinketsCollumnCount = 4;

		private const float collumntGap = 180f;

		private const float rowGap = 180f;

		private const float startXOffset = 270f;

		private const float startYOffset = 10f;

		private const float bottomGap = 20f;
	}
}
