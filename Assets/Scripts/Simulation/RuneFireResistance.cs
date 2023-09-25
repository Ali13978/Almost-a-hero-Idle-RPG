using System;

namespace Simulation
{
	public class RuneFireResistance : Rune
	{
		public RuneFireResistance(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataFireResistance(RuneFireResistance.HEAT_MAX_ADD), RuneIds.FIRE_FIRE_RESISTANCE, "RUNE_NAME_FIRE_RESISTANCE", "RUNE_DESC_FIRE_RESISTANCE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneFireResistance.HEAT_MAX_ADD / 50f, false)));
		}

		public static float HEAT_MAX_ADD = 50f;
	}
}
