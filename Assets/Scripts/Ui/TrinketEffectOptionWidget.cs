using System;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui
{
	public class TrinketEffectOptionWidget : Selectable, IPointerClickHandler, IEventSystemHandler
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

		public void DoBoingAnimation()
		{
			if (this.selectAnimation != null && this.selectAnimation.isPlaying)
			{
				this.selectAnimation.Goto(0f, true);
			}
			else
			{
				this.selectAnimation = DOTween.Sequence();
				this.selectAnimation.Append(this.rectTransform.DOPunchScale(new Vector3(0.03f, -0.02f, 0f), 0.5f, 7, 1f));
				this.selectAnimation.Insert(0.05f, this.selectionOutline.DOColor(Color.white, 0.3f).SetEase(EaseFactory.Kick(Ease.OutQuint, Ease.InQuint)));
				this.selectAnimation.Play<Sequence>();
			}
		}

		public void EnableNotAvailableDesc()
		{
			this.bodyParent.gameObject.SetActive(false);
			this.notavailableDesc.gameObject.SetActive(true);
			this.SetNormal();
		}

		public void DisableNotAvailableDesc()
		{
			this.bodyParent.gameObject.SetActive(true);
			this.notavailableDesc.gameObject.SetActive(false);
		}

		public void SetSelected()
		{
			this.selectionOutline.gameObject.SetActive(true);
			this.background.color = TrinketEffectOptionWidget.selectedBackgroundColor;
			this.countParent.DOScale(1f, 0.1f).SetEase(Ease.OutBack).Play<Tweener>();
		}

		public void SetNormal()
		{
			this.selectionOutline.gameObject.SetActive(false);
			this.background.color = TrinketEffectOptionWidget.normalBackgroundColor;
			this.countParent.SetScale(0.78f);
		}

		public void EnableBottomLine()
		{
			this.bottomLine.gameObject.SetActive(true);
		}

		public void DisableBottomLine()
		{
			this.bottomLine.gameObject.SetActive(false);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!base.interactable)
			{
				return;
			}
			this.onClick(this.index);
		}

		public void PlayEffectSelectedAnim(int newAmount, TrinketEffect effect, Action onComplete, Action<TrinketEffectOptionWidget, TrinketEffect> setDisabledCallback)
		{
			Sequence s = DOTween.Sequence().Append(this.countParent.DOScale(1.5f, 0.3f).SetEase(Ease.InBack)).AppendCallback(delegate
			{
				this.textRemaining.text = "x" + newAmount;
				if (newAmount <= 0)
				{
					setDisabledCallback(this, effect);
				}
			});
			s.Append(this.countParent.DOScale(1f, 0.3f).SetEase(Ease.OutBack)).AppendInterval(0.1f).AppendCallback(delegate
			{
				onComplete();
			}).Play<Sequence>();
		}

		private RectTransform m_rectTransform;

		public static Color normalBackgroundColor = new Color(0.07843138f, 0.1137255f, 0.1647059f, 1f);

		public static Color selectedBackgroundColor = new Color(0.1333333f, 0.2078431f, 0.3215686f, 1f);

		public Text textDescription;

		public Text textRemaining;

		public Text textMaxLevel;

		public Image selectionOutline;

		public Image background;

		public Image bottomLine;

		public CanvasGroup canvasGroup;

		public RectTransform bodyParent;

		public RectTransform countParent;

		public Text notavailableDesc;

		[NonSerialized]
		public int index;

		public Action<int> onClick;

		private Sequence selectAnimation;
	}
}
