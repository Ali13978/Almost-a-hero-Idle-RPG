using System;
using Simulation;
using UnityEngine;

namespace Ui
{
	public class UiCommandCollectRiftReward : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryCollectRiftReward(this.startPos, this.invTransform);
		}

		public Transform invTransform;

		public Vector3 startPos;
	}
}
