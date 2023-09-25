using System;
using UnityEngine;

public abstract class SoundVaried : Sound
{
	public SoundVaried()
	{
		this.SetRandom();
	}

	public void SetVariation(int variation)
	{
		this.variation = variation;
	}

	public void SetRandom()
	{
		this.variation = -1;
	}

	private bool IsRandom()
	{
		return this.variation < 0;
	}

	protected int GetClipIndex(int numClips)
	{
		if (this.IsRandom())
		{
			return GameMath.GetRandomInt(0, numClips, GameMath.RandType.NoSeed);
		}
		return this.variation % numClips;
	}

	protected void PlayVaried(SoundType soundType, string by, bool isVoice, float priority, AudioClipPromise[] clips, float volume, SoundManager soundManager)
	{
		if (clips.Length <= 0)
		{
			return;
		}
		int numClips = clips.Length;
		int clipIndex = this.GetClipIndex(numClips);
		soundManager.PlaySimple(soundType, by, isVoice, priority, volume, clips[clipIndex], 0f, float.MaxValue);
	}

	protected void PlayVaried(SoundType soundType, string by, bool isVoice, float priority, AudioClip[] clips, float volume, SoundManager soundManager)
	{
		if (clips.Length <= 0)
		{
			return;
		}
		int numClips = clips.Length;
		int clipIndex = this.GetClipIndex(numClips);
		soundManager.PlaySimple(soundType, by, isVoice, priority, volume, clips[clipIndex], 0f, float.MaxValue);
	}

	private int variation;
}
