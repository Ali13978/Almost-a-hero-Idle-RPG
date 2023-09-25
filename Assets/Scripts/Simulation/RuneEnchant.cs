using System;

namespace Simulation
{
	public class RuneEnchant : Rune
	{
		public RuneEnchant(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataEnchantEarth(RuneEnchant.DURATION_RECHARGE, RuneEnchant.NEXT_ATTACK_MULT), RuneIds.EARTH_ENCHANT, "RUNE_NAME_ENCHANT", "RUNE_DESC_ENCHANT", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneEnchant.NEXT_ATTACK_MULT, false)), AM.csr(GameMath.GetTimeInSecondsString(RuneEnchant.DURATION_RECHARGE)));
		}

		public static float DURATION_RECHARGE = 3f;

		public static double NEXT_ATTACK_MULT = 1.0;
	}
}
