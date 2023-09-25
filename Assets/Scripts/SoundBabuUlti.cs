using System;
using Simulation;

public class SoundBabuUlti : Sound
{
	public SoundBabuUlti(SkillActiveData skillData, float volume = 1f)
	{
		this.skillData = skillData;
		this.volume = volume;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		soundManager.PlaySimple(type, by, isVoice, priority, this.volume, soundManager.babuUltiStart, 0f, float.MaxValue);
		SkillAnimEvent skillAnimEvent = this.skillData.animEvents[1];
		SkillAnimEvent skillAnimEvent2 = this.skillData.animEvents[this.skillData.animEvents.Count - 1];
		float time = skillAnimEvent.time;
		float time2 = skillAnimEvent2.time;
		soundManager.PlayLooped(type, by, priority, this.volume, soundManager.babuUltiLoop, time - 0.5f, time, time2, time2 + 0.5f);
		soundManager.PlayDelayed(type, by, priority, this.volume, soundManager.babuUltiEnd, time2);
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	public SkillActiveData skillData;

	protected float volume;
}
