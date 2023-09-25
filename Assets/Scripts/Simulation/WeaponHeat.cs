using System;

namespace Simulation
{
	public class WeaponHeat : Weapon
	{
		public override void Init(Hero by, World world)
		{
			base.Init(by, world);
			this.isActive = false;
			this.attackTime = 0f;
			this.waitTime = 0f;
			this.hasDamaged = false;
			this.heat = 0f;
			this.isOverHeated = false;
		}

		public override Weapon Clone()
		{
			return new WeaponHeat
			{
				durAttack = this.durAttack,
				durWait = this.durWait,
				damageType = this.damageType,
				damageMoment = this.damageMoment,
				heatMax = this.heatMax,
				heatPerDamage = this.heatPerDamage,
				coolingSpeed = this.coolingSpeed,
				overCoolingSpeed = this.overCoolingSpeed,
				soundsAttack = this.soundsAttack,
				id = this.id,
				projectileIndexPattern = this.projectileIndexPattern
			};
		}

		public void SetTiming(float durAttack, float damageMoment, float durAdditionalWait)
		{
			this.durAttack = durAttack;
			this.durWait = durAttack + durAdditionalWait;
			this.damageMoment = damageMoment;
		}

		public override double GetDps()
		{
			return this.by.GetDamage() / (double)this.durWait;
		}

		public override float GetBarTimeRatio()
		{
			return GameMath.GetMinFloat(1f, (this.GetHeatMax() - this.heat) / this.GetHeatMax());
		}

		public float GetHeatMax()
		{
			return this.heatMax * this.by.buffTotalEffect.heatMaxFactor;
		}

		public override float GetAnimTimeRatio()
		{
			return this.attackTime / this.durAttack;
		}

		public override void UpdateActive(float dt)
		{
			this.UpdateWaitTime(dt);
			this.UpdateHeat(dt);
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
			if (this.attackTime > this.damageMoment && !this.hasDamaged)
			{
				this.Damage();
			}
			if (this.attackTime > this.durAttack)
			{
				this.isActive = false;
				this.numHits++;
			}
			base.PlayTimedSounds(this.attackTime / this.durAttack);
		}

		public override void UpdatePassive(float dt)
		{
			this.UpdateWaitTime(dt);
			this.UpdateHeat(dt);
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

		private void UpdateHeat(float dt)
		{
			if (this.isOverHeated)
			{
				this.heat -= this.GetOvercoolingSpeed() * dt;
			}
			else
			{
				this.heat -= this.coolingSpeed * dt;
			}
			if (this.heat <= 0f)
			{
				this.heat = 0f;
				if (this.isOverHeated)
				{
					this.isOverHeated = false;
					if (this.by.GetId() == "IDA")
					{
						this.world.OnVexxCool();
					}
				}
			}
		}

		private void Damage()
		{
			this.hasDamaged = true;
			UnitHealthy unitHealthy = this.target;
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
			if (!this.by.HasWeaponAntiHeatBuff())
			{
				this.heat += this.heatPerDamage;
				if (this.heat >= this.GetHeatMax())
				{
					this.heat = this.GetHeatMax();
					this.isOverHeated = true;
				}
			}
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
			if (this.isOverHeated)
			{
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
			if (unitHealthy != this.target)
			{
				base.OnAttackTargetChanged(unitHealthy, this.target);
			}
		}

		private void Activate()
		{
			this.isActive = true;
			this.waitTime = 0f;
			this.attackTime = 0f;
			this.hasDamaged = false;
			this.atSound = 0;
		}

		public override void OnDied()
		{
			this.isActive = false;
			this.attackTime = 0f;
			this.waitTime = 0f;
			this.hasDamaged = false;
			this.atSound = 0;
		}

		public override void OnInterrupted()
		{
			this.isActive = false;
			this.attackTime = 0f;
			this.waitTime = 0f;
			this.hasDamaged = false;
			this.atSound = 0;
		}

		public override bool IsOverheated()
		{
			return this.isOverHeated;
		}

		public override float GetOverheatTimeLeft()
		{
			return this.heat / this.GetOvercoolingSpeed();
		}

		private float GetOvercoolingSpeed()
		{
			return this.overCoolingSpeed * this.by.buffTotalEffect.heatOvercoolFactor;
		}

		public override void Cool(float coolRatio)
		{
			this.heat -= coolRatio * this.GetHeatMax();
			if (this.heat <= 0f)
			{
				this.heat = 0f;
				if (this.isOverHeated)
				{
					this.isOverHeated = false;
					if (this.by.GetId() == "IDA")
					{
						this.world.OnVexxCool();
					}
				}
			}
		}

		public override void CancelCurrentOverheat()
		{
			this.isOverHeated = false;
		}

		public override void AttackImmediately(UnitHealthy unit)
		{
			this.immediatelyTarget = unit;
		}

		private float durAttack;

		private float durWait;

		private float damageMoment;

		public float heatMax;

		public float heatPerDamage;

		public float coolingSpeed;

		public float overCoolingSpeed;

		private bool isActive;

		private float attackTime;

		private float waitTime;

		private bool hasDamaged;

		private float heat;

		private bool isOverHeated;

		private UnitHealthy target;

		private UnitHealthy immediatelyTarget;
	}
}
