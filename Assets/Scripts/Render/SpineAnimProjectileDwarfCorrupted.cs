using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimProjectileDwarfCorrupted : SpineAnim
	{
		public SpineAnimProjectileDwarfCorrupted() : base(SpineAnimProjectileDwarfCorrupted.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;

		public static string[] skinNames = new string[]
		{
			"skin_1",
			"skin_2",
			"skin_3"
		};
	}
}
