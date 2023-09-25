using System;

namespace Simulation
{
	public class BuffDataTasteOfRevenge : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			this.genericTimer = GameMath.GetMaxFloat(this.genericTimer - dt, 0f);
			if (this.genericTimer > 0f)
			{
				totEffect.damageAddFactor += (double)this.damageIncrease;
				this.visuals |= 8;
			}
			else
			{
				this.visuals = 0;
			}
		}

		public override void OnDeathAlly(Buff buff, UnitHealthy dead)
		{
			this.genericTimer = GameMath.GetMinFloat(this.timeMax, this.genericTimer + this.addTime);
		}

		public float addTime;

		public float damageIncrease;

		public float timeMax;
	}
}
