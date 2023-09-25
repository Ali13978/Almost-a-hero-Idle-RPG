using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class BuffDataBounce : BuffData
	{
		public BuffDataBounce(double secondaryDamage)
		{
			this.secondaryDamage = secondaryDamage;
			this.id = 21;
		}

		public override void OnAfterLightning(Buff buff, UnitHealthy target, Damage damage)
		{
			List<UnitHealthy> list = new List<UnitHealthy>();
			foreach (UnitHealthy unitHealthy in buff.GetBy().GetOpponents())
			{
				if (unitHealthy != target && unitHealthy.IsAttackable())
				{
					list.Add(unitHealthy);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			int randomInt = GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed);
			UnitHealthy unitHealthy2 = list[randomInt];
			Damage copy = damage.GetCopy();
			copy.amount *= this.secondaryDamage;
			buff.GetWorld().DamageMain(buff.GetBy(), unitHealthy2, copy);
			Vector3 pos = target.pos;
			pos.y += target.GetHeight() / 2f;
			Vector3 pos2 = unitHealthy2.pos;
			VisualLinedEffect item = new VisualLinedEffect(VisualLinedEffect.Type.TOTEM_LIGHTNING, pos, pos2, 0.3f);
			buff.GetWorld().visualLinedEffects.Add(item);
		}

		private double secondaryDamage;
	}
}
