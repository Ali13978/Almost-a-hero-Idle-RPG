using System;
using System.Collections.Generic;

namespace Simulation
{
	public class RiftEffectDyingHealsAllies : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return 0.25f;
			}
		}

		public override void OnDeathAny(Unit unit)
		{
			IEnumerable<UnitHealthy> allies = unit.GetAllies();
			foreach (UnitHealthy unitHealthy in allies)
			{
				BuffDataHealthRegen buffDataHealthRegen = new BuffDataHealthRegen();
				buffDataHealthRegen.dur = this.dur;
				buffDataHealthRegen.id = 316;
				buffDataHealthRegen.healthRegenAdd = this.healRatio / (double)buffDataHealthRegen.dur;
				buffDataHealthRegen.visuals |= 64;
				unitHealthy.AddBuff(buffDataHealthRegen, 0, false);
			}
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get("RIFT_EFFECT_DYING_HEALS"), GameMath.GetPercentString(this.healRatio, false), GameMath.GetTimeInMilliSecondsString(this.dur));
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectDyingHealsAllies
			{
				healRatio = this.healRatio
			};
		}

		public double healRatio = 0.5;

		private float dur = 0.5f;
	}
}
