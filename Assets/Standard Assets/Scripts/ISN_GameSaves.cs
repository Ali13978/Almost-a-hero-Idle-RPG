using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_GameSaves : Singleton<ISN_GameSaves>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_SaveRemoveResult> ActionSaveRemoved;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_SaveResult> ActionGameSaved;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_FetchResult> ActionSavesFetched;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_SavesResolveResult> ActionSavesResolved;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void SaveGame(byte[] data, string name)
	{
	}

	public void FetchSavedGames()
	{
	}

	public void DeleteSavedGame(string name)
	{
	}

	public void ResolveConflictingSavedGames(List<GK_SavedGame> conflicts, byte[] data)
	{
	}

	public void LoadSaveData(GK_SavedGame save)
	{
	}

	public void OnSaveSuccess(string data)
	{
		GK_SavedGame save = this.DeserializeGameSave(data);
		GK_SaveResult obj = new GK_SaveResult(save);
		ISN_GameSaves.ActionGameSaved(obj);
	}

	public void OnSaveFailed(string erroData)
	{
		GK_SaveResult obj = new GK_SaveResult(erroData);
		ISN_GameSaves.ActionGameSaved(obj);
	}

	public void OnFetchSuccess(string data)
	{
		List<GK_SavedGame> list = new List<GK_SavedGame>();
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == "endofline")
			{
				break;
			}
			GK_SavedGame item = this.DeserializeGameSave(array[i]);
			list.Add(item);
		}
		GK_FetchResult obj = new GK_FetchResult(list);
		ISN_GameSaves.ActionSavesFetched(obj);
	}

	public void OnFetchFailed(string errorData)
	{
		GK_FetchResult obj = new GK_FetchResult(errorData);
		ISN_GameSaves.ActionSavesFetched(obj);
	}

	public void OnResolveSuccess(string data)
	{
		List<GK_SavedGame> list = new List<GK_SavedGame>();
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == "endofline")
			{
				break;
			}
			GK_SavedGame item = this.DeserializeGameSave(array[i]);
			list.Add(item);
		}
		GK_SavesResolveResult obj = new GK_SavesResolveResult(list);
		ISN_GameSaves.ActionSavesResolved(obj);
	}

	public void OnResolveFailed(string errorData)
	{
		GK_SavesResolveResult obj = new GK_SavesResolveResult(errorData);
		ISN_GameSaves.ActionSavesResolved(obj);
	}

	public void OnDeleteSuccess(string name)
	{
		GK_SaveRemoveResult obj = new GK_SaveRemoveResult(name);
		ISN_GameSaves.ActionSaveRemoved(obj);
	}

	public void OnDeleteFailed(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		string name = array[0];
		string errorData = array[1];
		GK_SaveRemoveResult obj = new GK_SaveRemoveResult(name, errorData);
		ISN_GameSaves.ActionSaveRemoved(obj);
	}

	private void OnSaveDataLoaded(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		string key = array[0];
		string base64Data = array[1];
		if (ISN_GameSaves._CachedGameSaves.ContainsKey(key))
		{
			ISN_GameSaves._CachedGameSaves[key].GenerateDataLoadEvent(base64Data);
		}
	}

	private void OnSaveDataLoadFailed(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		string key = array[0];
		string erorrData = array[1];
		if (ISN_GameSaves._CachedGameSaves.ContainsKey(key))
		{
			ISN_GameSaves._CachedGameSaves[key].GenerateDataLoadFailedEvent(erorrData);
		}
	}

	private GK_SavedGame DeserializeGameSave(string serializedData)
	{
		string[] array = serializedData.Split(new char[]
		{
			'|'
		});
		string id = array[0];
		string name = array[1];
		string device = array[2];
		string dateString = array[3];
		GK_SavedGame gk_SavedGame = new GK_SavedGame(id, name, device, dateString);
		if (ISN_GameSaves._CachedGameSaves.ContainsKey(gk_SavedGame.Id))
		{
			ISN_GameSaves._CachedGameSaves[gk_SavedGame.Id] = gk_SavedGame;
		}
		else
		{
			ISN_GameSaves._CachedGameSaves.Add(gk_SavedGame.Id, gk_SavedGame);
		}
		return gk_SavedGame;
	}

	// Note: this type is marked as 'beforefieldinit'.
	static ISN_GameSaves()
	{
		ISN_GameSaves.ActionSaveRemoved = delegate(GK_SaveRemoveResult A_0)
		{
		};
		ISN_GameSaves.ActionGameSaved = delegate(GK_SaveResult A_0)
		{
		};
		ISN_GameSaves.ActionSavesFetched = delegate(GK_FetchResult A_0)
		{
		};
		ISN_GameSaves.ActionSavesResolved = delegate(GK_SavesResolveResult A_0)
		{
		};
		ISN_GameSaves._CachedGameSaves = new Dictionary<string, GK_SavedGame>();
	}

	private static Dictionary<string, GK_SavedGame> _CachedGameSaves;
}
