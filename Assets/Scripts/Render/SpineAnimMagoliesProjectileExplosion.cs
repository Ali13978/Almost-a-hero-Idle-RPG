using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimMagoliesProjectileExplosion : SpineAnim
	{
		public SpineAnimMagoliesProjectileExplosion() : base(SpineAnimMagoliesProjectileExplosion.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
