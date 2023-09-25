using System;

namespace Simulation
{
	public abstract class CurseEffectData : EnchantmentEffectData
	{
		public override string GetConditionDescFormat()
		{
			return LM.Get("CURSE_CONDITION_DESCRIPTION");
		}

		public virtual void IncrementLevel(ChallengeRift challenge)
		{
			this.level++;
			CurseBuff curseBuff = this.RefreshBuffStats(challenge);
			if (this.level == 0)
			{
				curseBuff.state = EnchantmentBuffState.READY;
			}
		}

		public virtual void DecrementLevel(ChallengeRift challenge)
		{
			this.level--;
			if (this.level < 0)
			{
				this.level = -1;
			}
			CurseBuff curseBuff = this.RefreshBuffStats(challenge);
			curseBuff.progress = 0f;
		}

		public void Recalculate(ChallengeRift challenge)
		{
			CurseBuff curseBuff = this.RefreshBuffStats(challenge);
			if (curseBuff != null && this.level >= 0)
			{
				curseBuff.state = EnchantmentBuffState.READY;
			}
		}

		public abstract float GetWeight();

		public abstract string GetConditionDescriptionNoColor();

		protected abstract CurseBuff RefreshBuffStats(ChallengeRift challenge);
	}
}
