using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimTotemEarth : SpineAnim
	{
		public SpineAnimTotemEarth() : base(SpineAnimTotemEarth.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
