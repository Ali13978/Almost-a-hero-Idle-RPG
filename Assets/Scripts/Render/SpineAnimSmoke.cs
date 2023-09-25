using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimSmoke : SpineAnim
	{
		public SpineAnimSmoke() : base(SpineAnimSmoke.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
