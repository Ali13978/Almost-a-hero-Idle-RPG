using System;
using System.Collections.Generic;
using SA.Common.Util;

namespace SA.IOSDeploy
{
	[Serializable]
	public class Variable
	{
		public void AddChild(Variable v)
		{
			if (this.Type.Equals(PlistValueTypes.Dictionary))
			{
				foreach (string uniqueIdKey in this.ChildrensIds)
				{
					Variable variableByKey = ISD_Settings.Instance.getVariableByKey(uniqueIdKey);
					if (variableByKey.Name.Equals(v.Name))
					{
						ISD_Settings.Instance.RemoveVariable(variableByKey, this.ChildrensIds);
						break;
					}
				}
			}
			else if (this.Type.Equals(PlistValueTypes.Array) && v.Type.Equals(PlistValueTypes.String))
			{
				foreach (string uniqueIdKey2 in this.ChildrensIds)
				{
					Variable variableByKey2 = ISD_Settings.Instance.getVariableByKey(uniqueIdKey2);
					if (variableByKey2.Type.Equals(PlistValueTypes.String) && v.StringValue.Equals(variableByKey2.StringValue))
					{
						ISD_Settings.Instance.RemoveVariable(variableByKey2, this.ChildrensIds);
						break;
					}
				}
			}
			string text = IdFactory.NextId.ToString();
			ISD_Settings.Instance.AddVariableToDictionary(text, v);
			this.ChildrensIds.Add(text);
		}

		public bool IsOpen = true;

		public bool IsListOpen = true;

		public string Name = string.Empty;

		public PlistValueTypes Type;

		public string StringValue = string.Empty;

		public int IntegerValue;

		public float FloatValue;

		public bool BooleanValue = true;

		public List<string> ChildrensIds = new List<string>();
	}
}
