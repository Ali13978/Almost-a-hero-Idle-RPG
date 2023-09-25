using System;

namespace Simulation
{
	public class RuneLuminosity : Rune
	{
		public RuneLuminosity(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataLuminosity((double)RuneLuminosity.REDUCTION, (double)RuneLuminosity.CAP), RuneIds.FIRE_LUMINOSITY, "RUNE_NAME_LUMINOSITY", "RUNE_DESC_LUMINOSITY", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneLuminosity.REDUCTION, false)), AM.csr(GameMath.GetPercentString(RuneLuminosity.CAP, false)));
		}

		public static float REDUCTION = 0.05f;

		public static float CAP = 0.75f;
	}
}
