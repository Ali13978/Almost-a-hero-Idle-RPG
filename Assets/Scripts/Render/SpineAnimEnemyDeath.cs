using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimEnemyDeath : SpineAnim
	{
		public SpineAnimEnemyDeath() : base(SpineAnimEnemyDeath.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation anim;

		public static string[] skinNames = new string[]
		{
			"common",
			"corrupted",
			"half_corrupted",
			"snakes"
		};
	}
}
