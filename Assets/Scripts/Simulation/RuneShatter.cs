using System;

namespace Simulation
{
	public class RuneShatter : Rune
	{
		public RuneShatter(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataShatter(RuneShatter.DAMAGE_DEC, RuneShatter.MANA_REQ), RuneIds.ICE_SHATTER, "RUNE_NAME_SHATTER", "RUNE_DESC_SHATTER", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneShatter.MANA_REQ, false)), AM.csr(GameMath.GetPercentString(RuneShatter.DAMAGE_DEC, false)));
		}

		public static float DAMAGE_DEC = 0.25f;

		public static float MANA_REQ = 0.5f;
	}
}
