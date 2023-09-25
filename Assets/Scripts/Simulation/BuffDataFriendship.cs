using System;

namespace Simulation
{
	public class BuffDataFriendship : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			double num = this.damageAdd * (double)buff.GetWorld().GetNumHeroesAliveExcluding(buff.GetBy());
			totEffect.damageAddFactor += num;
		}

		public double damageAdd;
	}
}
