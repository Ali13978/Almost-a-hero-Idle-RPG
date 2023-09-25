using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBossWiseSnake : SpineAnim
	{
		public SpineAnimBossWiseSnake() : base(SpineAnimBossWiseSnake.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation animSpawn;

		public static Spine.Animation animIdle;

		public static Spine.Animation[] animsAttack;

		public static Spine.Animation animDie;

		public static Spine.Animation animHurt;

		public static Spine.Animation animSummon;
	}
}
