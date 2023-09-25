using System;

namespace Simulation
{
	public class RuneZap : Rune
	{
		public RuneZap(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataZap(RuneZap.GOLD_FACTOR_ADD), RuneIds.LIGHTNING_ZAP, "RUNE_NAME_ZAP", "RUNE_DESC_ZAP", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_LIGHTNING")), AM.csr(GameMath.GetPercentString(RuneZap.GOLD_FACTOR_ADD, false)));
		}

		public static double GOLD_FACTOR_ADD = 0.5;
	}
}
