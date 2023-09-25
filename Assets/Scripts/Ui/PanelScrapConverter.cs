using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Simulation;
using Spine.Unity;
using UnityEngine.UI;

namespace Ui
{
	public class PanelScrapConverter : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
		}

		public void InitStrings()
		{
			this.textHeader.text = LM.Get("UI_CONVERSION_HEADER");
			this.textDesc.text = LM.Get("UI_SCRAP_CONVERTER_DESC");
			this.buttonConvert.text.text = LM.Get("UI_CONVERSION_BUTTON");
		}

		public void SetValues(Simulator sim)
		{
			Currency candies = sim.GetCandies();
			this.textCandyAmount.text = candies.GetString();
			World world = sim.GetWorld(GameMode.STANDARD);
			List<Simulation.MerchantItem> eventMerchantItems = world.eventMerchantItems;
			double num = 0.0;
			this.candyAmountOfSnow = (double)eventMerchantItems[0].GetNumInInventory();
			this.candyAmountOfCup = (double)eventMerchantItems[1].GetNumInInventory();
			this.candyAmountOfOrnament = (double)eventMerchantItems[2].GetNumInInventory();
			this.candyAmountOriginal = Math.Floor(candies.GetAmount());
			num += this.candyAmountOfCup * 338.0;
			num += this.candyAmountOfSnow * 225.0;
			num += this.candyAmountOfOrnament * 281.0;
			num += this.candyAmountOriginal;
			this.textCupAmount.text = GameMath.GetDoubleString(this.candyAmountOfCup);
			this.textSnowAmount.text = GameMath.GetDoubleString(this.candyAmountOfSnow);
			this.textOrnamentAmount.text = GameMath.GetDoubleString(this.candyAmountOfOrnament);
			this.scrapAmountOriginal = Math.Floor(num * 0.1);
			this.textScrapAmountCounter.text = GameMath.GetDoubleString(this.scrapAmountOriginal);
			this.buttonConvert.text.text = LM.Get("UI_CONVERSION_BUTTON");
		}

		public void PlayConvertAnimation(TweenCallback onConvert, GameButton.VoidFunc onComplete)
		{
			this.skeletonGraphic.AnimationState.SetAnimation(0, "animation", false);
			this.buttonConvert.transform.parent.DOScale(0f, 0.3f).SetEase(Ease.InBack);
			GameButtonHandler buttonHandler = this.buttonConvert.raycastTarget.GetComponent<GameButtonHandler>();
			buttonHandler.enabled = false;
			this.buttonConvert.enabled = false;
			float f = 1f;
			this.textScrapAmountCounter.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
			DOTween.To(() => f, delegate(float x)
			{
				f = x;
			}, 0f, 1f).OnUpdate(delegate
			{
				this.textCupAmount.text = GameMath.GetDoubleString(this.candyAmountOfCup * (double)f);
				this.textSnowAmount.text = GameMath.GetDoubleString(this.candyAmountOfSnow * (double)f);
				this.textOrnamentAmount.text = GameMath.GetDoubleString(this.candyAmountOfOrnament * (double)f);
				this.textCandyAmount.text = GameMath.GetDoubleString(this.candyAmountOriginal * (double)f);
			}).OnComplete(delegate
			{
				this.textScrapAmountCounter.transform.DOScale(1.3f, 0.3f).OnComplete(delegate
				{
					this.textScrapAmountCounter.transform.DOScale(1f, 0.2f).SetDelay(0.2f);
				});
			}).SetDelay(1f);
			Utility.TimerTween(3f).OnComplete(delegate
			{
				this.skeletonGraphic.timeScale = 0f;
				this.buttonConvert.transform.parent.DOScale(1f, 0.3f).SetEase(Ease.InBack);
				this.buttonConvert.text.text = LM.Get("UI_COLLECT");
				this.buttonConvert.onClick = delegate()
				{
					onConvert();
					this.skeletonGraphic.timeScale = 1f;
					this.buttonConvert.transform.parent.DOScale(0f, 0.3f).SetEase(Ease.OutBack);
					buttonHandler.enabled = false;
					this.buttonConvert.enabled = false;
					Utility.TimerTween(1.5f).OnComplete(delegate
					{
						onComplete();
					});
				};
				buttonHandler.enabled = true;
				this.buttonConvert.enabled = true;
			});
		}

		public GameButton buttonConvert;

		public Text textHeader;

		public Text textDesc;

		public Text textCandyAmount;

		public Text textCupAmount;

		public Text textSnowAmount;

		public Text textOrnamentAmount;

		public Text textScrapAmountCounter;

		public MenuShowCurrency scrapCurrency;

		public SkeletonGraphic skeletonGraphic;

		private double candyAmountOfCup;

		private double candyAmountOfSnow;

		private double candyAmountOfOrnament;

		private double candyAmountOriginal;

		private double scrapAmountOriginal;
	}
}
