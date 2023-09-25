using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBossElf : SpineAnim
	{
		public SpineAnimBossElf() : base(SpineAnimBossElf.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation animSpawn;

		public static Spine.Animation animIdle;

		public static Spine.Animation[] animsAttack;

		public static Spine.Animation animDie;

		public static Spine.Animation animLeave;
	}
}
