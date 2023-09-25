using System;

namespace Simulation
{
	public class RuneIgnition : Rune
	{
		public RuneIgnition(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataIgnition(RuneIgnition.DAMAGE_INC, RuneIgnition.HEAT_INC), RuneIds.FIRE_IGNITION, "RUNE_NAME_IGNITION", "RUNE_DESC_IGNITION", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneIgnition.DAMAGE_INC, false)), AM.csr(GameMath.GetPercentString(RuneIgnition.HEAT_INC, false)));
		}

		public static float DAMAGE_INC = 2f;

		public static float HEAT_INC = 0.35f;
	}
}
