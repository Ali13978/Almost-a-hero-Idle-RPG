using System;

namespace Simulation
{
	public abstract class EnchantmentEffectData
	{
		public string descKey
		{
			get
			{
				return this.baseData.descKey;
			}
		}

		public string nameKey
		{
			get
			{
				return this.baseData.nameKey;
			}
		}

		public string conditionKey
		{
			get
			{
				return this.baseData.conditionKey;
			}
		}

		public abstract string GetConditionDescFormat();

		public string GetName()
		{
			return LM.Get(this.baseData.nameKey);
		}

		public abstract string GetDesc();

		public abstract string GetConditionDescription();

		public abstract void Apply(ChallengeRift challenge);

		public int level = -1;

		public EnchantmentDataBase baseData;

		public bool isNew;

		public bool isLoading;
	}
}
