using System;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;

namespace Simulation
{
	public class ProjectilePathMortar : ProjectilePath
	{
		public override void Init(Vector3 posStart, Vector3 posEnd)
		{
			base.Init(posStart, posEnd);
		}

		public override Vector3 GetPos(float timeRatio)
		{
			float num;
			if (timeRatio <= 0.5f)
			{
				num = EaseManager.Evaluate(Ease.OutQuad, null, timeRatio, 0.5f, 0f, 0f) * 0.5f;
			}
			else
			{
				num = 0.5f + EaseManager.Evaluate(Ease.InQuad, null, timeRatio - 0.5f, 0.5f, 0f, 0f) * 0.5f;
			}
			float num2 = num - 0.5f;
			num2 *= 2f;
			num2 *= num2;
			num2 = 1f - num2;
			num2 *= this.heightAddMax;
			Vector3 result = GameMath.Lerp(this.posStart, this.posEnd, timeRatio);
			result.y += num2;
			return result;
		}

		public override Vector3 GetDir(float timeRatio)
		{
			float num = 0.01f;
			return this.GetPos(timeRatio + num) - this.GetPos(timeRatio - num);
		}

		public override ProjectilePath GetCopy()
		{
			return new ProjectilePathMortar
			{
				posStart = this.posStart,
				posEnd = this.posEnd,
				heightAddMax = this.heightAddMax
			};
		}

		public float heightAddMax;
	}
}
