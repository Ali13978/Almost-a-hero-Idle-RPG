using System;

namespace Simulation
{
	public class PrestigeRunStat
	{
		public string GetMythstoneString()
		{
			if (this.wasMega)
			{
				return "2x" + GameMath.GetDoubleString(this.mythStones);
			}
			return GameMath.GetDoubleString(this.mythStones);
		}

		public double playTime;

		public double mythStones;

		public int stage;

		public bool wasMega;
	}
}
