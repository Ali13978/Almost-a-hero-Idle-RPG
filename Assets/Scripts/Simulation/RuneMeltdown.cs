using System;

namespace Simulation
{
	public class RuneMeltdown : Rune
	{
		public RuneMeltdown(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataMeltdown(RuneMeltdown.DAMAGE_FACTOR, RuneMeltdown.CAP), RuneIds.FIRE_MELTDOWN, "RUNE_NAME_MELTDOWN", "RUNE_DESC_MELTDOWN", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneMeltdown.DAMAGE_FACTOR, false)), AM.csr(GameMath.GetPercentString(RuneMeltdown.CAP, false)));
		}

		public static double DAMAGE_FACTOR = 0.15;

		public static double CAP = 3.0;
	}
}
