using System;

public class SoundDruidUlti : Sound
{
	public SoundDruidUlti(AudioClipPromise ultiStartSound, float volume = 1f)
	{
		this.volume = volume;
		this.ultiStartSound = ultiStartSound;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		soundManager.PlaySimple(type, by, isVoice, priority, this.volume, this.ultiStartSound, 0f, float.MaxValue);
		soundManager.PlayDelayed(type, by, priority, this.volume, soundManager.druidUltiEnd, 28.2f);
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	private float volume;

	private AudioClipPromise ultiStartSound;
}
