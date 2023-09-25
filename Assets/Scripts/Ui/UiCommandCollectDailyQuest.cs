using System;
using Simulation;
using UnityEngine;

namespace Ui
{
	public class UiCommandCollectDailyQuest : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryCollectDailyQuest(this.startPos, this.invTransform);
		}

		public Vector3 startPos;

		public Transform invTransform;
	}
}
