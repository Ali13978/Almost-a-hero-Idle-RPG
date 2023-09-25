using System;

namespace DG.Tweening.Plugins.Options
{
	public struct Vector3ArrayOptions : IPlugOptions
	{
		public void Reset()
		{
			this.axisConstraint = AxisConstraint.None;
			this.snapping = false;
			this.durations = null;
		}

		public AxisConstraint axisConstraint;

		public bool snapping;

		internal float[] durations;
	}
}
