using System;

namespace Simulation
{
	public class RuneCooler : Rune
	{
		public RuneCooler(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataCooler(RuneCooler.COOL_FACTOR, RuneCooler.OVER_COOL_FACTOR), RuneIds.FIRE_COOLER, "RUNE_NAME_COOLER", "RUNE_DESC_COOLER", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneCooler.OVER_COOL_FACTOR, false)), AM.csr(GameMath.GetPercentString(RuneCooler.COOL_FACTOR, false)), AM.csr(LM.Get("RING_SPECIAL_FIRE")));
		}

		public static float COOL_FACTOR = 0.3f;

		public static float OVER_COOL_FACTOR = 0.75f;
	}
}
