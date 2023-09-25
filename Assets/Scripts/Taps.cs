using System;
using System.Collections.Generic;
using UnityEngine;

public class Taps
{
	public Taps()
	{
		this.newSimPoses = new List<Vector3>();
		this.oldSimPoses = new List<Vector3>();
	}

	public void Reset()
	{
		this.newSimPoses.Clear();
		this.oldSimPoses.Clear();
	}

	public void AddNewSimPos(Vector3 newPos)
	{
		this.newSimPoses.Add(newPos);
	}

	public void AddOldSimPos(Vector3 newPos)
	{
		this.oldSimPoses.Add(newPos);
	}

	public bool HasAtLeastOneNew()
	{
		return this.newSimPoses.Count > 0;
	}

	public bool HasAtLeastOneOld()
	{
		return this.oldSimPoses.Count > 0;
	}

	public bool HasAny()
	{
		return this.HasAtLeastOneNew() || this.HasAtLeastOneOld();
	}

	public bool HasNoNew()
	{
		return this.newSimPoses.Count == 0;
	}

	public bool HasNoOld()
	{
		return this.oldSimPoses.Count == 0;
	}

	public bool HasNone()
	{
		return this.HasNoNew() && this.HasNoOld();
	}

	public Vector3 GetAnyNew()
	{
		return this.newSimPoses[0];
	}

	public Vector3 GetAnyOld()
	{
		return this.oldSimPoses[0];
	}

	public Vector3 GetAny()
	{
		if (this.oldSimPoses.Count > 0)
		{
			return this.oldSimPoses[0];
		}
		return this.newSimPoses[0];
	}

	public int GetNumNew()
	{
		return this.newSimPoses.Count;
	}

	public int GetNumOld()
	{
		return this.oldSimPoses.Count;
	}

	public Vector3 GetNewSimTap(int index)
	{
		return this.newSimPoses[index];
	}

	public Vector3 GetOldSimTap(int index)
	{
		return this.oldSimPoses[index];
	}

	public IEnumerable<Vector3> GetAll()
	{
		foreach (Vector3 p in this.newSimPoses)
		{
			yield return p;
		}
		foreach (Vector3 p2 in this.oldSimPoses)
		{
			yield return p2;
		}
		yield break;
	}

	public static Vector3 GetRandomAutoTapPos()
	{
		return new Vector3(GameMath.GetRandomFloat(-0.5f, 0f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.5f, 0.5f, GameMath.RandType.NoSeed));
	}

	public static void AddTouchOnUi(int fingerId, bool isOnUi)
	{
		if (!Taps.touchStartedOnUi.ContainsKey(fingerId))
		{
			Taps.touchStartedOnUi.Add(fingerId, isOnUi);
		}
	}

	public static bool WasTouchOnUi(int fingerId)
	{
		return Taps.touchStartedOnUi != null && Taps.touchStartedOnUi.ContainsKey(fingerId) && Taps.touchStartedOnUi[fingerId];
	}

	public static void ResetTouchStartedOnUi()
	{
		if (Taps.touchStartedOnUi.Count > 0)
		{
			Taps.touchStartedOnUi.Clear();
		}
	}

	private List<Vector3> newSimPoses = new List<Vector3>();

	private List<Vector3> oldSimPoses = new List<Vector3>();

	private static Dictionary<int, bool> touchStartedOnUi = new Dictionary<int, bool>();

	public static bool mouseStartedOnUi;

	public static readonly Vector3 ICE_RING_AUTO_CHARGE_POS = Vector3.zero;
}
