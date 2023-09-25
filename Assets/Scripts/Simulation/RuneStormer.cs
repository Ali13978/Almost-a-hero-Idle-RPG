using System;

namespace Simulation
{
	public class RuneStormer : Rune
	{
		public RuneStormer(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataStormer(RuneStormer.CRIT_CHANCE), RuneIds.ICE_STORMER, "RUNE_NAME_STORMER", "RUNE_DESC_STORMER", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneStormer.CRIT_CHANCE, false)));
		}

		public static float CRIT_CHANCE = 0.12f;
	}
}
