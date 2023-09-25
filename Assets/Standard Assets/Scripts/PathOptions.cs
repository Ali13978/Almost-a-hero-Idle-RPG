using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Options
{
	public struct PathOptions : IPlugOptions
	{
		public void Reset()
		{
			this.mode = PathMode.Ignore;
			this.orientType = OrientType.None;
			this.lockPositionAxis = (this.lockRotationAxis = AxisConstraint.None);
			this.isClosedPath = false;
			this.lookAtPosition = Vector3.zero;
			this.lookAtTransform = null;
			this.lookAhead = 0f;
			this.hasCustomForwardDirection = false;
			this.forward = Quaternion.identity;
			this.useLocalPosition = false;
			this.parent = null;
			this.startupRot = Quaternion.identity;
			this.startupZRot = 0f;
		}

		public PathMode mode;

		public OrientType orientType;

		public AxisConstraint lockPositionAxis;

		public AxisConstraint lockRotationAxis;

		public bool isClosedPath;

		public Vector3 lookAtPosition;

		public Transform lookAtTransform;

		public float lookAhead;

		public bool hasCustomForwardDirection;

		public Quaternion forward;

		public bool useLocalPosition;

		public Transform parent;

		public bool isRigidbody;

		internal Quaternion startupRot;

		internal float startupZRot;
	}
}
