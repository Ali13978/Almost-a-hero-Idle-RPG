using System;

namespace Simulation
{
	public class BuffEventStunAll : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			if (by is Enemy)
			{
				foreach (Hero hero in world.heroes)
				{
					hero.AddBuff(this.effect, 0, false);
				}
			}
			else
			{
				foreach (Enemy enemy in world.activeChallenge.enemies)
				{
					enemy.AddBuff(this.effect, 0, false);
				}
			}
		}

		public BuffDataStun effect;
	}
}
