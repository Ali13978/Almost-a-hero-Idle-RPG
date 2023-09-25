using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimProjectileSheela : SpineAnim
	{
		public SpineAnimProjectileSheela() : base(SpineAnimProjectileSheela.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;

		public static string[] skinNames = new string[]
		{
			"skin_1",
			"skin_2",
			"skin_3",
			"skin_4",
			"skin_5",
			"skin_6"
		};
	}
}
