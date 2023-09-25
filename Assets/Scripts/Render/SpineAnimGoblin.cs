using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimGoblin : SpineAnim
	{
		public SpineAnimGoblin() : base(SpineAnimGoblin.Prefab)
		{
			base.SetRootBone("all_movement");
		}

		public static void Init(SkeletonData animData)
		{
			SpineAnimGoblin.animsIdle = new Spine.Animation[]
			{
				animData.FindAnimation("idle_3"),
				animData.FindAnimation("idle_2")
			};
			SpineAnimGoblin.animsAttack = new Spine.Animation[]
			{
				animData.FindAnimation("attack_1"),
				animData.FindAnimation("attack_2")
			};
			SpineAnimGoblin.animUlti = animData.FindAnimation("ultimate");
			SpineAnimGoblin.animsAuto = new Spine.Animation[]
			{
				animData.FindAnimation("passive_1"),
				animData.FindAnimation("passive_2")
			};
			SpineAnimGoblin.animDeath = animData.FindAnimation("death");
		}

		public static GameObject Prefab;

		public static Spine.Animation[] animsIdle;

		public static Spine.Animation[] animsAttack;

		public static Spine.Animation animUlti;

		public static Spine.Animation[] animsAuto;

		public static Spine.Animation animDeath;

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
