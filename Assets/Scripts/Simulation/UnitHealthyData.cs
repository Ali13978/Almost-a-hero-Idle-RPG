using System;
using UnityEngine;

namespace Simulation
{
	public abstract class UnitHealthyData : UnitData
	{
		public abstract Vector3 GetProjectileTargetOffset();

		public abstract float GetProjectileTargetRandomness();

		public double healthMax;

		public double healthRegen;

		public double damageTakenFactor;

		public float dodgeChance;
	}
}
