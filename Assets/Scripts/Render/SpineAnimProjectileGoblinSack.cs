using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimProjectileGoblinSack : SpineAnim
	{
		public SpineAnimProjectileGoblinSack() : base(SpineAnimProjectileGoblinSack.Prefab)
		{
		}

		public static GameObject Prefab;

		public static string[] skinNames = new string[]
		{
			"skin_1",
			"skin_6",
			"skin_7"
		};

		public static Spine.Animation anim;
	}
}
