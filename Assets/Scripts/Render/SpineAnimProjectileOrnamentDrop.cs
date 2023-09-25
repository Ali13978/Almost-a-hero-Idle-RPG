using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimProjectileOrnamentDrop : SpineAnim
	{
		public SpineAnimProjectileOrnamentDrop() : base(SpineAnimProjectileOrnamentDrop.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation loop;

		public static Spine.Animation explode;
	}
}
