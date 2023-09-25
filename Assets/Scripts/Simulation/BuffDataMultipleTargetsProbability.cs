using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffDataMultipleTargetsProbability : BuffData
	{
		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (damage.type == DamageType.SKILL || !GameMath.GetProbabilityOutcome(this.firstProbability, GameMath.RandType.NoSeed))
			{
				return;
			}
			int num = this.firstEnemiesAmount;
			if (GameMath.GetProbabilityOutcome(this.secondProbability, GameMath.RandType.NoSeed))
			{
				num = this.secondEnemiesAmount;
			}
			List<UnitHealthy> list = new List<UnitHealthy>();
			foreach (UnitHealthy unitHealthy in target.GetAllies())
			{
				if (unitHealthy != target)
				{
					list.Add(unitHealthy);
				}
			}
			while (num > 0 && list.Count > 0)
			{
				Damage copy = damage.GetCopy();
				copy.type = DamageType.SKILL;
				UnitHealthy randomListElement = list.GetRandomListElement<UnitHealthy>();
				list.Remove(randomListElement);
				buff.GetWorld().DamageMain(buff.GetBy(), randomListElement, copy);
			}
		}

		public float firstProbability;

		public int firstEnemiesAmount;

		public float secondProbability;

		public int secondEnemiesAmount;
	}
}
