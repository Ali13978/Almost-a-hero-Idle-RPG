using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffAttackFast : SpineAnim
	{
		public SpineAnimBuffAttackFast() : base(SpineAnimBuffAttackFast.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
