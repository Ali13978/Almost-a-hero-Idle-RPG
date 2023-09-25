using System;
using UnityEngine;

public class SoundTotemThrow : SoundVaried
{
	public SoundTotemThrow(float volume = 1f)
	{
		this.volume = volume;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		int numClips = soundManager.totemThrows.Length;
		int clipIndex = base.GetClipIndex(numClips);
		AudioClip clip = soundManager.totemThrows[clipIndex];
		soundManager.PlaySimple(type, by, isVoice, priority, this.volume, clip, 0f, float.MaxValue);
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	protected float volume;
}
