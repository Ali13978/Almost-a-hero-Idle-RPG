using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffDefenseless : SpineAnim
	{
		public SpineAnimBuffDefenseless() : base(SpineAnimBuffDefenseless.Prefab)
		{
		}

		public static GameObject Prefab;

		public static float DUR_FADE_IN = 1.667f;

		public static float DUR_FADE_OUT = 0.5f;

		public static Spine.Animation start;

		public static Spine.Animation loop;

		public static Spine.Animation end;
	}
}
