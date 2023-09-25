using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimDruid : SpineAnim
	{
		public SpineAnimDruid() : base(SpineAnimDruid.Prefab)
		{
			base.SetRootBone("all_movement");
		}

		public static GameObject Prefab;

		public static Spine.Animation animIdle;

		public static Spine.Animation[] animAttacks;

		public static Spine.Animation animUltiStart;

		public static Spine.Animation animUltiRepeat;

		public static Spine.Animation animUltiEnd;

		public static Spine.Animation[] animsAuto;

		public static Spine.Animation animDeath;

		public static Spine.Animation animDeathTransformed;

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
			"skin_9"
		};

		public static readonly Vector3 NormalStateScale = Vector3.one;

		public static readonly Vector3 TransformedStateScale = Vector3.one * 1.8f;

		public const int LastAttackFrequency = 10;
	}
}
