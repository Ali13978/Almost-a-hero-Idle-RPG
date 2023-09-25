using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBabu : SpineAnim
	{
		public SpineAnimBabu() : base(SpineAnimBabu.Prefab)
		{
			base.SetRootBone("all_movement");
		}

		public static GameObject Prefab;

		public static Spine.Animation[] animsIdle;

		public static Spine.Animation[] animsAttack;

		public static Spine.Animation animUltiStart;

		public static Spine.Animation animUltiLoop;

		public static Spine.Animation animUltiEnd;

		public static Spine.Animation[] animsAuto;

		public static Spine.Animation animDeath;

		public static Spine.Animation animDeathUlti;

		public static Spine.Animation animVictory;

		public static string[] skinNames = new string[]
		{
			"skin_1",
			"skin_2",
			"skin_3",
			"skin_4",
			"skin_5",
			"skin_6",
			"skin_7",
			"skin_8",
			"skin_9",
			"skin_10"
		};
	}
}
