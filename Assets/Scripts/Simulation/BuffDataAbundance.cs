using System;

namespace Simulation
{
	public class BuffDataAbundance : BuffData
	{
		public BuffDataAbundance(float cooldownDuration, float timeWarpDuration)
		{
			this.cdDur = cooldownDuration;
			this.twDur = timeWarpDuration;
			this.id = 290;
			this.timer = cooldownDuration;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			this.timer -= buff.GetWorld().dtUnwarp;
			if (this.timer <= 0f)
			{
				World world = buff.GetWorld();
				world.StartTimeWarp(this.twDur, 2.5f * world.universalBonus.timeWarpSpeedFactor);
				this.timer = this.cdDur;
			}
		}

		private float cdDur;

		private float twDur;

		private float timer;
	}
}
