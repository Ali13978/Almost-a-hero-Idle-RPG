using System;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class CharmEffectsPanel : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.widgetMatch = new ObjectMatcher<CharmBuff, CharmEffectWidget>(new Func<CharmEffectWidget>(this.WidgetCreator), new Action<CharmEffectWidget, bool>(this.DestroyObject));
		}

		private void DestroyObject(CharmEffectWidget widget, bool challengeActive)
		{
			widget.DoCurseOutAnimation(delegate
			{
				this.needReorder = true;
			}, challengeActive);
		}

		private CharmEffectWidget WidgetCreator()
		{
			CharmEffectWidget charmEffectWidget = UnityEngine.Object.Instantiate<CharmEffectWidget>(this.charmEffectWidgetPrefab, this.widgetParent);
			charmEffectWidget.transform.localScale = Vector3.one * 0.75f;
			charmEffectWidget.SetRectSize(new Vector2(80f, 80f));
			return charmEffectWidget;
		}

		public static Vector2 GetWidgetPosition(int index)
		{
			float num = 62f;
			float num2 = 32f;
			int num3 = index / 12;
			int num4 = index % 12;
			return new Vector2(-num2 - (float)num4 * num, (float)num3 * num);
		}

		public void DoCharmGoAnimation(Vector2 startPosition, Vector2 targetPosition)
		{
			this.isAnimatingNewCharm = true;
			this.iconToTransform.rectTransform.position = startPosition;
			this.iconToTransform.gameObject.SetActive(true);
			this.iconToTransform.SetAlpha(1f);
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.iconToTransform.rectTransform.DOAnchorPos(new Vector2(-372f, 400f), 0.5f, false).SetEase(Ease.InOutCubic)).Join(this.iconToTransform.transform.DOScale(1.4f, 0.5f).SetEase(Ease.OutBack)).Append(this.iconToTransform.DOFade(0.3f, 0.5f).SetEase(Ease.InFlash, 8f)).AppendInterval(0.2f).Append(this.iconToTransform.rectTransform.DOAnchorPos(targetPosition, 0.6f, false).SetEase(Ease.InBack)).Join(this.iconToTransform.transform.DOScale(0.6f, 0.5f)).Join(this.iconToTransform.DOFade(0.6f, 0.5f)).OnComplete(delegate
			{
				this.iconToTransform.gameObject.SetActive(false);
				this.isAnimatingNewCharm = false;
				this.needReorder = true;
			});
			sequence.Play<Sequence>();
		}

		[NonSerialized]
		public ObjectMatcher<CharmBuff, CharmEffectWidget> widgetMatch;

		public CharmEffectWidget charmEffectWidgetPrefab;

		public Image iconToTransform;

		public RectTransform widgetParent;

		public RectTransform toolTipParent;

		public Text ttName;

		public Text ttActivation;

		public int toolTipIndex = -1;

		public int activateIndex = -1;

		public bool isAnimatingNewCharm;

		public bool needReorder;
	}
}
