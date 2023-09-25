using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffDataMirrorDamageAlly : BuffData
	{
		public override void OnPreTakeDamage(Buff buff, Unit attacker, Damage damage)
		{
			Unit by = buff.GetBy();
			if (GameMath.GetProbabilityOutcome(this.mirrorChance, GameMath.RandType.NoSeed))
			{
				List<UnitHealthy> list = new List<UnitHealthy>();
				foreach (UnitHealthy unitHealthy in by.GetAllies())
				{
					if (unitHealthy != by && unitHealthy.IsAlive())
					{
						list.Add(unitHealthy);
					}
				}
				if (list.Count > 0 && !damage.isMirrored)
				{
					int randomInt = GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed);
					UnitHealthy unitHealthy2 = list[randomInt];
					Damage copy = damage.GetCopy();
					damage.amount *= 1.0 - this.mirrorPercentage;
					damage.isMirrored = true;
					copy.isMirrored = true;
					copy.amount *= this.mirrorPercentage;
					unitHealthy2.TakeDamage(copy, attacker, 0.0);
					if (by.GetId() == "WARLOCK")
					{
						by.world.OnWarlockRedirectDamage();
					}
				}
			}
		}

		public float mirrorChance;

		public double mirrorPercentage;
	}
}
