using System;

public abstract class Sound
{
	public abstract void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager);

	public abstract float GetVolume();
}
