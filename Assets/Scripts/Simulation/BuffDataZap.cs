using System;

namespace Simulation
{
	public class BuffDataZap : BuffData
	{
		public BuffDataZap(double goldFactor)
		{
			this.goldFactor = goldFactor;
			this.id = 196;
		}

		public override void OnPreThunderbolt(Buff buff, UnitHealthy target, Damage damage, bool isSecondary)
		{
			target.AddBuff(new BuffDataDropGold
			{
				id = 69,
				dur = float.PositiveInfinity,
				isStackable = false,
				dropGoldFactorAdd = this.goldFactor
			}, 0, false);
			target.UpdateStats(0f);
		}

		public double goldFactor;
	}
}
