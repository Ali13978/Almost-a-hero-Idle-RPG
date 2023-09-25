using System;
using UnityEngine;

public class SoundSimple : Sound
{
	public SoundSimple(AudioClip clip, float volume = 1f, float fadeOutTime = 3.40282347E+38f)
	{
		this.clip = clip;
		this.volume = volume;
		this.fadeOutTime = fadeOutTime;
	}

	public SoundSimple(AudioClipPromise promise, float volume = 1f)
	{
		this.promise = promise;
		this.volume = volume;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		if (this.clip == null)
		{
			float num = this.volume;
			AudioClipPromise audioClipPromise = this.promise;
			float num2 = this.fadeOutTime;
			soundManager.PlaySimple(type, by, isVoice, priority, num, audioClipPromise, 0f, num2);
		}
		else
		{
			float num = this.volume;
			AudioClip audioClip = this.clip;
			float num3 = this.fadeOutTime;
			soundManager.PlaySimple(type, by, isVoice, priority, num, audioClip, 0f, num3);
		}
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	private AudioClip clip;

	private AudioClipPromise promise;

	protected float volume;

	protected float fadeOutTime;
}
