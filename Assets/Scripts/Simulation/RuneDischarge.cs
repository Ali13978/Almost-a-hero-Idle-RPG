using System;

namespace Simulation
{
	public class RuneDischarge : Rune
	{
		public RuneDischarge(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataDischarge(RuneDischarge.CHANCE), RuneIds.LIGHTNING_DISCHARGE, "RUNE_NAME_DISCHARGE", "RUNE_DESC_DISCHARGE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneDischarge.CHANCE, false)), AM.csr(LM.Get("RING_SPECIAL_LIGHTNING")));
		}

		public static float CHANCE = 0.25f;
	}
}
