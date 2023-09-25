using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelCharmPackSelect : MonoBehaviour
	{
		public void InitStrings()
		{
			this.textHeader.text = LM.Get("CHARM_PACK");
			this.buttonOpen.text.text = LM.Get("UI_MERCHANT_SELECT_BUY");
			this.buttonBuy.textDown.text = LM.Get("UI_BUY");
			this.textOneCharm.text = LM.Get("CHARM_PACK_DESC_1");
			this.textCopyCount.text = string.Format(LM.Get("CHARM_PACK_DESC_2"), 15);
		}

		public void SetPackSkin(bool isBigPack)
		{
			this.skeletonGraphic.Initialize(false);
			if (isBigPack)
			{
				this.skeletonGraphic.Skeleton.SetSkin("specialDefault");
			}
			else
			{
				this.skeletonGraphic.Skeleton.SetSkin("anotherDefault");
			}
		}

		public Text textHeader;

		public Text textOneCharm;

		public Text textCopyCount;

		public ButtonUpgradeAnim buttonBuy;

		public GameButton buttonOpen;

		public GameButton buttonClose;

		public GameButton buttonCloseCross;

		public SkeletonGraphic skeletonGraphic;

		public RectTransform popupRect;

		public bool isBigPack;
	}
}
