using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelBuyCharmFlashOffer : PanelBuyFlashOffer
	{
		public override void Register()
		{
			base.Register();
			base.AddToInits();
		}

		public override void AahUpdate(float dt)
		{
			this.timeInPanel += dt;
		}

		public override void Init()
		{
			this.charmOptionCard.interactable = false;
			this.charmOptionCard.charmCard.levelProgresBarParent.gameObject.SetActive(true);
			this.charmOptionCard.selectionOutline.gameObject.SetActive(false);
		}

		public void DoComeIn()
		{
			this.glowFirst.gameObject.SetActive(false);
			this.glowSecond.gameObject.SetActive(false);
			this.timeInPanel = 0f;
			this.charmOptionCard.rectTransform.SetAnchorPosY(1000f);
			this.popupParent.SetAnchorPosY(-1200f);
			this.charmOptionCard.charmCard.copyCounterParent.rectTransform.SetSizeDeltaX(52f);
			this.charmOptionCard.charmCard.copyCounterParent.gameObject.SetActive(false);
			this.state = 0;
			this.seq = DOTween.Sequence();
			this.seq.Append(this.charmOptionCard.rectTransform.DOAnchorPosY(434f, 0.4f, false).SetEase(Ease.OutCubic)).Join(this.popupParent.DOAnchorPosY(-171f, 0.4f, false).SetEase(Ease.OutCubic)).OnComplete(delegate
			{
				this.state = 1;
			});
			this.seq.Play<Sequence>();
		}

		public void DoBuyCount()
		{
			this.state = 2;
			if (this.counterCharmCards != null && this.counterCharmCards.Count > 0)
			{
				foreach (CharmCard charmCard in this.counterCharmCards)
				{
					UnityEngine.Object.Destroy(charmCard.gameObject);
				}
				this.counterCharmCards.Clear();
			}
			else if (this.counterCharmCards == null)
			{
				this.counterCharmCards = new List<CharmCard>();
			}
			CharmCard charmCard2 = UnityEngine.Object.Instantiate<CharmCard>(this.charmOptionCard.charmCard, this.charmOptionCard.charmCard.transform.parent);
			charmCard2.levelProgresBarParent.gameObject.SetActive(false);
			charmCard2.levelParent.gameObject.SetActive(false);
			charmCard2.rectTransform.position = this.counterCardPositioner.position;
			charmCard2.rectTransform.rotation = this.counterCardPositioner.rotation;
			charmCard2.gameObject.SetActive(false);
			int siblingIndex = this.charmOptionCard.charmCard.transform.GetSiblingIndex();
			charmCard2.transform.SetSiblingIndex(siblingIndex);
			this.counterCharmCards.Add(charmCard2);
			for (int i = 0; i < this.cardCounterAmount - 1; i++)
			{
				CharmCard charmCard3 = UnityEngine.Object.Instantiate<CharmCard>(charmCard2, this.charmOptionCard.charmCard.transform.parent);
				charmCard3.transform.position = charmCard2.transform.position;
				charmCard3.transform.SetSiblingIndex(siblingIndex);
				this.counterCharmCards.Add(charmCard3);
			}
			this.glowSecond.transform.SetParent(this.charmOptionCard.charmCard.transform);
			this.glowSecond.transform.SetAsLastSibling();
			this.glowFirst.transform.SetParent(this.charmOptionCard.charmCard.transform);
			this.glowFirst.transform.SetSiblingIndex(2);
			this.glowFirst.SetAlpha(0f);
			this.glowSecond.SetAlpha(0f);
			this.glowFirst.rectTransform.anchoredPosition = default(Vector2);
			this.glowSecond.rectTransform.anchoredPosition = default(Vector2);
			this.glowFirst.gameObject.SetActive(true);
			this.glowSecond.gameObject.SetActive(true);
			Sequence sequence = DOTween.Sequence();
			Sequence cardCounterSequence = this.GetCardCounterSequence();
			sequence.Append(cardCounterSequence).Insert(0.3f, DOTween.To(() => this.oldDupplicateCount, delegate(int x)
			{
				this.oldDupplicateCount = x;
			}, this.targetDupplicate, cardCounterSequence.Duration(true) - 0.3f).SetEase(Ease.Linear).OnUpdate(new TweenCallback(this.SetSliderAndText))).Insert(cardCounterSequence.Duration(true) - 0.2f, this.GetGlowSequence()).OnComplete(delegate
			{
				this.state = 3;
				this.glowFirst.gameObject.SetActive(false);
				this.glowSecond.gameObject.SetActive(false);
				this.glowSecond.transform.SetParent(base.transform);
				this.glowFirst.transform.SetParent(base.transform);
			});
			sequence.Play<Sequence>();
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiFlashOfferPurchase, 1f));
		}

		private Sequence GetGlowSequence()
		{
			return DOTween.Sequence().Append(this.glowFirst.DOFade(1f, 0.3f).SetEase(Ease.OutCubic)).Insert(0.2f, this.glowSecond.DOFade(1f, 0.2f).SetEase(Ease.InCubic)).Append(this.glowFirst.DOFade(0f, 0.5f).SetEase(Ease.OutCubic)).Join(this.glowSecond.DOFade(0f, 0.4f).SetEase(Ease.OutCubic));
		}

		private Sequence GetCardCounterSequence()
		{
			Sequence sequence = DOTween.Sequence();
			for (int i = this.cardCounterAmount - 1; i >= 0; i--)
			{
				CharmCard card = this.counterCharmCards[i];
				sequence.InsertCallback(0.1f * (float)i, delegate
				{
					card.gameObject.SetActive(true);
				});
				sequence.Insert(0.1f * (float)i, card.rectTransform.DOAnchorPos(this.charmOptionCard.charmCard.rectTransform.anchoredPosition, 0.3f, false).SetEase(Ease.InOutCubic)).Insert(0.1f * (float)i + 0.1f, this.charmOptionCard.charmCard.transform.DOPunchScale(Vector3.one * 0.1f, 0.1f, 10, 1f)).Insert(0.1f * (float)i, card.rectTransform.DORotate(Vector3.zero, 0.3f, RotateMode.Fast)).AppendCallback(delegate
				{
					card.gameObject.SetActive(false);
				});
			}
			return sequence;
		}

		public void DoBought()
		{
			this.state = 2;
			this.seq = DOTween.Sequence();
			this.seq.Append(this.charmOptionCard.rectTransform.DOAnchorPosY(0f, 0.4f, false).SetEase(Ease.InOutCubic)).Join(this.popupParent.DOAnchorPosY(-1200f, 0.2f, false).SetEase(Ease.InOutCubic)).AppendCallback(delegate
			{
				this.charmOptionCard.charmCard.copyCounterParent.gameObject.SetActive(true);
			}).Append(this.charmOptionCard.charmCard.copyCounterParent.rectTransform.DOSizeDelta(new Vector2(220f, 65f), 0.2f, false).SetEase(Ease.OutBack)).Append(DOTween.To(() => this.oldDupplicateCount, delegate(int x)
			{
				this.oldDupplicateCount = x;
			}, this.targetDupplicate, 0.4f).SetEase(Ease.Linear)).OnUpdate(new TweenCallback(this.SetSliderAndText)).OnComplete(delegate
			{
				this.state = 3;
			});
			this.seq.Play<Sequence>();
		}

		private void SetSliderAndText()
		{
			this.charmOptionCard.charmCard.levelProgresBar.SetScale(GameMath.Clamp((float)this.oldDupplicateCount / (float)this.neededCount, 0f, 1f));
			this.charmOptionCard.charmCard.levelProgresBarText.text = this.oldDupplicateCount + "/" + this.neededCount;
		}

		public CharmOptionCard charmOptionCard;

		public Image cardFront;

		public RectTransform counterCardPositioner;

		public Image glowFirst;

		public Image glowSecond;

		public int targetDupplicate;

		public int oldDupplicateCount;

		public int neededCount;

		private int cardCounterAmount = 5;

		private List<CharmCard> counterCharmCards;

		private Sequence seq;

		public int state;
	}
}
