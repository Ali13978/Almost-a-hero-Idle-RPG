using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelWorldUpgrade : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
		}

		public void SetButtonState(bool canAfford, bool isUnlocked, ChallengeUpgrade worldUpgrade)
		{
			this.buttonCantUpgrade.SetActive(!isUnlocked);
			this.buttonUpgrade.gameObject.SetActive(isUnlocked);
			if (isUnlocked)
			{
				this.buttonUpgrade.interactable = canAfford;
				this.buttonUpgradeAnim.textShadowUp.enabled = canAfford;
				this.buttonUpgradeAnim.textShadowDown.enabled = canAfford;
			}
			this.papyr.SetUnlocked(isUnlocked, worldUpgrade);
		}

		public void SetBarState(int currentStage, int reqStage)
		{
			float num = Mathf.Min(1f, 1f * (float)currentStage / (float)reqStage);
			if (num < 0f)
			{
				num = 1f;
			}
			if (this.targetXpBarScale != num)
			{
				this.targetXpBarScale = num;
				if (this.xpTweener != null && this.xpTweener.isPlaying)
				{
					this.xpTweener.Kill(false);
				}
				this.xpTweener = DOTween.To(new DOGetter<float>(this.xpBar.GetScale), new DOSetter<float>(this.xpBar.SetScale), this.targetXpBarScale, 2f).SetSpeedBased(true);
			}
		}

		public void OnUpgrade()
		{
			this.papyr.OnUpgrade();
		}

		public Text textName;

		public GameButton buttonUpgrade;

		public ButtonUpgradeAnim buttonUpgradeAnim;

		public GameObject buttonCantUpgrade;

		public Text textCantUpgrade;

		public Scaler xpBar;

		public Papyr papyr;

		public float targetXpBarScale;

		private Tweener xpTweener;
	}
}
