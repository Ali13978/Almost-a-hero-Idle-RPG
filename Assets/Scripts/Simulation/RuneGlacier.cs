using System;

namespace Simulation
{
	public class RuneGlacier : Rune
	{
		public RuneGlacier(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataGlacier(RuneGlacier.STUN_DAMAGE_FACTOR, RuneGlacier.NORMAL_DAMAGE_FACTOR), RuneIds.ICE_GLACIER, "RUNE_NAME_GLACIER", "RUNE_DESC_GLACIER", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneGlacier.STUN_DAMAGE_FACTOR, false)), AM.csr(GameMath.GetPercentString(RuneGlacier.NORMAL_DAMAGE_FACTOR, false)));
		}

		public static double STUN_DAMAGE_FACTOR = 4.0;

		public static double NORMAL_DAMAGE_FACTOR = -0.5;
	}
}
