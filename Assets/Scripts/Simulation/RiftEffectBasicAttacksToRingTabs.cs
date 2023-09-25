using System;
using UnityEngine;

namespace Simulation
{
	public class RiftEffectBasicAttacksToRingTabs : RiftEffect
	{
		public override float difficultyFactor
		{
			get
			{
				return -1f;
			}
		}

		public override RiftEffect Clone()
		{
			return new RiftEffectBasicAttacksToRingTabs();
		}

		public override string GetDesc()
		{
			return LM.Get("RIFT_EFFECT_BASIC_ATTACKS_TO_RING_ATTACK");
		}

		public override void OnPreAttack(Hero by, Damage newDamage, Projectile projectile)
		{
			if (projectile != null)
			{
				projectile.discarded = true;
			}
			else
			{
				newDamage.amount = 0.0;
				newDamage.dontShow = true;
			}
			World world = by.world;
			world.AddRingTap(by.pos + Vector3.up * by.GetHeight() * 0.5f);
		}
	}
}
