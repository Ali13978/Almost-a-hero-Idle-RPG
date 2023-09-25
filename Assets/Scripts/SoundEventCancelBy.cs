using System;

public class SoundEventCancelBy : SoundEvent
{
	public SoundEventCancelBy(string by)
	{
		this.by = by;
	}

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
		soundManager.CancelBy(this.by);
	}

	public string by;
}
