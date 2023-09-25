using System;

public class SoundEventCancelAll : SoundEvent
{
	public override bool IsCancel
	{
		get
		{
			return true;
		}
	}

	public override bool IsVoice
	{
		get
		{
			return false;
		}
	}

	public override void Apply(SoundManager soundManager)
	{
		soundManager.CancelSoundEffects();
	}
}
