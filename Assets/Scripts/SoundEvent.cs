using System;

public abstract class SoundEvent
{
	public virtual bool IsCancel
	{
		get
		{
			return false;
		}
	}

	public abstract bool IsVoice { get; }

	public abstract void Apply(SoundManager soundManager);
}
