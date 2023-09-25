using System;

namespace Simulation
{
	public class BuffDataHealthMaxSelfAndAllies : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.heroHealthMaxFactor += this.healthMaxAddAllies;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.healthMaxFactor += this.healthMaxAddSelf - this.healthMaxAddAllies;
		}

		public double healthMaxAddSelf;

		public double healthMaxAddAllies;
	}
}
