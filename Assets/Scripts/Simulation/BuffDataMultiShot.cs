using System;

namespace Simulation
{
	public class BuffDataMultiShot : BuffData
	{
		public override void OnPreProjectile(Buff buff, Projectile projectile)
		{
			if (GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				Unit by = buff.GetBy();
				foreach (UnitHealthy unitHealthy in by.GetOpponents())
				{
					if (unitHealthy != projectile.target)
					{
						Projectile copy = projectile.GetCopy();
						copy.target = unitHealthy;
						copy.InitPath();
						buff.GetWorld().projectiles.Add(copy);
					}
				}
			}
		}

		public float chance;
	}
}
