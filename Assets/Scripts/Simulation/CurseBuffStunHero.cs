using System;

namespace Simulation
{
	public class CurseBuffStunHero : CurseBuff
	{
		public override void OnDeathAny(Unit unit)
		{
			if (this.state == EnchantmentBuffState.INACTIVE)
			{
				return;
			}
			if (unit is Enemy)
			{
				Hero randomAliveHero = this.world.GetRandomAliveHero();
				if (randomAliveHero != null && !randomAliveHero.IsInvulnerable())
				{
					BuffDataStun buffDataStun = new BuffDataStun();
					buffDataStun.id = 319;
					buffDataStun.dur = this.stunDuration;
					buffDataStun.isPermenant = false;
					buffDataStun.isStackable = false;
					buffDataStun.visuals |= 512;
					randomAliveHero.AddBuff(buffDataStun, 0, false);
				}
			}
		}

		public override void OnEnemyStunned(Enemy enemy)
		{
			this.AddProgress(this.pic);
		}

		public float stunDuration;
	}
}
