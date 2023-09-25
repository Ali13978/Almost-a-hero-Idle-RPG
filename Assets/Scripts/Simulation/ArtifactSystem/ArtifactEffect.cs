using System;
using UnityEngine;

namespace Simulation.ArtifactSystem
{
	public abstract class ArtifactEffect
	{
		public abstract EffectOperation GetEffectOperation();

		public abstract void Apply(UniversalTotalBonus universalTotalBonus, int level);

		public abstract string GetDescription();

		public abstract string GetValueString(int level, UniversalTotalBonus universalTotalBonus);

		public virtual string GetNextLevelIncreaseString(int level, UniversalTotalBonus universalTotalBonus)
		{
			throw new NotImplementedException();
		}

		public string GetSignedNextLevelIncreaseSting(int level, UniversalTotalBonus universalTotalBonus)
		{
			if (this.GetEffectOperation() == EffectOperation.Increaser)
			{
				return "+" + this.GetNextLevelIncreaseString(level, universalTotalBonus);
			}
			if (this.GetEffectOperation() == EffectOperation.Reducer)
			{
				return "-" + this.GetNextLevelIncreaseString(level, universalTotalBonus);
			}
			if (this.GetEffectOperation() == EffectOperation.Multiplier)
			{
				return "x" + this.GetNextLevelIncreaseString(level, universalTotalBonus);
			}
			throw new Exception("Invalid artifact effect operation type: " + this.GetEffectOperation());
		}

		public string GetSignedJumpString(int level, int jump, UniversalTotalBonus universalTotalBonus)
		{
			if (this.GetEffectOperation() == EffectOperation.Increaser)
			{
				return "+" + GameMath.GetPercentString(this.GetJumpDiff(level, jump, universalTotalBonus), false);
			}
			if (this.GetEffectOperation() == EffectOperation.Reducer)
			{
				return "-" + GameMath.GetPercentString(this.GetJumpDiff(level, jump, universalTotalBonus), false);
			}
			if (this.GetEffectOperation() == EffectOperation.Multiplier)
			{
				return "x" + GameMath.GetPercentString(this.GetJumpDiff(level, jump, universalTotalBonus), false);
			}
			throw new Exception("Invalid artifact effect operation type: " + this.GetEffectOperation());
		}

		public double GetJumpDiff(int level, int jump, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetValue(level + jump, universalTotalBonus) - this.GetValue(level, universalTotalBonus);
		}

		public string GetDescriptionWithValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			return this.GetDescription() + " " + AM.cs(this.GetSignedValue(level, universalTotalBonus), Color.white);
		}

		public string GetSignedValueUsingPercentage(int level, UniversalTotalBonus universalTotalBonus)
		{
			if (this.GetEffectOperation() != EffectOperation.Multiplier)
			{
				return this.GetSignedValue(level, universalTotalBonus);
			}
			double value = this.GetValue(level, universalTotalBonus);
			if (value < 1.0)
			{
				return "-" + GameMath.GetPercentString(1.0 - value, false);
			}
			return "+" + GameMath.GetPercentString(value - 1.0, false);
		}

		public string GetSignedValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			if (this.GetEffectOperation() == EffectOperation.Increaser)
			{
				return "+" + this.GetValueString(level, universalTotalBonus);
			}
			if (this.GetEffectOperation() == EffectOperation.Reducer)
			{
				return "-" + this.GetValueString(level, universalTotalBonus);
			}
			if (this.GetEffectOperation() == EffectOperation.Multiplier)
			{
				return "x" + this.GetValueString(level, universalTotalBonus);
			}
			throw new Exception("Invalid artifact effect operation type: " + this.GetEffectOperation());
		}

		public virtual double GetValue(int level, UniversalTotalBonus universalTotalBonus)
		{
			throw new NotImplementedException();
		}
	}
}
