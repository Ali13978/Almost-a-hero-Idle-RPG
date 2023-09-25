using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimSnake : SpineAnim
	{
		public SpineAnimSnake() : base(SpineAnimSnake.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation animSpawn;

		public static Spine.Animation animIdle;

		public static Spine.Animation[] animsAttack;

		public static Spine.Animation animDie;

		public static string[] skinNames = new string[]
		{
			"skin1",
			"skin2"
		};
	}
}
