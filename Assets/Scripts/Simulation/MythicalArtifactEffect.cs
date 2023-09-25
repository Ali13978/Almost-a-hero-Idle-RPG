using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class MythicalArtifactEffect : ArtifactEffect
	{
		public int GetRank()
		{
			return this.rank;
		}

		public virtual double GetUpgradeCost(int rank)
		{
			return 100000.0 * GameMath.PowInt(1.18, rank);
		}

		public abstract int GetMinRequiredMythical();

		public abstract int GetMaxRank();

		public abstract void SetRank(int rank);

		public abstract string GetName();

		public abstract string GetNameEN();

		public override double GetQuality()
		{
			return (double)((this.rank + 1) * 5000);
		}

		public override double GetQuality(double amount)
		{
			return (double)((this.rank + 1) * 5000);
		}

		public void IncreaseRank()
		{
			this.SetRank(this.rank + 1);
		}

		public void IncreaseRank(int amount)
		{
			this.SetRank(this.rank + amount);
		}

		public bool IsMaxRanked()
		{
			return this.GetRank() >= this.GetMaxRank();
		}

		public override bool IsLimited()
		{
			throw new NotImplementedException();
		}

		public override double GetReqMinQuality()
		{
			throw new NotImplementedException();
		}

		public override double GetAmountMin()
		{
			throw new NotImplementedException();
		}

		public override double GetAmountMax()
		{
			throw new NotImplementedException();
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			throw new NotImplementedException();
		}

		public override void SetQuality(double quality)
		{
			throw new NotImplementedException();
		}

		public override double GetAmount()
		{
			throw new NotImplementedException();
		}

		public override string GetAmountString()
		{
			throw new NotImplementedException();
		}

		public abstract bool CanBeDisabled();

		protected int rank;

		public bool forcedDisable;
	}
}
