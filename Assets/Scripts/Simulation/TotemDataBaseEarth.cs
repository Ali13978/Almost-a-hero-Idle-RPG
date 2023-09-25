using System;

namespace Simulation
{
	public class TotemDataBaseEarth : TotemDataBase
	{
		public TotemDataBaseEarth()
		{
			this.id = "totemEarth";
			this.nameKey = "RING_NAME_EARTH";
			this.descKey = this.GetDesc();
			this.projectile = new Projectile();
			this.projectile.durFly = 0.5f;
			this.projectile.type = Projectile.Type.TOTEM_EARTH;
			this.projectile.targetType = Projectile.TargetType.ALL_ENEMIES;
			ProjectilePathFromAbove projectilePathFromAbove = new ProjectilePathFromAbove();
			projectilePathFromAbove.speedVertical = 3.5f;
			this.projectile.path = projectilePathFromAbove;
			this.projectile.damageMomentTimeRatio = 1f;
			this.projectileMeteorite = new Projectile();
			this.projectileMeteorite.durFly = 0.5f;
			this.projectileMeteorite.type = Projectile.Type.TOTEM_EARTH_SMALL;
			this.projectileMeteorite.targetType = Projectile.TargetType.SINGLE_ENEMY;
			ProjectilePathFromAbove projectilePathFromAbove2 = new ProjectilePathFromAbove();
			projectilePathFromAbove2.speedVertical = 2.5f;
			this.projectileMeteorite.path = projectilePathFromAbove2;
			this.projectileMeteorite.damageMomentTimeRatio = 1f;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RING_DESC_EARTH"), LM.Get("RING_SPECIAL_EARTH"));
		}

		public float GetTimeChargedMax()
		{
			return 5f;
		}

		public float GetMeteorAreaR()
		{
			return 0.7f;
		}

		public float GetMeteoriteAreaR()
		{
			return 0.1f;
		}

		public int GetMaxNumMeteors()
		{
			return 5;
		}

		public Projectile projectile;

		public Projectile projectileMeteorite;
	}
}
