using System;
using Simulation;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimChest : SpineAnim
	{
		public SpineAnimChest() : base(SpineAnimChest.Prefab)
		{
		}

		public static string GetSkinName(Simulator simulator)
		{
			if (simulator.halloweenEnabled)
			{
				return SpineAnimChest.skins[1];
			}
			if (simulator.IsChristmasTreeEnabled())
			{
				return SpineAnimChest.skins[2];
			}
			if (simulator.IsSecondAnniversaryEventEnabled())
			{
				return SpineAnimChest.skins[3];
			}
			return SpineAnimChest.skins[0];
		}

		public static GameObject Prefab;

		public static Spine.Animation animSpawnTranslate;

		public static Spine.Animation animSpawnDrop;

		public static Spine.Animation animIdle;

		public static Spine.Animation[] animsHit;

		public static Spine.Animation animDie;

		private static readonly string[] skins = new string[]
		{
			"goblin",
			"goblin_halloween",
			"goblin_xmas",
			"goblin_birthday"
		};
	}
}
