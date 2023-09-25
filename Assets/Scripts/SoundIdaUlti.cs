using System;
using Simulation;

public class SoundIdaUlti : Sound
{
	public SoundIdaUlti(SkillActiveData skillData, float volume = 1f)
	{
		this.skillData = skillData;
		this.volume = volume;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		soundManager.PlaySimple(type, by, isVoice, priority, this.volume, soundManager.idaUltiStart, 0f, float.MaxValue);
		int count = this.skillData.animEvents.Count;
		int num = soundManager.idaUltiLoop.Length;
		for (int i = 0; i < count; i++)
		{
			soundManager.PlayDelayed(type, by, priority, this.volume, soundManager.idaUltiLoop[i % num], this.skillData.animEvents[i].time - 0.65f);
		}
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	public SkillActiveData skillData;

	protected float volume;
}
