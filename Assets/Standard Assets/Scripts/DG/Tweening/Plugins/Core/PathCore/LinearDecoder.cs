using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
	internal class LinearDecoder : ABSPathDecoder
	{
		internal override void FinalizePath(Path p, Vector3[] wps, bool isClosedPath)
		{
			p.controlPoints = null;
			p.subdivisions = wps.Length * p.subdivisionsXSegment;
			this.SetTimeToLengthTables(p, p.subdivisions);
		}

		internal override Vector3 GetPoint(float perc, Vector3[] wps, Path p, ControlPoint[] controlPoints)
		{
			if (perc <= 0f)
			{
				p.linearWPIndex = 1;
				return wps[0];
			}
			int num = 0;
			int num2 = 0;
			int num3 = p.timesTable.Length;
			for (int i = 1; i < num3; i++)
			{
				if (p.timesTable[i] >= perc)
				{
					num = i - 1;
					num2 = i;
					break;
				}
			}
			float num4 = p.timesTable[num];
			float num5 = perc - num4;
			float maxLength = p.length * num5;
			Vector3 vector = wps[num];
			Vector3 a = wps[num2];
			p.linearWPIndex = num2;
			return vector + Vector3.ClampMagnitude(a - vector, maxLength);
		}

		internal void SetTimeToLengthTables(Path p, int subdivisions)
		{
			float num = 0f;
			int num2 = p.wps.Length;
			float[] array = new float[num2];
			Vector3 b = p.wps[0];
			for (int i = 0; i < num2; i++)
			{
				Vector3 vector = p.wps[i];
				float num3 = Vector3.Distance(vector, b);
				num += num3;
				b = vector;
				array[i] = num3;
			}
			float[] array2 = new float[num2];
			float num4 = 0f;
			for (int j = 1; j < num2; j++)
			{
				num4 += array[j];
				array2[j] = num4 / num;
			}
			p.length = num;
			p.wpLengths = array;
			p.timesTable = array2;
		}

		internal void SetWaypointsLengths(Path p, int subdivisions)
		{
		}
	}
}
