using System;

namespace Simulation
{
	public class RuneInnerFire : Rune
	{
		public RuneInnerFire(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataInnerFire(RuneInnerFire.DAMAGE_ADD, RuneInnerFire.DUR), RuneIds.FIRE_INNER_FIRE, "RUNE_NAME_INNER_FIRE", "RUNE_DESC_INNER_FIRE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneInnerFire.DAMAGE_ADD, false)), AM.csr(GameMath.GetTimeInSecondsString(RuneInnerFire.DUR)), AM.csr(LM.Get("RING_SPECIAL_FIRE")));
		}

		public static double DAMAGE_ADD = 1.0;

		public static float DUR = 5f;
	}
}
