using System;
using UnityEngine;

public class SoundEventUiSimple : SoundEvent
{
	public SoundEventUiSimple(AudioClip clip, float volume = 1f)
	{
		this.clip = clip;
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
		soundManager.PlaySimple(SoundType.UI, string.Empty, false, 0f, this.volume, this.clip, 0f, float.MaxValue);
	}

	public AudioClip clip;

	protected float volume;
}
