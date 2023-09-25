using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Options
{
	public struct QuaternionOptions : IPlugOptions
	{
		public void Reset()
		{
			this.rotateMode = RotateMode.Fast;
			this.axisConstraint = AxisConstraint.None;
			this.up = Vector3.zero;
		}

		internal RotateMode rotateMode;

		internal AxisConstraint axisConstraint;

		internal Vector3 up;
	}
}
