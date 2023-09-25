using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffImmunity : SpineAnim
	{
		public SpineAnimBuffImmunity() : base(SpineAnimBuffImmunity.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
