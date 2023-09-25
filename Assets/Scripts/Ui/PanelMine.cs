using System;
using DG.Tweening;
using Simulation;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelMine : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.textDesc.text = LM.Get("MINE_DESC");
			this.nextLevelHeader.text = LM.Get("MINE_NEXT_LEVEL");
			this.nextLevelAmountDescLabel.text = LM.Get("MINE_PRODUCES");
			this.bonusNextLevelLabel.text = LM.Get("MINE_NEXT_LEVEL");
		}

		public void DoUpgradeAnimation(string levelLabel)
		{
			this.mineSpineAnimation.AnimationState.SetAnimation(0, "upgrade", false);
			this.mineSpineAnimation.AnimationState.AddAnimation(0, "idle", false, 1.733f);
			if (this.upgradeAnimSequence != null && this.upgradeAnimSequence.IsPlaying())
			{
				this.upgradeAnimSequence.Complete(true);
			}
			this.dontUpdateLevel = true;
			this.upgradeAnimSequence = DOTween.Sequence();
			this.upgradeAnimSequence.Append(this.currentLevel.DOFade(0f, 0.2f)).AppendInterval(0.8f).AppendCallback(delegate
			{
				this.currentLevel.SetAlpha(1f);
				this.currentLevel.text = levelLabel;
			}).OnComplete(delegate
			{
				this.dontUpdateLevel = false;
			});
			this.upgradeAnimSequence.Play<Sequence>();
		}

		public void SetMineStatus(bool isFull, Mine mine)
		{
			bool flag = mine is MineToken;
			if (this.mineSpineAnimation == null || this.mineSpineAnimation.Skeleton == null)
			{
				return;
			}
			if (flag)
			{
				this.mineSpineAnimation.Skeleton.SetSkin((!isFull) ? 3 : 4);
			}
			else
			{
				this.mineSpineAnimation.Skeleton.SetSkin((!isFull) ? 1 : 2);
			}
		}

		public RectTransform popupRect;

		public Image imageIcon;

		public Text textBanner;

		public Text textDesc;

		public Text currentLevel;

		public Text amountToCollect;

		public Image amountToCollectCurrencyIcon;

		public GameObject iconMaxed;

		public GameObject nextLevelParent;

		public Text nextLevelHeader;

		public Text nextLevelAmountDescLabel;

		public Text nextLevelAmountLabel;

		public Image nextLevelCurrencyIcon;

		public Text bonusLabel;

		public Text bonusAmount;

		public Text bonusNextLevelLabel;

		public Text bonusNextLevelAmount;

		public ButtonUpgradeAnim buttonMineUpgrade;

		public GameButton buttonMineCollect;

		public MenuShowCurrency menuShowCurrency;

		public MenuShowCurrency menuShowCurrencyAeon;

		public GameButton buttonClose;

		public GameButton buttonCloseCross;

		public RectTransform mineImageParent;

		public float timer;

		public Mine selected;

		public const float BUTTON_X = 150f;

		public bool isCd;

		public Image flagPlate;

		public Color scrapMineColor;

		public Color tokenMineColor;

		public SkeletonGraphic mineSpineAnimation;

		private Sequence upgradeAnimSequence;

		public bool dontUpdateLevel;

		internal UiState stateToReturn;
	}
}
