using System;
using UnityEngine;

namespace Simulation
{
	public class SwarmDragon
	{
		public void Update(float dt)
		{
			this.totalTime += dt;
			this.stateTime += dt;
		}

		public void SetState(SwarmDragon.State st)
		{
			if (st == this.state)
			{
				return;
			}
			this.state = st;
			this.stateTime = 0f;
		}

		public Vector3 pos;

		public UnitHealthy target;

		public Vector2 direction;

		public Vector2 targetDirection;

		public Vector3 targetPos;

		public float speed;

		public float speedMult = 1f;

		public float targetRotation;

		public float currentRotation;

		public float rotationSpeed;

		public float totalTime;

		public float stateTime;

		public float seekTime;

		public Unit by;

		public Damage damage;

		public SwarmDragon.State state;

		public enum State
		{
			INITIAL_KICK,
			SEEKING,
			HEADING,
			HIT,
			DISAPPEAR,
			DEAD
		}
	}
}
