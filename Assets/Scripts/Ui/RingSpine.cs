using System;
using Simulation;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Ui
{
	public class RingSpine : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.spineLightningScale = this.spineLightning.transform.localScale;
			this.spineLightning.transform.localScale = new Vector3(0f, 0f, 0f);
			this.spineLightningBigScale = this.spineLightningBig.transform.localScale;
			this.spineLightningBig.transform.localScale = new Vector3(0f, 0f, 0f);
			this.spineFiresScale = new Vector3[this.spineFires.Length];
			int i = 0;
			int num = this.spineFires.Length;
			while (i < num)
			{
				SpineGraphic spineGraphic = this.spineFires[i];
				this.spineFiresScale[i] = spineGraphic.transform.localScale;
				spineGraphic.transform.localScale = new Vector3(0f, 0f, 0f);
				i++;
			}
			this.spineFireOverheatScale = this.spineFireOverheat.transform.localScale;
			this.spineFireOverheat.transform.localScale = new Vector3(0f, 0f, 0f);
		}

		public override void AahUpdate(float dt)
		{
			if (this.spineLightning.AnimationState != null && this.spineLightningBig.AnimationState != null)
			{
				this.spineLightning.AnimationState.Complete += this.SpineLightningComplete;
				this.spineLightningBig.AnimationState.Complete += this.SpineLightningBigComplete;
				base.RemoveFromUpdates();
			}
		}

		private bool IsSpineFireAnimationStateReady()
		{
			return false;
		}

		public void PlayEffectLightning()
		{
			if (this.spineLightning.AnimationState != null)
			{
				this.spineLightning.gameObject.SetActive(true);
				this.spineLightning.transform.localScale = this.spineLightningScale;
				this.spineLightning.AnimationState.SetAnimation(0, "attack_1", true);
			}
		}

		public void PlayEffectLightningBig()
		{
			if (this.spineLightningBig.AnimationState != null)
			{
				this.spineLightningBig.gameObject.SetActive(true);
				this.spineLightningBig.transform.localScale = this.spineLightningBigScale;
				this.spineLightningBig.AnimationState.SetAnimation(0, "attack_1", true);
			}
		}

		public void PlayEffectFire()
		{
			int randomInt = GameMath.GetRandomInt(0, this.spineFires.Length, GameMath.RandType.NoSeed);
			SpineGraphic spineGraphic = this.spineFires[randomInt];
			if (spineGraphic.skeletonGraphic.AnimationState != null)
			{
				spineGraphic.gameObject.SetActive(true);
				spineGraphic.transform.localScale = this.spineFiresScale[randomInt];
				spineGraphic.skeletonGraphic.Skeleton.SetToSetupPose();
				if (GameMath.GetProbabilityOutcome(0.5f, GameMath.RandType.NoSeed))
				{
					spineGraphic.skeletonGraphic.AnimationState.SetAnimation(0, "tap", true);
				}
				else
				{
					spineGraphic.skeletonGraphic.AnimationState.SetAnimation(0, "tap2", true);
				}
			}
		}

		public void PlayEffectOverheat(bool isOverHeated)
		{
			if (isOverHeated && !this.spineFirePlayingOverheat)
			{
				this.spineFirePlayingOverheat = true;
				this.spineFireOverheat.transform.localScale = this.spineFireOverheatScale;
			}
			else if (!isOverHeated && this.spineFirePlayingOverheat)
			{
				this.spineFirePlayingOverheat = false;
				this.spineFireOverheat.transform.localScale = new Vector3(0f, 0f, 0f);
			}
		}

		private void SpineLightningComplete(TrackEntry trackEntry)
		{
			this.spineLightning.transform.localScale = new Vector3(0f, 0f, 0f);
		}

		private void SpineLightningBigComplete(TrackEntry trackEntry)
		{
			this.spineLightningBig.transform.localScale = new Vector3(0f, 0f, 0f);
		}

		public void PlayGenericEffect(bool playSound = false)
		{
			if (this.totem is TotemDataBaseFire)
			{
				this.PlayEffectFire();
				if (playSound)
				{
					UiManager.sounds.Add(new SoundEventUiVariedSimple(SoundArchieve.inst.totemThrows, 1f));
				}
			}
			else if (this.totem is TotemDataBaseLightning)
			{
				this.PlayEffectLightning();
				if (playSound)
				{
					UiManager.sounds.Add(new SoundEventUiVariedSimple(SoundArchieve.inst.lightnings, 1f));
				}
			}
			else if (this.totem is TotemDataBaseIce)
			{
				if (playSound)
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.totemIceChargeStart, 1f));
				}
			}
			else if (this.totem is TotemDataBaseEarth && playSound)
			{
				UiManager.sounds.Add(new SoundEventUiVariedSimple(SoundArchieve.inst.earthRingMeteorImpact, 1f));
			}
		}

		public TotemDataBase totem;

		public SpineGraphic[] spineFires;

		public SkeletonGraphic spineFireOverheat;

		public SkeletonGraphic spineLightning;

		public SkeletonGraphic spineLightningBig;

		private Vector3[] spineFiresScale;

		private Vector3 spineFireOverheatScale;

		private Vector3 spineLightningScale;

		private Vector3 spineLightningBigScale;

		private bool spineFirePlayingOverheat;
	}
}
