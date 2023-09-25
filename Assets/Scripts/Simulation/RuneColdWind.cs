using System;

namespace Simulation
{
	public class RuneColdWind : Rune
	{
		public RuneColdWind(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataColdWind(RuneColdWind.DURATION, RuneColdWind.ATTACK_SPEED), RuneIds.ICE_COLD_WIND, "RUNE_NAME_COLD_WIND", "RUNE_DESC_COLD_WIND", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneColdWind.ATTACK_SPEED, false)), AM.csr(GameMath.GetTimeLessDetailedString((double)RuneColdWind.DURATION, false)));
		}

		public static float DURATION = 3f;

		public static float ATTACK_SPEED = 0.5f;
	}
}
