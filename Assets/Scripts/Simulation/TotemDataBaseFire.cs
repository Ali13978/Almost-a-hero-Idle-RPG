using System;

namespace Simulation
{
	public class TotemDataBaseFire : TotemDataBase
	{
		public TotemDataBaseFire()
		{
			this.id = "totemFire";
			this.nameKey = "RING_NAME_FIRE";
			this.descKey = this.GetDesc();
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RING_DESC_FIRE"), LM.Get("RING_SPECIAL_FIRE"));
		}

		public float GetHeatMax()
		{
			return 50f;
		}

		public float GetHeatPerFire()
		{
			return 1f;
		}

		public float GetCoolSpeed()
		{
			return 5f;
		}

		public float GetOverCoolSpeed()
		{
			return 5f;
		}

		public const float DEFAULT_HEAT_MAX = 50f;
	}
}
