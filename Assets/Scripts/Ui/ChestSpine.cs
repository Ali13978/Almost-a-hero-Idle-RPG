using System;
using Simulation;
using Spine.Unity;
using UnityEngine;

namespace Ui
{
	public class ChestSpine : AahMonoBehaviour
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

		public void SetSkin(ShopPack shopPack, bool normalizeScale = false)
		{
			this.shopPack = shopPack;
			if (this.skeletonGraphic.Skeleton != null)
			{
				if (shopPack is ShopPackLootpackFree)
				{
					this.skeletonGraphic.initialSkinName = "common";
					this.skeletonGraphic.Skeleton.SetSkin("common");
					if (normalizeScale)
					{
						base.transform.localScale = Vector3.one * 0.8f;
					}
				}
				else if (shopPack is ShopPackLootpackRare)
				{
					this.skeletonGraphic.initialSkinName = "rare";
					this.skeletonGraphic.Skeleton.SetSkin("rare");
					if (normalizeScale)
					{
						base.transform.localScale = Vector3.one * 0.7f;
					}
				}
				else if (shopPack is ShopPackLootpackEpic)
				{
					this.skeletonGraphic.initialSkinName = "epic";
					this.skeletonGraphic.Skeleton.SetSkin("epic");
					if (normalizeScale)
					{
						base.transform.localScale = Vector3.one * 0.6f;
					}
				}
				else if (shopPack is ShopPackFiveTrinkets || shopPack is ShopPackCurrency || shopPack is ShopPackRune || shopPack is ShopPackStarter || shopPack is ShopPackToken || shopPack is ShopPackThreePijama || shopPack is ShopPackBigGem || shopPack is ShopPackBigGemTwo || shopPack is ShopPackStage100 || shopPack is ShopPackRiftOffer01 || shopPack is ShopPackRiftOffer02 || shopPack is ShopPackRiftOffer03 || shopPack is ShopPackRiftOffer04 || shopPack is ShopPackRegional01 || shopPack is ShopPackHalloweenGems || shopPack is ShopPackChristmasGemsSmall || shopPack is ShopPackChristmasGemsBig)
				{
					this.skeletonGraphic.initialSkinName = "goblin";
					this.skeletonGraphic.Skeleton.SetSkin("goblin");
					base.transform.localScale = Vector3.one * 0.6f;
				}
				else if (shopPack is ShopPackTrinket)
				{
					this.skeletonGraphic.initialSkinName = "trinket";
					this.skeletonGraphic.Skeleton.SetSkin("trinket");
					if (normalizeScale)
					{
						base.transform.localScale = Vector3.one * 0.8f;
					}
				}
				this.isSkinSet = true;
			}
			else
			{
				this.isSkinSet = false;
			}
		}

		public void SetAnimation(string anim, bool loop, bool setToSetupPose)
		{
			if (!this.isSkinSet)
			{
				this.SetSkin(this.shopPack, false);
			}
			if (this.skeletonGraphic.AnimationState != null)
			{
				this.skeletonGraphic.AnimationState.SetAnimation(0, anim, loop);
				this.skeletonGraphic.Update(0f);
			}
			if (setToSetupPose && this.skeletonGraphic.Skeleton != null)
			{
				this.skeletonGraphic.Skeleton.SetToSetupPose();
			}
		}

		public void Spawn()
		{
			this.SetAnimation("spawn", false, true);
		}

		public void Open()
		{
			this.SetAnimation("openning", false, false);
		}

		public void NewItemAppears(int numItemsLeft)
		{
			this.SetAnimation("release", false, false);
		}

		public void NewItemAppears()
		{
			this.SetAnimation("release", false, false);
		}

		public SkeletonGraphic skeletonGraphic;

		private ShopPack shopPack;

		private bool isSkinSet;

		private RectTransform m_rectTransform;
	}
}
