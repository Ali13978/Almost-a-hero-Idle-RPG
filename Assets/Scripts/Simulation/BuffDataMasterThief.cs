using System;

namespace Simulation
{
	public class BuffDataMasterThief : BuffData
	{
		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			Enemy enemy = target as Enemy;
			if (enemy == null)
			{
				return;
			}
			if (enemy.data.dataBase.type == EnemyDataBase.Type.CHEST)
			{
				return;
			}
			enemy.AddBuff(this.lootIncBuff, 0, false);
			target.UpdateStats(0f);
		}

		public BuffDataDropGold lootIncBuff;
	}
}
