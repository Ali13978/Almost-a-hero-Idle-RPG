using System;
using Simulation;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimAdDragon : SpineAnim
	{
		public SpineAnimAdDragon() : base(SpineAnimAdDragon.Prefab)
		{
		}

		public static string GetSkinName(Simulator simulator, int defaultSkinIndex)
		{
			if (simulator.IsSecondAnniversaryEventEnabled())
			{
				return SpineAnimAdDragon.skins[2];
			}
			return SpineAnimAdDragon.skins[defaultSkinIndex];
		}

		public static GameObject Prefab;

		public static Spine.Animation animIdle;

		public static Spine.Animation animActivate;

		public static Spine.Animation animEscape;

		public static readonly string[] skins = new string[]
		{
			"addragon",
			"addragon_goblin",
			"addragon_pinata",
			"addragon_xmas",
			"addragon_anniversary",
			"addragon_goblin_tourist",
			"addragon_halloween"
		};
	}
}
