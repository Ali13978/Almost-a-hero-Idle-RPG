using System;

namespace Simulation
{
	public class CurseBuffAttackSpeedReduction : CurseBuff
	{
		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.INACTIVE)
			{
				return;
			}
			this.world.buffTotalEffect.heroAttackSpeedFactor *= this.attackSpeedFactor;
		}

		public override void OnWavePassed()
		{
			this.AddProgress(this.pic);
		}

		public float attackSpeedFactor;
	}
}
