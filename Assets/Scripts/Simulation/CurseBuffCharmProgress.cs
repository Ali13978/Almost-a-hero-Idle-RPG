using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CurseBuffCharmProgress : CurseBuff
	{
		protected override void OnUpdate(float dt)
		{
			if (this.world.activeChallenge.state != Challenge.State.ACTION)
			{
				return;
			}
			this.time += dt;
			if (this.time >= this.factorLostEverySeconds)
			{
				this.time -= this.factorLostEverySeconds;
				List<CharmBuff> charmBuffs = (this.world.activeChallenge as ChallengeRift).charmBuffs;
				foreach (CharmBuff charmBuff in charmBuffs)
				{
					if (!(charmBuff is CharmBuffPermanent))
					{
						charmBuff.RemoveProgress(this.factorLost);
					}
				}
			}
		}

		public override void OnCollectGold()
		{
			this.AddProgress(this.pic);
		}

		public float factorLost;

		public float factorLostEverySeconds;

		private float time;
	}
}
