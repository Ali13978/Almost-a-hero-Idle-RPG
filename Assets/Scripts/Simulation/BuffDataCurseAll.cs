using System;

namespace Simulation
{
	public class BuffDataCurseAll : BuffData
	{
		public override void OnNewEnemies(Buff buff)
		{
			float timeRemaining = buff.GetTimeRemaining();
			BuffDataDefense buffDataDefense = new BuffDataDefense();
			buffDataDefense.id = 58;
			buffDataDefense.dur = timeRemaining;
			buffDataDefense.damageTakenFactor = this.damageTakenFactor;
			foreach (Enemy enemy in buff.GetWorld().activeChallenge.enemies)
			{
				enemy.AddBuff(buffDataDefense, 0, false);
			}
		}

		public double damageTakenFactor;
	}
}
