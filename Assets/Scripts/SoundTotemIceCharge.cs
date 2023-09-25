using System;
using UnityEngine;

public class SoundTotemIceCharge : Sound
{
	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		AudioClip totemIceChargeStart = soundManager.totemIceChargeStart;
		soundManager.PlaySimple(type, by, isVoice, priority, 1f, totemIceChargeStart, 0f, float.MaxValue);
		AudioClip totemIceChargeLoop = soundManager.totemIceChargeLoop;
		soundManager.PlayLooped(type, by, priority, 1f, totemIceChargeLoop, 0f, totemIceChargeStart.length, float.PositiveInfinity, float.PositiveInfinity);
	}

	public override float GetVolume()
	{
		return 1f;
	}
}
