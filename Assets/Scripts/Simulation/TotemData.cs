using System;

namespace Simulation
{
	public abstract class TotemData : UnitData
	{
		public abstract TotemDataBase GetDataBase();

		public string id
		{
			get
			{
				return this.GetDataBase().id;
			}
		}

		public string name
		{
			get
			{
				return this.GetDataBase().GetName();
			}
		}

		public override float GetHeight()
		{
			return -1f;
		}

		public override float GetScaleHealthBar()
		{
			return -1f;
		}

		public override float GetScaleBuffVisual()
		{
			return -1f;
		}

		public void SetProgress(int progress)
		{
			this.damage = this.GetDamageFromProgress(progress);
			this.critChance = this.GetDataBase().critChance;
			this.critFactor = this.GetDataBase().critFactor;
		}

		public double GetDamageFromProgress(int progress)
		{
			return this.GetDataBase().damage * GameMath.PowInt(UnitMath.TOTEM_DAMAGE_INC, progress);
		}
	}
}
