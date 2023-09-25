using System;

namespace Simulation
{
	public class TotemDataEarth : TotemData
	{
		public TotemDataEarth(TotemDataBaseEarth dataBase, int progress)
		{
			this.dataBase = dataBase;
			base.SetProgress(progress);
		}

		public override TotemDataBase GetDataBase()
		{
			return this.dataBase;
		}

		public Projectile projectile
		{
			get
			{
				return this.dataBase.projectile;
			}
		}

		public Projectile projectileMeteorite
		{
			get
			{
				return this.dataBase.projectileMeteorite;
			}
		}

		public float GetTimeChargedMax()
		{
			return this.dataBase.GetTimeChargedMax();
		}

		public float GetMeteorAreaR()
		{
			return this.dataBase.GetMeteorAreaR();
		}

		public float GetMeteoriteAreaR()
		{
			return this.dataBase.GetMeteoriteAreaR();
		}

		public int GetMaxNumMeteors()
		{
			return this.dataBase.GetMaxNumMeteors();
		}

		public TotemDataBaseEarth dataBase;
	}
}
