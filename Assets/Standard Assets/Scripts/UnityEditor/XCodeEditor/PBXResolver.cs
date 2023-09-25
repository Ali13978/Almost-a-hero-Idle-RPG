using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.XCodeEditor
{
	public class PBXResolver
	{
		public PBXResolver(PBXDictionary pbxData)
		{
			this.objects = (PBXDictionary)pbxData["objects"];
			this.index = new PBXResolver.PBXResolverReverseIndex();
			this.rootObject = (string)pbxData["rootObject"];
			this.BuildReverseIndex();
		}

		private void BuildReverseIndex()
		{
			foreach (KeyValuePair<string, object> keyValuePair in this.objects)
			{
				if (keyValuePair.Value is PBXBuildPhase)
				{
					IEnumerator enumerator2 = ((PBXBuildPhase)keyValuePair.Value).files.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							object obj = enumerator2.Current;
							string key = (string)obj;
							this.index[key] = keyValuePair.Key;
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator2 as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
				}
				else if (keyValuePair.Value is PBXGroup)
				{
					IEnumerator enumerator3 = ((PBXGroup)keyValuePair.Value).children.GetEnumerator();
					try
					{
						while (enumerator3.MoveNext())
						{
							object obj2 = enumerator3.Current;
							string key2 = (string)obj2;
							this.index[key2] = keyValuePair.Key;
						}
					}
					finally
					{
						IDisposable disposable2;
						if ((disposable2 = (enumerator3 as IDisposable)) != null)
						{
							disposable2.Dispose();
						}
					}
				}
			}
		}

		public string ResolveName(string guid)
		{
			if (!this.objects.ContainsKey(guid))
			{
				UnityEngine.Debug.LogWarning(this + " ResolveName could not resolve " + guid);
				return string.Empty;
			}
			object obj = this.objects[guid];
			if (obj is PBXBuildFile)
			{
				return this.ResolveName(((PBXBuildFile)obj).fileRef);
			}
			if (obj is PBXFileReference)
			{
				PBXFileReference pbxfileReference = (PBXFileReference)obj;
				return (pbxfileReference.name == null) ? pbxfileReference.path : pbxfileReference.name;
			}
			if (obj is PBXGroup)
			{
				PBXGroup pbxgroup = (PBXGroup)obj;
				return (pbxgroup.name == null) ? pbxgroup.path : pbxgroup.name;
			}
			if (obj is PBXProject || guid == this.rootObject)
			{
				return "Project object";
			}
			if (obj is PBXFrameworksBuildPhase)
			{
				return "Frameworks";
			}
			if (obj is PBXResourcesBuildPhase)
			{
				return "Resources";
			}
			if (obj is PBXShellScriptBuildPhase)
			{
				return "ShellScript";
			}
			if (obj is PBXSourcesBuildPhase)
			{
				return "Sources";
			}
			if (obj is PBXCopyFilesBuildPhase)
			{
				return "CopyFiles";
			}
			if (obj is XCConfigurationList)
			{
				XCConfigurationList xcconfigurationList = (XCConfigurationList)obj;
				if (xcconfigurationList.data.ContainsKey("defaultConfigurationName"))
				{
					return (string)xcconfigurationList.data["defaultConfigurationName"];
				}
				return null;
			}
			else
			{
				if (!(obj is PBXNativeTarget))
				{
					if (obj is XCBuildConfiguration)
					{
						XCBuildConfiguration xcbuildConfiguration = (XCBuildConfiguration)obj;
						if (xcbuildConfiguration.data.ContainsKey("name"))
						{
							return (string)xcbuildConfiguration.data["name"];
						}
					}
					else if (obj is PBXObject)
					{
						PBXObject pbxobject = (PBXObject)obj;
						if (pbxobject.data.ContainsKey("name"))
						{
							UnityEngine.Debug.Log(string.Concat(new string[]
							{
								"PBXObject ",
								(string)pbxobject.data["name"],
								" ",
								guid,
								" ",
								(pbxobject != null) ? pbxobject.ToString() : string.Empty
							}));
						}
						return (string)pbxobject.data["name"];
					}
					UnityEngine.Debug.LogWarning("UNRESOLVED GUID:" + guid);
					return null;
				}
				PBXNativeTarget pbxnativeTarget = (PBXNativeTarget)obj;
				if (pbxnativeTarget.data.ContainsKey("name"))
				{
					return (string)pbxnativeTarget.data["name"];
				}
				return null;
			}
		}

		public string ResolveBuildPhaseNameForFile(string guid)
		{
			if (this.objects.ContainsKey(guid))
			{
				object obj = this.objects[guid];
				if (obj is PBXObject)
				{
					PBXObject pbxobject = (PBXObject)obj;
					if (this.index.ContainsKey(pbxobject.guid))
					{
						string key = this.index[pbxobject.guid];
						if (this.objects.ContainsKey(key))
						{
							object obj2 = this.objects[key];
							if (obj2 is PBXBuildPhase)
							{
								return this.ResolveName(((PBXBuildPhase)obj2).guid);
							}
						}
					}
				}
			}
			return null;
		}

		private PBXDictionary objects;

		private string rootObject;

		private PBXResolver.PBXResolverReverseIndex index;

		private class PBXResolverReverseIndex : Dictionary<string, string>
		{
		}
	}
}
