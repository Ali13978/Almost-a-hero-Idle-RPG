using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimSheela : SpineAnim
	{
		public SpineAnimSheela() : base(SpineAnimSheela.Prefab)
		{
			base.SetRootBone("all_movement");
		}

		public static GameObject Prefab;

		public static Spine.Animation animIdle;

		public static Spine.Animation[] animsAttack;

		public static Spine.Animation animUltiStart;

		public static Spine.Animation animUltiEnd;

		public static Spine.Animation[] animsAuto;

		public static Spine.Animation animDeath;

		public static Spine.Animation animReload;

		public static Spine.Animation animIdleUlti;

		public static Spine.Animation animAttackUlti;

		public static Spine.Animation animVictory;

		public static string[] skinNames = new string[]
		{
			"skin_1",
			"skin_2",
			"skin_3",
			"skin_4",
			"skin_5",
			"skin_6",
			"skin_7"
		};
	}
}
