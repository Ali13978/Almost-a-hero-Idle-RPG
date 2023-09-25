using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffDataDividedWeFall : BuffData
	{
		public override void OnDeathAlly(Buff buff, UnitHealthy dead)
		{
			List<UnitHealthy> list = new List<UnitHealthy>();
			foreach (UnitHealthy unitHealthy in dead.GetAllies())
			{
				if (unitHealthy.IsAlive())
				{
					list.Add(unitHealthy);
				}
			}
			int count = list.Count;
			if (count == 0)
			{
				return;
			}
			int randomInt = GameMath.GetRandomInt(0, count, GameMath.RandType.NoSeed);
			UnitHealthy unitHealthy2 = list[randomInt];
			unitHealthy2.GainShield(this.shieldRatio, this.shieldDur);
			unitHealthy2.AddVisualBuff(3f, 256);
		}

		public double shieldRatio;

		public float shieldDur;
	}
}
