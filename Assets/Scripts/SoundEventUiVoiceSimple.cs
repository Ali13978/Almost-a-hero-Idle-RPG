using System;
using UnityEngine;

public class SoundEventUiVoiceSimple : SoundEvent
{
	public SoundEventUiVoiceSimple(AudioClip clip, string heroId, float volume = 1f)
	{
		this.clip = clip;
		this.heroId = heroId;
		this.volume = volume;
	}

	public SoundEventUiVoiceSimple(AudioClipPromise promise, string heroId, float volume = 1f)
	{
		this.promise = promise;
		this.heroId = heroId;
		this.volume = volume;
	}

	public override bool IsVoice
	{
		get
		{
			return true;
		}
	}

	public override void Apply(SoundManager soundManager)
	{
		if (this.clip == null)
		{
			soundManager.PlaySimple(SoundType.UI, this.heroId, true, 0f, this.volume, this.promise, 0f, float.MaxValue);
		}
		else
		{
			soundManager.PlaySimple(SoundType.UI, this.heroId, true, 0f, this.volume, this.clip, 0f, float.MaxValue);
		}
	}

	private AudioClip clip;

	private AudioClipPromise promise;

	public string heroId;

	protected float volume;
}
