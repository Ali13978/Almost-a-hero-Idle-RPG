using System;

namespace Simulation
{
	public class RuneSmash : Rune
	{
		public RuneSmash(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataSmashEarth(RuneSmash.DMG_RATIO, RuneSmash.DURATION_ADD), RuneIds.EARTH_SMASH, "RUNE_NAME_SMASH", "RUNE_DESC_SMASH", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneSmash.DMG_RATIO, false)), AM.csr(GameMath.GetTimeInSecondsString(RuneSmash.DURATION_ADD)));
		}

		public static double DMG_RATIO = 0.03;

		public static float DURATION_ADD = 3f;
	}
}
