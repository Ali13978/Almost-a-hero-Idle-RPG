using System;
using UnityEngine;

public class SoundLooped : Sound
{
	public SoundLooped(AudioClip clip, float volume = 1f)
	{
		this.clip = clip;
		this.volume = volume;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		soundManager.PlayLoopedInfinite(type, by, priority, this.volume, this.clip, 0f);
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	public AudioClip clip;

	protected float volume;
}
