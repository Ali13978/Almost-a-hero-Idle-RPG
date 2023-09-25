using System;

namespace Simulation
{
	public class RuneAbundance : Rune
	{
		public RuneAbundance(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataAbundance(RuneAbundance.DURATION_CD, RuneAbundance.DURATION_TIMEWARP), RuneIds.EARTH_ABUDANCE, "RUNE_NAME_ABUDANCE", "RUNE_DESC_ABUDANCE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetTimeInSecondsString(RuneAbundance.DURATION_CD)), AM.csr(GameMath.GetTimeInSecondsString(RuneAbundance.DURATION_TIMEWARP)));
		}

		public static float DURATION_CD = 120f;

		public static float DURATION_TIMEWARP = 15f;
	}
}
