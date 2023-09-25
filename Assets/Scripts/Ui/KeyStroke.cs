using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui
{
	public class KeyStroke
	{
		public KeyStroke()
		{
			this.commandContexts = new Dictionary<int, List<KeyCommand>>();
			this.queuedContextId = null;
			this.AddContext(0, true);
			this.masterContext = new List<KeyCommand>();
		}

		public void Update()
		{
			CommandAction commandAction = null;
			CommandAction commandAction2 = null;
			foreach (KeyCommand keyCommand in this.masterContext)
			{
				if (keyCommand.condition == null || keyCommand.condition())
				{
					keyCommand.Activate(Time.unscaledTime, ref commandAction);
				}
			}
			if (commandAction != null)
			{
				commandAction();
			}
			foreach (KeyCommand keyCommand2 in this.activeContext)
			{
				if (keyCommand2.condition == null || keyCommand2.condition())
				{
					keyCommand2.Activate(Time.unscaledTime, ref commandAction2);
				}
			}
			if (commandAction2 != null)
			{
				commandAction2();
			}
		}

		public void AddContext(int contextId, bool setAsActive = false)
		{
			List<KeyCommand> value = new List<KeyCommand>();
			this.commandContexts.Add(contextId, value);
			if (setAsActive)
			{
				if (this.isContextLocked)
				{
					this.queuedContextId = new int?(contextId);
				}
				else
				{
					this.activeContextId = contextId;
					this.SetActiveContext(value);
				}
			}
		}

		public void SetActiveContext(int contextId)
		{
			if (this.isContextLocked)
			{
				this.queuedContextId = new int?(contextId);
			}
			else
			{
				this.activeContextId = contextId;
				this.SetActiveContext(this.commandContexts[contextId]);
			}
		}

		public int GetActiveContextId()
		{
			return this.activeContextId;
		}

		private void SetActiveContext(List<KeyCommand> context)
		{
			this.activeContext = context;
		}

		public void AddMasterCommand(KeyCommand command)
		{
			this.AddCommand(command, this.masterContext);
		}

		public void AddCommand(KeyCommand command, int contextId)
		{
			List<KeyCommand> context = this.commandContexts[contextId];
			this.AddCommand(command, context);
		}

		public void AddCommand(KeyCommand command)
		{
			this.AddCommand(command, this.activeContext);
		}

		private void AddCommand(KeyCommand command, List<KeyCommand> context)
		{
			if (this.autoAddCtrlModifier)
			{
				command.SetCtrlModifier();
			}
			if (this.autoAddShiftModifier)
			{
				command.SetShiftModifier();
			}
			if (this.autoAddAltModifier)
			{
				command.SetAltModifier();
			}
			context.Add(command);
		}

		public void EnableAutoAddShiftModifier()
		{
			this.autoAddShiftModifier = true;
		}

		public void EnableAutoAddCtrlModifier()
		{
			this.autoAddCtrlModifier = true;
		}

		public void EnableAutoAddAltModifier()
		{
			this.autoAddAltModifier = true;
		}

		public void DisableAutoAddShiftModifier()
		{
			this.autoAddShiftModifier = false;
		}

		public void DisableAutoAddCtrlModifier()
		{
			this.autoAddCtrlModifier = false;
		}

		public void DisableAutoAddAltModifier()
		{
			this.autoAddAltModifier = false;
		}

		public void DisableModifierAdders()
		{
			this.autoAddCtrlModifier = false;
			this.autoAddAltModifier = false;
			this.autoAddShiftModifier = false;
		}

		public void LockContext()
		{
			this.isContextLocked = true;
		}

		public void UnlockContext()
		{
			this.isContextLocked = false;
		}

		public void SetQueuedContext()
		{
			if (this.queuedContextId != null)
			{
				this.SetActiveContext(this.queuedContextId.Value);
				this.queuedContextId = null;
			}
		}

		public const int CONTEXT_CHEATS = -100;

		public const int CONTEXT_DEFAULT = 0;

		public const int CONTEXT_MAIN_HUB = 10;

		public const int CONTEXT_NONE_GAMEPLAY = 20;

		public const int CONTEXT_TAB_MODE = 30;

		public const int CONTEXT_TAB_HEROES = 40;

		public const int CONTEXT_TAB_ARTIFACT = 50;

		public const int CONTEXT_TAB_SHOP = 60;

		public Dictionary<int, List<KeyCommand>> commandContexts;

		public List<KeyCommand> activeContext;

		public List<KeyCommand> masterContext;

		private int? queuedContextId;

		private bool autoAddShiftModifier;

		private bool autoAddCtrlModifier;

		private bool autoAddAltModifier;

		private bool isContextLocked;

		private int activeContextId;
	}
}
