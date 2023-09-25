using System;
using UnityEngine;

namespace Render
{
	public abstract class RenderPoolSmartProjectile : RenderPool<SmartProjectileRenderer>
	{
		public RenderPoolSmartProjectile(SmartProjectileRenderer prefab, Transform parent) : base(parent)
		{
			this.prefab = prefab;
		}

		public bool HasNotValidPrefab
		{
			get
			{
				return this.prefab == null;
			}
		}

		public void ChangePrefab(SmartProjectileRenderer prefab)
		{
			this.prefab = prefab;
		}

		protected override void Activate(SmartProjectileRenderer instance)
		{
			instance.gameObject.SetActive(true);
		}

		protected override void Deactivate(SmartProjectileRenderer instance)
		{
			instance.gameObject.SetActive(false);
		}

		protected override void Destroy(SmartProjectileRenderer instance)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}

		protected override bool IsActive(SmartProjectileRenderer instance)
		{
			return instance.gameObject.activeSelf;
		}

		protected override SmartProjectileRenderer Create()
		{
			return UnityEngine.Object.Instantiate<SmartProjectileRenderer>(this.prefab, this.parent);
		}

		private SmartProjectileRenderer prefab;
	}
}
