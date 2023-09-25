using System;

namespace Simulation
{
	public class Damage
	{
		public Damage(double amount, bool isCrit = false, bool isDodged = false, bool isMissed = false, bool isUltraCrit = false)
		{
			this.amount = amount;
			this.isCrit = isCrit;
			this.isUltraCrit = isUltraCrit;
			this.isMissed = isMissed;
			this.isDodged = isDodged;
		}

		public Damage GetCopy()
		{
			return new Damage(this.amount, this.isCrit, this.isDodged, this.isMissed, this.isUltraCrit)
			{
				isExact = this.isExact,
				isPure = this.isPure,
				realDamageDealt = this.realDamageDealt,
				blockFactor = this.blockFactor,
				type = this.type,
				canNotBeDodged = this.canNotBeDodged,
				doNotHighlight = this.doNotHighlight,
				ignoreReduction = this.ignoreReduction,
				ignoreShield = this.ignoreShield,
				dontCountForDps = this.dontCountForDps
			};
		}

		public double amount;

		public double realDamageDealt;

		public bool isCrit;

		public bool isUltraCrit;

		public bool isDodged;

		public bool isMissed;

		public bool isExact;

		public bool isPure;

		public bool isMirrored;

		public bool dontShow;

		public bool showAsPer;

		public bool canNotBeDodged;

		public bool doNotHighlight;

		public DamageType type;

		public double blockFactor;

		public bool ignoreShield;

		public bool ignoreReduction;

		public bool dontCountForDps;
	}
}
