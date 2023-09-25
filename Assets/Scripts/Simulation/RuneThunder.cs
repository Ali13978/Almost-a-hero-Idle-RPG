using System;

namespace Simulation
{
	public class RuneThunder : Rune
	{
		public RuneThunder(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataThunder(RuneThunder.DAMAGE_BONUS_ADD, RuneThunder.CHARGE_ADD), RuneIds.LIGHTNING_THUNDER, "RUNE_NAME_THUNDER", "RUNE_DESC_THUNDER", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(LM.Get("RING_SPECIAL_LIGHTNING")), AM.csr(GameMath.GetPercentString(RuneThunder.DAMAGE_BONUS_ADD, false)), AM.csr(RuneThunder.CHARGE_ADD.ToString()));
		}

		public static double DAMAGE_BONUS_ADD = 2.0;

		public static int CHARGE_ADD = 10;
	}
}
