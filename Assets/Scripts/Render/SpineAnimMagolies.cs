using System;
using Spine;
using UnityEngine;

namespace Render
{
	public class SpineAnimMagolies : SpineAnim
	{
		public SpineAnimMagolies() : base(SpineAnimMagolies.Prefab)
		{
		}

		public static GameObject Prefab;

		public static Spine.Animation animSpawnTranslate;

		public static Spine.Animation animSpawnDrop;

		public static Spine.Animation animIdle;

		public static Spine.Animation[] animsAttack;

		public static Spine.Animation[] animsDie;

		public static string[] skinNames = new string[]
		{
			"skin1",
			"skin2",
			"skin3"
		};
	}
}
