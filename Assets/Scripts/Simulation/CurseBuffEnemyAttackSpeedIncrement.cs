using System;

namespace Simulation
{
	public class CurseBuffEnemyAttackSpeedIncrement : CurseBuff
	{
		protected override void OnUpdate(float dt)
		{
			if (this.state == EnchantmentBuffState.INACTIVE)
			{
				return;
			}
			this.world.buffTotalEffect.enemyAttackSpeedAdd += this.attackSpeedIncrement;
		}

		public override void OnAbilityCast(Skill skill)
		{
			this.AddProgress(this.pic);
		}

		public float attackSpeedIncrement;
	}
}
