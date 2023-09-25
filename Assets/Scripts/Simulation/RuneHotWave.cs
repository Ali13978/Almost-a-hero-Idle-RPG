using System;

namespace Simulation
{
	public class RuneHotWave : Rune
	{
		public RuneHotWave(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataHotWave(RuneHotWave.PERENTAGE, RuneHotWave.RADIUS), RuneIds.FIRE_HOTWAVE, "RUNE_NAME_HOTWAVE", "RUNE_DESC_HOTWAVE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneHotWave.PERENTAGE, false)));
		}

		public static double PERENTAGE = 0.9;

		public static float RADIUS = 0.4f;
	}
}
