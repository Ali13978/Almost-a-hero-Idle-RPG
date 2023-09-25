using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class SkillActiveDataBase : SkillTreeNode
	{
		public override bool IsActive()
		{
			return true;
		}

		public override bool IsPassive()
		{
			return false;
		}

		public override bool IsEnhancer()
		{
			return false;
		}

		public abstract void SetLevel(SkillActiveData data, int level, List<SkillEnhancer> skillEnhancers);

		public abstract void PlaySound(World world, Unit by, SkillActiveData skillData);

		public virtual void StopSound(World world, Unit by)
		{
			world.AddSoundEvent(new SoundEventCancelBy(by.GetId()));
		}

		protected void AddSoundEvent(World world, Unit by, Sound sound)
		{
			this.AddSoundEvent(world, by.GetId(), false, sound);
		}

		protected void AddSoundVoEvent(World world, Unit by, Sound sound)
		{
			this.AddSoundEvent(world, by.GetId(), true, sound);
		}

		private void AddSoundEvent(World world, string by, bool isVoice, Sound sound)
		{
			SoundEventSound e = new SoundEventSound(SoundType.GAMEPLAY, by, isVoice, 0f, sound);
			world.AddSoundEvent(e);
		}

		public virtual void RemoveSkillBuffs(Unit unit)
		{
		}

		public float timeFadeInStart = 0.1f;

		public float timeFadeInEnd = 0.3f;

		public float timeFadeOutStart = 1.5f;

		public float timeFadeOutEnd = 1.9f;

		public float durStayInFrontCurtain = 2f;

		public float durNonslowable = 3f;

		public SkillActiveDataBase.OnStunBehaviour stunBehaviour;

		public enum OnStunBehaviour
		{
			PAUSE,
			REMOVE
		}
	}
}
