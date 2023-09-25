using System;
using UnityEngine;

public class SoundEventUiVariedVoiceSimple : SoundEvent
{
	public SoundEventUiVariedVoiceSimple(AudioClip[] clips, string heroId, float volume = 1f)
	{
		this.clips = clips;
		this.heroId = heroId;
		this.volume = volume;
	}

	public SoundEventUiVariedVoiceSimple(AudioClipPromise[] promises, string heroId, float volume = 1f)
	{
		this.promises = promises;
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
		if (this.clips == null)
		{
			if (this.promises.Length == 0)
			{
				return;
			}
			int randomInt = GameMath.GetRandomInt(0, this.promises.Length, GameMath.RandType.NoSeed);
			soundManager.PlaySimple(SoundType.UI, this.heroId, true, 0f, this.volume, this.promises[randomInt], 0f, float.MaxValue);
		}
		else
		{
			if (this.clips.Length == 0)
			{
				return;
			}
			int randomInt2 = GameMath.GetRandomInt(0, this.clips.Length, GameMath.RandType.NoSeed);
			soundManager.PlaySimple(SoundType.UI, this.heroId, true, 0f, this.volume, this.clips[randomInt2], 0f, float.MaxValue);
		}
	}

	private AudioClipPromise[] promises;

	private AudioClip[] clips;

	private string heroId;

	protected float volume;
}
