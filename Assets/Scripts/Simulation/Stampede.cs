using System;
using UnityEngine;

namespace Simulation
{
	public class Stampede
	{
		public void Update(float dt)
		{
			this.stateTime += dt;
			this.totalTime += dt;
		}

		public void SetState(Stampede.State st)
		{
			if (st == this.state)
			{
				return;
			}
			this.state = st;
			this.stateTime = 0f;
		}

		public static readonly Vector3[] AnimalsOffsets = new Vector3[]
		{
			new Vector3(-0.2f, 0.3f, 0f),
			new Vector3(0f, 0f, 0f),
			new Vector3(-0.2f, -0.3f, 0f)
		};

		public const float SpawnDuration = 0.3f;

		public const float DisappearDuration = 1.5f;

		public Vector3 pos;

		public Vector3 movement;

		public float timeToReachTarget;

		public float stateTime;

		public float totalTime;

		public Unit by;

		public Damage damage;

		public Stampede.State state;

		public enum State
		{
			HEADING,
			HIT,
			DISAPPEAR,
			DEAD
		}
	}
}
