using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation
{
	public class RiftEffectOnlyAttackCharms : RiftEffect
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
			return new RiftEffectOnlyAttackCharms();
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_ONLY_ATTACK_CHARMS");
		}

		public override List<CharmEffectData> OnCharmDraft(List<CharmEffectData> list)
		{
			return (from eff in list
			where eff.BaseData.charmType == CharmType.Attack
			select eff).ToList<CharmEffectData>();
		}
	}
}
