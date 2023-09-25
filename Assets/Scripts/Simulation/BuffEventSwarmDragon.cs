using System;
using UnityEngine;

namespace Simulation
{
	public class BuffEventSwarmDragon : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			SwarmDragon swarmDragon = new SwarmDragon();
			swarmDragon.rotationSpeed = 4f;
			swarmDragon.speed = 1.6f;
			swarmDragon.pos = by.pos + new Vector3(-0.2f, 1.1f) * 0.2f;
			swarmDragon.by = by;
			swarmDragon.damage = new Damage(by.GetDamage() * this.damageMul, false, false, false, false);
			swarmDragon.damage.type = DamageType.SKILL;
			if (GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed))
			{
				swarmDragon.damage.amount *= by.GetCritFactor();
				swarmDragon.damage.isCrit = true;
			}
			world.swarmDragons.Add(swarmDragon);
		}

		public double damageMul;
	}
}
