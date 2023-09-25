using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimBat : SpineAnim
	{
		public SpineAnimBat() : base(SpineAnimBat.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation animSpawnDrop;

		public static Spine.Animation animIdle;

		public static Spine.Animation[] animsAttack;

		public static Spine.Animation animDie;
	}
}
