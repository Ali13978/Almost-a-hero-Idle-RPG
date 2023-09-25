using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation
{
	public class RiftEffectOnlyUtilityCharms : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -0.2f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectOnlyUtilityCharms();
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_ONLY_UTILITY_CHARMS");
		}

		public override List<CharmEffectData> OnCharmDraft(List<CharmEffectData> list)
		{
			return (from eff in list
			where eff.BaseData.charmType == CharmType.Utility
			select eff).ToList<CharmEffectData>();
		}
	}
}
