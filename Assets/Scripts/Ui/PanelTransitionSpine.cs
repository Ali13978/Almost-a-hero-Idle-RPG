using System;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Ui
{
	public class PanelTransitionSpine : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToUpdates();
			base.AddToInits();
		}

		public override void Init()
		{
			this.time = this.DUR_CLOSE;
			this.uiStateToTransitionTo = UiState.NONE;
			this.shouldPlayTransitionSound = false;
			this.shouldCloseTransition = true;
			this.willCallTransitionEvent = false;
			this.callTransitionEventNow = false;
		}

		public override void AahUpdate(float dt)
		{
			if (!this.spineEventAdded && this.spineAnim != null && this.spineAnim.AnimationState != null)
			{
				this.spineEventAdded = true;
				this.spineAnim.AnimationState.Complete += this.SpineAnimComplete;
			}
			this.time += dt;
			if (this.time >= this.DUR_OPEN + this.durstay)
			{
				if (this.loadEventSent && (this.uiStateToTransitionTo != UiState.NONE || Main.instance.AreAllGameAssetsLoaded()))
				{
					if (this.willCallTransitionEvent)
					{
						this.willCallTransitionEvent = false;
						this.callTransitionEventNow = true;
					}
					if (this.shouldCloseTransition && this.spineAnim.AnimationState != null)
					{
						this.shouldCloseTransition = false;
						this.animName = "open";
						this.spineAnim.AnimationState.SetAnimation(0, this.animName, false);
					}
					if (this.shouldPlayTransitionSound)
					{
						this.shouldPlayTransitionSound = false;
						UiManager.sounds.Add(new SoundEventCancelAll());
						UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTransitionDisappear, 1f));
					}
					if (this.time >= this.DUR_OPEN + this.durstay + this.DUR_CLOSE)
					{
						this.spineAnim.gameObject.SetActive(false);
						this.isAnimating = false;
						this.inputBlocker.SetActive(false);
						this.firstBoot = false;
						this.callTransitionEndEventNow = true;
					}
				}
				else
				{
					this.durstay += dt;
					if (!this.loadEventSent)
					{
						if (this.uiStateToTransitionTo == UiState.NONE)
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
		}

		public void ChangeSpeed(bool isNormal)
		{
			if (isNormal)
			{
				this.DUR_CLOSE = 0.333f;
				this.DUR_STAY = 0.2f;
				this.DUR_OPEN = 0.9f;
			}
			else
			{
				this.DUR_CLOSE = 0.15f;
				this.DUR_STAY = 0.15f;
				this.DUR_OPEN = 0.15f;
			}
		}

		private void SpineAnimComplete(TrackEntry trackEntry)
		{
			if (this.animName == "close")
			{
				this.animName = "idle";
				if (this.spineAnim.AnimationState != null)
				{
					this.spineAnim.AnimationState.SetAnimation(0, this.animName, true);
				}
			}
		}

		public void StartAnim(UiState uiStateToTransitionTo = UiState.NONE)
		{
			if (this.isAnimating)
			{
				UnityEngine.Debug.LogError("Something's wrong.");
			}
			this.isAnimating = true;
			this.uiStateToTransitionTo = uiStateToTransitionTo;
			this.loadEventSent = false;
			if (UiManager.DEBUGDontShowTransitionInEditor || this.spineAnim.Skeleton == null || this.spineAnim.AnimationState == null)
			{
				this.callTransitionEventNow = true;
			}
			else
			{
				this.time = 0f;
				this.durstay = this.DUR_STAY;
				if (this.firstBoot)
				{
					this.durstay += 0.8f;
				}
				base.gameObject.SetActive(true);
				this.shouldPlayTransitionSound = true;
				this.shouldCloseTransition = true;
				this.animName = "close";
				this.spineAnim.gameObject.SetActive(true);
				this.inputBlocker.SetActive(true);
				this.spineAnim.Skeleton.SetToSetupPose();
				if (this.spineAnim.AnimationState != null)
				{
					this.spineAnim.AnimationState.SetAnimation(0, this.animName, false);
				}
				this.spineAnim.Update(0f);
				this.willCallTransitionEvent = true;
			}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiTransitionAppear, 1f));
		}

		public float GetAnimRatioClose()
		{
			return this.time / (this.DUR_OPEN + this.durstay + this.DUR_CLOSE);
		}

		public bool IsPlaying()
		{
			return this.isAnimating;
		}

		private float DUR_CLOSE = 0.333f;

		private float DUR_STAY = 0.2f;

		private float DUR_OPEN = 0.9f;

		private bool firstBoot = true;

		private bool loadEventSent;

		public SkeletonGraphic spineAnim;

		public GameObject inputBlocker;

		private string animName;

		private const string AnimNameOpen = "close";

		private const string AnimNameIdle = "idle";

		private const string AnimNameClose = "open";

		private float time;

		private float durstay;

		private bool shouldPlayTransitionSound;

		private bool shouldCloseTransition;

		private bool spineEventAdded;

		public bool callTransitionEventNow;

		public bool callTransitionEndEventNow;

		private bool willCallTransitionEvent;

		private bool isAnimating;

		public UiState uiStateToTransitionTo;

		private bool isPlaying;
	}
}
