using System;

namespace Simulation
{
	public class BuffDataChillDown : BuffData
	{
		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			Unit by = buff.GetBy();
			if (!(by is Hero))
			{
				return;
			}
			Hero hero = (Hero)by;
			hero.CoolWeapon(this.coolRatio);
		}

		public float coolRatio;
	}
}
