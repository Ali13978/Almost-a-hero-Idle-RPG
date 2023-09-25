using System;

namespace Simulation
{
	public struct HeroUnlockDescKey
	{
		public string GetString()
		{
			string arg = '\n' + AM.SizeText(this.amount.ToString(), 60) + '\n';
			return string.Format(LM.Get(this.descKey), arg);
		}

		public string GetStringDarkBrownAmount()
		{
			string arg = '\n' + AM.SizeText(AM.csdb(this.amount.ToString()), 60) + '\n';
			return string.Format(LM.Get(this.descKey), arg);
		}

		public string descKey;

		public int amount;

		public bool isAmountHidden;
	}
}
