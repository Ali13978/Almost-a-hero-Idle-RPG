using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimProjectileBombermanFirework : SpineAnim
	{
		public SpineAnimProjectileBombermanFirework() : base(SpineAnimProjectileBombermanFirework.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
