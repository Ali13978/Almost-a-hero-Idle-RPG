using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimTotemSmoke : SpineAnim
	{
		public SpineAnimTotemSmoke() : base(SpineAnimTotemSmoke.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
