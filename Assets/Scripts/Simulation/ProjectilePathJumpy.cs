using System;
using UnityEngine;

namespace Simulation
{
	public class ProjectilePathJumpy : ProjectilePath
	{
		public override void Init(Vector3 posStart, Vector3 posEnd)
		{
			base.Init(posStart, posEnd);
		}

		public override Vector3 GetPos(float timeRatio)
		{
			float jumpHeight = this.GetJumpHeight(timeRatio);
			Vector3 result = GameMath.Lerp(this.posStart, this.posEnd, Easing.SineEaseOut(timeRatio, 0f, 1f, 1f));
			result.y += jumpHeight;
			return result;
		}

		public float GetJumpHeight(float timeRatio)
		{
			int num = Mathf.FloorToInt(timeRatio * (float)this.jumpCount) + 1;
			if (this.dontJumpFirst && num == 1)
			{
				return 0f;
			}
			return this.heightAddMax * Mathf.Abs(GameMath.Sin((float)this.jumpCount * timeRatio * 3.14159274f)) / (float)num;
		}

		public override Vector3 GetDir(float timeRatio)
		{
			float num = 0.01f;
			return this.GetPos(timeRatio + num) - this.GetPos(timeRatio - num);
		}

		public override ProjectilePath GetCopy()
		{
			return new ProjectilePathJumpy
			{
				posStart = this.posStart,
				posEnd = this.posEnd,
				heightAddMax = this.heightAddMax,
				jumpCount = this.jumpCount,
				dontJumpFirst = this.dontJumpFirst
			};
		}

		public float heightAddMax;

		public int jumpCount;

		public bool dontJumpFirst;
	}
}
