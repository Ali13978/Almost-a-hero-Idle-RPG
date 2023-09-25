using System;
using UnityEngine;

namespace Ui
{
	public class KeyCommand
	{
		public KeyCommand(string commandName, KeyCode key)
		{
			this.commandName = commandName;
			this.key = key;
			this.activationMode = KeyCommand.ActivationMode.OnDown;
		}

		public KeyCommand(string commandName)
		{
			this.commandName = commandName;
			this.activationMode = KeyCommand.ActivationMode.OnDown;
		}

		public KeyCommand(KeyCode key)
		{
			this.commandName = null;
			this.key = key;
			this.activationMode = KeyCommand.ActivationMode.OnDown;
		}

		public static KeyCommand Create(string name)
		{
			return new KeyCommand(name);
		}

		public static KeyCommand Create(KeyCode key)
		{
			return new KeyCommand(key);
		}

		public static KeyCommand Create(string name, KeyCode key)
		{
			return new KeyCommand(name, key);
		}

		public KeyCommand SetShiftModifier()
		{
			this.hasShiftModifier = true;
			return this;
		}

		public KeyCommand SetCtrlModifier()
		{
			this.hasCtrlModifier = true;
			return this;
		}

		public KeyCommand SetAltModifier()
		{
			this.hasAltModifier = true;
			return this;
		}

		public KeyCommand SetKey(KeyCode key)
		{
			this.key = key;
			return this;
		}

		public KeyCommand SetAction(CommandAction action)
		{
			this.action = action;
			return this;
		}

		public KeyCommand SetCondition(CommandCondition condition)
		{
			this.condition = condition;
			return this;
		}

		public KeyCommand SetActivationMode(KeyCommand.ActivationMode activationMode)
		{
			this.activationMode = activationMode;
			return this;
		}

		public bool CheckOnUp()
		{
			bool modifierState = this.GetModifierState();
			return modifierState && UnityEngine.Input.GetKeyUp(this.key);
		}

		public bool CheckOnDown()
		{
			bool modifierState = this.GetModifierState();
			return modifierState && UnityEngine.Input.GetKeyDown(this.key);
		}

		public bool CheckState()
		{
			bool modifierState = this.GetModifierState();
			return modifierState && UnityEngine.Input.GetKey(this.key);
		}

		private bool GetModifierState()
		{
			bool flag = UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift);
			bool flag2 = (!this.hasShiftModifier) ? (!flag) : flag;
			bool flag3 = UnityEngine.Input.GetKey(KeyCode.LeftControl) || UnityEngine.Input.GetKey(KeyCode.RightControl);
			bool flag4 = (!this.hasCtrlModifier) ? (!flag3) : flag3;
			bool flag5 = UnityEngine.Input.GetKey(KeyCode.LeftAlt) || UnityEngine.Input.GetKey(KeyCode.RightAlt);
			bool flag6 = (!this.hasAltModifier) ? (!flag5) : flag5;
			return flag2 && flag4 && flag6;
		}

		public void Activate(float t, ref CommandAction commandAction)
		{
			bool modifierState = this.GetModifierState();
			switch (this.activationMode)
			{
			case KeyCommand.ActivationMode.OnDown:
				if (modifierState && UnityEngine.Input.GetKeyDown(this.key))
				{
					commandAction = (CommandAction)Delegate.Combine(commandAction, this.action);
				}
				break;
			case KeyCommand.ActivationMode.OnUp:
				if (modifierState && UnityEngine.Input.GetKeyUp(this.key))
				{
					commandAction = (CommandAction)Delegate.Combine(commandAction, this.action);
				}
				break;
			case KeyCommand.ActivationMode.Repeat:
				if (t >= this.nextPressTime && modifierState && UnityEngine.Input.GetKey(this.key))
				{
					commandAction = (CommandAction)Delegate.Combine(commandAction, this.action);
					if (!this.isRepeating)
					{
						this.nextPressTime = t + 0.5f;
						this.isRepeating = true;
					}
					else
					{
						this.nextPressTime = t + 0.05f;
					}
				}
				else if (UnityEngine.Input.GetKeyUp(this.key))
				{
					this.isRepeating = false;
				}
				break;
			}
		}

		public string commandName;

		public KeyCode key;

		public bool hasShiftModifier;

		public bool hasCtrlModifier;

		public bool hasAltModifier;

		public CommandCondition condition;

		public CommandAction action;

		public KeyCommand.ActivationMode activationMode;

		private float nextPressTime;

		private bool isRepeating;

		public enum ActivationMode
		{
			OnDown,
			OnUp,
			Repeat
		}
	}
}
