using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class BuffEventProjectile : BuffEvent
	{
		public override void Apply(Unit by, World world)
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
			bool isCrit = false;
			if (this.canCrit && GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed))
			{
				num *= by.GetCritFactor();
				isCrit = true;
			}
			Damage damage = new Damage(num, false, false, false, false);
			damage.isCrit = isCrit;
			damage.type = this.damageType;
			by.OnEventProjectile(this.projectileType, this.targetType, this.durFly, this.path, damage, this.buffs, this.visualEffect, false, Vector3.zero, this.damageTimeRatio);
		}

		public Projectile.Type projectileType;

		public Projectile.TargetType targetType;

		public float durFly;

		public float damageTimeRatio = 1f;

		public ProjectilePath path;

		public List<BuffData> buffs;

		public double damageInDps;

		public double damageInTeamDps;

		public bool canCrit;

		public VisualEffect visualEffect;

		public DamageType damageType;
	}
}
