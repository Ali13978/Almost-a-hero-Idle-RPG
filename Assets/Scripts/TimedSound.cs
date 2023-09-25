using System;

public class TimedSound
{
	public TimedSound()
	{
	}

	public TimedSound(float time, Sound sound)
	{
		this.time = time;
		this.sound = sound;
	}

	public float time;

	public Sound sound;
}
