using System;

namespace Simulation
{
	public class RuneOverload : Rune
	{
		public RuneOverload(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataOverload(RuneOverload.DAMAGE_BONUS), RuneIds.LIGHTNING_OVERLOAD, "RUNE_NAME_OVERLOAD", "RUNE_DESC_OVERLOAD", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_LIGHTNING")), AM.csr(GameMath.GetPercentString(RuneOverload.DAMAGE_BONUS, false)));
		}

		public static double DAMAGE_BONUS = 1.0;
	}
}
