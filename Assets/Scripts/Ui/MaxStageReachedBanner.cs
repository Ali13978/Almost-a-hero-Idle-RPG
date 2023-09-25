using System;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class MaxStageReachedBanner : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.banner.SetScale(0f);
			this.banner.SetSizeDeltaX(130f);
			base.gameObject.SetActive(false);
		}

		public void InitStrings()
		{
			this.label.text = LM.Get("NEW_MAX_STAGE_REACHED");
		}

		public void UpdateBanner(Simulator simulator)
		{
			if (UiManager.adventureMaxStageJustReached && simulator.numPrestiges > 0 && simulator.GetActiveWorld().gameMode == GameMode.STANDARD)
			{
				UiManager.adventureMaxStageJustReached = false;
				DOTween.Sequence().AppendCallback(delegate
				{
					base.gameObject.SetActive(true);
				}).Append(this.banner.DOScale(1f, 0.1f)).Append(this.banner.DOSizeDeltaX(700f, 0.4f, false).SetEase(Ease.OutBack)).AppendInterval(3.5f).Append(this.banner.DOSizeDeltaX(130f, 0.2f, false).SetEase(Ease.InBack)).Append(this.banner.DOScale(0f, 0.1f)).AppendCallback(delegate
				{
					base.gameObject.SetActive(false);
				}).Play<Sequence>();
			}
		}

		[SerializeField]
		private Text label;

		[SerializeField]
		private RectTransform banner;
	}
}
