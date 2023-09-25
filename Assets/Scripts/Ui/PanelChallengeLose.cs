using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelChallengeLose : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
			this.skullsEndY = this.leftSkullRectTransform.anchoredPosition.y;
			this.midSkullEndY = this.midSkullRectTransform.anchoredPosition.y;
		}

		public void InitStrings()
		{
			this.textTitle.text = LM.Get("UI_CHALLENGE_LOSE");
			this.button.text.text = LM.Get("UI_OKAY");
			this.riftLostHintMessage = LM.Get("UI_LOSE_RIFT_HINT");
			this.timeChallengeLostHintMessage = LM.Get("UI_TIME_CHALLENGE_HINT");
		}

		public void SetStartPosition()
		{
			this.popupRectTransform.localScale = Vector3.zero;
			this.textTitle.transform.localScale = Vector3.one * 2f;
			Color color = this.textTitle.color;
			color.a = 0f;
			this.textTitle.color = color;
			this.popupRectTransform.SetSizeDeltaY(this.startHeight);
			this.paperScrollRectTrasform.SetSizeDeltaX(this.paperRollStartWidth);
			this.leftSkullRectTransform.SetAnchorPosY(this.skullsStartY);
			this.rightSkullRectTransform.SetAnchorPosY(this.skullsStartY);
			this.canvasGroup.alpha = 0f;
			this.midSkullRectTransform.gameObject.SetActive(!this.isRift);
			this.midStone.gameObject.SetActive(this.isRift);
			this.rightTorch.gameObject.SetActive(this.isRift);
			this.leftTorch.gameObject.SetActive(this.isRift);
			if (this.isRift)
			{
				this.midStone.SetAnchorPosY(this.skullsStartY);
			}
			else
			{
				this.midSkullRectTransform.SetAnchorPosY(this.skullsStartY);
				this.midSkullRectTransform.eulerAngles = new Vector3(0f, 0f, (float)((!GameMath.GetProbabilityOutcome(0.5f, GameMath.RandType.NoSeed)) ? -15 : 15));
			}
			this.challengeLostHintParent.SetActive(this.showHint);
			if (this.showHint)
			{
				this.challengeLostHint.text = ((!this.isRift) ? this.timeChallengeLostHintMessage : this.riftLostHintMessage);
			}
		}

		public void FadeIn()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.popupRectTransform.DOScale(0.8f, 0.2f).SetEase(Ease.OutQuint)).Insert(0.05f, this.paperScrollRectTrasform.DOSizeDelta(new Vector2(this.paperRollEndWidth, this.paperScrollRectTrasform.sizeDelta.y), 0.5f, false).SetEase(Ease.OutElastic, 1.7f, 0.4f)).Insert(0.4f, this.textTitle.rectTransform.DOScale(1f, 0.15f).SetEase(Ease.InSine)).Insert(0.4f, this.textTitle.DOFade(1f, 0.15f).SetEase(Ease.InSine)).Insert(0.1f, this.popupRectTransform.DOSizeDelta(new Vector2(this.popupRectTransform.sizeDelta.x, this.endHeight), 0.3f, false).SetEase(Ease.OutBack)).Insert(0.3f, this.leftSkullRectTransform.DOAnchorPosY(this.skullsEndY, 0.3f, false).SetEase(Ease.OutBack)).Insert(0.35f, this.rightSkullRectTransform.DOAnchorPosY(this.skullsEndY, 0.3f, false).SetEase(Ease.OutBack));
			if (this.isRift)
			{
				sequence.Insert(0.4f, this.midStone.DOAnchorPosY(this.midSkullEndY, 0.3f, false).SetEase(Ease.OutBack));
			}
			else
			{
				sequence.Insert(0.4f, this.midSkullRectTransform.DOAnchorPosY(this.midSkullEndY, 0.3f, false).SetEase(Ease.OutBack)).Insert(0.5f, this.midSkullRectTransform.DORotate(new Vector3(0f, 0f, 0f), 0.3f, RotateMode.Fast).SetEase(Ease.OutElastic, 1.7f, 0.4f));
			}
			sequence.Insert(0f, this.canvasGroup.DOFade(0.85f, 0.3f).SetEase(Ease.OutCubic));
			sequence.Play<Sequence>();
		}

		public void FadeOut(Action onComplete)
		{
			this.challengeLostHintParent.SetActive(false);
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.paperScrollRectTrasform.DOSizeDelta(new Vector2(this.paperRollStartWidth, this.paperScrollRectTrasform.sizeDelta.y), 0.5f, false).SetEase(Ease.InBack)).Insert(0.1f, this.textTitle.rectTransform.DOScale(0f, 0.4f).SetEase(Ease.InBack)).Insert(0.1f, this.popupRectTransform.DOSizeDelta(new Vector2(this.popupRectTransform.sizeDelta.x, this.startHeight), 0.3f, false).SetEase(Ease.InBack)).Insert(0.4f, this.popupRectTransform.DOScale(0f, 0.2f).SetEase(Ease.InSine));
			if (this.isRift)
			{
				sequence.Insert(0.2f, this.midStone.DOAnchorPosY(this.skullsStartY, 0.3f, false).SetEase(Ease.InBack));
			}
			else
			{
				sequence.Insert(0.2f, this.midSkullRectTransform.DOAnchorPosY(this.skullsStartY, 0.3f, false).SetEase(Ease.InBack));
			}
			sequence.Insert(0.25f, this.rightSkullRectTransform.DOAnchorPosY(this.skullsStartY, 0.3f, false).SetEase(Ease.InBack)).Insert(0.3f, this.leftSkullRectTransform.DOAnchorPosY(this.skullsStartY, 0.3f, false).SetEase(Ease.InBack)).Insert(0f, this.canvasGroup.DOFade(0f, 0.3f).SetEase(Ease.OutCubic)).OnComplete(delegate
			{
				onComplete();
			});
			sequence.Play<Sequence>();
		}

		public void SetProgress(int currentWave, int totWave)
		{
			float x = 1f * (float)currentWave / (float)totWave;
			this.fillMask.anchorMax = new Vector2(x, 1f);
			this.textProgress.text = string.Format(LM.Get("UI_CHALLENGE_LOSE_DESC"), (currentWave - 1).ToString(), totWave.ToString());
		}

		public GameButton button;

		public RectTransform rectProgress;

		public RectTransform fillMask;

		public Text textProgress;

		public Text textTitle;

		public float timer;

		public CanvasGroup canvasGroup;

		public RectTransform popupRectTransform;

		public RectTransform paperScrollRectTrasform;

		public RectTransform midSkullRectTransform;

		public RectTransform leftSkullRectTransform;

		public RectTransform rightSkullRectTransform;

		public RectTransform midStone;

		public RectTransform rightTorch;

		public RectTransform leftTorch;

		public float startHeight = 60f;

		public float endHeight = 894f;

		public float paperRollStartWidth = 150f;

		public float paperRollEndWidth = 422f;

		public float skullsStartY;

		public float skullsEndY;

		public float midSkullEndY;

		public bool isRift;

		public bool showHint;

		public GameObject challengeLostHintParent;

		public Text challengeLostHint;

		private string timeChallengeLostHintMessage;

		private string riftLostHintMessage;
	}
}
