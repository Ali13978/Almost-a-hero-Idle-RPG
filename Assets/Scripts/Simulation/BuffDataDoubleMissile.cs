using System;

namespace Simulation
{
	public class BuffDataDoubleMissile : BuffData
	{
		public override void OnPreProjectile(Buff buff, Projectile projectile)
		{
			if (projectile.type == Projectile.Type.DEREK_BOOK)
			{
				return;
			}
			if (!GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				return;
			}
			Projectile copy = projectile.GetCopy();
			projectile.ReTarget(buff.GetWorld().GetRandomAliveEnemy());
			buff.GetWorld().AddProjectile(copy);
		}

		public float chance;
	}
}
