using System;

namespace Simulation
{
	public class CurseBuffHeroDamage : CurseBuff
	{
		protected override void OnUpdate(float dt)
		{
			this.world.buffTotalEffect.heroDamageFactor *= this.damageFactor;
		}

		public override void OnHeroHealed(Hero hero, double ratioHealed)
		{
			this.AddProgress(this.pic * (float)ratioHealed);
		}

		public double damageFactor;
	}
}
