using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SA.Common.Animation;
using SA.Common.Util;
using UnityEngine;

public static class SA_UnityExtensions
{
	public static void MoveTo(this GameObject go, Vector3 position, float time, EaseType easeType = EaseType.linear, Action OnCompleteAction = null)
	{
		ValuesTween valuesTween = go.AddComponent<ValuesTween>();
		valuesTween.DestoryGameObjectOnComplete = false;
		valuesTween.VectorTo(go.transform.position, position, time, easeType);
		valuesTween.OnComplete += OnCompleteAction;
	}

	public static void ScaleTo(this GameObject go, Vector3 scale, float time, EaseType easeType = EaseType.linear, Action OnCompleteAction = null)
	{
		ValuesTween valuesTween = go.AddComponent<ValuesTween>();
		valuesTween.DestoryGameObjectOnComplete = false;
		valuesTween.ScaleTo(go.transform.localScale, scale, time, easeType);
		valuesTween.OnComplete += OnCompleteAction;
	}

	public static Bounds GetRealBounds(this GameObject go)
	{
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(false);
		Bounds result = new Bounds(go.transform.position, Vector3.zero);
		foreach (Renderer renderer in componentsInChildren)
		{
			if (renderer.bounds.size != Vector3.zero)
			{
				result.Encapsulate(renderer.bounds);
			}
		}
		return result;
	}

	public static Bounds GetRealBounds(this Component go)
	{
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(false);
		Bounds result = new Bounds(go.transform.position, Vector3.zero);
		foreach (Renderer renderer in componentsInChildren)
		{
			if (renderer.bounds.size != Vector3.zero)
			{
				result.Encapsulate(renderer.bounds);
			}
		}
		return result;
	}

	public static Bounds GetRendererBounds(this GameObject go)
	{
		return SA_UnityExtensions.CalculateBounds(go);
	}

	public static Vector3 GetVertex(this GameObject go, VertexX x, VertexY y, VertexZ z)
	{
		Bounds rendererBounds = go.GetRendererBounds();
		return rendererBounds.GetVertex(x, y, z);
	}

	public static void Reset(this GameObject go)
	{
		go.transform.Reset();
	}

	public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}

	public static void Reset(this Transform t)
	{
		t.localScale = Vector3.one;
		t.localPosition = Vector3.zero;
		t.localRotation = Quaternion.identity;
	}

	public static Bounds GetRendererBounds(this Transform t)
	{
		return t.gameObject.GetRendererBounds();
	}

	public static Vector3 GetVertex(this Transform t, VertexX x, VertexY y, VertexZ z)
	{
		return t.gameObject.GetVertex(x, y, z);
	}

	public static Transform Clear(this Transform transform)
	{
		if (transform.childCount == 0)
		{
			return transform;
		}
		Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>();
		foreach (Transform transform2 in componentsInChildren)
		{
			if (transform2 != transform && transform2 != null)
			{
				UnityEngine.Object.DestroyImmediate(transform2.gameObject);
			}
		}
		return transform;
	}

	public static Vector3 GetVertex(this Bounds bounds, VertexX x, VertexY y, VertexZ z)
	{
		Vector3 center = bounds.center;
		if (x != VertexX.Right)
		{
			if (x == VertexX.Left)
			{
				center.x += bounds.extents.x;
			}
		}
		else
		{
			center.x -= bounds.extents.x;
		}
		if (y != VertexY.Bottom)
		{
			if (y == VertexY.Top)
			{
				center.y += bounds.extents.y;
			}
		}
		else
		{
			center.y -= bounds.extents.y;
		}
		if (z != VertexZ.Back)
		{
			if (z == VertexZ.Front)
			{
				center.z += bounds.extents.z;
			}
		}
		else
		{
			center.z -= bounds.extents.z;
		}
		return center;
	}

	public static void SetAlpha(this Material material, float value)
	{
		if (material.HasProperty("_Color"))
		{
			Color color = material.color;
			color.a = value;
			material.color = color;
		}
	}

	public static Sprite ToSprite(this Texture2D texture)
	{
		return Sprite.Create(texture, new Rect(0f, 0f, (float)texture.width, (float)texture.height), new Vector2(0.5f, 0.5f));
	}

	public static Texture2D Rotate(this Texture2D texture, float angle)
	{
		return IconManager.Rotate(texture, angle);
	}

	public static Vector3 Reset(this Vector3 vec)
	{
		return Vector3.zero;
	}

	public static Vector3 ResetXCoord(this Vector3 vec)
	{
		Vector3 result = vec;
		result.x = 0f;
		return result;
	}

	public static Vector3 ResetYCoord(this Vector3 vec)
	{
		Vector3 result = vec;
		result.y = 0f;
		return result;
	}

	public static Vector3 ResetZCoord(this Vector3 vec)
	{
		Vector3 result = vec;
		result.z = 0f;
		return result;
	}

	public static Vector3 Average(this Vector3[] f)
	{
		Vector3 vector = Vector3.zero;
		int num = 0;
		for (int i = 0; i < f.Length - 1; i++)
		{
			if (f[i] != Vector3.zero)
			{
				vector += f[i];
				num++;
			}
		}
		num = ((num <= 0) ? 1 : num);
		vector.x /= (float)num;
		vector.y /= (float)num;
		vector.z /= (float)num;
		return vector;
	}

	public static string GetLast(this string source, int count)
	{
		if (count >= source.Length)
		{
			return source;
		}
		return source.Substring(source.Length - count);
	}

	public static string GetFirst(this string source, int count)
	{
		if (count >= source.Length)
		{
			return source;
		}
		return source.Substring(0, count);
	}

	public static void CopyToClipboard(this string source)
	{
	}

	public static Uri CovertToURI(this string source)
	{
		return new Uri(source);
	}

	public static Dictionary<string, string> ParseQueryString(this Uri uri)
	{
		Match match = SA_UnityExtensions._regex.Match(uri.PathAndQuery);
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		while (match.Success)
		{
			dictionary.Add(match.Groups[1].Value, match.Groups[2].Value);
			match = match.NextMatch();
		}
		return dictionary;
	}

	public static Bounds CalculateBounds(GameObject obj)
	{
		bool flag = false;
		Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
		Renderer[] componentsInChildren = obj.GetComponentsInChildren<Renderer>();
		Renderer component = obj.GetComponent<Renderer>();
		if (component != null)
		{
			bounds = component.bounds;
			flag = true;
		}
		foreach (Renderer renderer in componentsInChildren)
		{
			if (!flag)
			{
				bounds = renderer.bounds;
				flag = true;
			}
			else
			{
				bounds.Encapsulate(renderer.bounds);
			}
		}
		return bounds;
	}

	private static readonly Regex _regex = new Regex("[?|&]([\\w\\.]+)=([^?|^&]+)");
}
