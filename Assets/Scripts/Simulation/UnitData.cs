using System;

namespace Simulation
{
	public abstract class UnitData
	{
		public abstract float GetHeight();

		public abstract float GetScaleHealthBar();

		public abstract float GetScaleBuffVisual();

		public double damage;

		public float critChance;

		public double critFactor;
	}
}
