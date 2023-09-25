using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimSnakeProjectileExplosion : SpineAnim
	{
		public SpineAnimSnakeProjectileExplosion() : base(SpineAnimSnakeProjectileExplosion.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
