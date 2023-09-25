using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffShield : SpineAnim
	{
		public SpineAnimBuffShield() : base(SpineAnimBuffShield.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
