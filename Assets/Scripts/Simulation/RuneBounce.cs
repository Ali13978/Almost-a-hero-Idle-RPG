using System;

namespace Simulation
{
	public class RuneBounce : Rune
	{
		public RuneBounce(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataBounce(RuneBounce.SECONDARY_DAMAGE), RuneIds.LIGHTNING_BOUNCE, "RUNE_NAME_BOUNCE", "RUNE_DESC_BOUNCE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneBounce.SECONDARY_DAMAGE, false)));
		}

		public static double SECONDARY_DAMAGE = 0.5;
	}
}
