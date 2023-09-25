using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CharmBuffEndlessSparks : CharmBuff
	{
		protected override bool TryActivating()
		{
			ChallengeRift challengeRift = this.world.activeChallenge as ChallengeRift;
			if (challengeRift == null)
			{
				return true;
			}
			List<CharmBuff> charmsThatAreNotAlwaysActive = challengeRift.GetCharmsThatAreNotAlwaysActive();
			foreach (CharmBuff charmBuff in charmsThatAreNotAlwaysActive)
			{
				if (!(charmBuff is CharmBuffEndlessSparks))
				{
					charmBuff.AddProgress(GameMath.GetRandomFloat(this.progressMin, this.progressMax, GameMath.RandType.NoSeed));
				}
			}
			return true;
		}

		protected override void OnUpdate(float dt)
		{
			this.AddProgress(this.pic * dt);
		}

		public float progressMin;

		public float progressMax;
	}
}
