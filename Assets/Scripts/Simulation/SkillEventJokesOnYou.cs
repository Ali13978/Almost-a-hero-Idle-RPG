using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class SkillEventJokesOnYou : SkillEvent
	{
		public override void Apply(Unit by)
		{
			List<Hero> numberOfRandomAliveHeroExcluding = by.world.GetNumberOfRandomAliveHeroExcluding(by, 2);
			foreach (Hero hero in numberOfRandomAliveHeroExcluding)
			{
				if (hero == null)
				{
					break;
				}
				double amount = hero.GetHealthMax() * this.damageInPer;
				Damage damage = new Damage(amount, false, false, false, false);
				damage.type = this.damageType;
				damage.ignoreReduction = true;
				damage.ignoreShield = false;
				damage.isExact = true;
				by.AddProjectile(new Projectile(by, this.projectileType, this.targetType, hero, this.durFly, this.path, this.targetPosition)
				{
					targetLocked = this.targetLocked,
					damage = damage,
					buffs = this.buffs,
					visualEffect = this.visualEffect,
					damageMomentTimeRatio = 1f
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

		public bool targetLocked;

		public DamageType damageType;

		public Vector3 targetPosition;

		public List<BuffData> buffs;

		public double damageInPer;

		public VisualEffect visualEffect;
	}
}
