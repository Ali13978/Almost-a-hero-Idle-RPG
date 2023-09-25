using System;

namespace SA.IOSNative.Gestures
{
	public class ForceInfo
	{
		public ForceInfo(float force, float maxForce)
		{
			this._Force = force;
			this._MaxForce = maxForce;
		}

		public float Force
		{
			get
			{
				return this._Force;
			}
		}

		public float MaxForce
		{
			get
			{
				return this._MaxForce;
			}
		}

		private float _Force;

		private float _MaxForce;
	}
}
