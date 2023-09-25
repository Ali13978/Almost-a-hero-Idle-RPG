using System;

namespace Simulation
{
	public class BuffDataSonOfForest : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (buff.GetWorld().IsEnvironmentForest())
			{
				totEffect.damageAddFactor += this.damageAdd;
			}
		}

		public double damageAdd;
	}
}
