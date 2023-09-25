using System;
using Simulation;

public class SoundDerekSkillAuto : Sound
{
	public SoundDerekSkillAuto(int index, SkillActiveData skillData, float volume = 1f)
	{
		this.index = index;
		this.skillData = skillData;
		this.volume = volume;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		AudioClipPromise audioClipPromise = soundManager.derekAutoSkills[this.index];
		soundManager.PlaySimple(type, by, isVoice, priority, this.volume, audioClipPromise, 0f, float.MaxValue);
		if (this.index == 0)
		{
			int count = this.skillData.animEvents.Count;
			int num = count - 1;
			int exclusiveMax = soundManager.derekAttacks.Length;
			for (int i = 0; i < num; i++)
			{
				int randomInt = GameMath.GetRandomInt(0, exclusiveMax, GameMath.RandType.NoSeed);
				AudioClipPromise promise = soundManager.derekAttacks[randomInt];
				soundManager.PlayDelayed(type, by, priority, this.volume, promise, this.skillData.animEvents[i].time);
			}
		}
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	public int index;

	public SkillActiveData skillData;

	protected float volume;
}
