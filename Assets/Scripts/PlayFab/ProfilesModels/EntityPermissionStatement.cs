using System;

namespace PlayFab.ProfilesModels
{
	[Serializable]
	public class EntityPermissionStatement
	{
		public string Action;

		public string Comment;

		public object Condition;

		public EffectType Effect;

		public object Principal;

		public string Resource;
	}
}
