using System;

namespace Simulation
{
	public class RuneMagma : Rune
	{
		public RuneMagma(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataMagma(RuneMagma.CRIT_FACTOR_ADD), RuneIds.FIRE_MAGMA, "RUNE_NAME_MAGMA", "RUNE_DESC_MAGMA", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneMagma.CRIT_FACTOR_ADD, false)));
		}

		public static double CRIT_FACTOR_ADD = 2.0;
	}
}
