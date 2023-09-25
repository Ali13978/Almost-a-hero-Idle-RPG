using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimDuck : SpineAnim
	{
		public SpineAnimDuck() : base(SpineAnimDuck.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
