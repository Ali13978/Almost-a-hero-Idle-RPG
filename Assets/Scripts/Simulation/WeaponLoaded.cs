using System;

namespace Simulation
{
	public class WeaponLoaded : Weapon
	{
		public override void Init(Hero by, World world)
		{
			base.Init(by, world);
			this.isActive = false;
			this.attackTime = 0f;
			this.waitTime = 0f;
			this.hasDamaged = false;
			this.isReloading = true;
			this.load = 0;
		}

		public override Weapon Clone()
		{
			return new WeaponLoaded
			{
				durAttack = this.durAttack,
				durWait = this.durWait,
				damageType = this.damageType,
				damageMoments = this.damageMoments,
				soundsAttack = this.soundsAttack,
				durReload = this.durReload,
				soundReload = this.soundReload,
				loadMax = this.loadMax,
				id = this.id,
				projectileIndexPattern = this.projectileIndexPattern
			};
		}

		public void SetTiming(float durAttack, float[] damageMoments, float durAdditionalWait)
		{
			this.durAttack = durAttack;
			this.durWait = durAttack + durAdditionalWait;
			this.damageMoments = damageMoments;
		}

		public override double GetDps()
		{
			return this.by.GetDamage() / (double)this.durWait;
		}

		public override float GetBarTimeRatio()
		{
			if (this.isReloading)
			{
				return this.reloadTime / this.durReload;
			}
			return GameMath.GetMinFloat(1f, (float)this.load / (float)this.GetLoadMax());
		}

		public override float GetAnimTimeRatio()
		{
			if (this.isReloading)
			{
				return this.reloadTime / this.durReload;
			}
			return this.attackTime / this.durAttack;
		}

