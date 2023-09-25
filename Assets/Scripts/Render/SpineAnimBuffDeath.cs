using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffDeath : SpineAnim
	{
		public SpineAnimBuffDeath() : base(SpineAnimBuffDeath.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
