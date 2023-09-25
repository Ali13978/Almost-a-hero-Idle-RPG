using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimProjectileTotemShard : SpineAnim
	{
		public SpineAnimProjectileTotemShard() : base(SpineAnimProjectileTotemShard.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
