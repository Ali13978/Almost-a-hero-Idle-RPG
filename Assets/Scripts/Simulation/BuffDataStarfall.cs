using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffDataStarfall : BuffData
	{
		public BuffDataStarfall(double healAmount, float duration)
		{
			this.healAmount = healAmount;
			this.duration = duration;
			this.id = 165;
		}

		public override void OnMeteorShower(Buff buff)
		{
			BuffDataHealthRegen buffDataHealthRegen = new BuffDataHealthRegen();
			buffDataHealthRegen.id = 96;
			buffDataHealthRegen.isStackable = true;
			buffDataHealthRegen.dur = this.duration;
			buffDataHealthRegen.healthRegenAdd = this.healAmount / (double)this.duration;
			buffDataHealthRegen.visuals |= 64;
			Hero aliveHeroWithMinHealthExcluding = buff.GetWorld().GetAliveHeroWithMinHealthExcluding(null);
			if (aliveHeroWithMinHealthExcluding != null)
			{
				this.projectile = new Projectile();
				this.projectile.durFly = 0.5f;
				this.projectile.type = Projectile.Type.TOTEM_EARTH_STAR;
				this.projectile.targetType = Projectile.TargetType.SINGLE_ALLY_MIN_HEALTH;
				ProjectilePathFromAbove projectilePathFromAbove = new ProjectilePathFromAbove();
				projectilePathFromAbove.speedVertical = 1.5f;
				this.projectile.path = projectilePathFromAbove;
				this.projectile.buffs = new List<BuffData>();
				this.projectile.buffs.Add(buffDataHealthRegen);
				this.projectile.damageMomentTimeRatio = 1f;
				this.projectile.target = aliveHeroWithMinHealthExcluding;
				this.projectile.InitPath(aliveHeroWithMinHealthExcluding.pos, aliveHeroWithMinHealthExcluding.pos);
				this.projectile.visualEffect = new VisualEffect(VisualEffect.Type.TOTEM_EARTH_STAR_IMPACT, 0.567f);
				buff.GetBy().AddProjectile(this.projectile);
			}
		}

		private Projectile projectile;

		private double healAmount;

		private float duration;
	}
}
