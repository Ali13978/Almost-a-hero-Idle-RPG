using System;
using Simulation;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBossHuman : SpineAnim
	{
		public SpineAnimBossHuman() : base(SpineAnimBossHuman.Prefab)
		{
		}

		public static string GetSkinName(Simulator simulator)
		{
			if (simulator.halloweenEnabled)
			{
				return SpineAnimBossHuman.Skins[1];
			}
			return SpineAnimBossHuman.Skins[0];
		}

		public static GameObject Prefab;

		public static Spine.Animation animSpawn;

		public static Spine.Animation animIdle;

		public static Spine.Animation[] animsAttack;

		public static Spine.Animation animDie;

		public static Spine.Animation animLeave;

		private static readonly string[] Skins = new string[]
		{
			"skin1",
			"skin2"
		};
	}
}
