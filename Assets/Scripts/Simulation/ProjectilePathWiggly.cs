using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public class ProjectilePathWiggly : ProjectilePath
	{
		public override void Init(Vector3 posStart, Vector3 posEnd)
		{
			base.Init(posStart, posEnd);
			this.xcs = new List<float>
			{
				GameMath.GetRandomFloat(-8f, -6f, GameMath.RandType.NoSeed),
				GameMath.GetRandomFloat(2f, 4f, GameMath.RandType.NoSeed)
			};
			this.ycs = new List<float>
			{
				GameMath.GetRandomFloat(1.5f, 2.5f, GameMath.RandType.NoSeed)
			};
		}

		public override Vector3 GetPos(float timeRatio)
		{
			float num = 0f;
			foreach (float num2 in this.xcs)
			{
				float num3 = num2;
				num += num3;
				num *= timeRatio;
			}
			num *= 1f - timeRatio;
			num += timeRatio;
			num = this.posStart.x + num * (this.posEnd.x - this.posStart.x);
			float num4 = 0f;
			foreach (float num5 in this.ycs)
			{
				float num6 = num5;
				num4 += num6;
				num4 *= timeRatio;
			}
			num4 *= 1f - timeRatio;
			float num7 = this.posEnd.y - this.posStart.y;
			num4 /= num7;
			num4 += timeRatio;
			num4 = this.posStart.y + num4 * num7;
			return new Vector3(num, num4);
		}

		public override Vector3 GetDir(float timeRatio)
		{
			float num = 0.01f;
			return this.GetPos(timeRatio + num) - this.GetPos(timeRatio - num);
		}

		public override ProjectilePath GetCopy()
		{
			return new ProjectilePathWiggly
			{
				posStart = this.posStart,
				posEnd = this.posEnd,
				xcs = this.xcs,
				ycs = this.ycs
			};
		}

		public List<float> xcs;

		public List<float> ycs;
	}
}
