using System;

namespace Simulation
{
	public class BuffDataRepel : BuffData
	{
		public override void OnShieldBreakSelf(Buff buff)
		{
			this.AddBuff(buff.GetBy() as Hero);
		}

		public override void OnShieldBreakAlly(Buff buff, UnitHealthy ally)
		{
			this.AddBuff(ally as Hero);
		}

		private void AddBuff(Hero hero)
		{
			if (hero == null)
			{
				return;
			}
			hero.AddBuff(this.damageBuff, 0, false);
		}

		public BuffDataDamageMul damageBuff;
	}
}
