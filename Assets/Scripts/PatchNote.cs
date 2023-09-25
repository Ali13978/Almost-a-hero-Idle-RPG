using System;
using System.Diagnostics;
using UnityEngine;

public class PatchNote
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action onPatchNoteInitialized;

	public bool HasUri()
	{
		return !string.IsNullOrEmpty(this.redirectUri);
	}

	public void OpenUri()
	{
		Application.OpenURL(this.redirectUri);
	}

	public static void InitPatchNotes(PatchNote[] notes)
	{
		PatchNote.m_patchNotes = notes;
		if (PatchNote.onPatchNoteInitialized == null)
		{
			return;
		}
		PatchNote.onPatchNoteInitialized();
	}

	public static PatchNote[] patchNotes
	{
		get
		{
			return PatchNote.m_patchNotes;
		}
	}

	public string header;

	public string body;

	public string redirectUri;

	private static PatchNote[] m_patchNotes;
}
