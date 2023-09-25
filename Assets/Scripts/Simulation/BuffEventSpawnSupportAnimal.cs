using System;
using UnityEngine;

namespace Simulation
{
	public class BuffEventSpawnSupportAnimal : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			Vector3 vector = new Vector3(-1.5f, by.pos.y + GameMath.GetRandomFloat(-0.05f, 0.05f, GameMath.RandType.NoSeed), 0f);
			SupportAnimal item = new SupportAnimal
			{
				speed = 0.5f,
				buff = this.buff,
				pos = vector,
				initialPos = vector,
				skin = this.skin,
				master = (by as Hero),
				spawnSound = this.spawnSound,
				disappearSound = this.disappearSound
			};
			world.supportAnimals.Add(item);
		}

		public BuffData buff;

		public SupportAnimal.Skin skin;

		public SoundEvent spawnSound;

		public SoundEvent disappearSound;
	}
}
