using System;

public abstract class AudioSourceTimed : AudioSourceSimple
{
	public AudioSourceTimed()
	{
		this.time = 0f;
		this.hasStarted = false;
	}

	public override bool HasEnded()
	{
		return this.hasStarted && !this.source.isPlaying;
	}

	protected float time;
}
