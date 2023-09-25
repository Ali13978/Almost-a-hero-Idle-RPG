using System;

namespace Simulation
{
	public class BuffDataReviveHalf : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (this.tryNow)
			{
				this.tryNow = false;
				if (GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
				{
					Hero hero = buff.GetBy() as Hero;
					hero.SetTillReviveTime(hero.GetTillReviveTime() * 0.5f);
				}
			}
		}

		public override void OnDeathSelf(Buff buff)
		{
			this.tryNow = true;
		}

		public float chance;

		public bool tryNow;
	}
}
