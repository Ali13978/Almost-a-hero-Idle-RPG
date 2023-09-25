using System;
using UnityEngine;

namespace Simulation
{
	public class UnitHealthyDataBase : UnitDataBase
	{
		public UnitHealthyDataBase()
		{
			this.damageTakenFactor = 1.0;
			this.scaleHealthBar = 1f;
			this.scaleBuffVisual = 1f;
		}

		public double healthMax;

		public double healthRegen;

		public double damageTakenFactor;

		public float dodgeChance;

		public float scaleHealthBar;

		public float scaleBuffVisual;

		public Vector3 projectileTargetOffset;

		public float projectileTargetRandomness;
	}
}
