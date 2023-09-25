using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimTotemEarthStarfall : SpineAnim
	{
		public SpineAnimTotemEarthStarfall() : base(SpineAnimTotemEarthStarfall.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;

		public static Spine.Animation animImpact;
	}
}
