using System;

namespace Simulation
{
	public class BuffDataManOfHill : BuffData
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
			enemy.AddBuff(this.effect, 0, false);
			enemy.UpdateStats(0f);
		}

		public BuffDataDropGold effect;
	}
}
