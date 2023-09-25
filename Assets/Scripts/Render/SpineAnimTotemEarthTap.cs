using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimTotemEarthTap : SpineAnim
	{
		public SpineAnimTotemEarthTap() : base(SpineAnimTotemEarthTap.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
