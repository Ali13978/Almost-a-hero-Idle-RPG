using System;
using UnityEngine;

namespace Simulation
{
	public class CurrencyDragon
	{
		public void SetState(CurrencyDragon.State state)
		{
			if (this.state == state)
			{
				return;
			}
			this.state = state;
			this.stateTime = 0f;
		}

		public void PlayLoopSound(World world)
		{
			string by = this.GetHashCode().ToString();
			world.sounds.Add(new SoundEventCancelBy(by));
			world.sounds.Add(new SoundEventSound(SoundType.GAMEPLAY, by, false, 0f, new SoundLooped(SoundArchieve.inst.adDragonLoop, 1f)));
		}

		public void PlayActivateSound(World world)
		{
			string by = this.GetHashCode().ToString();
			world.sounds.Add(new SoundEventCancelBy(by));
			world.sounds.Add(new SoundEventSound(SoundType.GAMEPLAY, by, false, 0f, new SoundSimple(SoundArchieve.inst.adDragonTap, 1f, float.MaxValue)));
		}

		public void PlayLeaveSound(World world)
		{
			string by = this.GetHashCode().ToString();
			world.sounds.Add(new SoundEventCancelBy(by));
			world.sounds.Add(new SoundEventSound(SoundType.GAMEPLAY, by, false, 0f, new SoundSimple(SoundArchieve.inst.adDragonLeave, 1f, float.MaxValue)));
		}

		public float stateTime;

		public float stateOffset;

		public float maxTime = 22f;

		public float yOffset;

		public Vector3 pos;

		public float direction;

		public float speed;

		public CurrencyType dropCurrency;

		public double dropAmount;

		public CurrencyDragon.State state;

		public bool dropsSpawned;

		public int visualVariation;

		public enum State
		{
			ENTER,
			IDLE,
			DROP,
			EXIT
		}
	}
}
