using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHeroesRunes : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.wornRunePositions = new Vector2[this.wornRunes.Length];
			int i = 0;
			int num = this.wornRunePositions.Length;
			while (i < num)
			{
				this.wornRunePositions[i] = this.wornRunes[i].rectTransform.anchoredPosition;
				i++;
			}
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.textShopBg.text = LM.Get("UI_RUNES_SHOP_DESC");
			this.buttonShop.text.text = LM.Get("UI_GO_TO_SHOP");
			int i = 0;
			int num = this.panelRunes.Length;
			while (i < num)
			{
				this.panelRunes[i].buttonUse.text.text = LM.Get("UI_RUNES_USE");
				this.panelRunes[i].buttonRemove.text.text = LM.Get("UI_RUNES_REMOVE");
				i++;
			}
		}

		public void TweenToSlot(int panelRuneId, int wornRuneId)
		{
			if (panelRuneId < 0 || panelRuneId >= this.wornRunes.Length)
			{
				return;
			}
			this.wornRunes[wornRuneId].SetImageAlpha(1f);
			this.wornRunes[wornRuneId].transform.localScale = Vector3.one;
			this.wornRunes[wornRuneId].rectTransform.position = this.panelRunes[panelRuneId].imageIcon.rectTransform.position;
			this.wornRunes[wornRuneId].rectTransform.DOAnchorPos(this.wornRunePositions[wornRuneId], 0.2f, false).SetEase(Ease.OutSine);
			this.ringSpine.PlayGenericEffect(true);
		}

		public void SetSprites(int numRunes)
		{
			if (numRunes > 0)
			{
				this.imageRingBg.sprite = this.spriteRingBgGlow;
				this.imageRingBg.SetNativeSize();
			}
			else
			{
				this.imageRingBg.sprite = this.spriteRingBgNormal;
				this.imageRingBg.SetNativeSize();
			}
			this.imageRing.sprite = this.spriteRing;
			int i = 0;
			int num = this.ringRuneSlots.Length;
			while (i < num)
			{
				if (i < numRunes)
				{
					this.ringRuneSlots[i].bgGlow.sprite = this.spriteRuneBgGlow;
					this.ringRuneSlots[i].bgGlow.SetNativeSize();
					this.ringRuneSlots[i].line.rectTransform.SetSizeDeltaX(39f);
					this.ringRuneSlots[i].line.sprite = this.spriteRuneLineGlow;
				}
				else
				{
					this.ringRuneSlots[i].bgGlow.sprite = this.spriteRuneBgNormal;
					this.ringRuneSlots[i].bgGlow.SetNativeSize();
					this.ringRuneSlots[i].line.rectTransform.SetSizeDeltaX(17f);
					this.ringRuneSlots[i].line.sprite = this.spriteRuneLineNormal;
				}
				i++;
			}
		}

		public Text textName;

		public Text textDesc;

		public Image imageRing;

		public Image imageRingBg;

		public PanelRune[] panelRunes;

		public RingRuneSlot[] ringRuneSlots;

		public Image[] wornRunes;

		public Image imageShopBg;

		public Text textShopBg;

		public GameButton buttonShop;

		public GameObject scrollviewContent;

		public RingSpine ringSpine;

		public Sprite spriteRingBgNormal;

		public Sprite spriteRingBgGlow;

		public Sprite spriteRuneBgNormal;

		public Sprite spriteRuneBgGlow;

		public Sprite spriteRuneLineNormal;

		public Sprite spriteRuneLineGlow;

		public Sprite spriteRing;

		private Vector2[] wornRunePositions;

		private const float wornRunePeriod = 0.2f;
	}
}
