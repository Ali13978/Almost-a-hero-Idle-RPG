using System;

namespace Simulation
{
	public class CurseBuffEnemyHealingPerSecond : CurseBuff
	{
		protected override void OnUpdate(float dt)
		{
			if (this.world.activeChallenge.state != Challenge.State.ACTION)
			{
				return;
			}
			this.time += dt;
			if (this.time >= 1f)
			{
				this.time -= 1f;
				foreach (Enemy enemy in this.world.activeChallenge.enemies)
				{
					if (enemy.IsAlive())
					{
						enemy.Heal(this.healingFactor);
					}
				}
			}
		}

		public override void OnCollectGold()
		{
			this.AddProgress(this.pic);
		}

		public double healingFactor;

		private float time;
	}
}
