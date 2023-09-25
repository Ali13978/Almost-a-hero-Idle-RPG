using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class LoadingTransition : AahMonoBehaviour
	{
		public CanvasGroup canvasGroup
		{
			get
			{
				CanvasGroup result;
				if ((result = this.m_canvasGroup) == null)
				{
					result = (this.m_canvasGroup = base.GetComponent<CanvasGroup>());
				}
				return result;
			}
		}

		public override void Register()
		{
			base.AddToUpdates();
			base.AddToInits();
		}

		public bool IsTransitioning()
		{
			return this.isAnimating;
		}

		public override void Init()
		{
			this.isClosed = true;
			this.allAnimations = new List<LoadingTransitionAnim>();
			foreach (LoadingTransitionSpine loadingTransitionSpine in this.spineAnimations)
			{
				loadingTransitionSpine.Init();
				this.allAnimations.Add(loadingTransitionSpine);
			}
			this.allAnimations.Add(this.swordAnimation);
			this.allAnimations.Add(this.homeHoop);
			this.allAnimations.Shuffle<LoadingTransitionAnim>();
		}

		public LoadingTransitionAnim GetCurrentTransitionAnim()
		{
			return this.currentAnimation;
		}

		public void SetAnimationType(LoadingTransition.TransitionAnimType type)
		{
			switch (type)
			{
			case LoadingTransition.TransitionAnimType.Sword:
				this.currentAnimation = this.swordAnimation;
				break;
			case LoadingTransition.TransitionAnimType.DuckHoop:
				this.currentAnimation = this.duckHoop;
				break;
			case LoadingTransition.TransitionAnimType.HomeHoop:
				this.currentAnimation = this.homeHoop;
				break;
			case LoadingTransition.TransitionAnimType.RotatingCoin:
				this.currentAnimation = this.rotateCoingAnimation;
				break;
			default:
				this.currentAnimation = this.swordAnimation;
				break;
			}
		}

		public override void AahUpdate(float dt)
		{
			if (this.isClosed)
			{
				this.timer += dt;
				if (this.loadEventSent && this.timer >= this.durStay && (this.stateToGo != UiState.NONE || Main.instance.AreAllGameAssetsLoaded()))
				{
					if (this.willCallTransitionEvent)
					{
						this.willCallTransitionEvent = false;
						this.callTransitionEventNow = true;
					}
					this.FadeOut();
				}
				else if (!this.loadEventSent && this.timer >= this.loadingDelay)
				{
					if (this.stateToGo == UiState.NONE)
					{
						Main.instance.LoadGameAssets();
					}
					else
					{
						Main.instance.UnloadInGameAssets();
					}
					this.loadEventSent = true;
				}
			}
		}

		public void DoTransition(UiState stateToGo, float delay = 0f, float loadingDelay = 0f)
		{
			if (this.currentAnimation != null)
			{
				this.currentAnimation.OnComplete();
			}
			this.currentAnimation = this.allAnimations[this.currentAnimationIndex];
			this.currentAnimationIndex++;
			if (this.currentAnimationIndex >= this.allAnimations.Count)
			{
				this.currentAnimationIndex = 0;
				this.allAnimations.Shuffle<LoadingTransitionAnim>();
			}
			this.stateToGo = stateToGo;
			this.timer = 0f;
			this.willCallTransitionEvent = true;
			this.loadEventSent = false;
			this.loadingDelay = loadingDelay - this.fadeDuration;
			this.FadeIn(delay);
		}

		public void InitStrings()
		{
		}

		public void FadeIn(float delay = 0f)
		{
			this.isAnimating = true;
			this.inputBlocker.SetActive(true);
			this.canvasGroup.blocksRaycasts = true;
			this.canvasGroup.interactable = true;
			LoadingTransitionAnim anim = this.GetCurrentTransitionAnim();
			Sequence s = DOTween.Sequence();
			if (delay > 0f)
			{
				s.AppendInterval(delay);
			}
			s.Append(this.canvasGroup.DOFade(1f, this.fadeDuration / 2f)).Append(anim.GetFadeInAnimation()).OnComplete(delegate
			{
				Tween loopAnimation = anim.GetLoopAnimation();
				if (loopAnimation != null)
				{
					loopAnimation.Play<Tween>();
				}
				this.isClosed = true;
			}).Play<Sequence>();
		}

		private void FadeOut()
		{
			this.isAnimating = true;
			this.isClosed = false;
			LoadingTransitionAnim anim = this.GetCurrentTransitionAnim();
			Tween currentLoopingAnimation = anim.currentLoopingAnimation;
			if (currentLoopingAnimation != null)
			{
				currentLoopingAnimation.Kill(false);
			}
			DOTween.Sequence().Append(anim.GetFadeOutAnimation()).Append(this.canvasGroup.DOFade(0f, this.fadeDuration)).OnComplete(delegate
			{
				anim.OnComplete();
				this.isAnimating = false;
				this.callTransitionEndEventNow = true;
				LoadingTransition.canUnloadPreloaderScene = true;
				this.inputBlocker.SetActive(false);
				this.canvasGroup.blocksRaycasts = false;
				this.canvasGroup.interactable = false;
			}).Play<Sequence>();
		}

		public float GetAnimRatioClose()
		{
			return this.timer / (this.fadeDuration + this.durStay + this.fadeDuration);
		}

		public bool IsPlaying()
		{
			return this.isAnimating;
		}

		public UiState stateToGo;

		public float fadeDuration = 0.6f;

		public float durStay = 0.8f;

		public bool callTransitionEventNow;

		public bool callTransitionEndEventNow;

		public Text loadingText;

		public GameObject inputBlocker;

		private CanvasGroup m_canvasGroup;

		private bool isClosed;

		private bool isAnimating;

		private bool loadEventSent;

		private float loadingDelay;

		private bool willCallTransitionEvent;

		private float timer;

		public SwordAnimation swordAnimation;

		public HoopingAnimation duckHoop;

		public HoopingAnimation homeHoop;

		public RotateCoingAnimation rotateCoingAnimation;

		public LoadingTransitionSpine[] spineAnimations;

		public LoadingTransitionAnim currentAnimation;

		private List<LoadingTransitionAnim> allAnimations;

		private int currentAnimationIndex;

		public bool abandonOrFailed;

		public static bool canUnloadPreloaderScene;

		public enum TransitionAnimType
		{
			Sword,
			DuckHoop,
			HomeHoop,
			RotatingCoin
		}
	}
}
