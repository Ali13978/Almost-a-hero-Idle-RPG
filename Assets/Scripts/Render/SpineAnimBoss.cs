using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBoss : SpineAnim
	{
		public SpineAnimBoss() : base(SpineAnimBoss.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation animSpawn;

		public static Spine.Animation animIdle;

		public static Spine.Animation animAttackProjectile;

		public static Spine.Animation animAttackMelee;

		public static Spine.Animation animDie;

		public static Spine.Animation animLeave;

		public static string[] skinNames = new string[]
		{
			"skin_1",
			"skin_2",
			"skin_3"
		};
	}
}
