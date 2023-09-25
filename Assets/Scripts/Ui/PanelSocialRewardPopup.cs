using System;
using SocialRewards;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelSocialRewardPopup : AahMonoBehaviour
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
			this.buttonCancel.text.text = LM.Get("UI_CANCEL");
			this.buttonCollect.text.text = LM.Get("UI_COLLECT");
			this.buttonFollow.text.text = LM.Get("UI_OKAY");
		}

		public void SetDetails(SocialRewards.Network socialNetwork, double gemsAmount, bool giveReward, UiState previousState, string socialNetWorkName)
		{
			this.previousState = previousState;
			this.selectedSocialNetwork = socialNetwork;
			bool flag = Manager.IsOfferAvailable(socialNetwork);
			if (flag)
			{
				this.textTitle.text = string.Format(LM.Get("SOCIAL_REWARD_OFFER_TITLE", socialNetWorkName), new object[0]);
				this.buttonFollow.text.text = LM.Get("UI_OKAY");
			}
			else
			{
				this.textTitle.text = socialNetWorkName.ToUpperInvariant();
				this.buttonFollow.text.text = LM.Get("SOCIAL_REWARD_VISIT");
			}
			if (giveReward)
			{
				this.buttonFollow.gameObject.SetActive(false);
				this.buttonCancel.gameObject.SetActive(false);
				this.buttonCollect.gameObject.SetActive(true);
				this.textAmount.gameObject.SetActive(true);
				this.textAmount.text = GameMath.GetDoubleString(50.0);
				this.textAmount.rectTransform.SetAnchorPosY(-177.6f);
				this.mainContext.SetAnchorPosY(-15f);
				this.textOffer.text = LM.Get("SOCIAL_REWARD_OFFER_THANKS");
				this.offerImage.sprite = this.gemsRewardSprite;
			}
			else
			{
				this.buttonFollow.gameObject.SetActive(true);
				this.buttonCancel.gameObject.SetActive(true);
				this.buttonCollect.gameObject.SetActive(false);
				this.textAmount.gameObject.SetActive(flag);
				if (flag)
				{
					this.textOffer.text = string.Format(LM.Get("SOCIAL_REWARD_OFFER_TEXT"), socialNetWorkName, gemsAmount.ToString());
					this.textAmount.rectTransform.SetAnchorPosY(-165.8f);
					this.mainContext.SetAnchorPosY(38f);
					this.textAmount.text = GameMath.GetDoubleString(50.0);
				}
				else
				{
					this.textOffer.text = LM.Get("SOCIAL_REWARD_WHATS_NEW_DESC");
					this.mainContext.SetAnchorPosY(-15f);
				}
				this.offerImage.sprite = this.GetSocialNetworkSprite(socialNetwork);
			}
		}

		private Sprite GetSocialNetworkSprite(SocialRewards.Network socialNetwork)
		{
			switch (socialNetwork)
			{
			case SocialRewards.Network.Facebook:
				return this.facebookSprite;
			case SocialRewards.Network.Twitter:
				return this.twitterSprite;
			case SocialRewards.Network.Instagram:
				return this.instagramSprite;
			default:
				throw new NotImplementedException();
			}
		}

		public RectTransform mainContext;

		public RectTransform popupRect;

		public GameButton buttonFollow;

		public GameButton buttonCancel;

		public GameButton buttonBg;

		public GameButton buttonCollect;

		public Text textTitle;

		public Text textOffer;

		public Text textAmount;

		public Image offerImage;

		public Sprite gemsRewardSprite;

		public Sprite facebookSprite;

		public Sprite twitterSprite;

		public Sprite instagramSprite;

		[HideInInspector]
		public SocialRewards.Network selectedSocialNetwork;

		[HideInInspector]
		public UiState previousState;
	}
}
