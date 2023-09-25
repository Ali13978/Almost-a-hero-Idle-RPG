using System;
using UnityEngine;

public class SoundDelayed : Sound
{
	public SoundDelayed(float delayTime, AudioClip clip, float volume = 1f)
	{
		this.delayTime = delayTime;
		this.clip = clip;
		this.volume = volume;
	}

	public SoundDelayed(float delayTime, AudioClipPromise promise, float volume = 1f)
	{
		this.delayTime = delayTime;
		this.promise = promise;
		this.volume = volume;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		if (this.clip == null)
		{
			soundManager.PlayDelayed(type, by, priority, this.volume, this.promise, this.delayTime);
		}
		else
		{
			soundManager.PlayDelayed(type, by, priority, this.volume, this.clip, this.delayTime);
		}
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	private AudioClip clip;

	private AudioClipPromise promise;

	protected float volume;

	protected float delayTime;
}
