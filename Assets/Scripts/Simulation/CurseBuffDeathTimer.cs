using System;

namespace Simulation
{
	public class CurseBuffDeathTimer : CurseBuff
	{
		protected override void OnUpdate(float dt)
		{
			if (this.world.activeChallenge.state != Challenge.State.ACTION)
			{
				return;
			}
			this.world.buffTotalEffect.reviveTimeFactor *= 1f + this.timerFactor;
		}

		public override void OnAnyCharmTriggered(World world, CharmBuff charmBuff)
		{
			this.AddProgress(this.pic);
		}

		public float timerFactor;
	}
}
