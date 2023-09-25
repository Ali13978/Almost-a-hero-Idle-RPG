using System;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Common.Util
{
	public static class IconManager
	{
		public static Texture2D GetIconFromHtmlColorString(string htmlString)
		{
			return IconManager.GetIconFromHtmlColorString(htmlString, Color.black);
		}

		public static Texture2D GetIconFromHtmlColorString(string htmlString, Color fallback)
		{
			return IconManager.GetIcon(fallback, 1, 1);
		}

		public static Texture2D GetIcon(Color color, int width = 1, int height = 1)
		{
			float key = color.r * 100000f + color.g * 10000f + color.b * 1000f + color.a * 100f + (float)width * 10f + (float)height;
			if (IconManager.s_colorIcons.ContainsKey(key) && IconManager.s_colorIcons[key] != null)
			{
				return IconManager.s_colorIcons[key];
			}
			Texture2D texture2D = new Texture2D(width, height);
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					texture2D.SetPixel(i, j, color);
				}
			}
			texture2D.Apply();
			IconManager.s_colorIcons[key] = texture2D;
			return IconManager.GetIcon(color, width, height);
		}

		public static Texture2D GetIconAtPath(string path)
		{
			if (IconManager.s_icons.ContainsKey(path))
			{
				return IconManager.s_icons[path];
			}
			Texture2D texture2D = Resources.Load(path) as Texture2D;
			if (texture2D == null)
			{
				texture2D = new Texture2D(1, 1);
			}
			IconManager.s_icons.Add(path, texture2D);
			return IconManager.GetIconAtPath(path);
		}

		public static Texture2D Rotate(Texture2D tex, float angle)
		{
			Texture2D texture2D = new Texture2D(tex.width, tex.height);
			int width = tex.width;
			int height = tex.height;
			float num = IconManager.rot_x(angle, (float)(-(float)width) / 2f, (float)(-(float)height) / 2f) + (float)width / 2f;
			float num2 = IconManager.rot_y(angle, (float)(-(float)width) / 2f, (float)(-(float)height) / 2f) + (float)height / 2f;
			float num3 = IconManager.rot_x(angle, 1f, 0f);
			float num4 = IconManager.rot_y(angle, 1f, 0f);
			float num5 = IconManager.rot_x(angle, 0f, 1f);
			float num6 = IconManager.rot_y(angle, 0f, 1f);
			float num7 = num;
			float num8 = num2;
			for (int i = 0; i < tex.width; i++)
			{
				float num9 = num7;
				float num10 = num8;
				for (int j = 0; j < tex.height; j++)
				{
					num9 += num3;
					num10 += num4;
					texture2D.SetPixel((int)Mathf.Floor((float)i), (int)Mathf.Floor((float)j), IconManager.getPixel(tex, num9, num10));
				}
				num7 += num5;
				num8 += num6;
			}
			texture2D.Apply();
			return texture2D;
		}

		private static Color getPixel(Texture2D tex, float x, float y)
		{
			int num = (int)Mathf.Floor(x);
			int num2 = (int)Mathf.Floor(y);
			Color result;
			if (num > tex.width || num < 0 || num2 > tex.height || num2 < 0)
			{
				result = Color.clear;
			}
			else
			{
				result = tex.GetPixel(num, num2);
			}
			return result;
		}

		private static float rot_x(float angle, float x, float y)
		{
			float num = Mathf.Cos(angle / 180f * 3.14159274f);
			float num2 = Mathf.Sin(angle / 180f * 3.14159274f);
			return x * num + y * -num2;
		}

		private static float rot_y(float angle, float x, float y)
		{
			float num = Mathf.Cos(angle / 180f * 3.14159274f);
			float num2 = Mathf.Sin(angle / 180f * 3.14159274f);
			return x * num2 + y * num;
		}

		private static Dictionary<string, Texture2D> s_icons = new Dictionary<string, Texture2D>();

		private static Dictionary<float, Texture2D> s_colorIcons = new Dictionary<float, Texture2D>();
	}
}
