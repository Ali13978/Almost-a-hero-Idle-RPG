using System;

namespace Simulation
{
	public class RuneShock : Rune
	{
		public RuneShock(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataShock(RuneShock.PROBABILITY, RuneShock.DURATION), RuneIds.LIGHTNING_SHOCK, "RUNE_NAME_SHOCK", "RUNE_DESC_SHOCK", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_LIGHTNING")), AM.csr(GameMath.GetPercentString(RuneShock.PROBABILITY, false)), AM.csr(GameMath.GetTimeLessDetailedString((double)RuneShock.DURATION, false)));
		}

		public static float PROBABILITY = 0.3f;

		public static float DURATION = 2f;
	}
}
