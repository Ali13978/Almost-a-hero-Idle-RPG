using System;

namespace Simulation
{
	public class BuffDataHealOnKill : BuffData
	{
		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			Unit by = buff.GetBy();
			if (!(by is UnitHealthy))
			{
				return;
			}
			UnitHealthy unitHealthy = (UnitHealthy)by;
			unitHealthy.Heal(this.healRatio);
			unitHealthy.world.OnLiaStealHealth();
		}

		public double healRatio;
	}
}
