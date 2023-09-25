using System;
using UnityEngine;

namespace Simulation
{
	public class SkillEventVisualEffectByPos : SkillEvent
	{
		public override void Apply(Unit by)
		{
			this.visualEffect.time = 0f;
			this.visualEffect.pos = by.pos + this.relativePos;
			Hero hero = by as Hero;
			if (hero != null)
			{
				this.visualEffect.skinIndex = hero.GetEquippedSkinIndex();
			}
			by.world.visualEffects.Add(this.visualEffect);
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
			if (timePassedSinceActivation <= this.visualEffect.dur)
			{
				by.world.visualEffects.Remove(this.visualEffect);
			}
		}

		public VisualEffect visualEffect;

		public Vector3 relativePos;
	}
}
