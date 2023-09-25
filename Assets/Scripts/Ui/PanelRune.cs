using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelRune : AahMonoBehaviour
	{
		public void SetDetails(Rune rune, bool isBought, bool canUse, bool canRemove, Color? runeColorSprite = null, Sprite spriteIcon = null)
		{
			this.runeId = rune.id;
			this.textName.enabled = isBought;
			this.textDesc.enabled = isBought;
			this.textBlank.enabled = !isBought;
			this.imageIcon.enabled = isBought;
			if (isBought)
			{
				this.textName.text = LM.Get(rune.nameKey);
				this.textDesc.text = rune.GetDesc();
				this.imageIconBg.sprite = this.spriteIconBgFull;
				this.imageIcon.sprite = spriteIcon;
				this.imageIcon.SetNativeSize();
				this.imageIcon.color = runeColorSprite.Value;
				this.buttonUse.gameObject.SetActive(canUse || !canRemove);
				this.buttonUse.interactable = canUse;
				this.buttonRemove.gameObject.SetActive(canRemove);
			}
			else
			{
				this.textBlank.text = LM.Get(rune.nameKey);
				this.imageIconBg.sprite = this.spriteIconBgEmpty;
				this.buttonUse.gameObject.SetActive(false);
				this.buttonRemove.gameObject.SetActive(false);
			}
			this.imageIcon.SetAlpha((!canRemove) ? 1f : 0.35f);
		}

		public Image imageIconBg;

		public Image imageIcon;

		public Text textName;

		public Text textDesc;

		public Text textBlank;

		public GameButton buttonUse;

		public GameButton buttonRemove;

		public string runeId;

		public Sprite spriteIconBgEmpty;

		public Sprite spriteIconBgFull;
	}
}
