using System;
using UnityEngine;

public class SoundEventUiLooped : SoundEvent
{
	public SoundEventUiLooped(AudioClip clip, string by, float volume = 1f)
	{
		this.clip = clip;
		this.by = by;
		this.volume = volume;
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
		soundManager.PlayLoopedInfinite(SoundType.UI, this.by, 0f, this.volume, this.clip, 0f);
	}

	public AudioClip clip;

	protected float volume;

	protected string by;
}
