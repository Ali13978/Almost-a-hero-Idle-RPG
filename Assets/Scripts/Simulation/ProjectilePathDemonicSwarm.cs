using System;
using UnityEngine;

namespace Simulation
{
	public class ProjectilePathDemonicSwarm : ProjectilePath
	{
		public override void Init(Vector3 posStart, Vector3 posEnd)
		{
			this.posStart = posStart;
			this.posEnd = posEnd;
			this.hadd = GameMath.GetRandomFloat(-this.heightAddMax, this.heightAddMax, GameMath.RandType.NoSeed);
			this.timeMult = 1f / (1f - this.linearDur);
		}

		public override Vector3 GetPos(float timeRatio)
		{
			Vector3 result = default(Vector3);
			if (timeRatio <= this.linearDur)
			{
				result = GameMath.Lerp(this.posStart, new Vector3(this.posEnd.x * 2f, this.posStart.y, this.posEnd.z), timeRatio);
			}
			else
			{
				float num = (timeRatio - this.linearDur) * this.timeMult - 0.5f;
				num *= 2f;
				num *= num;
				num = 1f - num;
				num *= this.hadd;
				Vector3 a = GameMath.Lerp(this.posStart, new Vector3(this.posEnd.x * 2f, this.posStart.y, this.posEnd.z), this.linearDur);
				result = GameMath.Lerp(a, this.posEnd, (timeRatio - this.linearDur) * this.timeMult);
				result.y += num;
			}
			return result;
		}

		public override Vector3 GetDir(float timeRatio)
		{
			float num = 0.01f;
			return this.GetPos(timeRatio + num) - this.GetPos(timeRatio - num);
		}

		public override ProjectilePath GetCopy()
		{
			return new ProjectilePathDemonicSwarm
			{
				posStart = this.posStart,
				posEnd = this.posEnd,
				linearDur = this.linearDur,
				timeMult = this.timeMult,
				heightAddMax = this.heightAddMax,
				hadd = this.hadd
			};
		}

		private float linearDur = 0.15f;

		private float timeMult;

		public float heightAddMax;

		private float hadd;
	}
}
