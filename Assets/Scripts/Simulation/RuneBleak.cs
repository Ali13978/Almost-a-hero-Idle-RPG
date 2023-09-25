using System;

namespace Simulation
{
	public class RuneBleak : Rune
	{
		public RuneBleak(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataBleak(RuneBleak.SPEED_FACTOR), RuneIds.ICE_BLEAK, "RUNE_NAME_BLEAK", "RUNE_DESC_BLEAK", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_ICE")), AM.csr(GameMath.GetPercentString(RuneBleak.PERCENTAGE, false)));
		}

		public static float SPEED_FACTOR = 0.6666667f;

		public static float PERCENTAGE = 1f / (1f - RuneBleak.SPEED_FACTOR) - 1f;
	}
}
