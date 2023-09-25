using System;

namespace Simulation
{
	public class TotemDataFire : TotemData
	{
		public TotemDataFire(TotemDataBaseFire dataBase, int progress)
		{
			this.dataBase = dataBase;
			base.SetProgress(progress);
		}

		public override TotemDataBase GetDataBase()
		{
			return this.dataBase;
		}

		public float GetHeatMax()
		{
			return this.dataBase.GetHeatMax();
		}

		public float GetHeatPerFire()
		{
			return this.dataBase.GetHeatPerFire();
		}

		public float GetCoolSpeed()
		{
			return this.dataBase.GetCoolSpeed();
		}

		public float GetOverCoolSpeed()
		{
			return this.dataBase.GetOverCoolSpeed();
		}

		public TotemDataBaseFire dataBase;
	}
}
