using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimDwarfSemiCorrupted : SpineAnim
	{
		public SpineAnimDwarfSemiCorrupted() : base(SpineAnimDwarfSemiCorrupted.Prefab)
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
			"skin_1",
			"skin_2",
			"skin_3"
		};
	}
}
