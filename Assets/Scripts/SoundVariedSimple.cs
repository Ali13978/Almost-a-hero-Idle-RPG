using System;
using UnityEngine;

public class SoundVariedSimple : SoundVaried
{
	public SoundVariedSimple(AudioClipPromise[] clipPromises, float volume = 1f)
	{
		this.promises = clipPromises;
		this.volume = volume;
	}

	public SoundVariedSimple(AudioClip[] clips, float volume = 1f)
	{
		this.clips = clips;
		this.volume = volume;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		if (this.clips == null)
		{
			base.PlayVaried(type, by, isVoice, priority, this.promises, this.volume, soundManager);
		}
		else
		{
			base.PlayVaried(type, by, isVoice, priority, this.clips, this.volume, soundManager);
		}
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	private AudioClipPromise[] promises;

	private AudioClip[] clips;

	protected float volume;
}
