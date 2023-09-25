using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class LootPackTrinketCard : AahMonoBehaviour
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
			this.newTrinketLabel.text = LM.Get("UI_NEW_TRINKET");
		}

		public void SetTrinkets(Trinket trinket)
		{
			float num = 0f;
			float num2 = 10f;
			for (int i = 0; i < 3; i++)
			{
				PanelTrinketEffect panelTrinketEffect = this.trinketAttributes[i];
				TrinketEffect effectOfGroup = trinket.GetEffectOfGroup((TrinketEffectGroup)i);
				if (effectOfGroup != null)
				{
					panelTrinketEffect.textHeader.gameObject.SetActive(true);
					panelTrinketEffect.textDesc.gameObject.SetActive(true);
					panelTrinketEffect.nonExisting.gameObject.SetActive(false);
					panelTrinketEffect.textHeader.text = UiManager.GetTrinketEffectRarityLocString(effectOfGroup.GetGroup());
					panelTrinketEffect.textDesc.text = effectOfGroup.GetDesc(false, 0);
					Sprite[] sprites = effectOfGroup.GetSprites();
					if (sprites.Length > 2)
					{
						Sprite sprite = sprites[0];
						sprites[0] = sprites[1];
						sprites[1] = sprite;
					}
					if (sprites.Length > 1)
					{
						panelTrinketEffect.basicImagesParent.gameObject.SetActive(true);
						panelTrinketEffect.otherImage.gameObject.SetActive(false);
						for (int j = 0; j < panelTrinketEffect.basicImages.Length; j++)
						{
							Image image = panelTrinketEffect.basicImages[j];
							if (j < sprites.Length)
							{
								image.gameObject.SetActive(true);
								Sprite sprite2 = sprites[j];
								if (image.sprite != sprite2)
								{
									image.sprite = sprite2;
									image.SetNativeSize();
								}
							}
							else
							{
								image.gameObject.SetActive(false);
							}
						}
					}
					else
					{
						panelTrinketEffect.basicImagesParent.gameObject.SetActive(false);
						panelTrinketEffect.otherImage.gameObject.SetActive(true);
						Sprite sprite3 = sprites[0];
						if (panelTrinketEffect.otherImage.sprite != sprite3)
						{
							panelTrinketEffect.otherImage.sprite = sprite3;
							panelTrinketEffect.otherImage.SetNativeSize();
						}
					}
				}
				else
				{
					panelTrinketEffect.basicImagesParent.gameObject.SetActive(false);
					panelTrinketEffect.otherImage.gameObject.SetActive(false);
					panelTrinketEffect.textHeader.gameObject.SetActive(false);
					panelTrinketEffect.textDesc.gameObject.SetActive(false);
					panelTrinketEffect.nonExisting.gameObject.SetActive(true);
				}
				RectTransform rectTransform = panelTrinketEffect.transform as RectTransform;
				rectTransform.SetAnchorPosY(num - num2 * (float)i);
				num -= rectTransform.sizeDelta.y;
			}
		}

		public PanelTrinketEffect[] trinketAttributes;

		public RectTransform attributeParent;

		public Text newTrinketLabel;

		private const int trinketAttributeCount = 3;
	}
}
