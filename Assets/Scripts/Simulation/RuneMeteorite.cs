using System;

namespace Simulation
{
	public class RuneMeteorite : Rune
	{
		public RuneMeteorite(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataMeteorite(RuneMeteorite.METEORITE_PERIOD, RuneMeteorite.DMG_METEORITES, RuneMeteorite.DURATION_ADD), RuneIds.EARTH_METEORITE, "RUNE_NAME_METEORITE", "RUNE_DESC_METEORITE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csr(GameMath.GetTimeInMilliSecondsString(RuneMeteorite.METEORITE_PERIOD)),
				AM.csr(GameMath.GetPercentString(RuneMeteorite.DMG_METEORITES, false)),
				AM.csr(GameMath.GetTimeInSecondsString(RuneMeteorite.DURATION_ADD)),
				AM.csr(LM.Get("RING_SPECIAL_EARTH"))
			});
		}

		public static float METEORITE_PERIOD = 0.7f;

		public static double DMG_METEORITES = 0.9;

		public static float DURATION_ADD = 3f;
	}
}
