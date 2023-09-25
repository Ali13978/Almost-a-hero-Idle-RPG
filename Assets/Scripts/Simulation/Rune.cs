using System;

namespace Simulation
{
	public abstract class Rune
	{
		public virtual void Initialize(TotemDataBase belongsTo, BuffData buff, string id, string nameKey, string descKey, float lootpackChance = 1f)
		{
			this.belongsTo = belongsTo;
			this.buff = buff;
			this.id = id;
			this.nameKey = nameKey;
			this.descKey = descKey;
			this.lootpackChance = lootpackChance;
			this.buff.isPermenant = true;
		}

		public abstract string GetDesc();

		public string id;

		public string nameKey;

		public string descKey;

		public TotemDataBase belongsTo;

		public BuffData buff;

		public float lootpackChance;
	}
}
