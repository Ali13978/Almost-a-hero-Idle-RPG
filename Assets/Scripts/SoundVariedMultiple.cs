using System;

public class SoundVariedMultiple : Sound
{
	public SoundVariedMultiple(float volume, SoundVariedMultiple.SoundsInfo[] sounds)
	{
		this.sounds = sounds;
		this.volume = volume;
	}

	public void SetVariation(int variation)
	{
		this.variation = variation;
	}

	protected int GetClipIndex()
	{
		int result = -1;
		int num = -1;
		for (int i = 0; i < this.sounds.Length; i++)
		{
			SoundVariedMultiple.SoundsInfo soundsInfo = this.sounds[i];
			if ((this.variation + soundsInfo.variationOffset) % soundsInfo.module == soundsInfo.expectedResult && num < soundsInfo.module)
			{
				result = i;
				num = soundsInfo.module;
			}
		}
		return result;
	}

	public override float GetVolume()
	{
		return this.volume;
	}

	public override void Play(SoundType type, string by, bool isVoice, float priority, SoundManager soundManager)
	{
		if (this.sounds.Length == 0)
		{
			return;
		}
		int clipIndex = this.GetClipIndex();
		this.sounds[clipIndex].sounds.Play(type, by, isVoice, priority, soundManager);
	}

	private int variation;

	private float volume;

	private SoundVariedMultiple.SoundsInfo[] sounds;

	public class SoundsInfo
	{
		public int module;

		public int expectedResult;

		public int variationOffset;

		public SoundVariedSimple sounds;
	}
}
