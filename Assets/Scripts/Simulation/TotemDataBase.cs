using System;

namespace Simulation
{
	public abstract class TotemDataBase : UnitDataBase
	{
		public string GetName()
		{
			return LM.Get(this.nameKey);
		}

		public virtual string GetDesc()
		{
			return LM.Get(this.descKey);
		}

		public static int[] LEVEL_XPS = new int[]
		{
			6,
			6,
			6,
			6,
			6,
			7,
			7,
			7,
			7,
			7,
			8,
			8,
			8,
			8,
			8,
			9,
			9,
			9,
			9,
			9,
			10,
			10,
			10,
			10,
			10,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			14,
			14,
			14,
			14,
			14,
			16,
			16,
			16,
			16,
			19,
			19,
			19,
			19,
			19,
			19,
			21,
			21,
			21,
			21,
			21,
			21,
			21,
			21,
			21,
			21,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			22,
			9999999,
			-1
		};

		public string id;

		public string nameKey;

		public string descKey;
	}
}
