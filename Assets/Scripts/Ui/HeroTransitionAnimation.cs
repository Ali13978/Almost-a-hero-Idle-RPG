using System;
using DG.Tweening;
using UnityEngine;

namespace Ui
{
	public class HeroTransitionAnimation
	{
		public HeroTransitionAnimation(HeroAnimation heroAnimation, PanelGearScreen panelGearScreen)
		{
			this.heroAnimation = heroAnimation;
			this.panelGearScreen = panelGearScreen;
			this.state = HeroTransitionAnimation.TransitionState.Init;
		}

		public HeroTransitionAnimation(HeroAnimation heroAnimation)
		{
			this.heroAnimation = heroAnimation;
			this.panelGearScreen = null;
			this.state = HeroTransitionAnimation.TransitionState.Init;
		}

		public void Update()
		{
			if (this.state == HeroTransitionAnimation.TransitionState.WaitLoad && this.heroAnimation.HeroLoaded)
			{
				this.AppearAnimation();
			}
		}

		public void SetHeroAnimation(string heroName, int skinIndex, bool forceCommonIdle, int evolveLevel = 0)
		{
			if (heroName == this.heroAnimation.loadingHeroName && evolveLevel == this.evolveLevel && this.heroAnimation.lastSkinIndex == skinIndex)
			{
				return;
			}
			this.evolveLevel = evolveLevel;
			switch (this.state)
			{
			case HeroTransitionAnimation.TransitionState.Init:
				this.heroAnimation.SetHeroAnimation(heroName, skinIndex, forceCommonIdle, true, false, false);
				this.heroAnimation.transitionPivot.anchoredPosition = this.heroAnimation.upPosition;
				this.heroAnimation.transitionPivot.localScale = Vector3.zero;
				if (this.panelGearScreen != null)
				{
					this.panelGearScreen.heroBasePivotTransform.localScale = Vector3.zero;
					this.panelGearScreen.heroBaseAnimationPivotTransform.localScale = Vector3.zero;
				}
				this.state = HeroTransitionAnimation.TransitionState.WaitLoad;
				break;
			case HeroTransitionAnimation.TransitionState.Static:
				if (!this.heroAnimation.HeroInitialized || !this.heroAnimation.LoadedHeroName.Equals(heroName))
				{
					this.heroAnimation.SetHeroAnimation(heroName, skinIndex, forceCommonIdle, true, false, false);
					this.DisappearAnimation();
				}
				else
				{
					if (this.heroAnimation.lastSkinIndex != skinIndex)
					{
						this.heroAnimation.SetHeroAnimation(heroName, skinIndex, forceCommonIdle, false, false, false);
					}
					if (this.panelGearScreen != null)
					{
						this.panelGearScreen.InitHeroBase(evolveLevel);
					}
				}
				break;
			case HeroTransitionAnimation.TransitionState.Disappear:
			case HeroTransitionAnimation.TransitionState.WaitLoad:
				if (!this.heroAnimation.loadingHeroName.Equals(heroName))
				{
					this.heroAnimation.SetHeroAnimation(heroName, skinIndex, forceCommonIdle, true, false, false);
				}
				break;
			case HeroTransitionAnimation.TransitionState.Appear:
				if (!this.heroAnimation.loadingHeroName.Equals(heroName))
				{
					this.heroAnimation.SetHeroAnimation(heroName, skinIndex, forceCommonIdle, true, false, false);
					this.RewindAppearAnim();
				}
				break;
			case HeroTransitionAnimation.TransitionState.Rewind:
				if (!this.heroAnimation.loadingHeroName.Equals(heroName))
				{
					this.heroAnimation.SetHeroAnimation(heroName, skinIndex, forceCommonIdle, true, false, false);
				}
				break;
			}
		}

		private void RewindAppearAnim()
		{
			this.state = HeroTransitionAnimation.TransitionState.Rewind;
			if (this.appearAnimHero.ElapsedPercentage(true) < 0.3f)
			{
				this.appearAnimHero.Rewind(true);
				if (this.panelGearScreen != null)
				{
					this.appearAnimBase.Rewind(true);
					this.appearAnimBaseAnimation.Rewind(true);
				}
				this.OnAppearAnimationRewinded();
			}
			else
			{
				this.appearAnimHero.onRewind = new TweenCallback(this.OnAppearAnimationRewinded);
				this.appearAnimHero.SmoothRewind();
				if (this.panelGearScreen != null)
				{
					this.appearAnimBase.SmoothRewind();
					this.appearAnimBaseAnimation.SmoothRewind();
				}
			}
		}

