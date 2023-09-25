using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimTotemEarthImpact : SpineAnim
	{
		public SpineAnimTotemEarthImpact() : base(SpineAnimTotemEarthImpact.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
