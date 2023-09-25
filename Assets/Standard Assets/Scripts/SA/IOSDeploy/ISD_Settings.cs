using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SA.IOSDeploy
{
	public class ISD_Settings : ScriptableObject
	{
		public static ISD_Settings Instance
		{
			get
			{
				if (ISD_Settings.instance == null)
				{
					ISD_Settings.instance = (Resources.Load("ISD_Settings") as ISD_Settings);
					if (ISD_Settings.instance == null)
					{
						ISD_Settings.instance = ScriptableObject.CreateInstance<ISD_Settings>();
					}
				}
				return ISD_Settings.instance;
			}
		}

		public void AddVariable(Variable var)
		{
			foreach (Variable variable in this.PlistVariables.ToList<Variable>())
			{
				if (variable.Name.Equals(var.Name))
				{
					this.PlistVariables.Remove(variable);
				}
			}
			this.PlistVariables.Add(var);
		}

		public void AddVariableToDictionary(string uniqueIdKey, Variable var)
		{
			VariableId variableId = new VariableId();
			variableId.uniqueIdKey = uniqueIdKey;
			variableId.VariableValue = var;
			this.VariableDictionary.Add(variableId);
		}

		public void RemoveVariable(Variable v, IList ListWithThisVariable)
		{
			if (ISD_Settings.Instance.PlistVariables.Contains(v))
			{
				ISD_Settings.Instance.PlistVariables.Remove(v);
			}
			else
			{
				foreach (VariableId variableId in this.VariableDictionary)
				{
					if (variableId.VariableValue.Equals(v))
					{
						this.VariableDictionary.Remove(variableId);
						string uniqueIdKey = variableId.uniqueIdKey;
						if (ListWithThisVariable.Contains(uniqueIdKey))
						{
							ListWithThisVariable.Remove(variableId.uniqueIdKey);
						}
						break;
					}
				}
			}
		}

		public Variable getVariableByKey(string uniqueIdKey)
		{
			foreach (VariableId variableId in this.VariableDictionary)
			{
				if (variableId.uniqueIdKey.Equals(uniqueIdKey))
				{
					return variableId.VariableValue;
				}
			}
			return null;
		}

		public Variable GetVariableByName(string name)
		{
			foreach (Variable variable in ISD_Settings.Instance.PlistVariables)
			{
				if (variable.Name.Equals(name))
				{
					return variable;
				}
			}
			return null;
		}

		public string getKeyFromVariable(Variable var)
		{
			foreach (VariableId variableId in this.VariableDictionary)
			{
				if (variableId.VariableValue.Equals(var))
				{
					return variableId.uniqueIdKey;
				}
			}
			return null;
		}

		public bool ContainsPlistVarWithName(string name)
		{
			foreach (Variable variable in ISD_Settings.Instance.PlistVariables)
			{
				if (variable.Name.Equals(name))
				{
					return true;
				}
			}
			return false;
		}

		public bool ContainsFramework(iOSFramework framework)
		{
			foreach (Framework framework2 in ISD_Settings.Instance.Frameworks)
			{
				if (framework2.Type.Equals(framework))
				{
					return true;
				}
			}
			return false;
		}

		public Framework GetFramework(iOSFramework framework)
		{
			foreach (Framework framework2 in ISD_Settings.Instance.Frameworks)
			{
				if (framework2.Type.Equals(framework))
				{
					return framework2;
				}
			}
			return null;
		}

		public Framework AddFramework(iOSFramework framework, bool embaded = false)
		{
			Framework framework2 = this.GetFramework(framework);
			if (framework2 == null)
			{
				framework2 = new Framework(framework, embaded);
				ISD_Settings.Instance.Frameworks.Add(framework2);
			}
			return framework2;
		}

		public bool ContainsLibWithName(string name)
		{
			foreach (Lib lib in ISD_Settings.Instance.Libraries)
			{
				if (lib.Name.Equals(name))
				{
					return true;
				}
			}
			return false;
		}

		public Lib GetLibrary(iOSLibrary library)
		{
			foreach (Lib lib in ISD_Settings.instance.Libraries)
			{
				if (lib.Type.Equals(library))
				{
					return lib;
				}
			}
			return null;
		}

		public Lib AddLibrary(iOSLibrary library)
		{
			Lib lib = this.GetLibrary(library);
			if (lib == null)
			{
				lib = new Lib(library);
				ISD_Settings.Instance.Libraries.Add(lib);
			}
			return lib;
		}

		public void AddLinkerFlag(string s)
		{
			Flag flag = new Flag();
			flag.Name = s;
			flag.Type = FlagType.LinkerFlag;
			foreach (Flag flag2 in this.Flags)
			{
				if (flag2.Type.Equals(FlagType.LinkerFlag) && flag2.Name.Equals(s))
				{
					break;
				}
			}
			this.Flags.Add(flag);
		}

		public const string VERSION_NUMBER = "3.4/21";

		public bool IsfwSettingOpen;

		public bool IsLibSettingOpen;

		public bool IslinkerSettingOpne;

		public bool IscompilerSettingsOpen;

		public bool IsPlistSettingsOpen;

		public bool IsLanguageSettingOpen = true;

		public bool IsDefFrameworksOpen;

		public bool IsDefLibrariesOpen;

		public bool IsBuildSettingsOpen;

		public int ToolbarIndex;

		public bool enableBitCode;

		public bool enableTestability;

		public bool generateProfilingCode;

		public List<Framework> Frameworks = new List<Framework>();

		public List<Lib> Libraries = new List<Lib>();

		public List<Flag> Flags = new List<Flag>();

		public List<Variable> PlistVariables = new List<Variable>();

		public List<VariableId> VariableDictionary = new List<VariableId>();

		public List<string> langFolders = new List<string>();

		public List<AssetFile> Files = new List<AssetFile>();

		private const string ISDAssetName = "ISD_Settings";

		private const string ISDAssetExtension = ".asset";

		private static ISD_Settings instance;
	}
}
