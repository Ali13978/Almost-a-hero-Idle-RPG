using System;

namespace DG.Tweening.Plugins.Options
{
	public struct VectorOptions : IPlugOptions
	{
		public void Reset()
		{
			this.axisConstraint = AxisConstraint.None;
			this.snapping = false;
		}

		public AxisConstraint axisConstraint;

		public bool snapping;
	}
}
