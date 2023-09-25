using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillEventDrainProjectile : SkillEvent
	{
		public override void Apply(Unit by)
		{
			double num = 0.0;
			if (this.damageInDps > 0.0)
			{
				num += this.damageInDps * by.GetDps();
			}
			if (this.damageInTeamDps > 0.0)
			{
				num += this.damageInTeamDps * by.GetDpsTeam();
			}
			Damage damage = null;
			if (num > 0.0)
			{
				damage = new Damage(num, false, false, false, false);
				damage.type = this.damageType;
				if (this.canCrit && GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed))
				{
					damage.isCrit = true;
					damage.amount *= by.GetCritFactor();
				}
			}
			if (this.damageType == DamageType.SKILL)
			{
				World world = by.world;
				damage.amount *= world.universalBonus.damageHeroSkillFactor / world.universalBonus.damageHeroNonSkillFactor;
			}
			UnitHealthy unitHealthy;
			if (this.targetType == Projectile.TargetType.ALL_ENEMIES)
			{
				unitHealthy = null;
			}
			else if (this.targetType == Projectile.TargetType.SINGLE_ENEMY)
			{
				unitHealthy = by.world.GetRandomAliveEnemy();
			}
			else if (this.targetType == Projectile.TargetType.SINGLE_ALLY_ANY)
			{
				unitHealthy = by.world.GetRandomAliveHeroExcluding(by);
				if (unitHealthy == null)
				{
					return;
				}
			}
			else if (this.targetType == Projectile.TargetType.SINGLE_ALLY_ANY_SELF)
			{
				unitHealthy = by.world.GetRandomAliveHeroExcluding(by);
				if (unitHealthy == null)
				{
					unitHealthy = (by as UnitHealthy);
				}
				if (unitHealthy == null)
				{
					return;
				}
			}
			else
			{
				if (this.targetType != Projectile.TargetType.SINGLE_ALLY_MIN_HEALTH)
				{
					throw new NotImplementedException();
				}
				unitHealthy = by.world.GetAliveHeroWithMinHealthExcluding(by);
				if (unitHealthy == null)
				{
					return;
				}
			}
			if (unitHealthy != null)
			{
				unitHealthy.AddProjectile(new Projectile(unitHealthy, this.projectileType, Projectile.TargetType.SINGLE_ALLY_ANY, by as UnitHealthy, this.durFly, this.path)
				{
					damage = damage,
					buffs = this.buffs,
					visualEffect = this.visualEffect
				});
			}
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
		}

		public Projectile.Type projectileType;

		public Projectile.TargetType targetType;

		public float durFly;

		public ProjectilePath path;

		public bool canCrit;

		public DamageType damageType;

		public List<BuffData> buffs;

		public double damageInDps;

		public double damageInTeamDps;

		public VisualEffect visualEffect;
	}
}
