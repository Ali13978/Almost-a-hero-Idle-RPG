using System;

namespace Simulation
{
	public class RiftEffectLongerRespawns : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -1.2f;
			}
		}

		public override void Apply(World world, float dt)
		{
			world.buffTotalEffect.reviveTimeFactor *= 1f + this.respawnSlowAmount;
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectLongerRespawns
			{
				respawnSlowAmount = this.respawnSlowAmount
			};
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_LONGER_RESPAWNS"), GameMath.GetPercentString(this.respawnSlowAmount, false));
		}

		public float respawnSlowAmount = 1f;
	}
}
