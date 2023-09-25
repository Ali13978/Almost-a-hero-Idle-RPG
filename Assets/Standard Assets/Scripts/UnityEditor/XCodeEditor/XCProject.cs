using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UnityEditor.XCodeEditor
{
	public class XCProject : IDisposable
	{
		public XCProject()
		{
		}

		public XCProject(string filePath) : this()
		{
			if (!Directory.Exists(filePath))
			{
				UnityEngine.Debug.LogWarning("XCode project path does not exist: " + filePath);
				return;
			}
			if (filePath.EndsWith(".xcodeproj"))
			{
				UnityEngine.Debug.Log("Opening project " + filePath);
				this.projectRootPath = Path.GetDirectoryName(filePath);
				this.filePath = filePath;
			}
			else
			{
				string[] directories = Directory.GetDirectories(filePath, "*.xcodeproj");
				if (directories.Length == 0)
				{
					UnityEngine.Debug.LogWarning("Error: missing xcodeproj file");
					return;
				}
				this.projectRootPath = filePath;
				if (!Path.IsPathRooted(this.projectRootPath))
				{
					this.projectRootPath = Application.dataPath.Replace("Assets", string.Empty) + this.projectRootPath;
				}
				this.filePath = directories[0];
			}
			this.projectFileInfo = new FileInfo(Path.Combine(this.filePath, "project.pbxproj"));
			string data = this.projectFileInfo.OpenText().ReadToEnd();
			PBXParser pbxparser = new PBXParser();
			this._datastore = pbxparser.Decode(data);
			if (this._datastore == null)
			{
				throw new Exception("Project file not found at file path " + filePath);
			}
			if (!this._datastore.ContainsKey("objects"))
			{
				UnityEngine.Debug.Log("Errore " + this._datastore.Count);
				return;
			}
			this._objects = (PBXDictionary)this._datastore["objects"];
			this.modified = false;
			this._rootObjectKey = (string)this._datastore["rootObject"];
			if (!string.IsNullOrEmpty(this._rootObjectKey))
			{
				this._project = new PBXProject(this._rootObjectKey, (PBXDictionary)this._objects[this._rootObjectKey]);
				this._rootGroup = new PBXGroup(this._rootObjectKey, (PBXDictionary)this._objects[this._project.mainGroupID]);
			}
			else
			{
				UnityEngine.Debug.LogWarning("error: project has no root object");
				this._project = null;
				this._rootGroup = null;
			}
		}

		public string projectRootPath { get; private set; }

		public string filePath { get; private set; }

		public PBXProject project
		{
			get
			{
				return this._project;
			}
		}

		public PBXGroup rootGroup
		{
			get
			{
				return this._rootGroup;
			}
		}

		public PBXSortedDictionary<PBXBuildFile> buildFiles
		{
			get
			{
				if (this._buildFiles == null)
				{
					this._buildFiles = new PBXSortedDictionary<PBXBuildFile>(this._objects);
				}
				return this._buildFiles;
			}
		}

		public PBXSortedDictionary<PBXGroup> groups
		{
			get
			{
				if (this._groups == null)
				{
					this._groups = new PBXSortedDictionary<PBXGroup>(this._objects);
				}
				return this._groups;
			}
		}

		public PBXSortedDictionary<PBXFileReference> fileReferences
		{
			get
			{
				if (this._fileReferences == null)
				{
					this._fileReferences = new PBXSortedDictionary<PBXFileReference>(this._objects);
				}
				return this._fileReferences;
			}
		}

		public PBXDictionary<PBXVariantGroup> variantGroups
		{
			get
			{
				if (this._variantGroups == null)
				{
					this._variantGroups = new PBXDictionary<PBXVariantGroup>(this._objects);
				}
				return this._variantGroups;
			}
		}

		public PBXDictionary<PBXNativeTarget> nativeTargets
		{
			get
			{
				if (this._nativeTargets == null)
				{
					this._nativeTargets = new PBXDictionary<PBXNativeTarget>(this._objects);
				}
				return this._nativeTargets;
			}
		}

		public PBXDictionary<XCBuildConfiguration> buildConfigurations
		{
			get
			{
				if (this._buildConfigurations == null)
				{
					this._buildConfigurations = new PBXDictionary<XCBuildConfiguration>(this._objects);
				}
				return this._buildConfigurations;
			}
		}

		public PBXSortedDictionary<XCConfigurationList> configurationLists
		{
			get
			{
				if (this._configurationLists == null)
				{
					this._configurationLists = new PBXSortedDictionary<XCConfigurationList>(this._objects);
				}
				return this._configurationLists;
			}
		}

		public PBXDictionary<PBXFrameworksBuildPhase> frameworkBuildPhases
		{
			get
			{
				if (this._frameworkBuildPhases == null)
				{
					this._frameworkBuildPhases = new PBXDictionary<PBXFrameworksBuildPhase>(this._objects);
				}
				return this._frameworkBuildPhases;
			}
		}

		public PBXDictionary<PBXResourcesBuildPhase> resourcesBuildPhases
		{
			get
			{
				if (this._resourcesBuildPhases == null)
				{
					this._resourcesBuildPhases = new PBXDictionary<PBXResourcesBuildPhase>(this._objects);
				}
				return this._resourcesBuildPhases;
			}
		}

		public PBXDictionary<PBXShellScriptBuildPhase> shellScriptBuildPhases
		{
			get
			{
				if (this._shellScriptBuildPhases == null)
				{
					this._shellScriptBuildPhases = new PBXDictionary<PBXShellScriptBuildPhase>(this._objects);
				}
				return this._shellScriptBuildPhases;
			}
		}

		public PBXDictionary<PBXSourcesBuildPhase> sourcesBuildPhases
		{
			get
			{
				if (this._sourcesBuildPhases == null)
				{
					this._sourcesBuildPhases = new PBXDictionary<PBXSourcesBuildPhase>(this._objects);
				}
				return this._sourcesBuildPhases;
			}
		}

		public PBXDictionary<PBXCopyFilesBuildPhase> copyBuildPhases
		{
			get
			{
				if (this._copyBuildPhases == null)
				{
					this._copyBuildPhases = new PBXDictionary<PBXCopyFilesBuildPhase>(this._objects);
				}
				return this._copyBuildPhases;
			}
		}

		public bool AddOtherCFlags(string flag)
		{
			return this.AddOtherCFlags(new PBXList(flag));
		}

		public bool AddOtherCFlags(PBXList flags)
		{
			foreach (KeyValuePair<string, XCBuildConfiguration> keyValuePair in this.buildConfigurations)
			{
				keyValuePair.Value.AddOtherCFlags(flags);
			}
			this.modified = true;
			return this.modified;
		}

		public bool AddOtherLinkerFlags(string flag)
		{
			return this.AddOtherLinkerFlags(new PBXList(flag));
		}

		public bool AddOtherLinkerFlags(PBXList flags)
		{
			foreach (KeyValuePair<string, XCBuildConfiguration> keyValuePair in this.buildConfigurations)
			{
				keyValuePair.Value.AddOtherLinkerFlags(flags);
			}
			this.modified = true;
			return this.modified;
		}

		public bool overwriteBuildSetting(string settingName, string newValue, string buildConfigName = "all")
		{
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				"overwriteBuildSetting ",
				settingName,
				" ",
				newValue,
				" ",
				buildConfigName
			}));
			foreach (KeyValuePair<string, XCBuildConfiguration> keyValuePair in this.buildConfigurations)
			{
				XCBuildConfiguration value = keyValuePair.Value;
				if ((string)value.data["name"] == buildConfigName || buildConfigName == "all")
				{
					keyValuePair.Value.overwriteBuildSetting(settingName, newValue);
					this.modified = true;
				}
			}
			return this.modified;
		}

		public bool AddHeaderSearchPaths(string path)
		{
			return this.AddHeaderSearchPaths(new PBXList(path));
		}

		public bool AddHeaderSearchPaths(PBXList paths)
		{
			UnityEngine.Debug.Log("AddHeaderSearchPaths " + paths);
			foreach (KeyValuePair<string, XCBuildConfiguration> keyValuePair in this.buildConfigurations)
			{
				keyValuePair.Value.AddHeaderSearchPaths(paths, true);
			}
			this.modified = true;
			return this.modified;
		}

		public bool AddLibrarySearchPaths(string path)
		{
			return this.AddLibrarySearchPaths(new PBXList(path));
		}

		public bool AddLibrarySearchPaths(PBXList paths)
		{
			UnityEngine.Debug.Log("AddLibrarySearchPaths " + paths);
			foreach (KeyValuePair<string, XCBuildConfiguration> keyValuePair in this.buildConfigurations)
			{
				keyValuePair.Value.AddLibrarySearchPaths(paths, true);
			}
			this.modified = true;
			return this.modified;
		}

		public bool AddFrameworkSearchPaths(string path)
		{
			return this.AddFrameworkSearchPaths(new PBXList(path));
		}

		public bool AddFrameworkSearchPaths(PBXList paths)
		{
			foreach (KeyValuePair<string, XCBuildConfiguration> keyValuePair in this.buildConfigurations)
			{
				keyValuePair.Value.AddFrameworkSearchPaths(paths, true);
			}
			this.modified = true;
			return this.modified;
		}

		public object GetObject(string guid)
		{
			return this._objects[guid];
		}

		public PBXDictionary AddFile(string filePath, PBXGroup parent = null, string tree = "SOURCE_ROOT", bool createBuildFiles = true, bool weak = false)
		{
			PBXDictionary pbxdictionary = new PBXDictionary();
			if (filePath == null)
			{
				UnityEngine.Debug.LogError("AddFile called with null filePath");
				return pbxdictionary;
			}
			string text = string.Empty;
			if (Path.IsPathRooted(filePath))
			{
				UnityEngine.Debug.Log("Path is Rooted");
				text = filePath;
			}
			else if (tree.CompareTo("SDKROOT") != 0)
			{
				text = Path.Combine(Application.dataPath, filePath);
			}
			if (!File.Exists(text) && !Directory.Exists(text) && tree.CompareTo("SDKROOT") != 0)
			{
				UnityEngine.Debug.Log("Missing file: " + filePath);
				return pbxdictionary;
			}
			if (tree.CompareTo("SOURCE_ROOT") == 0)
			{
				UnityEngine.Debug.Log("Source Root File");
				Uri uri = new Uri(text);
				Uri uri2 = new Uri(this.projectRootPath + "/.");
				filePath = uri2.MakeRelativeUri(uri).ToString();
			}
			else if (tree.CompareTo("GROUP") == 0)
			{
				UnityEngine.Debug.Log("Group File");
				filePath = Path.GetFileName(filePath);
			}
			if (parent == null)
			{
				parent = this._rootGroup;
			}
			PBXFileReference pbxfileReference = this.GetFile(Path.GetFileName(filePath));
			if (pbxfileReference != null)
			{
				UnityEngine.Debug.Log("File already exists: " + filePath);
				return null;
			}
			pbxfileReference = new PBXFileReference(filePath, (TreeEnum)Enum.Parse(typeof(TreeEnum), tree));
			parent.AddChild(pbxfileReference);
			this.fileReferences.Add(pbxfileReference);
			pbxdictionary.Add(pbxfileReference.guid, pbxfileReference);
			if (!string.IsNullOrEmpty(pbxfileReference.buildPhase) && createBuildFiles)
			{
				string buildPhase = pbxfileReference.buildPhase;
				if (buildPhase != null)
				{
					if (!(buildPhase == "PBXFrameworksBuildPhase"))
					{
						if (!(buildPhase == "PBXResourcesBuildPhase"))
						{
							if (!(buildPhase == "PBXShellScriptBuildPhase"))
							{
								if (!(buildPhase == "PBXSourcesBuildPhase"))
								{
									if (!(buildPhase == "PBXCopyFilesBuildPhase"))
									{
										UnityEngine.Debug.LogWarning("File Not Supported.");
										return null;
									}
									foreach (KeyValuePair<string, PBXCopyFilesBuildPhase> currentObject in this.copyBuildPhases)
									{
										UnityEngine.Debug.Log("Adding Copy Files Build Phase");
										this.BuildAddFile(pbxfileReference, currentObject, weak);
									}
								}
								else
								{
									foreach (KeyValuePair<string, PBXSourcesBuildPhase> currentObject2 in this.sourcesBuildPhases)
									{
										UnityEngine.Debug.Log("Adding Source Build File");
										this.BuildAddFile(pbxfileReference, currentObject2, weak);
									}
								}
							}
							else
							{
								foreach (KeyValuePair<string, PBXShellScriptBuildPhase> currentObject3 in this.shellScriptBuildPhases)
								{
									UnityEngine.Debug.Log("Adding Script Build File");
									this.BuildAddFile(pbxfileReference, currentObject3, weak);
								}
							}
						}
						else
						{
							foreach (KeyValuePair<string, PBXResourcesBuildPhase> currentObject4 in this.resourcesBuildPhases)
							{
								UnityEngine.Debug.Log("Adding Resources Build File");
								this.BuildAddFile(pbxfileReference, currentObject4, weak);
							}
						}
					}
					else
					{
						foreach (KeyValuePair<string, PBXFrameworksBuildPhase> currentObject5 in this.frameworkBuildPhases)
						{
							this.BuildAddFile(pbxfileReference, currentObject5, weak);
						}
						if (!string.IsNullOrEmpty(text) && tree.CompareTo("SOURCE_ROOT") == 0)
						{
							string firstValue = Path.Combine("$(SRCROOT)", Path.GetDirectoryName(filePath));
							if (File.Exists(text))
							{
								this.AddLibrarySearchPaths(new PBXList(firstValue));
							}
							else
							{
								this.AddFrameworkSearchPaths(new PBXList(firstValue));
							}
						}
					}
				}
				else
				{
					UnityEngine.Debug.LogWarning("File Not Supported: " + filePath);
				}
			}
			return pbxdictionary;
		}

		public PBXNativeTarget GetNativeTarget(string name)
		{
			PBXNativeTarget result = null;
			foreach (KeyValuePair<string, PBXNativeTarget> keyValuePair in this.nativeTargets)
			{
				string a = (string)keyValuePair.Value.data["name"];
				if (a == name)
				{
					result = keyValuePair.Value;
					break;
				}
			}
			return result;
		}

		public int GetBuildActionMask()
		{
			int result = 0;
			using (Dictionary<string, PBXCopyFilesBuildPhase>.Enumerator enumerator = this.copyBuildPhases.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<string, PBXCopyFilesBuildPhase> keyValuePair = enumerator.Current;
					result = (int)keyValuePair.Value.data["buildActionMask"];
				}
			}
			return result;
		}

		public PBXCopyFilesBuildPhase AddEmbedFrameworkBuildPhase()
		{
			PBXCopyFilesBuildPhase pbxcopyFilesBuildPhase = null;
			PBXNativeTarget nativeTarget = this.GetNativeTarget("Unity-iPhone");
			if (nativeTarget == null)
			{
				UnityEngine.Debug.Log("Not found Correct NativeTarget.");
				return pbxcopyFilesBuildPhase;
			}
			foreach (KeyValuePair<string, PBXCopyFilesBuildPhase> keyValuePair in this.copyBuildPhases)
			{
				object obj = null;
				if (keyValuePair.Value.data.TryGetValue("name", out obj))
				{
					string a = (string)obj;
					if (a == "Embed Frameworks")
					{
						return keyValuePair.Value;
					}
				}
			}
			int buildActionMask = this.GetBuildActionMask();
			pbxcopyFilesBuildPhase = new PBXCopyFilesBuildPhase(buildActionMask);
			ArrayList arrayList = (ArrayList)nativeTarget.data["buildPhases"];
			arrayList.Add(pbxcopyFilesBuildPhase.guid);
			this.copyBuildPhases.Add(pbxcopyFilesBuildPhase);
			return pbxcopyFilesBuildPhase;
		}

		public void AddEmbedFramework(string fileName)
		{
			UnityEngine.Debug.Log("Add Embed Framework: " + fileName);
			PBXFileReference file = this.GetFile(Path.GetFileName(fileName));
			if (file == null)
			{
				UnityEngine.Debug.Log("Embed Framework must added already: " + fileName);
				return;
			}
			PBXCopyFilesBuildPhase pbxcopyFilesBuildPhase = this.AddEmbedFrameworkBuildPhase();
			if (pbxcopyFilesBuildPhase == null)
			{
				UnityEngine.Debug.Log("AddEmbedFrameworkBuildPhase Failed.");
				return;
			}
			PBXBuildFile pbxbuildFile = new PBXBuildFile(file, false);
			pbxbuildFile.AddCodeSignOnCopy();
			this.buildFiles.Add(pbxbuildFile);
			pbxcopyFilesBuildPhase.AddBuildFile(pbxbuildFile);
		}

		private void BuildAddFile(PBXFileReference fileReference, KeyValuePair<string, PBXFrameworksBuildPhase> currentObject, bool weak)
		{
			PBXBuildFile pbxbuildFile = new PBXBuildFile(fileReference, weak);
			this.buildFiles.Add(pbxbuildFile);
			currentObject.Value.AddBuildFile(pbxbuildFile);
		}

		private void BuildAddFile(PBXFileReference fileReference, KeyValuePair<string, PBXResourcesBuildPhase> currentObject, bool weak)
		{
			PBXBuildFile pbxbuildFile = new PBXBuildFile(fileReference, weak);
			this.buildFiles.Add(pbxbuildFile);
			currentObject.Value.AddBuildFile(pbxbuildFile);
		}

		private void BuildAddFile(PBXFileReference fileReference, KeyValuePair<string, PBXShellScriptBuildPhase> currentObject, bool weak)
		{
			PBXBuildFile pbxbuildFile = new PBXBuildFile(fileReference, weak);
			this.buildFiles.Add(pbxbuildFile);
			currentObject.Value.AddBuildFile(pbxbuildFile);
		}

		private void BuildAddFile(PBXFileReference fileReference, KeyValuePair<string, PBXSourcesBuildPhase> currentObject, bool weak)
		{
			PBXBuildFile pbxbuildFile = new PBXBuildFile(fileReference, weak);
			this.buildFiles.Add(pbxbuildFile);
			currentObject.Value.AddBuildFile(pbxbuildFile);
		}

		private void BuildAddFile(PBXFileReference fileReference, KeyValuePair<string, PBXCopyFilesBuildPhase> currentObject, bool weak)
		{
			PBXBuildFile pbxbuildFile = new PBXBuildFile(fileReference, weak);
			this.buildFiles.Add(pbxbuildFile);
			currentObject.Value.AddBuildFile(pbxbuildFile);
		}

		public bool AddFolder(string folderPath, PBXGroup parent = null, string[] exclude = null, bool recursive = true, bool createBuildFile = true)
		{
			UnityEngine.Debug.Log("Folder PATH: " + folderPath);
			if (!Directory.Exists(folderPath))
			{
				UnityEngine.Debug.Log("Directory doesn't exist?");
				return false;
			}
			if (folderPath.EndsWith(".lproj"))
			{
				UnityEngine.Debug.Log("Ended with .lproj");
				return this.AddLocFolder(folderPath, parent, exclude, createBuildFile);
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
			if (exclude == null)
			{
				UnityEngine.Debug.Log("Exclude was null");
				exclude = new string[0];
			}
			if (parent == null)
			{
				UnityEngine.Debug.Log("Parent was null");
				parent = this.rootGroup;
			}
			PBXGroup group = this.GetGroup(directoryInfo.Name, null, parent);
			UnityEngine.Debug.Log("New Group created");
			foreach (string text in Directory.GetDirectories(folderPath))
			{
				UnityEngine.Debug.Log("DIR: " + text);
				if (text.EndsWith(".bundle"))
				{
					UnityEngine.Debug.LogWarning("This is a special folder: " + text);
					this.AddFile(text, group, "SOURCE_ROOT", createBuildFile, false);
				}
				else if (recursive)
				{
					UnityEngine.Debug.Log("recursive");
					this.AddFolder(text, group, exclude, recursive, createBuildFile);
				}
			}
			string pattern = string.Format("{0}", string.Join("|", exclude));
			foreach (string text2 in Directory.GetFiles(folderPath))
			{
				if (!Regex.IsMatch(text2, pattern))
				{
					UnityEngine.Debug.Log("Adding Files for Folder");
					this.AddFile(text2, group, "SOURCE_ROOT", createBuildFile, false);
				}
			}
			this.modified = true;
			return this.modified;
		}

		public bool AddLocFolder(string folderPath, PBXGroup parent = null, string[] exclude = null, bool createBuildFile = true)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
			if (exclude == null)
			{
				exclude = new string[0];
			}
			if (parent == null)
			{
				parent = this.rootGroup;
			}
			Uri uri = new Uri(this.projectFileInfo.DirectoryName);
			Uri uri2 = new Uri(folderPath);
			string path = uri.MakeRelativeUri(uri2).ToString();
			PBXGroup group = this.GetGroup(directoryInfo.Name, path, parent);
			string name = directoryInfo.Name;
			string region = name.Substring(0, name.Length - ".lproj".Length);
			this.project.AddRegion(region);
			string pattern = string.Format("{0}", string.Join("|", exclude));
			foreach (string text in Directory.GetFiles(folderPath))
			{
				if (!Regex.IsMatch(text, pattern))
				{
					PBXVariantGroup pbxvariantGroup = new PBXVariantGroup(Path.GetFileName(text), null, "GROUP");
					this.variantGroups.Add(pbxvariantGroup);
					group.AddChild(pbxvariantGroup);
					this.AddFile(text, pbxvariantGroup, "GROUP", createBuildFile, false);
				}
			}
			this.modified = true;
			return this.modified;
		}

		public PBXFileReference GetFile(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			foreach (KeyValuePair<string, PBXFileReference> keyValuePair in this.fileReferences)
			{
				if (!string.IsNullOrEmpty(keyValuePair.Value.name) && keyValuePair.Value.name.CompareTo(name) == 0)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		public PBXGroup GetGroup(string name, string path = null, PBXGroup parent = null)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (parent == null)
			{
				parent = this.rootGroup;
			}
			foreach (KeyValuePair<string, PBXGroup> keyValuePair in this.groups)
			{
				if (string.IsNullOrEmpty(keyValuePair.Value.name))
				{
					if (keyValuePair.Value.path.CompareTo(name) == 0 && parent.HasChild(keyValuePair.Key))
					{
						return keyValuePair.Value;
					}
				}
				else if (keyValuePair.Value.name.CompareTo(name) == 0 && parent.HasChild(keyValuePair.Key))
				{
					return keyValuePair.Value;
				}
			}
			PBXGroup pbxgroup = new PBXGroup(name, path, "SOURCE_ROOT");
			this.groups.Add(pbxgroup);
			parent.AddChild(pbxgroup);
			this.modified = true;
			return pbxgroup;
		}

		public void ApplyMod(string pbxmod)
		{
			XCMod xcmod = new XCMod(pbxmod);
			IEnumerator enumerator = xcmod.libs.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object arg = enumerator.Current;
					UnityEngine.Debug.Log("Library: " + arg);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			this.ApplyMod(xcmod);
		}

		public void ApplyMod(XCMod mod)
		{
			PBXGroup group = this.GetGroup(mod.group, null, null);
			UnityEngine.Debug.Log("Adding libraries...");
			IEnumerator enumerator = mod.libs.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					XCModFile xcmodFile = (XCModFile)obj;
					string text = Path.Combine("usr/lib", xcmodFile.filePath);
					UnityEngine.Debug.Log("Adding library " + text);
					this.AddFile(text, group, "SDKROOT", true, xcmodFile.isWeak);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			UnityEngine.Debug.Log("Adding frameworks...");
			PBXGroup group2 = this.GetGroup("Frameworks", null, null);
			IEnumerator enumerator2 = mod.frameworks.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					string text2 = (string)obj2;
					string[] array = text2.Split(new char[]
					{
						':'
					});
					bool weak = array.Length > 1;
					string filePath = Path.Combine("System/Library/Frameworks", array[0]);
					this.AddFile(filePath, group2, "SDKROOT", true, weak);
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
			UnityEngine.Debug.Log("Adding files...");
			IEnumerator enumerator3 = mod.files.GetEnumerator();
			try
			{
				while (enumerator3.MoveNext())
				{
					object obj3 = enumerator3.Current;
					string path = (string)obj3;
					string filePath2 = Path.Combine(mod.path, path);
					this.AddFile(filePath2, group, "SOURCE_ROOT", true, false);
				}
			}
			finally
			{
				IDisposable disposable3;
				if ((disposable3 = (enumerator3 as IDisposable)) != null)
				{
					disposable3.Dispose();
				}
			}
			UnityEngine.Debug.Log("Adding embed binaries...");
			if (mod.embed_binaries != null)
			{
				this.overwriteBuildSetting("LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks", "Release");
				this.overwriteBuildSetting("LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks", "Debug");
				IEnumerator enumerator4 = mod.embed_binaries.GetEnumerator();
				try
				{
					while (enumerator4.MoveNext())
					{
						object obj4 = enumerator4.Current;
						string path2 = (string)obj4;
						string fileName = Path.Combine(mod.path, path2);
						this.AddEmbedFramework(fileName);
					}
				}
				finally
				{
					IDisposable disposable4;
					if ((disposable4 = (enumerator4 as IDisposable)) != null)
					{
						disposable4.Dispose();
					}
				}
			}
			UnityEngine.Debug.Log("Adding folders...");
			IEnumerator enumerator5 = mod.folders.GetEnumerator();
			try
			{
				while (enumerator5.MoveNext())
				{
					object obj5 = enumerator5.Current;
					string path3 = (string)obj5;
					string text3 = Path.Combine(Application.dataPath, path3);
					UnityEngine.Debug.Log("Adding folder " + text3);
					this.AddFolder(text3, group, (string[])mod.excludes.ToArray(typeof(string)), true, true);
				}
			}
			finally
			{
				IDisposable disposable5;
				if ((disposable5 = (enumerator5 as IDisposable)) != null)
				{
					disposable5.Dispose();
				}
			}
			UnityEngine.Debug.Log("Adding headerpaths...");
			IEnumerator enumerator6 = mod.headerpaths.GetEnumerator();
			try
			{
				while (enumerator6.MoveNext())
				{
					object obj6 = enumerator6.Current;
					string text4 = (string)obj6;
					if (text4.Contains("$(inherited)"))
					{
						UnityEngine.Debug.Log("not prepending a path to " + text4);
						this.AddHeaderSearchPaths(text4);
					}
					else
					{
						string path4 = Path.Combine(mod.path, text4);
						this.AddHeaderSearchPaths(path4);
					}
				}
			}
			finally
			{
				IDisposable disposable6;
				if ((disposable6 = (enumerator6 as IDisposable)) != null)
				{
					disposable6.Dispose();
				}
			}
			UnityEngine.Debug.Log("Adding compiler flags...");
			IEnumerator enumerator7 = mod.compiler_flags.GetEnumerator();
			try
			{
				while (enumerator7.MoveNext())
				{
					object obj7 = enumerator7.Current;
					string flag = (string)obj7;
					this.AddOtherCFlags(flag);
				}
			}
			finally
			{
				IDisposable disposable7;
				if ((disposable7 = (enumerator7 as IDisposable)) != null)
				{
					disposable7.Dispose();
				}
			}
			UnityEngine.Debug.Log("Adding linker flags...");
			IEnumerator enumerator8 = mod.linker_flags.GetEnumerator();
			try
			{
				while (enumerator8.MoveNext())
				{
					object obj8 = enumerator8.Current;
					string flag2 = (string)obj8;
					this.AddOtherLinkerFlags(flag2);
				}
			}
			finally
			{
				IDisposable disposable8;
				if ((disposable8 = (enumerator8 as IDisposable)) != null)
				{
					disposable8.Dispose();
				}
			}
			UnityEngine.Debug.Log("Adding plist items...");
			string plistPath = this.projectRootPath + "/Info.plist";
			XCPlist xcplist = new XCPlist(plistPath);
			xcplist.Process(mod.plist);
			this.Consolidate();
		}

		public void Consolidate()
		{
			PBXDictionary pbxdictionary = new PBXDictionary();
			pbxdictionary.Append<PBXBuildFile>(this.buildFiles);
			pbxdictionary.Append<PBXCopyFilesBuildPhase>(this.copyBuildPhases);
			pbxdictionary.Append<PBXFileReference>(this.fileReferences);
			pbxdictionary.Append<PBXFrameworksBuildPhase>(this.frameworkBuildPhases);
			pbxdictionary.Append<PBXGroup>(this.groups);
			pbxdictionary.Append<PBXNativeTarget>(this.nativeTargets);
			pbxdictionary.Add(this.project.guid, this.project.data);
			pbxdictionary.Append<PBXResourcesBuildPhase>(this.resourcesBuildPhases);
			pbxdictionary.Append<PBXShellScriptBuildPhase>(this.shellScriptBuildPhases);
			pbxdictionary.Append<PBXSourcesBuildPhase>(this.sourcesBuildPhases);
			pbxdictionary.Append<PBXVariantGroup>(this.variantGroups);
			pbxdictionary.Append<XCBuildConfiguration>(this.buildConfigurations);
			pbxdictionary.Append<XCConfigurationList>(this.configurationLists);
			this._objects = pbxdictionary;
		}

		public void Backup()
		{
			string text = Path.Combine(this.filePath, "project.backup.pbxproj");
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			File.Copy(Path.Combine(this.filePath, "project.pbxproj"), text);
		}

		private void DeleteExisting(string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		private void CreateNewProject(PBXDictionary result, string path)
		{
			PBXParser pbxparser = new PBXParser();
			StreamWriter streamWriter = File.CreateText(path);
			streamWriter.Write(pbxparser.Encode(result, true));
			streamWriter.Close();
		}

		public void Save()
		{
			PBXDictionary pbxdictionary = new PBXDictionary();
			pbxdictionary.Add("archiveVersion", 1);
			pbxdictionary.Add("classes", new PBXDictionary());
			pbxdictionary.Add("objectVersion", 46);
			this.Consolidate();
			pbxdictionary.Add("objects", this._objects);
			pbxdictionary.Add("rootObject", this._rootObjectKey);
			string path = Path.Combine(this.filePath, "project.pbxproj");
			this.DeleteExisting(path);
			this.CreateNewProject(pbxdictionary, path);
		}

		public Dictionary<string, object> objects
		{
			get
			{
				return null;
			}
		}

		public void Dispose()
		{
		}

		private PBXDictionary _datastore;

		public PBXDictionary _objects;

		private PBXGroup _rootGroup;

		private string _rootObjectKey;

		private FileInfo projectFileInfo;

		private bool modified;

		private PBXSortedDictionary<PBXBuildFile> _buildFiles;

		private PBXSortedDictionary<PBXGroup> _groups;

		private PBXSortedDictionary<PBXFileReference> _fileReferences;

		private PBXDictionary<PBXNativeTarget> _nativeTargets;

		private PBXDictionary<PBXFrameworksBuildPhase> _frameworkBuildPhases;

		private PBXDictionary<PBXResourcesBuildPhase> _resourcesBuildPhases;

		private PBXDictionary<PBXShellScriptBuildPhase> _shellScriptBuildPhases;

		private PBXDictionary<PBXSourcesBuildPhase> _sourcesBuildPhases;

		private PBXDictionary<PBXCopyFilesBuildPhase> _copyBuildPhases;

		private PBXDictionary<PBXVariantGroup> _variantGroups;

		private PBXDictionary<XCBuildConfiguration> _buildConfigurations;

		private PBXSortedDictionary<XCConfigurationList> _configurationLists;

		private PBXProject _project;
	}
}
