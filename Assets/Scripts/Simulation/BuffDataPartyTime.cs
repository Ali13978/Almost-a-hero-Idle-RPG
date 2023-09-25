using System;

namespace Simulation
{
	public class BuffDataPartyTime : BuffData
	{
		public override void OnOpponentDeath(Buff buff, UnitHealthy dead)
		{
			if (!(dead is Enemy))
			{
				return;
			}
			Enemy enemy = (Enemy)dead;
			if (enemy.IsBoss())
			{
				foreach (Unit unit in enemy.GetOpponents())
				{
					unit.AddBuff(this.effect, 0, false);
				}
			}
		}

		public BuffDataDamageAdd effect;
	}
}
