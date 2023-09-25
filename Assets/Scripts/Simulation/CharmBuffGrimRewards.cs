using System;

namespace Simulation
{
	public class CharmBuffGrimRewards : CharmBuff
	{
		protected override bool TryActivating()
		{
			foreach (Hero hero in this.world.heroes)
			{
				hero.costMultiplier *= 1.0 - GameMath.GetRandomDouble(this.costReductionMin, this.costReductionMax, GameMath.RandType.NoSeed);
			}
			return true;
		}

		public override void OnDeathAny(Unit unit)
		{
			if (unit is Hero)
			{
				this.AddProgress(this.pic);
			}
		}

		public double costReductionMin;

		public double costReductionMax;
	}
}
