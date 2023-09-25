using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class Weapon
	{
		public virtual void Init(Hero by, World world)
		{
			this.by = by;
			this.world = world;
			this.numHits = 0;
			this.atSound = 0;
		}

		public abstract double GetDps();

		public abstract float GetBarTimeRatio();

		public abstract float GetAnimTimeRatio();

		public abstract void UpdateActive(float dt);

		public abstract void UpdatePassive(float dt);

		public abstract void OnDied();

		public abstract void OnInterrupted();

		public abstract bool IsActive();

		public abstract void TryActivate();

		public virtual bool IsOverheated()
		{
			return false;
		}

		public virtual float GetOverheatTimeLeft()
		{
			return 0f;
		}

		public virtual void Cool(float coolRatio)
		{
		}

		public virtual void CancelCurrentOverheat()
		{
		}

		public virtual bool IsReloading()
		{
			return false;
		}

		public virtual float GetReloadTimeRatio()
		{
			return -1f;
		}

		public abstract void AttackImmediately(UnitHealthy unit);

		protected void OnAttackTargetChanged(UnitHealthy oldTarget, UnitHealthy newTarget)
		{
			this.by.OnAttackTargetChanged(oldTarget, newTarget);
		}

		public int GetNumHits()
		{
			return this.numHits;
		}

		public int GetProjectileIndex()
		{
			if (this.projectileIndexPattern == null || this.projectileIndexPattern.Length == 0)
			{
				return 0;
			}
			return this.projectileIndexPattern[this.numHits % this.projectileIndexPattern.Length];
		}

		protected void PlayTimedSounds(float attackTimeRatio)
		{
			while (this.atSound < this.soundsAttack.Count && this.soundsAttack[this.atSound].time <= attackTimeRatio)
			{
				Sound sound = this.soundsAttack[this.atSound].sound;
				if (sound is SoundVaried)
				{
					SoundVaried soundVaried = (SoundVaried)sound;
					soundVaried.SetVariation(this.numHits);
				}
				else if (sound is SoundVariedMultiple)
				{
					(sound as SoundVariedMultiple).SetVariation(this.numHits);
				}
				SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, this.by.GetId(), false, 0f, sound);
				this.world.AddSoundEvent(e);
				this.atSound++;
			}
		}

		public abstract Weapon Clone();

		public DamageType damageType;

		protected Hero by;

		protected World world;

		protected int numHits;

		protected int atSound;

		public List<TimedSound> soundsAttack;

		public int id;

		public int[] projectileIndexPattern;

		public bool isRepeatedTempWeapon;
	}
}
