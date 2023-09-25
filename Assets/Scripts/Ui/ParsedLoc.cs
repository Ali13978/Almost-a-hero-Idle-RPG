using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui
{
	public class ParsedLoc : ScriptableObject
	{
		public ParsedLoc()
		{
			this.languages = new List<string>();
			this.keys = new List<string>();
			this.locValues = new List<LocValues>();
		}

		public void Clear()
		{
			this.languages.Clear();
			this.keys.Clear();
			this.locValues.Clear();
		}

		public List<string> languages;

		public List<string> keys;

		public List<LocValues> locValues;
	}
}
