using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelCharmSelect : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public void InitStrings()
		{
			this.header.text = LM.Get("CHARM_SELECT_HEADER");
			this.selectButtonText.text = LM.Get("UI_SELECT");
		}

		public override void Init()
		{
			this.charmOptionCards = new List<CharmOptionCard>();
			this.HandleOptionCards();
			this.selectButton.onClick = new GameButton.VoidFunc(this.Button_OnSubmit);
		}

		public void HideCards()
		{
			this.HandleOptionCards();
			this.headerRect.gameObject.SetActive(false);
			this.headerRect.SetSizeDeltaX(0f);
			this.selectButton.rectTransform.localScale = Vector3.zero;
			foreach (CharmOptionCard charmOptionCard in this.charmOptionCards)
			{
				charmOptionCard.charmCard.charmIcon.gameObject.SetActive(false);
				charmOptionCard.charmCard.levelParent.gameObject.SetActive(false);
				charmOptionCard.charmCard.background.sprite = UiData.inst.spriteCharmBack;
				charmOptionCard.charmCard.onlyCardBack = true;
				charmOptionCard.rectTransform.SetSizeDeltaX(170f);
				charmOptionCard.charmCardInfo.gameObject.SetActive(false);
				charmOptionCard.rectTransform.SetAnchorPosX(1200f);
			}
		}

		public void DoBringCards()
		{
			this.seq = DOTween.Sequence();
			for (int i = 0; i < this.count; i++)
			{
				CharmOptionCard charmOptionCard = this.charmOptionCards[i];
				charmOptionCard.charmCard.background.color = Color.white;
				charmOptionCard.charmCard.backgroundFrame.enabled = false;
				if (i == 0)
				{
					this.seq.Append(charmOptionCard.rectTransform.DOAnchorPosX(0f, 0.2f, false).SetEase(Ease.OutCirc));
					this.seq.InsertCallback(0f, delegate
					{
						this.PlaySound(SoundArchieve.inst.uiCharmChoiceEnter[0]);
					});
				}
				else
				{
					this.seq.Insert(0.1f * (float)i, charmOptionCard.rectTransform.DOAnchorPosX(0f, 0.2f, false).SetEase(Ease.OutCirc));
					AudioClip clip = SoundArchieve.inst.uiCharmChoiceEnter[i];
					this.seq.InsertCallback(0.1f * (float)i, delegate
					{
						this.PlaySound(clip);
					});
				}
			}
			float num = this.seq.Duration(true);
			for (int j = 0; j < this.count; j++)
			{
				CharmOptionCard card = this.charmOptionCards[j];
				this.seq.Insert(num + 0.15f * (float)j, this.DoCardShow(card));
				AudioClip clip = SoundArchieve.inst.uiCharmChoiceTurn[j];
				this.seq.InsertCallback(num + 0.15f * (float)j, delegate
				{
					this.PlaySound(clip);
				});
			}
			float atPosition = this.seq.Duration(true) - 0.3f;
			this.seq.InsertCallback(atPosition, delegate
			{
				this.headerRect.gameObject.SetActive(true);
			});
			this.seq.Insert(atPosition, this.headerRect.DOSizeDeltaX(650f, 0.3f, false).SetEase(Ease.OutBack));
			this.seq.Append(this.selectButton.rectTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack));
			this.seq.Play<Sequence>();
			if (this.skipAnimation)
			{
				this.skipAnimation = false;
				this.seq.Complete(true);
			}
		}

		private void PlaySound(AudioClip clip)
		{
			UiManager.sounds.Add(new SoundEventUiSimple(clip, 1f));
		}

		private Sequence DoCardShow(CharmOptionCard card)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(card.charmCard.rectTransform.DORotate(new Vector3(0f, -90f, 0f), 0.1f, RotateMode.Fast).SetEase(Ease.InSine)).AppendCallback(delegate
			{
				card.charmCard.charmIcon.gameObject.SetActive(true);
				card.charmCard.levelParent.gameObject.SetActive(true);
				card.charmCard.rectTransform.localRotation = Quaternion.Euler(0f, 89f, 0f);
				card.charmCard.onlyCardBack = false;
			}).Append(card.charmCard.rectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.1f, RotateMode.Fast).SetEase(Ease.OutSine)).AppendInterval(0.3f).AppendCallback(delegate
			{
				card.charmCardInfo.gameObject.SetActive(true);
			}).AppendCallback(delegate
			{
				card.charmCard.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			}).Append(card.rectTransform.DOSizeDelta(new Vector2(720f, card.rectTransform.sizeDelta.y), 0.2f, false).SetEase(Ease.InOutSine));
			return sequence.Play<Sequence>();
		}

		public void HandleOptionCards()
		{
			Utility.FillUiElementList<CharmOptionCard>(this.charmOptionCardPrefab, this.cardsParent, this.count, this.charmOptionCards);
			for (int i = 0; i < this.count; i++)
			{
				CharmOptionCard charmOptionCard = this.charmOptionCards[i];
				if (charmOptionCard.charmCard.button != null)
				{
					UnityEngine.Object.Destroy(charmOptionCard.charmCard.button);
				}
				charmOptionCard.rectTransform.anchoredPosition = this.GetCardPosition(i);
				charmOptionCard.onClick = new Action<int>(this.Button_OnCharmSelect);
				charmOptionCard.index = i;
				charmOptionCard.selectionOutline.gameObject.SetActive(false);
				charmOptionCard.transform.localScale = Vector3.one * 0.95f;
			}
			this.headerRect.anchoredPosition = this.GetCardPosition(0) + new Vector2(0f, 190f);
			this.selectButton.rectTransform.anchoredPosition = this.GetCardPosition(this.count - 1) + new Vector2(0f, -190f);
		}

		private Vector2 GetCardPosition(int index)
		{
			float num = 70f + 260f * (float)(this.count - 1) / 2f;
			return new Vector2(0f, num - 260f * (float)index);
		}

		public bool IsSelectionInValidRange()
		{
			return this.selectedIndex >= 0 && this.selectedIndex < this.charmOptionCards.Count;
		}

		private void Button_OnSubmit()
		{
			if (this.onCharmSelect == null)
			{
				return;
			}
			this.onCharmSelect(this.selectedIndex);
			if (!this.IsSelectionInValidRange())
			{
				return;
			}
			if (this.isCurseInfo)
			{
				return;
			}
			CharmOptionCard charmOptionCard = this.charmOptionCards[this.selectedIndex];
			charmOptionCard.selectionOutline.gameObject.SetActive(false);
			charmOptionCard.transform.localScale = Vector3.one * 0.95f;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPrestigeActivated, 1f));
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiCharmSelected, 1f));
		}

		private void Button_OnCharmSelect(int index)
		{
			if (this.selectedIndex != -1)
			{
				CharmOptionCard charmOptionCard = this.charmOptionCards[this.selectedIndex];
				charmOptionCard.selectionOutline.gameObject.SetActive(false);
				charmOptionCard.transform.DOScale(Vector3.one * 0.95f, 0.2f).SetEase(Ease.OutBack);
			}
			CharmOptionCard charmOptionCard2 = this.charmOptionCards[index];
			charmOptionCard2.selectionOutline.gameObject.SetActive(true);
			charmOptionCard2.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
			this.selectedIndex = index;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiArtifactSlotSelect, 1f));
		}

		public CharmOptionCard charmOptionCardPrefab;

		public RectTransform cardsParent;

		public RectTransform headerRect;

		public Text header;

		public Text selectButtonText;

		[NonSerialized]
		public List<CharmOptionCard> charmOptionCards;

		public GameButton selectButton;

		public Action<int> onCharmSelect;

		public int selectedIndex;

		public int count = 3;

		public Color selectedBackgroundColor;

		public Color selectedActivationBackgroundColor;

		public Color normalBackgroundColor;

		public Color normalActivationBackgroundColor;

		public GameButton closeButton;

		[NonSerialized]
		public bool isCurseInfo;

		private Sequence seq;

		internal bool skipAnimation;
	}
}
