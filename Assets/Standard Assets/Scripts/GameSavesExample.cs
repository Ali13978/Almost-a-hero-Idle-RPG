using System;
using System.Collections.Generic;
using System.Text;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class GameSavesExample : BaseIOSFeaturePreview
{
	private void Awake()
	{
	}

	private void OnGUI()
	{
		base.UpdateToStartPos();
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "GameCenter Game Saves", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Init"))
		{
			GameCenterManager.OnAuthFinished += this.HandleOnAuthFinished;
			GameCenterManager.Init();
		}
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Save Game"))
		{
			this.Save(this.test_name);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Save Game 2"))
		{
			this.Save(this.test_name_2);
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Fetch Saved Games"))
		{
			this.Fetch();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Load Saved Game"))
		{
			this.Load();
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Delete Saved Game"))
		{
			this.Delete(this.test_name);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Delete Saved Game 2"))
		{
			this.Delete(this.test_name_2);
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Resolve Conflicts"))
		{
			this.ResolveConflicts();
		}
	}

	private void Save(string name)
	{
		ISN_Logger.Log("Start to save game!", LogType.Log);
		ISN_GameSaves.ActionGameSaved += this.HandleActionGameSaved;
		byte[] bytes = Encoding.UTF8.GetBytes("Some data");
		Singleton<ISN_GameSaves>.Instance.SaveGame(bytes, name);
	}

	private void Fetch()
	{
		ISN_Logger.Log("Start to fetch games!", LogType.Log);
		ISN_GameSaves.ActionSavesFetched += this.HandleActionSavesFetched;
		Singleton<ISN_GameSaves>.Instance.FetchSavedGames();
	}

	private void Delete(string name)
	{
		ISN_Logger.Log("Start to delete game by name!", LogType.Log);
		ISN_GameSaves.ActionSaveRemoved += this.HandleActionSaveRemoved;
		Singleton<ISN_GameSaves>.Instance.DeleteSavedGame(name);
	}

	private void Load()
	{
		ISN_Logger.Log("Start to load game!", LogType.Log);
		GK_SavedGame loadedSave = this.GetLoadedSave(this.test_name);
		if (loadedSave == null)
		{
			ISN_Logger.Log("You don't have any saved game!", LogType.Log);
			return;
		}
		loadedSave.ActionDataLoaded += this.HandleActionDataLoaded;
		loadedSave.LoadData();
	}

	private void ResolveConflicts()
	{
		ISN_Logger.Log("Trying to fix conflicts", LogType.Log);
		List<GK_SavedGame> conflict = this.GetConflict();
		if (conflict == null)
		{
			ISN_Logger.Log("You don't have any conflicts!", LogType.Log);
			return;
		}
		ISN_GameSaves.ActionSavesResolved += this.HandleActionSavesResolved;
		byte[] bytes = Encoding.UTF8.GetBytes("Some data after resolving");
		Singleton<ISN_GameSaves>.Instance.ResolveConflictingSavedGames(conflict, bytes);
	}

	private GK_SavedGame GetLoadedSave(string saveGameName)
	{
		return (!this.GameSaves.ContainsKey(saveGameName)) ? null : this.GameSaves[saveGameName][0];
	}

	private List<GK_SavedGame> GetConflict()
	{
		List<GK_SavedGame> result = null;
		using (Dictionary<string, List<GK_SavedGame>>.ValueCollection.Enumerator enumerator = this.SavesConflicts.Values.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				List<GK_SavedGame> list = enumerator.Current;
				result = list;
				return result;
			}
		}
		return result;
	}

	private int GetConflictsCount()
	{
		ISN_Logger.Log("The total number of duplicates =" + this.SavesConflicts.Count, LogType.Log);
		return this.SavesConflicts.Count;
	}

	private void CheckSavesOnDuplicates()
	{
		Dictionary<string, List<GK_SavedGame>> dictionary = new Dictionary<string, List<GK_SavedGame>>(this.GameSaves);
		foreach (KeyValuePair<string, List<GK_SavedGame>> keyValuePair in dictionary)
		{
			if (keyValuePair.Value.Count > 1)
			{
				if (!this.SavesConflicts.ContainsKey(keyValuePair.Key))
				{
					this.SavesConflicts.Add(keyValuePair.Key, keyValuePair.Value);
				}
				this.GameSaves.Remove(keyValuePair.Key);
			}
		}
		ISN_Logger.Log("------------------------------------------", LogType.Log);
		ISN_Logger.Log("Duplicates " + this.SavesConflicts.Count, LogType.Log);
		ISN_Logger.Log("Unique saves " + this.GameSaves.Count, LogType.Log);
		ISN_Logger.Log("------------------------------------------", LogType.Log);
	}

	private void HandleOnAuthFinished(Result result)
	{
		GameCenterManager.OnAuthFinished -= this.HandleOnAuthFinished;
		if (result.IsSucceeded)
		{
			ISN_Logger.Log("Player Authed", LogType.Log);
		}
		else
		{
			IOSNativePopUpManager.showMessage("Game Center ", "Player authentication failed");
		}
	}

	private void HandleActionGameSaved(GK_SaveResult res)
	{
		ISN_GameSaves.ActionGameSaved -= this.HandleActionGameSaved;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Saved game with name " + res.SavedGame.Name, LogType.Log);
			ISN_Logger.Log("------------------------------------------", LogType.Log);
		}
		else
		{
			ISN_Logger.Log("Failed: " + res.Error.Message, LogType.Log);
		}
	}

	private void HandleActionSaveRemoved(GK_SaveRemoveResult res)
	{
		ISN_GameSaves.ActionSaveRemoved -= this.HandleActionSaveRemoved;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Deleted game with name " + res.SaveName, LogType.Log);
			ISN_Logger.Log("------------------------------------------", LogType.Log);
		}
		else
		{
			ISN_Logger.Log("Failed: " + res.Error.Message, LogType.Log);
		}
	}

	private void HandleActionDataLoaded(GK_SaveDataLoaded res)
	{
		res.SavedGame.ActionDataLoaded -= this.HandleActionDataLoaded;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Data loaded. data Length: " + res.SavedGame.Data.Length, LogType.Log);
		}
		else
		{
			ISN_Logger.Log("Failed: " + res.Error.Message, LogType.Log);
		}
	}

	private void HandleActionSavesFetched(GK_FetchResult res)
	{
		ISN_GameSaves.ActionSavesFetched -= this.HandleActionSavesFetched;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Received " + res.SavedGames.Count + " game saves", LogType.Log);
			foreach (GK_SavedGame gk_SavedGame in res.SavedGames)
			{
				ISN_Logger.Log("The name of the save game " + gk_SavedGame.Name, LogType.Log);
			}
			ISN_Logger.Log("------------------------------------------", LogType.Log);
			this.GameSaves.Clear();
			foreach (GK_SavedGame gk_SavedGame2 in res.SavedGames)
			{
				if (!this.GameSaves.ContainsKey(gk_SavedGame2.Name))
				{
					this.GameSaves.Add(gk_SavedGame2.Name, new List<GK_SavedGame>());
				}
				this.GameSaves[gk_SavedGame2.Name].Add(gk_SavedGame2);
			}
			ISN_Logger.Log("Check the saves on duplicates", LogType.Log);
			this.CheckSavesOnDuplicates();
		}
		else
		{
			ISN_Logger.Log(string.Concat(new object[]
			{
				"Failed: ",
				res.Error.Message,
				" with code ",
				res.Error.Code
			}), LogType.Log);
		}
	}

	private void HandleActionSavesResolved(GK_SavesResolveResult res)
	{
		ISN_GameSaves.ActionSavesResolved -= this.HandleActionSavesResolved;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("The conflict is resolved", LogType.Log);
			foreach (GK_SavedGame gk_SavedGame in res.SavedGames)
			{
				this.SavesConflicts.Remove(gk_SavedGame.Name);
				if (!this.GameSaves.ContainsKey(gk_SavedGame.Name))
				{
					this.GameSaves.Add(gk_SavedGame.Name, new List<GK_SavedGame>());
					this.GameSaves[gk_SavedGame.Name].Add(gk_SavedGame);
				}
			}
			ISN_Logger.Log("------------------------------------------", LogType.Log);
			ISN_Logger.Log("Duplicates " + this.SavesConflicts.Count, LogType.Log);
			ISN_Logger.Log("Unique saves " + this.GameSaves.Count, LogType.Log);
			ISN_Logger.Log("------------------------------------------", LogType.Log);
			foreach (GK_SavedGame gk_SavedGame2 in res.SavedGames)
			{
				ISN_Logger.Log("The name of the save game " + gk_SavedGame2.Name, LogType.Log);
			}
		}
		else
		{
			ISN_Logger.Log("Failed: " + res.Error.Message, LogType.Log);
		}
	}

	private Dictionary<string, List<GK_SavedGame>> GameSaves = new Dictionary<string, List<GK_SavedGame>>();

	private Dictionary<string, List<GK_SavedGame>> SavesConflicts = new Dictionary<string, List<GK_SavedGame>>();

	private string test_name = "savedgame1";

	private string test_name_2 = "savedgame2";
}
