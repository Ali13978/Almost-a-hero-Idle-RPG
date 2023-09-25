using System;

namespace Simulation
{
	public class BuffDataPlaySoundOnDeath : BuffData
	{
		public override void OnDeathSelf(Buff buff)
		{
			buff.GetWorld().AddSoundEvent(this.sound);
		}

		public SoundEvent sound;
	}
}
