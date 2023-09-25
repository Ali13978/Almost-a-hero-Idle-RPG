using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBabuProjectile : SpineAnim
	{
		public SpineAnimBabuProjectile() : base(SpineAnimBabuProjectile.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation loop;

		public static Spine.Animation explode;
	}
}
