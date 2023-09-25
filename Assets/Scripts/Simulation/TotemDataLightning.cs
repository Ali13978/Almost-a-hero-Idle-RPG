using System;

namespace Simulation
{
	public class TotemDataLightning : TotemData
	{
		public TotemDataLightning(TotemDataBaseLightning dataBase, int progress)
		{
			this.dataBase = dataBase;
			base.SetProgress(progress);
		}

		public override TotemDataBase GetDataBase()
		{
			return this.dataBase;
		}

		public int GetChargeReq()
		{
			return this.dataBase.GetChargeReq();
		}

		public TotemDataBaseLightning dataBase;
	}
}
