using System;

namespace Simulation
{
	public class RuneBlessing : Rune
	{
		public RuneBlessing(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataLunarBlessing(RuneBlessing.MAX_INC), RuneIds.ICE_LUNAR_BLESSING, "RUNE_NAME_BLESSING", "RUNE_DESC_BLESSING", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneBlessing.MAX_INC, false)));
		}

		public static float MAX_INC = 1f;
	}
}
