using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBuffCritChance : SpineAnim
	{
		public SpineAnimBuffCritChance() : base(SpineAnimBuffCritChance.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
