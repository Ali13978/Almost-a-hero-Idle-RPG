using System;

namespace Simulation
{
	public class WeaponWoodRanged : Weapon
	{
		public override void Init(Hero by, World world)
		{
			base.Init(by, world);
			this.isActive = false;
			this.attackTime = 0f;
			this.waitTime = 0f;
			this.hasThrown = false;
		}

		public override Weapon Clone()
		{
			return new WeaponWoodRanged
			{
				durAttack = this.durAttack,
				durWait = this.durWait,
				damageType = this.damageType,
				damageMoment = this.damageMoment,
				projectileType = this.projectileType,
				targetType = this.targetType,
				durFly = this.durFly,
				projectilePath = this.projectilePath,
				areaDamageRadius = this.areaDamageRadius,
				areaDamageRatio = this.areaDamageRatio,
				impactVisualEffect = this.impactVisualEffect,
				impactSound = this.impactSound,
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
			return -1f;
		}

		public override float GetAnimTimeRatio()
		{
			return this.attackTime / this.durAttack;
		}

		public override void UpdateActive(float dt)
		{
			this.UpdateWaitTime(dt);
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
			if (this.attackTime > this.damageMoment && !this.hasThrown)
			{
				this.Throw();
			}
			if (this.attackTime > this.durAttack)
			{
				this.attackTime = 0f;
				this.hasThrown = false;
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

		private void Throw()
		{
			this.hasThrown = true;
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
				if (this.by.GetId() == "BLIND_ARCHER")
				{
					this.world.OnLiaMiss();
				}
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
			Projectile projectile = new Projectile(this.by, this.projectileType, this.targetType, this.target, this.durFly, this.projectilePath);
			projectile.damage = damage2;
			if (this.areaDamageRatio > 0.0)
			{
				projectile.damageArea = new Damage(damage2.amount * this.areaDamageRatio * this.by.GetDamageAreaFactor(), false, false, false, false);
				projectile.damageAreaR = this.areaDamageRadius;
			}
			if (this.impactVisualEffect != null)
			{
				projectile.visualEffect = this.impactVisualEffect.GetCopy();
			}
			if (this.impactSound != null)
			{
				SoundEventSound soundImpact = new SoundEventSound(SoundType.GAMEPLAY, this.by.GetId(), false, 0f, this.impactSound);
				projectile.soundImpact = soundImpact;
			}
			this.world.OnPreAttack(this.by, damage2, projectile);
			this.world.OnWeaponUsed(this.by);
			this.by.AddProjectile(projectile);
		}

		public override void OnDied()
		{
			this.isActive = false;
			this.attackTime = 0f;
			this.waitTime = 0f;
			this.hasThrown = false;
		}

		public override void OnInterrupted()
		{
			this.attackTime = 0f;
			this.waitTime = 0f;
			this.hasThrown = false;
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

		public override void AttackImmediately(UnitHealthy unit)
		{
			this.immediatelyTarget = unit;
		}

		private float durAttack;

		private float durWait;

		private float damageMoment;

		public Projectile.Type projectileType;

		public Projectile.TargetType targetType;

		public float durFly;

		public ProjectilePath projectilePath;

		public double areaDamageRatio;

		public float areaDamageRadius;

		public VisualEffect impactVisualEffect;

		public Sound impactSound;

		private bool isActive;

		private float attackTime;

		private float waitTime;

		private bool hasThrown;

		private UnitHealthy target;

		private UnitHealthy immediatelyTarget;
	}
}
