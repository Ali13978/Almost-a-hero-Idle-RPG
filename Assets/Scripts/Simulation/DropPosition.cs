using System;
using UnityEngine;

namespace Simulation
{
	public class DropPosition
	{
		public DropPosition Clone()
		{
			return new DropPosition
			{
				startPos = this.startPos,
				endPos = this.endPos,
				invPos = this.invPos,
				showSideCurrency = this.showSideCurrency,
				targetToScaleOnReach = this.targetToScaleOnReach
			};
		}

		public Vector3 startPos;

		public Vector3 endPos;

		public Vector3 invPos;

		public bool showSideCurrency;

		public Transform targetToScaleOnReach;
	}
}
