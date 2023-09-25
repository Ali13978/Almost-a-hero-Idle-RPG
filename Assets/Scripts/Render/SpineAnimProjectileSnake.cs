using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimProjectileSnake : SpineAnim
	{
		public SpineAnimProjectileSnake() : base(SpineAnimProjectileSnake.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
