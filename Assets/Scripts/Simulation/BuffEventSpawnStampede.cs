using System;
using UnityEngine;

namespace Simulation
{
	public class BuffEventSpawnStampede : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			Stampede stampede = new Stampede();
			stampede.pos = new Vector3(-1.5f, GameMath.GetRandomFloat(-0.3f, 0.1f, GameMath.RandType.NoSeed));
			Vector3 vector = new Vector3(0.4f, stampede.pos.y) - stampede.pos;
			stampede.movement = vector.normalized * 1.2f;
			stampede.timeToReachTarget = vector.magnitude / 1.2f;
			stampede.by = by;
			stampede.damage = new Damage(by.GetDamage() * this.damageMul, false, false, false, false);
			stampede.damage.type = DamageType.SKILL;
			stampede.damage.doNotHighlight = true;
			if (GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed))
			{
				stampede.damage.amount *= by.GetCritFactor();
				stampede.damage.isCrit = true;
			}
			world.stampedes.Add(stampede);
		}

		public double damageMul;
	}
}
