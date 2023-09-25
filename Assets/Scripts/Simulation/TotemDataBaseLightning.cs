using System;

namespace Simulation
{
	public class TotemDataBaseLightning : TotemDataBase
	{
		public TotemDataBaseLightning()
		{
			this.id = "totemLightning";
			this.nameKey = "RING_NAME_LIGHTNING";
			this.descKey = this.GetDesc();
			this.critChance = 0.03f;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RING_DESC_LIGHTNING"), LM.Get("RING_SPECIAL_LIGHTNING"));
		}

		public int GetChargeReq()
		{
			return 40;
		}
	}
}
