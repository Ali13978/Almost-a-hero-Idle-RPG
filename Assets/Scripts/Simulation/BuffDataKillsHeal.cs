using System;

namespace Simulation
{
	public class BuffDataKillsHeal : BuffData
	{
		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			BuffDataHealthRegen buffDataHealthRegen = new BuffDataHealthRegen();
			buffDataHealthRegen.id = 100;
			buffDataHealthRegen.healthRegenAdd = this.healRatio / 4.0;
			buffDataHealthRegen.dur = 4f;
			buff.GetBy().AddBuff(buffDataHealthRegen, 0, false);
		}

		public double healRatio;

		public const float DUR = 4f;
	}
}
