using System;

namespace Simulation
{
	public class RuneSharpness : Rune
	{
		public RuneSharpness(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataSharpness(RuneSharpness.DAMAGE_BONUS, RuneSharpness.CAP), RuneIds.ICE_SHARPNESS, "RUNE_NAME_SHARPNESS", "RUNE_DESC_SHARPNESS", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_ICE")), AM.csr(GameMath.GetPercentString(RuneSharpness.DAMAGE_BONUS, false)));
		}

		public static double DAMAGE_BONUS = 0.2;

		public static double CAP = 6.0;
	}
}
