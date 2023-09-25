using System;

namespace Simulation
{
	public class RuneIceRage : Rune
	{
		public RuneIceRage(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataIceRage(RuneIceRage.MANA_INC), RuneIds.ICE_ICE_RAGE, "RUNE_NAME_ICE_RAGE", "RUNE_DESC_ICE_RAGE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneIceRage.MANA_INC, false)));
		}

		public static float MANA_INC = 0.2f;
	}
}
