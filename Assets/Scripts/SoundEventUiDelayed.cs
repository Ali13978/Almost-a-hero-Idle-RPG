using System;
using UnityEngine;

public class SoundEventUiDelayed : SoundEvent
{
	public SoundEventUiDelayed(AudioClip clip, float delay, float volume = 1f)
	{
		this.clip = clip;
		this.volume = volume;
		this.delay = delay;
	}

	public override bool IsVoice
	{
		get
		{
			return false;
		}
	}

	public override void Apply(SoundManager soundManager)
	{
		soundManager.PlayDelayed(SoundType.UI, string.Empty, 0f, this.volume, this.clip, this.delay);
	}

	public AudioClip clip;

	protected float volume;

	protected float delay;
}
