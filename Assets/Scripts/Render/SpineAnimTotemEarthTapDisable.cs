using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimTotemEarthTapDisable : SpineAnim
	{
		public SpineAnimTotemEarthTapDisable() : base(SpineAnimTotemEarthTapDisable.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
