using System;
using UnityEngine;

public class SoundLoopedAndDelayed : Sound
{
	public SoundLoopedAndDelayed(AudioClip clip, float delay, float volume = 1f)
	{
		this.clip = clip;
		this.volume = volume;
		this.delay = delay;
	}

	public SoundLoopedAndDelayed(AudioClipPromise promise, float delay, float volume = 1f)
	{
		this.promise = promise;
		this.volume = volume;
		this.delay = delay;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		if (this.clip == null)
		{
			soundManager.PlayDelayedLoopedInfinite(type, by, priority, this.volume, this.promise, 0f, this.delay);
		}
		else
		{
			soundManager.PlayDelayedLoopedInfinite(type, by, priority, this.volume, this.clip, 0f, this.delay);
		}
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	public AudioClip clip;

	public AudioClipPromise promise;

	public float delay;

	protected float volume;
}
