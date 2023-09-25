using System;
using Simulation;
using UnityEngine;

public class EnemyVoices
{
	public EnemyVoices.VoiceData spawn;

	public EnemyVoices.VoiceData summonMinions;

	public EnemyVoices.VoiceData hurt;

	public EnemyVoices.VoiceData death;

	public class VoiceData
	{
		public VoiceData(AudioClip clip, float probability)
		{
			this.probability = probability;
			this.sound = new SoundSimple(clip, 1f, float.MaxValue);
		}

		public VoiceData(AudioClip[] clips, float probability)
		{
			this.probability = probability;
			this.sound = new SoundVariedSimple(clips, 1f);
		}

		public void Play(World world, string id)
		{
			if (GameMath.GetProbabilityOutcome(this.probability, GameMath.RandType.NoSeed))
			{
				world.AddSoundEvent(new SoundEventSound(SoundType.GAMEPLAY, id, true, 0f, this.sound));
			}
		}

		private Sound sound;

		private float probability;
	}
}
