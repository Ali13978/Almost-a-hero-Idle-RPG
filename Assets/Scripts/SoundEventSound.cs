using System;

public class SoundEventSound : SoundEvent
{
	public SoundEventSound(SoundType soundType, string by, bool isVoice, float priority, Sound sound)
	{
		this.soundType = soundType;
		this.by = by;
		this.isVoice = isVoice;
		this.priority = priority;
		this.sound = sound;
	}

	public override bool IsVoice
	{
		get
		{
			return this.isVoice;
		}
	}

	public override void Apply(SoundManager soundManager)
	{
		this.sound.Play(this.soundType, this.by, this.isVoice, this.priority, soundManager);
	}

	public SoundType soundType;

	public string by;

	public bool isVoice;

	public float priority;

	public Sound sound;
}
