using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTrinketSmithing : AahMonoBehaviour
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

		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.toggleColor.onClick = delegate()
			{
				this.colorIndex = (this.colorIndex + 1) % 6;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
				this.UpdateTrinketVisual();
			};
			this.toggleShape.onClick = delegate()
			{
				this.bodyIndex = (this.bodyIndex + 1) % 6;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
				this.UpdateTrinketVisual();
			};
			for (int i = 0; i < this.trinketEffectSlots.Length; i++)
			{
				TrinketEffectSlot trinketEffectSlot = this.trinketEffectSlots[i];
				int ic = i;
				trinketEffectSlot.buttonDiscard.onClick = delegate()
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
					this.selectedEffects[ic] = null;
					this.UpdateTrinketVisual();
				};
			}
		}

		public void InitStrings()
		{
			this.trinketEffectSlots[0].groupLabel.text = PanelTrinketEffectScroller.GetGroupName(TrinketEffectGroup.COMMON);
			this.trinketEffectSlots[1].groupLabel.text = PanelTrinketEffectScroller.GetGroupName(TrinketEffectGroup.SECONDARY);
			this.trinketEffectSlots[2].groupLabel.text = PanelTrinketEffectScroller.GetGroupName(TrinketEffectGroup.SPECIAL);
			this.buttonCraft.textDown.text = LM.Get("UI_TRINKET_CRAFT");
		}

		public void UpdateTrinketVisual()
		{
			List<TrinketEffect> list = new List<TrinketEffect>();
			for (int i = 0; i < this.selectedEffects.Length; i++)
			{
				TrinketEffect trinketEffect = this.selectedEffects[i];
				TrinketEffectSlot trinketEffectSlot = this.trinketEffectSlots[i];
				Image image = this.buttonGlows[i];
				GameButton gameButton = this.effectButtons[i];
				if (trinketEffect != null)
				{
					list.Add(trinketEffect);
					if (!this.lineGraphics[i].gameObject.activeSelf)
					{
						this.lineGraphics[i].gameObject.SetActive(true);
						this.lineGraphics[i].AnimationState.SetAnimation(0, "idle", true);
					}
					image.color = Color.white;
					image.gameObject.SetActive(true);
					trinketEffectSlot.buttonDiscard.gameObject.SetActive(true);
					trinketEffectSlot.panelTrinket.gameObject.SetActive(true);
					trinketEffectSlot.groupLabel.gameObject.SetActive(false);
					gameButton.fakeDisabled = true;
					trinketEffectSlot.panelTrinket.textDesc.rectTransform.SetRightDelta(72f);
					TrinketInfoBody.UpdatePanelTrinketEffect(trinketEffect, trinketEffectSlot.panelTrinket, true);
				}
				else
				{
					if (!this.isAnimatingCraft)
					{
						image.gameObject.SetActive(false);
						this.lineGraphics[i].gameObject.SetActive(false);
					}
					trinketEffectSlot.buttonDiscard.gameObject.SetActive(false);
					trinketEffectSlot.panelTrinket.gameObject.SetActive(false);
					trinketEffectSlot.groupLabel.gameObject.SetActive(true);
					gameButton.fakeDisabled = false;
				}
			}
			if (this.isAnimatingCraft)
			{
				this.trinketVisual.gameObject.SetActive(false);
			}
			else
			{
				this.trinketVisual.gameObject.SetActive(true);
				Trinket trinket = new Trinket(list);
				trinket.bodyColorIndex = this.colorIndex;
				trinket.bodySpriteIndex = this.bodyIndex;
				this.trinketVisual.InitAnimated(trinket);
			}
			if (list.Count == 0)
			{
				this.HideCancelButton();
			}
			else
			{
				this.ShowCancelButton();
			}
		}

		public void HideCancelButton()
		{
			this.buttonCancelParent.SetScale(0f);
			this.buttonCraftParent.SetScale(0f);
		}

		public void ShowCancelButton()
		{
			this.buttonCancelParent.SetAnchorPosY((float)((!this.isBattlefield) ? 98 : 67));
			this.buttonCraftParent.SetAnchorPosY((float)((!this.isBattlefield) ? 98 : 67));
			this.buttonCancelParent.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
			this.buttonCraftParent.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
		}

		public void CancelCrafting()
		{
			for (int i = 0; i < this.selectedEffects.Length; i++)
			{
				this.selectedEffects[i] = null;
			}
			this.UpdateTrinketVisual();
			this.colorIndex = GameMath.GetRandomInt(0, 6, GameMath.RandType.NoSeed);
			this.bodyIndex = GameMath.GetRandomInt(0, 6, GameMath.RandType.NoSeed);
		}

		private RectTransform m_rectTransform;

		public GameButton buttonInfo;

		public GameButton toggleColor;

		public GameButton toggleShape;

		public GameButton buttonCancel;

		public RectTransform buttonCancelParent;

		public ButtonUpgradeAnim buttonCraft;

		public RectTransform buttonCraftParent;

		public GameButton[] effectButtons;

		public TrinketEffectSlot[] trinketEffectSlots;

		public TrinketEffect[] selectedEffects = new TrinketEffect[3];

		public SkeletonGraphic[] lineGraphics;

		public Image[] buttonGlows;

		public SkeletonGraphic trinketGlow;

		public TrinketUi trinketVisual;

		public Transform trinketCraftedParent;

		public RectTransform hintParent;

		public RectTransform bodyParent;

		public Text textUnlockSmithHint;

		public ParticleSystem[] craftParticles;

		[NonSerialized]
		public bool isBattlefield;

		[NonSerialized]
		public bool isAnimatingCraft;

		[NonSerialized]
		public int colorIndex;

		[NonSerialized]
		public int bodyIndex;
	}
}
