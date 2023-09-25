using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBombermanExplosion : SpineAnim
	{
		public SpineAnimBombermanExplosion() : base(SpineAnimBombermanExplosion.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
