using System;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ShopSkinPackOpening : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.stepButton.onClick = new GameButton.VoidFunc(this.Step);
		}

		public void Step()
		{
			if (this.state == ShopSkinPackOpening.State.WAITINGFORSTEP)
			{
				this.MakeALoop();
			}
			else if (this.state == ShopSkinPackOpening.State.END)
			{
				this.uiManager.state = UiState.SHOP;
			}
		}

		public void StartShopPack(ShopPack sp, params int[] skinIds)
		{
			this.skinIds = skinIds;
			this.loopCount = skinIds.Length;
			this.gearParent.localScale = Vector3.zero;
			this.state = ShopSkinPackOpening.State.FIRSTAPPER;
			this.DoChestSpawn(sp).OnComplete(delegate
			{
				this.MakeALoop();
			});
		}

		public void MakeCardHidden()
		{
			this.infoParent.gameObject.SetActive(false);
			this.emptyParent.gameObject.SetActive(true);
			this.heroAvatarParent.gameObject.SetActive(false);
			this.glowGroup.alpha = 0f;
		}

		public void MakeCardRevealed()
		{
			this.infoParent.gameObject.SetActive(true);
			this.emptyParent.gameObject.SetActive(false);
			this.heroAvatarParent.gameObject.SetActive(true);
			this.glowGroup.alpha = 0f;
		}

		public void MakeALoop()
		{
			if (this.loopCount > 0)
			{
				this.state = ShopSkinPackOpening.State.CARDAPPEAR;
				int num = this.skinIds[this.loopCount - 1];
				SkinData skinData = this.uiManager.sim.GetSkinData(num);
				this.heroAvatar.sprite = this.uiManager.GetHeroAvatar(num);
				this.heroAvatar.SetNativeSize();
				this.heroAnimation.SetHeroAnimation(skinData.belongsTo.id, skinData.index, false, false, false, true);
				this.skinName.text = skinData.GetName();
				this.GetLoopSequence();
			}
			else
			{
				this.state = ShopSkinPackOpening.State.END;
			}
			this.loopCount--;
		}

		public Sequence GetLoopSequence()
		{
			return this.DoCardAppear().OnComplete(delegate
			{
				this.DoAppearShine().OnComplete(delegate
				{
					if (this.loopCount > 0)
					{
						this.state = ShopSkinPackOpening.State.WAITINGFORSTEP;
					}
					else
					{
						this.state = ShopSkinPackOpening.State.END;
					}
				});
			});
		}

		public Tweener DoChestSpawn(ShopPack sp)
		{
			this.chestSpine.SetSkin(sp, true);
			this.chestSpine.Spawn();
			return DOTween.TimerTween(0.5f).Play<Tweener>();
		}

		public Sequence DoCardAppear()
		{
			this.chestSpine.NewItemAppears();
			this.MakeCardHidden();
			this.gearParent.localScale = Vector3.zero;
			this.gearParent.anchoredPosition = new Vector2(0f, -300f);
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.gearParent.DOAnchorPos(this.cardTargetPosition, 0.3f, false));
			sequence.Join(this.gearParent.DOScale(0.8f, 0.3f));
			sequence.Play<Sequence>();
			return sequence;
		}

		public Sequence DoAppearShine()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.glowGroup.DOFade(1f, 0.3f));
			sequence.Append(this.gearParent.DORotate(new Vector3(0f, 90f, 0f), 0.15f, RotateMode.Fast));
			sequence.AppendCallback(delegate
			{
				this.gearParent.localEulerAngles = new Vector3(0f, -90f, 0f);
				this.MakeCardRevealed();
			});
			sequence.Append(this.gearParent.DORotate(new Vector3(0f, 0f, 0f), 0.15f, RotateMode.Fast));
			sequence.AppendInterval(0.5f);
			sequence.Play<Sequence>();
			return sequence;
		}

		public RectTransform gearParent;

		public RectTransform infoParent;

		public RectTransform emptyParent;

		public RectTransform heroAvatarParent;

		public GameButton stepButton;

		public ChestSpine chestSpine;

		public Image heroAvatar;

		public CanvasGroup glowGroup;

		public HeroAnimation heroAnimation;

		public Text skinName;

		public ShopSkinPackOpening.State state;

		private Vector2 cardTargetPosition = new Vector2(0f, 355f);

		private int[] skinIds;

		private int loopCount;

		public bool test;

		public bool step;

		public UiManager uiManager;

		public enum State
		{
			FIRSTAPPER,
			WAITINGFORSTEP,
			CARDAPPEAR,
			END
		}
	}
}
