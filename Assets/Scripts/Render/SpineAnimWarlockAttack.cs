using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimWarlockAttack : SpineAnim
	{
		public SpineAnimWarlockAttack() : base(SpineAnimWarlockAttack.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