		private void DisappearAnimation()
		{
			this.state = HeroTransitionAnimation.TransitionState.Disappear;
			this.heroAnimation.transitionPivot.localScale = Vector3.one;
			this.heroAnimation.transitionPivot.anchoredPosition = this.heroAnimation.downPosition;
			DOTween.Sequence().Append(this.heroAnimation.transitionPivot.DOScale(new Vector3(1.1f, 0.92f, 1f), 0.100000009f)).Append(this.heroAnimation.transitionPivot.DOScale(new Vector3(0.21f, 1.39f, 1f), 0.06666667f)).Insert(0.13333334f, this.heroAnimation.transitionPivot.DOAnchorPosY(this.heroAnimation.upPosition.y, 0.13333334f, false)).AppendInterval(0.1f).AppendCallback(new TweenCallback(this.OnDisappearAnimFinished)).Play<Sequence>();
			if (this.panelGearScreen != null)
			{
				this.panelGearScreen.heroTransitionFX.AnimationState.SetAnimation(0, "disappear", false);
				DOTween.Sequence().Append(this.panelGearScreen.heroBaseAnimationPivotTransform.DOScaleX(1.13f, 0.13333334f)).Append(this.panelGearScreen.heroBaseAnimationPivotTransform.DOScale(new Vector3(0.29f, 0.31f, 1f), 0.06666667f)).Append(this.panelGearScreen.heroBaseAnimationPivotTransform.DOScale(Vector3.zero, 0.0333333351f)).Play<Sequence>();
				DOTween.Sequence().Append(this.panelGearScreen.heroBasePivotTransform.DOScale(1.13f, 0.13333334f)).Append(this.panelGearScreen.heroBasePivotTransform.DOScale(new Vector3(0.29f, 0.31f, 1f), 0.06666667f)).Append(this.panelGearScreen.heroBasePivotTransform.DOScale(Vector3.zero, 0.0333333351f)).Play<Sequence>();
			}
		}

		private void OnDisappearAnimFinished()
		{
			this.state = HeroTransitionAnimation.TransitionState.WaitLoad;
		}

		private void AppearAnimation()
		{
			this.state = HeroTransitionAnimation.TransitionState.Appear;
			this.heroAnimation.spineObject.SetActive(true);
			this.heroAnimation.InitHeroAssets(null, false);
			this.appearAnimHero = DOTween.Sequence().Append(this.heroAnimation.transitionPivot.DOScale(new Vector3(0.27f, 1f, 1f), 0.0333333351f)).Append(this.heroAnimation.transitionPivot.DOScale(new Vector3(0.2f, 1.24f, 1f), 0.100000009f)).Append(this.heroAnimation.transitionPivot.DOScale(new Vector3(1.19f, 0.72f, 1f), 0.0333333351f)).Append(this.heroAnimation.transitionPivot.DOScale(new Vector3(0.98f, 1.06f, 1f), 0.13333334f)).Append(this.heroAnimation.transitionPivot.DOScale(Vector3.one, 0.200000018f)).AppendCallback(new TweenCallback(this.OnAppearAnimFinished)).Insert(0f, this.heroAnimation.transitionPivot.DOAnchorPosY(this.heroAnimation.downPosition.y, 0.13333334f, false)).Play<Sequence>();
			if (this.panelGearScreen != null)
			{
				this.panelGearScreen.InitHeroBase(this.evolveLevel);
				DOTween.Sequence().AppendInterval(0.13333334f).AppendCallback(new TweenCallback(this.PlayFxAppearAnim)).Play<Sequence>();
				this.appearAnimBase = DOTween.Sequence().AppendInterval(0.2f).Append(this.panelGearScreen.heroBasePivotTransform.DOScale(new Vector3(0.29f, 0.31f, 1f), 0.0333333351f)).Append(this.panelGearScreen.heroBasePivotTransform.DOScale(new Vector3(1.13f, 1f, 1f), 0.13333334f)).Append(this.panelGearScreen.heroBasePivotTransform.DOScaleX(1f, 0.0333333351f)).Play<Sequence>();
				this.appearAnimBaseAnimation = DOTween.Sequence().AppendInterval(0.2f).Append(this.panelGearScreen.heroBaseAnimationPivotTransform.DOScale(new Vector3(0.29f, 0.31f, 1f), 0.0333333351f)).Append(this.panelGearScreen.heroBaseAnimationPivotTransform.DOScale(new Vector3(1.13f, 1f, 1f), 0.13333334f)).Append(this.panelGearScreen.heroBaseAnimationPivotTransform.DOScaleX(1f, 0.0333333351f)).Play<Sequence>();
			}
		}

		private void OnAppearAnimFinished()
		{
			this.state = HeroTransitionAnimation.TransitionState.Static;
		}

		private void OnAppearAnimationRewinded()
		{
			this.state = HeroTransitionAnimation.TransitionState.WaitLoad;
		}

		private void PlayFxAppearAnim()
		{
			this.panelGearScreen.heroTransitionFX.AnimationState.SetAnimation(0, "appear", false);
		}

		public void OnClose()
		{
			this.state = HeroTransitionAnimation.TransitionState.Init;
			this.heroAnimation.OnClose();
			if (this.appearAnimHero != null && this.appearAnimHero.isPlaying)
			{
				this.appearAnimHero.Kill(false);
				this.appearAnimHero = null;
			}
			if (this.appearAnimBase != null && this.appearAnimBase.isPlaying)
			{
				this.appearAnimBase.Kill(false);
				this.appearAnimBase = null;
			}
			if (this.appearAnimBaseAnimation != null && this.appearAnimBaseAnimation.isPlaying)
			{
				this.appearAnimBaseAnimation.Kill(false);
				this.appearAnimBaseAnimation = null;
			}
		}

		private HeroAnimation heroAnimation;

		private PanelGearScreen panelGearScreen;

		private Sequence appearAnimHero;

		private Sequence appearAnimBase;

		private Sequence appearAnimBaseAnimation;

		private HeroTransitionAnimation.TransitionState state;

		private int evolveLevel;

		private const float KeyFrameDur = 0.0333333351f;

		public enum TransitionState
		{
			Init,
			Static,
			Disappear,
			WaitLoad,
			Appear,
			Rewind
		}
	}
}
