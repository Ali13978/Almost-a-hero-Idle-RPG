using System;

namespace Simulation
{
	public class RuneEnchantedFire : Rune
	{
		public RuneEnchantedFire(TotemDataBase belongsTo)
		{
			this.Initialize(belongsTo, new BuffDataEnchantedFire(RuneEnchantedFire.REDUCTION), RuneIds.FIRE_ENCHANTED_FIRE, "RUNE_NAME_ENCHANTED_FIRE", "RUNE_DESC_ENCHANTED_FIRE", 1f);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), AM.csr(GameMath.GetPercentString(RuneEnchantedFire.REDUCTION, false)));
		}

		public static float REDUCTION = 0.25f;
	}
}
