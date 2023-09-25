using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimSamBottleExplosion : SpineAnim
	{
		public SpineAnimSamBottleExplosion() : base(SpineAnimSamBottleExplosion.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
