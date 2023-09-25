using System;

namespace Simulation
{
	public class TotemDataIce : TotemData
	{
		public TotemDataIce(TotemDataBaseIce dataBase, int progress)
		{
			this.dataBase = dataBase;
			base.SetProgress(progress);
		}

		public override TotemDataBase GetDataBase()
		{
			return this.dataBase;
		}

		public float GetManaMax()
		{
			return this.dataBase.GetManaMax();
		}

		public float GetManaGatherSpeed()
		{
			return this.dataBase.GetManaGatherSpeed();
		}

		public float GetManaUseSpeed()
		{
			return this.dataBase.GetManaUseSpeed();
		}

		public float GetShardReqMana()
		{
			return this.dataBase.GetShardReqMana();
		}

		public float GetShardAreaR()
		{
			return this.dataBase.GetShardAreaR();
		}

		public Projectile projectileShard
		{
			get
			{
				return this.dataBase.projectileShard;
			}
		}

		public TotemDataBaseIce dataBase;
	}
}
