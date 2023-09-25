using System;

namespace Simulation
{
	public class RiftEffectShorterRespawns : RiftEffect
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
			world.buffTotalEffect.reviveTimeFactor *= 0.1f;
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectShorterRespawns();
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_SHORTER_RESPAWNS"), AM.csa(GameMath.GetPercentString(0.9f, false)));
		}

		public const float REVIVE_REDUCTION = 0.1f;
	}
}
