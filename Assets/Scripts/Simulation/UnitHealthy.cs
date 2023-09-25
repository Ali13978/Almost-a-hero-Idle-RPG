using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public abstract class UnitHealthy : Unit
	{
		public UnitHealthy(World world) : base(world)
		{
			this.pastDamages = new Queue<PastDamage>();
		}

		public abstract Vector3 GetProjectileTargetOffset();

		public abstract float GetProjectileTargetRandomness();

		public double GetHealthMax()
		{
			return this.statCache.healthMax;
		}

		public double GetHealthRegen()
		{
			return this.statCache.healthRegen;
		}

		public double GetDamageTakenFactor()
		{
			return this.statCache.damageTakenFactor;
		}

		public float GetDodgeChance()
		{
			return this.statCache.dodgeChance;
		}

		public float GetTaunt()
		{
			return 1f + this.buffTotalEffect.tauntAdd;
		}

		public bool IsAttackable()
		{
			return this.IsAlive();
		}

		public double GetShield()
		{
			return this.shieldRatio * this.GetHealthMax();
		}

		public double GetShieldRatio()
		{
			return this.shieldRatio;
		}

		public bool HasZeroShield()
		{
			return this.shieldRatio == 0.0;
		}

		public float GetShieldTimeLeft()
		{
			return this.shieldTimeLeft;
		}

		public void GainShield(double ratio, float dur)
		{
			if (this.healthRatio <= 0.0)
			{
				return;
			}
			this.shieldRatio += ratio;
			if (this.shieldRatio > 1.0)
			{
				this.shieldRatio = 1.0;
			}
			this.world.OnUnitShielded(this);
			this.shieldTimeLeft = GameMath.GetMaxFloat(this.shieldTimeLeft, dur);
		}

		public double GetHealthRatio()
		{
			return this.healthRatio;
		}

		public void SetHealthRatio(double healthRatio)
		{
			this.healthRatio = healthRatio;
		}

		public double GetHealth()
		{
			return this.healthRatio * this.GetHealthMax();
		}

		public bool HasZeroHealth()
		{
			return this.healthRatio == 0.0;
		}

		public bool IsAlive()
		{
			return this.healthRatio > 0.0;
		}

		public bool HasFullHealth()
		{
			return this.healthRatio >= 1.0;
		}

		public bool IsAlly(Unit unit)
		{
			foreach (UnitHealthy unitHealthy in this.GetAllies())
			{
				if (unitHealthy == unit)
				{
					return true;
				}
			}
			return false;
		}

		public virtual void Heal(double healRatio)
		{
			if (healRatio > 0.0)
			{
				this.world.OnUnitPreHeal(this, ref healRatio);
				if (this.healthRatio + healRatio > 1.0)
				{
					healRatio = 1.0 - this.healthRatio;
				}
				this.healthRatio += healRatio;
				this.world.OnUnitHealed(this, healRatio);
			}
		}

		public void SetHealthFull()
		{
			this.healthRatio = 1.0;
		}

		public void DecrementHealthRatioWithoutKilling(double ratio)
		{
			this.healthRatio -= ratio;
			if (this.healthRatio < 1E-10)
			{
				this.healthRatio = 1E-10;
			}
		}

		public override void Update(float dt, int addVisuals)
		{
			base.Update(dt, addVisuals);
			if (this.lastDamage != null)
			{
				this.lastDamage.time += dt;
			}
			foreach (PastDamage pastDamage in this.pastDamages)
			{
				pastDamage.time += dt;
			}
			while (this.pastDamages.Count > 0 && this.pastDamages.Peek().time > 0.9f)
			{
				this.pastDamages.Dequeue();
			}
			if (!this.IsAlive())
			{
				return;
			}
			double healthRegen = this.GetHealthRegen();
			if (healthRegen > 0.0)
			{
				this.Heal(healthRegen * (double)dt);
			}
			else if (healthRegen < 0.0)
			{
				double healthMax = this.GetHealthMax();
				this.TakeDamage(new Damage(-healthMax * healthRegen * (double)dt, false, false, false, false)
				{
					doNotHighlight = true,
					dontShow = true
				}, null, 0.0);
			}
			if (this.shieldRatio > 0.0)
			{
				this.shieldTimeLeft -= dt;
				if (this.shieldTimeLeft <= 0f)
				{
					this.shieldRatio = 0.0;
				}
			}
			else
			{
				this.shieldTimeLeft = 0f;
			}
		}

		public virtual void TakeDamage(Damage damage, Unit by, double minHealth = 0.0)
		{
			if (damage.isMissed)
			{
				return;
			}
			if (!this.IsAlive())
			{
				return;
			}
			if (!damage.isPure && base.IsInvulnerable())
			{
				damage.amount = 0.0;
			}
			if (!damage.isPure && !damage.canNotBeDodged && GameMath.GetProbabilityOutcome(this.GetDodgeChance(), GameMath.RandType.NoSeed))
			{
				damage.isDodged = true;
				damage.amount = 0.0;
				if (by != null)
				{
					base.OnDodged(by, damage);
					this.world.OnUnitDodged(this, by, damage);
				}
			}
			if (by != null)
			{
				base.OnPreTakeDamage(by, damage);
			}
			double num = this.GetDamageTakenFactor();
			if (this is Enemy && by is Hero)
			{
				num *= 1.0 + this.statCache.extraDamageTakenFromHeroesFactor;
				double extraDamageTakenFromHeroes = PlayfabManager.extraDamageTakenFromHeroes;
				num *= 1.0 + extraDamageTakenFromHeroes;
			}
			else if (this is Enemy && by is Totem)
			{
				num *= 1.0 + this.statCache.extraDamageTakenFromRingFactor;
			}
			double num2 = damage.amount;
			if (!damage.ignoreReduction)
			{
				num2 *= num;
			}
			if (num2 < 0.0)
			{
				num2 = 0.0;
			}
			damage.realDamageDealt = num2;
			double num3 = this.GetHealth() + this.GetShield();
			if (num3 < num2)
			{
				damage.realDamageDealt = num3;
			}
			damage.amount = num2;
			double num4 = num2 / this.GetHealthMax();
			if (this is Enemy && by is Hero)
			{
				Enemy enemy = this as Enemy;
				enemy.percentageDamageTakenFromHeroes += GameMath.Clamp(num4, 0.0, 1.0);
			}
			if (!damage.ignoreShield && this.shieldRatio > 0.0)
			{
				if (this.shieldRatio > num4)
				{
					this.shieldRatio -= num4;
					num4 = 0.0;
				}
				else
				{
					num4 -= this.shieldRatio;
					this.shieldRatio = 0.0;
					this.OnShieldBreakSelf();
					foreach (UnitHealthy unitHealthy in this.GetAllies())
					{
						if (unitHealthy != this)
						{
							unitHealthy.OnShieldBreakAlly(this);
						}
					}
				}
			}
			this.healthRatio -= num4;
			if (this.healthRatio <= minHealth)
			{
				this.healthRatio = minHealth;
			}
			if (!this.IsAlive())
			{
				if (by != null)
				{
					by.OnKilled(this);
				}
				this.OnDeathSelf();
				if (this is Enemy)
				{
					this.world.dailyQuestKilledEnemyCounter++;
				}
				if (this.world.totem != null)
				{
					this.world.totem.OnDeathAny(this);
				}
				foreach (Unit unit in this.world.heroes)
				{
					if (unit != this)
					{
						unit.OnDeathAny(this);
					}
				}
				foreach (Unit unit2 in this.world.activeChallenge.enemies)
				{
					if (unit2 != this)
					{
						unit2.OnDeathAny(this);
					}
				}
				foreach (UnitHealthy unitHealthy2 in this.GetAllies())
				{
					if (unitHealthy2 != this)
					{
						unitHealthy2.OnDeathAlly(this);
					}
				}
				foreach (UnitHealthy unitHealthy3 in this.GetOpponents())
				{
					unitHealthy3.OnOpponentDeath(this);
				}
				if (this.world.totem != null)
				{
					if (this is Enemy)
					{
						this.world.totem.OnOpponentDeath(this);
					}
					else
					{
						this.world.totem.OnDeathAlly(this);
					}
				}
			}
			if (by != null)
			{
				base.OnTakenDamage(by, damage);
			}
			if (num4 > 0.0)
			{
				base.OnHealthLost(num4);
			}
			if (!damage.dontShow)
			{
				PastDamage item = new PastDamage(damage);
				this.pastDamages.Enqueue(item);
				this.lastDamage = item;
				GlobalPastDamage pastDamage = new GlobalPastDamage(this, damage);
				this.world.AddPastDamage(pastDamage);
			}
		}

		public abstract bool IsOnWorld();

		private double healthRatio;

		private double shieldRatio;

		private float shieldTimeLeft;

		public Queue<PastDamage> pastDamages;

		public PastDamage lastDamage;
	}
}
