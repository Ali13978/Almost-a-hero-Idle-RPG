using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimHumanSemiCorrupted : SpineAnim
	{
		public SpineAnimHumanSemiCorrupted() : base(SpineAnimHumanSemiCorrupted.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation animSpawnTranslate;

		public static Spine.Animation animSpawnDrop;

		public static Spine.Animation animIdle;

		public static Spine.Animation[] animsAttack;

		public static Spine.Animation animDie;

		public static string[] skinNames = new string[]
		{
			"skin1",
			"skin2",
			"skin 3"
		};
	}
}
