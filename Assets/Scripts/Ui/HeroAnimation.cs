using System;
using System.Collections.Generic;
using DG.Tweening;
using DynamicLoading;
using Render;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Ui
{
	public class HeroAnimation : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			if (HeroAnimation.heroPoseIdles == null)
			{
				HeroAnimation.heroPoseIdles = new Dictionary<string, string[]>
				{
					{
						"SHEELA",
						new string[]
						{
							"idle_2_buy"
						}
					},
					{
						"GOBLIN",
						new string[]
						{
							"idle_3"
						}
					}
				};
			}
			this.scale = this.spineObject.transform.localScale;
			this.spineObject.SetActive(false);
			this.rectTransform = (base.transform as RectTransform);
		}

		public override void AahUpdate(float dt)
		{
			if (this.justChanged)
			{
				this.justChanged = false;
				this.setScale = true;
			}
			else if (this.setScale)
			{
				this.setScale = false;
				this.spineObject.transform.localScale = this.scale * ((!(this.lastHeroName == "TAM")) ? 1f : 0.9f);
			}
			if (!this.dontChangeAnimation)
			{
				if (this.lastHeroName == "GOBLIN" && this.skeletonGraphic != null)
				{
					this.animTimer += dt;
					this.wasGoblin = true;
					if (this.animTimer >= SpineAnimGoblin.animsIdle[this.lastAnimIndex].duration / this.skeletonGraphic.timeScale)
					{
						this.animTimer = 0f;
						int num = (!GameMath.GetProbabilityOutcome(0.2f, GameMath.RandType.NoSeed)) ? 0 : 1;
						if (this.lastAnimIndex != num)
						{
							this.lastAnimIndex = num;
							this.skeletonGraphic.AnimationState.SetAnimation(0, SpineAnimGoblin.animsIdle[this.lastAnimIndex], true);
							this.skeletonGraphic.Update(0f);
						}
					}
				}
				else if (this.skeletonGraphic != null && this.wasGoblin)
				{
					this.animTimer = 0f;
					this.wasGoblin = false;
					this.skeletonGraphic.AnimationState.SetAnimation(0, "idle_1", true);
					this.skeletonGraphic.Update(0f);
				}
			}
			if (this.lastHeroName != this.loadingHeroName && this.HeroLoaded)
			{
				this.OnHeroAssetsLoaded();
			}
		}

		public bool HeroInitialized
		{
			get
			{
				return !this.lastHeroName.Equals("nobody") && this.lastHeroName.Equals(this.loadingHeroName);
			}
		}

		public bool HeroLoaded
		{
			get
			{
				return Main.instance.heroBundles.ContainsKey(this.loadingHeroName) && Main.instance.heroBundles[this.loadingHeroName] != null;
			}
		}

		public string LoadedHeroName
		{
			get
			{
				return this.lastHeroName;
			}
		}

		public void Reload()
		{
			if (this.lastHeroName != "nobody")
			{
				string heroName = this.lastHeroName;
				int skinIndex = this.lastSkinIndex;
				this.OnClose();
				this.SetHeroAnimation(heroName, skinIndex, true, false, true, true);
			}
		}

		public void SetHeroAnimation(string heroName, int skinIndex, string animation)
		{
			this.loadingHeroName = heroName;
			this.lastSkinIndex = skinIndex;
			this.dontChangeAnimation = true;
			this.unloadPreviousHero = false;
			if (this.skeletonGraphic == null)
			{
				this.skeletonGraphic = this.spineObject.GetComponent<SkeletonGraphic>();
			}
			this.spineObject.SetActive(true);
			this.InitHeroAssets(animation, false);
		}

		public void PoseHeroAnimation(string heroName, int skinIndex, string animation, bool mirror)
		{
			this.loadingHeroName = heroName;
			this.lastSkinIndex = skinIndex;
			this.dontChangeAnimation = true;
			this.unloadPreviousHero = false;
			if (this.skeletonGraphic == null)
			{
				this.skeletonGraphic = this.spineObject.GetComponent<SkeletonGraphic>();
			}
			if (Main.instance.heroBundles.ContainsKey(this.loadingHeroName))
			{
				this.spineObject.SetActive(true);
				HeroBundle heroBundle = Main.instance.heroBundles[this.loadingHeroName];
				this.skeletonGraphic.skeletonDataAsset = heroBundle.animation;
				this.skeletonGraphic.Initialize(true);
				this.skeletonGraphic.SetAllDirty();
				SkeletonData skeletonData = heroBundle.animation.GetSkeletonData(true);
				if (this.unloadPreviousHero && !this.lastHeroName.Equals("nobody") && !this.lastHeroName.Equals(this.loadingHeroName))
				{
					Main.instance.UnloadHeroMainAssets(this.lastHeroName);
				}
				this.lastHeroName = this.loadingHeroName;
				ExposedList<Skin> skins = skeletonData.skins;
				Spine.Animation animation2 = skeletonData.FindAnimation(animation);
				Skeleton skeleton = this.skeletonGraphic.Skeleton;
				skeleton.SetSkin(skins.Items[this.lastSkinIndex]);
				skeleton.SetToSetupPose();
				skeleton.ScaleX = (float)((!mirror) ? 1 : -1);
				animation2.Apply(skeleton, 0f, 0f, false, null, 1f, MixBlend.Setup, MixDirection.Out);
				this.skeletonGraphic.UpdateTTR();
				this.skeletonGraphic.Update();
				this.skeletonGraphic.LateUpdate();
				this.skeletonGraphic.AnimationState.SetAnimation(0, animation2, true);
			}
			else
			{
				this.spineObject.SetActive(false);
			}
		}

		public void SetHeroAnimation(string heroName, int skinIndex, bool forceCommonIdle, bool animateTransition = false, bool animatePopup = false, bool unloadPreviousHero = true)
		{
			if (heroName == this.loadingHeroName && skinIndex == this.lastSkinIndex)
			{
				return;
			}
			this.unloadPreviousHero = unloadPreviousHero;
			this.animatePopup = animatePopup;
			this.animateTransition = animateTransition;
			this.lastSkinIndex = skinIndex;
			this.forceCommonIdle = forceCommonIdle;
			if (this.skeletonGraphic == null)
			{
				this.skeletonGraphic = this.spineObject.GetComponent<SkeletonGraphic>();
			}
			if (animatePopup)
			{
				this.transitionPivot.localScale = Vector3.zero;
			}
			if (this.loadingHeroName != this.lastHeroName)
			{
				Main.instance.UnloadHeroMainAssets(this.loadingHeroName);
			}
			this.loadingHeroName = heroName;
			Main.instance.LoadHeroMainAssets(heroName, null);
			HeroBundle heroBundle = Main.instance.heroBundles[heroName];
			if (heroBundle != null)
			{
				if (this.lastHeroName == heroName)
				{
					SkeletonData skeletonData = heroBundle.animation.GetSkeletonData(true);
					ExposedList<Skin> skins = skeletonData.skins;
					Skeleton skeleton = this.skeletonGraphic.Skeleton;
					skeleton.SetSkin(skins.Items[this.lastSkinIndex]);
					skeleton.SetToSetupPose();
					this.skeletonGraphic.UpdateTTR();
					if (this.showTransformedVersion)
					{
						Skeleton skeleton2 = this.skeletonGraphicAlternative.Skeleton;
						skeleton2.SetSkin(skins.Items[this.lastSkinIndex + this.transformedSkinIndex]);
						skeleton2.SetToSetupPose();
						this.skeletonGraphicAlternative.color = this.skeletonGraphic.color;
						this.skeletonGraphicAlternative.UpdateTTR();
					}
					this.justChanged = true;
					if (animatePopup)
					{
						DOTween.Sequence().Append(this.transitionPivot.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack)).Play<Sequence>();
					}
				}
				else
				{
					this.OnHeroAssetsLoaded();
				}
			}
			else if (!animateTransition || this.lastHeroName.Equals("nobody"))
			{
				this.spineObject.SetActive(false);
			}
		}

		private void OnHeroAssetsLoaded()
		{
			this.spineObject.SetActive(true);
			if (!this.animateTransition)
			{
				this.InitHeroAssets(null, false);
				if (this.animatePopup)
				{
					DOTween.Sequence().Append(this.transitionPivot.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack)).Play<Sequence>();
				}
			}
		}

		public void InitHeroAssets(string animName = null, bool animLoop = false)
		{
			HeroBundle heroBundle = Main.instance.heroBundles[this.loadingHeroName];
			this.skeletonGraphic.skeletonDataAsset = heroBundle.animation;
			this.skeletonGraphic.initialSkinName = null;
			this.skeletonGraphic.Initialize(true);
			this.skeletonGraphic.SetAllDirty();
			SkeletonData skeletonData = heroBundle.animation.GetSkeletonData(true);
			if (this.loadingHeroName.Equals("GOBLIN") && SpineAnimGoblin.animsIdle == null)
			{
				SpineAnimGoblin.Init(skeletonData);
			}
			if (this.unloadPreviousHero && !this.lastHeroName.Equals("nobody") && !this.lastHeroName.Equals(this.loadingHeroName))
			{
				Main.instance.UnloadHeroMainAssets(this.lastHeroName);
			}
			this.lastHeroName = this.loadingHeroName;
			ExposedList<Skin> skins = skeletonData.skins;
			Skeleton skeleton = this.skeletonGraphic.Skeleton;
			skeleton.SetSkin(skins.Items[this.lastSkinIndex]);
			skeleton.SetToSetupPose();
			this.justChanged = true;
			if (this.skeletonGraphicAlternative)
			{
				UnityEngine.Object.Destroy(this.skeletonGraphicAlternative.gameObject);
			}
			if (this.showTransformedVersion)
			{
				this.skeletonGraphicAlternative = UnityEngine.Object.Instantiate<SkeletonGraphic>(this.skeletonGraphic, this.skeletonGraphic.transform.parent);
				this.skeletonGraphicAlternative.transform.SetAsFirstSibling();
				this.skeletonGraphicAlternative.rectTransform.SetAnchorPosX(this.skeletonGraphic.rectTransform.anchoredPosition.x - 50f);
				this.skeletonGraphicAlternative.rectTransform.SetAnchorPosY(this.skeletonGraphic.rectTransform.anchoredPosition.y + 12f);
				this.skeletonGraphicAlternative.transform.localScale = new Vector3(-1.6f, 1.6f, 1f);
				this.skeletonGraphicAlternative.Initialize(true);
				this.skeletonGraphicAlternative.Skeleton.SetSkin(skins.Items[this.lastSkinIndex + this.transformedSkinIndex]);
				this.skeletonGraphicAlternative.AnimationState.SetAnimation(0, "idle_beast", true);
				this.skeletonGraphicAlternative.Update(0.8f);
				this.rectTransform.SetAnchorPosX(103f * (this.rectTransform.localScale.x / 1.5f));
			}
			else
			{
				this.rectTransform.SetAnchorPosX(0f);
			}
			if (animName == null)
			{
				string[] array = new string[]
				{
					"idle_1"
				};
				if (HeroAnimation.heroPoseIdles.TryGetValue(this.lastHeroName, out array) && !this.forceCommonIdle)
				{
					this.skeletonGraphic.AnimationState.SetAnimation(0, array[0], true);
				}
				else
				{
					this.skeletonGraphic.AnimationState.SetAnimation(0, "idle_1", true);
				}
			}
			else
			{
				this.skeletonGraphic.AnimationState.SetAnimation(0, animName, animLoop);
			}
		}

		public void OnClose()
		{
			if (this.ignoreNextOnClose)
			{
				this.ignoreNextOnClose = false;
				return;
			}
			if (this.unloadPreviousHero)
			{
				if (this.lastHeroName != "nobody")
				{
					Main.instance.UnloadHeroMainAssets(this.lastHeroName);
				}
				if (this.loadingHeroName != "nobody" && this.loadingHeroName != this.lastHeroName)
				{
					Main.instance.UnloadHeroMainAssets(this.loadingHeroName);
				}
			}
			else
			{
				Main.instance.UnloadHeroesThatAreNotInGame();
			}
			this.lastHeroName = "nobody";
			this.loadingHeroName = "nobody";
		}

		public void SetAnimationTime(float time)
		{
			string[] array = new string[]
			{
				"idle_1"
			};
			if (HeroAnimation.heroPoseIdles.TryGetValue(this.lastHeroName, out array))
			{
				this.skeletonGraphic.AnimationState.SetAnimation(0, array[0], true);
			}
			else
			{
				this.skeletonGraphic.AnimationState.SetAnimation(0, "idle_1", true);
			}
		}

		public void SetSkeletonColor(Color color)
		{
			if (!this.animatingColor)
			{
				if (this.skeletonGraphic == null)
				{
					this.skeletonGraphic = this.spineObject.GetComponent<SkeletonGraphic>();
				}
				this.skeletonGraphic.color = color;
			}
		}

		public void DoSkeletonColor(Color color)
		{
			this.animatingColor = true;
			this.skeletonGraphic.DOColor(color, 0.3f).OnComplete(delegate
			{
				this.animatingColor = false;
			});
		}

		public void DoSkeletonFlash(Color color)
		{
			this.animatingColor = true;
			this.skeletonGraphic.DOColor(color, 0.3f).OnComplete(delegate
			{
				this.animatingColor = false;
			});
		}

		public void ShowTransformedVersion(int skinIndex)
		{
			this.showTransformedVersion = true;
			this.transformedSkinIndex = skinIndex;
		}

		public void DontShowTransformedVersion()
		{
			this.showTransformedVersion = false;
		}

		private static Dictionary<string, string[]> heroPoseIdles;

		private bool justChanged;

		private bool setScale;

		private Vector3 scale;

		[HideInInspector]
		public SkeletonGraphic skeletonGraphic;

		[HideInInspector]
		public SkeletonGraphic skeletonGraphicAlternative;

		public GameObject spineObject;

		public RectTransform transitionPivot;

		private float animTimer;

		private int lastAnimIndex;

		private bool wasGoblin;

		public bool dontChangeAnimation;

		public bool dontChangeTimescale;

		public bool ignoreNextOnClose;

		private RectTransform rectTransform;

		public const string NobodyName = "nobody";

		private string lastHeroName = "nobody";

		[HideInInspector]
		public string loadingHeroName = "nobody";

		private bool forceCommonIdle;

		[HideInInspector]
		public int lastSkinIndex = -1;

		private bool animatePopup;

		private bool animateTransition;

		private bool unloadPreviousHero;

		public Vector2 upPosition;

		public Vector2 downPosition;

		private bool animatingColor;

		private bool showTransformedVersion;

		private int transformedSkinIndex;

		public enum TransitionState
		{
			Static,
			AppearAnimationOnly,
			FullTransition,
			WaitForDisappear,
			RewindAppear,
			WaitForRewind
		}
	}
}
