using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffAttackSlow : SpineAnim
	{
		public SpineAnimBuffAttackSlow() : base(SpineAnimBuffAttackSlow.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation start;

		public static Spine.Animation loop;

		public static Spine.Animation end;

		public static float DUR_FADE_IN = 0.6f;

		public static float DUR_FADE_OUT = 0.833f;
	}
}
