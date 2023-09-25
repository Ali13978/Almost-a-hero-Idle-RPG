using System;
using UnityEngine;

public class SoundAmbient : Sound
{
	public SoundAmbient(float volume = 1f)
	{
		this.volume = volume;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		AudioClip ambientForest;
		if (this.ambientIndex == 0)
		{
			ambientForest = soundManager.ambientForest;
		}
		else if (this.ambientIndex == 1)
		{
			ambientForest = soundManager.ambientForest;
		}
		else
		{
			if (this.ambientIndex != 2)
			{
				throw new NotImplementedException();
			}
			ambientForest = soundManager.ambientForest;
		}
		soundManager.PlayLoopedInfinite(type, by, priority, this.volume, ambientForest, 0f);
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	public int ambientIndex;

	protected float volume;
}
