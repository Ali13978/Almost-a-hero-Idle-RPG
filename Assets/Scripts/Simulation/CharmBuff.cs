using System;
using UnityEngine;

namespace Simulation
{
	public abstract class CharmBuff : EnchantmentBuff
	{
		public override void Update(float dt)
		{
			this.stateTime += dt;
			if (this.state == EnchantmentBuffState.INACTIVE && this.progress >= 1f)
			{
				this.state = EnchantmentBuffState.READY;
				this.stateTime = 0f;
			}
			if (this.state == EnchantmentBuffState.READY)
			{
				bool flag = this.TryActivating();
				if (flag)
				{
					this.progress -= 1f;
					this.state = EnchantmentBuffState.ACTIVE;
					this.stateTime = 0f;
					this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, "charm", false, 0f, new SoundSimple(SoundArchieve.inst.charmTriggered, 1f, float.MaxValue)));
					this.world.OnAnyCharmTriggered(this);
				}
			}
			else if (this.state == EnchantmentBuffState.ACTIVE && this.stateTime >= this.durActive)
			{
				this.state = EnchantmentBuffState.INACTIVE;
				this.stateTime = 0f;
			}
			this.OnUpdate(dt);
		}

		public override void AddProgress(float dp)
		{
			if (this.state == EnchantmentBuffState.ACTIVE)
			{
				this.progress = Mathf.Min(0.99f, this.progress + dp);
			}
			else
			{
				this.progress = Mathf.Min(1.99f, this.progress + dp);
			}
		}

		public void RemoveProgress(float dp)
		{
			this.progress -= dp;
			if (this.progress < 0f)
			{
				this.progress = 0f;
			}
		}

		public virtual float GetActivationStateRate()
		{
			return GameMath.Clamp(this.stateTime / this.durActive, 0f, 1f);
		}

		public const float MAX_PROGRESS_WHILE_ACTIVE = 0.99f;

		public float durActive = 2f;

		public float stateTime;
	}
}
