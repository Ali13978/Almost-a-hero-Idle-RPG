using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffCritDamage : SpineAnim
	{
		public SpineAnimBuffCritDamage() : base(SpineAnimBuffCritDamage.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
