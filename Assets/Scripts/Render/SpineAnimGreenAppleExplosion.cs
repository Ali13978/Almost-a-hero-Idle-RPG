using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimGreenAppleExplosion : SpineAnim
	{
		public SpineAnimGreenAppleExplosion() : base(SpineAnimGreenAppleExplosion.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
