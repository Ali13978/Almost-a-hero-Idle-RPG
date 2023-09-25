using System;

namespace Simulation
{
	public class RiftEffectFasterCharmSpawns : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0.5f;
			}
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_FASTER_CHARM_SPAWN");
		}

		public override void Apply(World world, float dt)
		{
			ChallengeRift challengeRift = world.activeChallenge as ChallengeRift;
			challengeRift.charmSelectionAddTimer -= dt * this.timeModifier;
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectFasterCharmSpawns
			{
				timeModifier = this.timeModifier
			};
		}

		public float timeModifier = 1f;
	}
}
