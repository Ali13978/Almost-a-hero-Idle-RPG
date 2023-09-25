using System;

namespace Simulation
{
	public class RuneStarfall : Rune
	{
		public RuneStarfall(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataStarfall(RuneStarfall.HEAL_AMOUNT, RuneStarfall.DURATION), RuneIds.EARTH_STARFALL, "RUNE_NAME_STARFALL", "RUNE_DESC_STARFALL", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneStarfall.HEAL_AMOUNT, false)), AM.csr(GameMath.GetTimeInSecondsString(RuneStarfall.DURATION)), AM.csr(LM.Get("RING_SPECIAL_EARTH")));
		}

		public static double HEAL_AMOUNT = 0.15;

		public static float DURATION = 3f;
	}
}
