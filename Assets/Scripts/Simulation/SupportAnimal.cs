using System;
using UnityEngine;

namespace Simulation
{
	public class SupportAnimal
	{
		public void Update(float dt)
		{
			this.stateTime += dt;
			this.totalTime += dt;
		}

		public void SetState(SupportAnimal.State st)
		{
			if (st == this.state)
			{
				return;
			}
			this.state = st;
			this.stateTime = 0f;
		}

		public const float GiveBuffDuration = 0.733f;

		public const float GiveBuffTime = 0.3f;

		public Vector3 pos;

		public Vector3 initialPos;

		public Unit target;

		public Vector3 movement;

		public float timeToReachTarget;

		public BuffData buff;

		public float speed;

		public SupportAnimal.Skin skin;

		public Hero master;

		public SoundEvent spawnSound;

		public SoundEvent disappearSound;

		public float stateTime;

		public float totalTime;

		public bool buffGiven;

		public SupportAnimal.State state;

		public enum State
		{
			SELECTING_TARGET,
			HEADING,
			GIVING_BUFF,
			RETURNING,
			DEAD
		}

		public enum Skin
		{
			Larry,
			Curly,
			Moe
		}
	}
}
