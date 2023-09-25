using System;

namespace Simulation
{
	public class CurseBuffCharmSelectionTimer : CurseBuff
	{
		protected override void OnUpdate(float dt)
		{
			if (this.world.activeChallenge.state != Challenge.State.ACTION)
			{
				return;
			}
			this.world.buffTotalEffect.charmSelectionTimerFactor *= this.timerFactor;
		}

		public override void OnHeroHealed(Hero hero, double ratioHealed)
		{
			this.AddProgress((float)((double)this.pic * ratioHealed));
		}

		public float timerFactor;
	}
}
