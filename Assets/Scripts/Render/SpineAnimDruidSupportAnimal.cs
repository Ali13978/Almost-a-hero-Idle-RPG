using System;
using System.Collections.Generic;
using Simulation;
using Spine;
using UnityEngine;

namespace Render
{
	public abstract class SpineAnimDruidSupportAnimal : SpineAnim
	{
		static SpineAnimDruidSupportAnimal()
		{
			List<SupportAnimal.Skin> enumList = Utility.GetEnumList<SupportAnimal.Skin>();
			foreach (SupportAnimal.Skin key in enumList)
			{
				SpineAnimDruidSupportAnimal.AnimDataPerAnimal.Add((int)key, new SpineAnimDruidSupportAnimal.AnimData());
			}
		}

		public SpineAnimDruidSupportAnimal(GameObject prefab) : base(prefab)
		{
		}

		public static readonly Dictionary<int, SpineAnimDruidSupportAnimal.AnimData> AnimDataPerAnimal = new Dictionary<int, SpineAnimDruidSupportAnimal.AnimData>();

		public class AnimData
		{
			public GameObject Prefab;

			public Spine.Animation run;

			public Spine.Animation giveBuff;
		}
	}
}
