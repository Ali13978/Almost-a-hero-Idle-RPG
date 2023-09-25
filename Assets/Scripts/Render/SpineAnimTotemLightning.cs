using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimTotemLightning : SpineAnim
	{
		public SpineAnimTotemLightning() : base(SpineAnimTotemLightning.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
