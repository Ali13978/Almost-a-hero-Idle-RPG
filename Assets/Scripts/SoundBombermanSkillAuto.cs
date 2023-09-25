using System;
using Simulation;

public class SoundBombermanSkillAuto : Sound
{
	public SoundBombermanSkillAuto(int index, SkillActiveData skillData, float volume = 1f)
	{
		this.index = index;
		this.skillData = skillData;
		this.volume = volume;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		if (this.index == 0)
		{
			AudioClipPromise bombermanFireworkLaunch = soundManager.bombermanFireworkLaunch;
			SkillEventBuffSelf skillEventBuffSelf = (SkillEventBuffSelf)this.skillData.events[0];
			foreach (BuffEvent buffEvent in skillEventBuffSelf.buff.events)
			{
				if (buffEvent is BuffEventProjectile)
				{
					int randomInt = GameMath.GetRandomInt(0, soundManager.bombermanFireworks.Length, GameMath.RandType.NoSeed);
					AudioClipPromise promise = soundManager.bombermanFireworks[randomInt];
					soundManager.PlayDelayed(type, by, priority, this.volume * GameMath.GetRandomFloat(0.8f, 1f, GameMath.RandType.NoSeed), promise, buffEvent.time + 1f);
				}
			}
			soundManager.PlaySimple(type, by, false, priority, this.volume, bombermanFireworkLaunch, 0f, float.MaxValue);
		}
		else
		{
			if (this.index != 1)
			{
				throw new NotImplementedException();
			}
			soundManager.PlayDelayed(type, by, priority, this.volume * GameMath.GetRandomFloat(0.8f, 1f, GameMath.RandType.NoSeed), soundManager.bombermanFriendlyCatch, this.skillData.events[0].time + 0.8f);
			soundManager.PlayDelayed(type, by, priority, this.volume * GameMath.GetRandomFloat(0.8f, 1f, GameMath.RandType.NoSeed), soundManager.bombermanFriendlyThrow, 1.7f);
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
