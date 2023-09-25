using System;

namespace Simulation
{
	public class SkillEventVisualEffect : SkillEvent
	{
		public override void Apply(Unit by)
		{
			this.visualEffect.time = 0f;
			this.visualEffect.pos = by.pos;
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
	}
}
