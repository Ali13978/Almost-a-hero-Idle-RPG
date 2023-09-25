using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelCurseIntroduction : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.okayButton.text.text = LM.Get("UI_OKAY");
		}

		public void DoInAnimation()
		{
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.curseAppear, 1f));
			this.headerRect.SetSizeDeltaX(0f);
			this.headerRect.gameObject.SetActive(false);
			this.okayButton.rectTransform.localScale = Vector3.zero;
			this.charmCard.charmCard.charmIcon.gameObject.SetActive(false);
			this.charmCard.charmCard.levelParent.gameObject.SetActive(false);
			this.charmCard.charmCard.background.sprite = UiData.inst.spriteCharmBack;
			this.charmCard.charmCard.onlyCardBack = true;
			this.charmCard.rectTransform.SetSizeDeltaX(170f);
			this.charmCard.charmCardInfo.gameObject.SetActive(false);
			this.charmCard.rectTransform.SetAnchorPosX(1200f);
			this.seq = DOTween.Sequence();
			this.seq.Append(this.charmCard.rectTransform.DOAnchorPosX(0f, 0.2f, false).SetEase(Ease.OutCirc));
			this.seq.InsertCallback(0f, delegate
			{
				this.PlaySound(SoundArchieve.inst.uiCharmChoiceEnter[0]);
			});
			this.seq.Append(this.DoCardShow(this.charmCard));
			float atPosition = this.seq.Duration(true) - 0.3f;
			this.seq.InsertCallback(atPosition, delegate
			{
				this.headerRect.gameObject.SetActive(true);
			});
			this.seq.Insert(atPosition, this.headerRect.DOSizeDeltaX(650f, 0.3f, false).SetEase(Ease.OutBack));
			this.seq.Append(this.okayButton.rectTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack));
			this.seq.Play<Sequence>();
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

		private void PlaySound(AudioClip clip)
		{
			UiManager.sounds.Add(new SoundEventUiSimple(clip, 1f));
		}

		public RectTransform headerRect;

		public Text headerText;

		public GameButton okayButton;

		public CharmOptionCard charmCard;

		private Sequence seq;

		internal int curseId;
	}
}
