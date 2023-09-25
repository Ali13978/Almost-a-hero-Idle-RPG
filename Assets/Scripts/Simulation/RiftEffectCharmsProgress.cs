using System;
using System.Collections.Generic;

namespace Simulation
{
	public class RiftEffectCharmsProgress : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 1f;
			}
		}

		public override void Apply(World world, float dt)
		{
			ChallengeRift challengeRift = world.activeChallenge as ChallengeRift;
			if (challengeRift == null || challengeRift.state != Challenge.State.ACTION)
			{
				return;
			}
			this.timer += dt;
			while (this.timer >= 1f)
			{
				this.timer -= 1f;
				List<CharmBuff> charmsThatAreNotAlwaysActive = challengeRift.GetCharmsThatAreNotAlwaysActive();
				if (charmsThatAreNotAlwaysActive.Count > 0)
				{
					CharmBuff charmBuff = charmsThatAreNotAlwaysActive[GameMath.GetRandomInt(0, charmsThatAreNotAlwaysActive.Count, GameMath.RandType.NoSeed)];
					charmBuff.AddProgress(this.progressAdd);
				}
			}
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_CHARM_PROGRESS"), GameMath.GetPercentString(this.progressAdd, false));
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectCharmsProgress
			{
				progressAdd = this.progressAdd
			};
		}

		public float progressAdd = 0.1f;

		private float timer;
	}
}
