using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimProjectileDerekMeteor : SpineAnim
	{
		public SpineAnimProjectileDerekMeteor() : base(SpineAnimProjectileDerekMeteor.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;

		public static Spine.Animation animBook;
	}
}
