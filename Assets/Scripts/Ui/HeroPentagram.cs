using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class HeroPentagram : AahMonoBehaviour
	{
		public void SetSprite(int evolveLevel)
		{
			this.baseLeft.color = HeroPentagram.BaseColors[evolveLevel];
			this.baseRight.color = HeroPentagram.BaseColors[evolveLevel];
			this.glowLeft.color = HeroPentagram.GlowColors[evolveLevel];
			this.glowRight.color = HeroPentagram.GlowColors[evolveLevel];
		}

		private static readonly Color[] GlowColors = new Color[]
		{
			Utility.HexColor("ce9f5e"),
			Utility.HexColor("b87f2f"),
			Utility.HexColor("b2db0d"),
			Utility.HexColor("24b6ff"),
			Utility.HexColor("9315f3"),
			Utility.HexColor("ff7e00"),
			Utility.HexColor("f52399")
		};

		private static readonly Color[] BaseColors = new Color[]
		{
			Utility.HexColor("faf0c6"),
			Utility.HexColor("ffdb92"),
			Utility.HexColor("d8f878"),
			Utility.HexColor("6fffff"),
			Utility.HexColor("ff81ff"),
			Utility.HexColor("ffed6e"),
			Utility.HexColor("ff83c6")
		};

		[SerializeField]
		private Image baseLeft;

		[SerializeField]
		private Image baseRight;

		[SerializeField]
		private Image glowLeft;

		[SerializeField]
		private Image glowRight;
	}
}
