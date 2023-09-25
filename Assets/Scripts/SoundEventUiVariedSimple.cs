using System;
using UnityEngine;

public class SoundEventUiVariedSimple : SoundEvent
{
	public SoundEventUiVariedSimple(AudioClip[] clips, float volume = 1f)
	{
		this.clips = clips;
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
		int randomInt = GameMath.GetRandomInt(0, this.clips.Length, GameMath.RandType.NoSeed);
		AudioClip clip = this.clips[randomInt];
		soundManager.PlaySimple(SoundType.UI, string.Empty, false, 0f, this.volume, clip, 0f, float.MaxValue);
	}

	public AudioClip[] clips;

	protected float volume;
}
