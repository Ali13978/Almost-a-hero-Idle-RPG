using System;

namespace Simulation
{
	public class CurseBuffAntiRegeneration : CurseBuff
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
				foreach (Hero hero in this.world.heroes)
				{
					if (hero.IsAlive() && !hero.IsInvulnerable())
					{
						Damage damage = new Damage(hero.GetHealthMax() * this.damageFactor, false, false, false, false);
						damage.isPure = true;
						this.world.DamageMain(null, hero, damage);
					}
				}
			}
		}

		public override void OnAnyCharmTriggered(World world, CharmBuff charmBuff)
		{
			this.AddProgress(this.pic);
		}

		public double damageFactor;

		private float time;
	}
}
