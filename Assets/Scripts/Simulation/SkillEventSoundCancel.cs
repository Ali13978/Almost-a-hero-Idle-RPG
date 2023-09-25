using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillEventSoundCancel : SkillEvent
	{
		public override void Apply(Unit by)
		{
			foreach (SoundEvent e in this.soundEvents)
			{
				by.world.AddSoundEvent(e);
			}
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
		}

		public List<SoundEvent> soundEvents;
	}
}
