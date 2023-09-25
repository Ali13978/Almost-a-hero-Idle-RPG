using System;

namespace Simulation
{
	public abstract class TrinketUpgradeReq
	{
		public abstract float GetChanceWeight();

		public TrinketUpgradeReq GetCopy()
		{
			return (TrinketUpgradeReq)Activator.CreateInstance(base.GetType());
		}

		public bool IsSatisfied()
		{
			return this.progress >= this.req;
		}

		public string GetProgressString()
		{
			return string.Format("{0}/{1}", GameMath.GetDoubleString(this.progress), GameMath.GetDoubleString(this.req));
		}

		public float GetProgress()
		{
			return (float)(this.progress / this.req);
		}

		public abstract string GetDebugName();

		public void IncreaseProgress(int amount)
		{
			this.progress = Math.Min(this.progress + (double)amount, this.req);
		}

		public string GetDesc()
		{
			return string.Format(LM.Get(this.descKey), this.req);
		}

		public abstract int GetLevelReq(int level);

		public abstract int GetBodySpriteIndex();

		public double progress;

		public double req;

		public BuffData buffData;

		public string descKey;
	}
}