		public override void UpdateActive(float dt)
		{
			this.UpdateWaitTime(dt);
			this.UpdateReloading(dt);
			if (this.isReloading)
			{
				return;
			}
			float num = this.by.GetAttackSpeed();
			if (num < 0.5f)
			{
				num = 0.5f;
			}
			else if (num > 1f)
			{
				float num2 = (this.durWait - this.waitTime) / num;
				float num3 = this.durAttack - this.attackTime;
				if (num2 <= 0f)
				{
					num = float.MaxValue;
				}
				else if (num3 > num2)
				{
					num = num3 / num2;
				}
				else
				{
					num = 1f;
				}
			}
			this.attackTime += dt * num;
			if (this.attackTime > this.damageMoments[this.numHits % this.damageMoments.Length] && !this.hasDamaged)
			{
				this.Damage();
				Sound sound = new SoundSimple(SoundArchieve.inst.tamAttacks[this.numHits % SoundArchieve.inst.tamAttacks.Length], 1f);
				this.world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, this.by.GetId(), false, 0f, sound));
				this.load--;
			}
			if (this.attackTime > this.durAttack)
			{
				this.attackTime = 0f;
				this.hasDamaged = false;
				this.isActive = false;
				this.numHits++;
			}
			base.PlayTimedSounds(this.attackTime / this.durAttack);
		}

		public override void UpdatePassive(float dt)
		{
			this.UpdateWaitTime(dt);
		}

		private void UpdateWaitTime(float dt)
		{
			float num = this.by.GetAttackSpeed();
			if (num < 0f)
			{
				num = 0f;
			}
			this.waitTime += dt * num;
		}

		private void UpdateReloading(float dt)
		{
			if (this.isReloading)
			{
				if (this.reloadTime == 0f)
				{
					if (this.soundReload is SoundVaried)
					{
						SoundVaried soundVaried = (SoundVaried)this.soundReload;
						soundVaried.SetVariation(this.numHits / this.GetLoadMax());
					}
					SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, this.by.GetId(), false, 0f, this.soundReload);
					this.world.AddSoundEvent(e);
				}
				this.reloadTime += dt * this.by.GetReloadSpeed();
				if (this.reloadTime >= this.durReload)
				{
					this.isReloading = false;
					this.reloadTime = 0f;
					this.attackTime = 0f;
					this.waitTime = 0f;
					this.load = this.GetLoadMax();
				}
			}
		}

		private int GetLoadMax()
		{
			return this.loadMax + this.by.GetWeaponLoadExtra();
		}

		private void Damage()
		{
			this.hasDamaged = true;
			UnitHealthy unitHealthy = this.target;
			if (this.immediatelyTarget != null)
			{
				this.target = this.immediatelyTarget;
				this.immediatelyTarget = null;
			}
			if (this.target == null || !this.target.IsAlive() || !this.target.IsOnWorld())
			{
				this.target = this.world.GetRandomAliveEnemy();
			}
			if (this.target == null)
			{
				return;
			}
			if (this.target != unitHealthy)
			{
				base.OnAttackTargetChanged(unitHealthy, this.target);
			}
			bool flag = false;
			float missChance = this.by.GetMissChance();
			if (GameMath.GetProbabilityOutcome(missChance, GameMath.RandType.NoSeed))
			{
				flag = true;
				Damage damage = new Damage(0.0, false, false, true, false);
				GlobalPastDamage pastDamage = new GlobalPastDamage(this.by, damage);
				this.world.AddPastDamage(pastDamage);
			}
			double num = this.by.GetDamage();
			float critChance = this.by.GetCritChance();
			bool probabilityOutcome = GameMath.GetProbabilityOutcome(critChance, GameMath.RandType.NoSeed);
			if (probabilityOutcome)
			{
				num *= this.by.GetCritFactor();
			}
			Damage damage2 = new Damage(num, probabilityOutcome, false, flag, false);
			damage2.type = this.damageType;
			if (flag)
			{
				this.by.OnMissed(this.target, damage2);
			}
			this.world.OnPreAttack(this.by, damage2, null);
			this.world.OnWeaponUsed(this.by);
			this.world.DamageMain(this.by, this.target, damage2);
		}

		public override void OnDied()
		{
			this.isActive = false;
			this.attackTime = 0f;
			this.waitTime = 0f;
			this.hasDamaged = false;
		}

		public override void OnInterrupted()
		{
			this.attackTime = 0f;
			this.waitTime = 0f;
			this.reloadTime = 0f;
			this.isReloading = false;
			this.hasDamaged = false;
			this.isActive = false;
		}

		public override bool IsActive()
		{
			return this.isActive;
		}

		public override void TryActivate()
		{
			if (this.immediatelyTarget == null && this.waitTime < this.durWait)
			{
				return;
			}
			if (this.load == 0)
			{
				this.isReloading = true;
			}
			if (this.isReloading)
			{
				this.Activate();
				return;
			}
			UnitHealthy unitHealthy = this.target;
			if (this.immediatelyTarget != null)
			{
				this.target = this.immediatelyTarget;
				this.immediatelyTarget = null;
			}
			else
			{
				if (this.target != null && this.target.IsAlive())
				{
					this.Activate();
					return;
				}
				this.target = this.world.GetRandomAliveEnemy();
			}
			if (this.target != null && this.target.IsAlive())
			{
				this.Activate();
			}
			if (this.target != unitHealthy)
			{
				base.OnAttackTargetChanged(unitHealthy, this.target);
			}
		}

		private void Activate()
		{
			this.isActive = true;
			this.waitTime = 0f;
			this.attackTime = 0f;
			this.atSound = 0;
		}

		public override bool IsReloading()
		{
			return this.isReloading;
		}

		public override float GetReloadTimeRatio()
		{
			if (this.isReloading)
			{
				return this.reloadTime / this.durReload;
			}
			return -1f;
		}

		public override void AttackImmediately(UnitHealthy unit)
		{
			this.immediatelyTarget = unit;
		}

		private float durAttack;

		private float durWait;

		private float[] damageMoments;

		private bool isActive;

		private float attackTime;

		private float waitTime;

		private bool hasDamaged;

		private UnitHealthy target;

		private UnitHealthy immediatelyTarget;

		public int loadMax;

		public float durReload;

		private int load;

		private bool isReloading;

		private float reloadTime;

		public Sound soundReload;
	}
}
