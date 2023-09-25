using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimProjectileHiltExcalibur : SpineAnim
	{
		public SpineAnimProjectileHiltExcalibur() : base(SpineAnimProjectileHiltExcalibur.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;
	}
}
