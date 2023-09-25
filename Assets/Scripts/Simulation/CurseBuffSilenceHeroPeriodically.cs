using System;

namespace Simulation
{
	public class CurseBuffSilenceHeroPeriodically : CurseBuff
	{
		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.INACTIVE)
			{
				this.time = 0f;
				return;
			}
			this.time += dt;
			if (this.time >= this.period)
			{
				this.time -= this.period;
				Hero randomAliveHero = this.world.GetRandomAliveHero();
				if (randomAliveHero != null)
				{
					BuffData buffData = new BuffDataSilence
					{
						id = 349,
						dur = this.duration
					};
					randomAliveHero.AddBuff(buffData, 0, false);
				}
			}
		}

		public override void OnDeathAny(Unit unit)
		{
			if (unit is Enemy)
			{
				this.AddProgress(this.pic);
			}
		}

		public float period;

		public float duration;

		private float time;
	}
}
