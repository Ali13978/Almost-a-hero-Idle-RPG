using System;

namespace Simulation
{
	public class RuneBlaze : Rune
	{
		public RuneBlaze(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataBlaze(RuneBlaze.REDUCTION, RuneBlaze.DURATION), RuneIds.FIRE_BLAZE, "RUNE_NAME_BLAZE", "RUNE_DESC_BLAZE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneBlaze.REDUCTION, false)), AM.csr(GameMath.GetTimeInSecondsString(RuneBlaze.DURATION)));
		}

		public static double REDUCTION = 0.75;

		public static float DURATION = 3f;
	}
}
