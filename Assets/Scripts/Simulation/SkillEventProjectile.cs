using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class SkillEventProjectile : SkillEvent
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
				damage.ignoreReduction = this.ignoresReduction;
				damage.ignoreShield = this.ignoresShield;
				if (this.canCrit && GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed))
				{
					damage.isCrit = true;
					damage.amount *= by.GetCritFactor();
				}
				if (this.damageType == DamageType.SKILL)
				{
					World world = by.world;
					damage.amount *= world.universalBonus.damageHeroSkillFactor / world.universalBonus.damageHeroNonSkillFactor;
				}
			}
			by.OnEventProjectile(this.projectileType, this.targetType, this.durFly, this.path, damage, this.buffs, this.visualEffect, this.targetLocked, this.targetPosition, 1f);
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
		}

		public Projectile.Type projectileType;

		public Projectile.TargetType targetType;

		public float durFly;

		public ProjectilePath path;

		public bool canCrit;

		public bool targetLocked;

		public DamageType damageType;

		public bool ignoresShield;

		public bool ignoresReduction;

		public Vector3 targetPosition;

		public List<BuffData> buffs;

		public double damageInDps;

		public double damageInTeamDps;

		public VisualEffect visualEffect;
	}
}
