using System;

namespace Simulation
{
	public class BuffDataHealthMaxAllTE : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.heroHealthMaxFactorTE += this.healthMaxFactorAdd;
		}

		public double healthMaxFactorAdd;
	}
}
