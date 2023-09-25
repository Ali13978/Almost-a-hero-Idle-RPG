using System;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class CharmInfoPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.charmCard.button.enabled = false;
		}

		public void InitStrings()
		{
			this.upgradeButton.textDown.text = LM.Get("UI_UPGRADE");
			this.maxedText.text = LM.Get("UI_COMING_SOON");
		}

		public void DoUpgradeAnim()
		{
			if (this.sequence != null && this.isAnimating)
			{
				this.sequence.Complete(true);
			}
			this.isAnimating = true;
			this.dontUpdateLevel = true;
			this.sequence = DOTween.Sequence();
			this.effectParticle.gameObject.SetActive(true);
			this.effectParticle.AnimationState.SetAnimation(0, "animation", false);
			this.sequence.AppendCallback(delegate
			{
				this.effectGlow.gameObject.SetActive(true);
				this.effectGlow.SetAlpha(0f);
			}).Append(this.effectGlow.DOFade(1f, 0.5f)).Join(this.charmCard.transform.DOScale(0.9f, 0.5f).SetEase(Ease.OutCubic)).AppendInterval(0.05f).AppendCallback(delegate
			{
				this.dontUpdateLevel = false;
				this.needToUpdate = true;
			}).Append(this.effectGlow.DOFade(0f, 0.1f)).Join(this.charmCard.transform.DOScale(1f, 0.1f).SetEase(Ease.InCubic)).SetDelay(0.3f).Append(this.charmCard.levelText.transform.DOScale(1.5f, 0.3f).SetEase(Ease.OutExpo)).AppendInterval(0.1f).Append(this.charmCard.levelText.transform.DOScale(1f, 0.3f).SetEase(Ease.InExpo)).InsertCallback(1.4f, delegate
			{
				this.effectGlow.gameObject.SetActive(false);
				this.effectParticle.gameObject.SetActive(false);
				this.isAnimating = false;
			}).Insert(1.6f, this.effectShine.DoShine());
			this.sequence.Play<Sequence>();
		}

		public Text header;

		public Text charmTriggerDesc;

		public Text charmDesc;

		public Text maxedText;

		public RectTransform maxedTextParent;

		public ButtonUpgradeAnim upgradeButton;

		public GameButton buttonBack;

		public GameButton buttonBackX;

		public CharmCard charmCard;

		public int selectedCharmId;

		public bool needToUpdate;

		public UniversalBonusUnitWidget universalTotalBonusWidget;

		public Text cannotUpgradeDesc;

		public MenuShowCurrency menuShowCurrency;

		public ShineLightAnimator effectShine;

		public SkeletonGraphic effectParticle;

		public Image effectGlow;

		public bool isAnimating;

		public bool dontUpdateLevel;

		public Sequence sequence;
	}
}
