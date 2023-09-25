using System;
using Simulation;

public class SoundJimUlti : Sound
{
	public SoundJimUlti(SkillActiveData skillData, float volume = 1f)
	{
		this.skillData = skillData;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		soundManager.PlaySimple(type, by, isVoice, priority, this.volume, soundManager.jimUltiStart, 0f, float.MaxValue);
		SkillAnimEvent skillAnimEvent = this.skillData.animEvents[0];
		SkillAnimEvent skillAnimEvent2 = this.skillData.animEvents[this.skillData.animEvents.Count - 1];
		float time = skillAnimEvent.time;
		float time2 = skillAnimEvent2.time;
		soundManager.PlayLooped(type, by, priority, this.volume, soundManager.jimUltiLoop, time - 0.5f, time, time2, time2 + 0.5f);
		soundManager.PlayDelayed(type, by, priority, this.volume, soundManager.jimUltiEnd, time2);
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	public SkillActiveData skillData;

	protected float volume;
}
