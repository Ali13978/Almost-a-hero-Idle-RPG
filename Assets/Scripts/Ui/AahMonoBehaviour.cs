using System;
using UnityEngine;

namespace Ui
{
	public class AahMonoBehaviour : MonoBehaviour
	{
		public virtual void Register()
		{
		}

		public virtual void Init()
		{
			UnityEngine.Debug.LogError(base.gameObject.name + " is subscribed to AahMonoBehaviour Init function but the function is not overridden.");
		}

		public virtual void AahUpdate(float dt)
		{
			UnityEngine.Debug.LogError(base.gameObject.name + " is subscribed to AahMonoBehaviour AahUpdate function but the function is not overridden.");
		}

		protected void AddToInits()
		{
            
			UiManager.toInits.Add(this);
        }

		protected void AddToUpdates()
		{
			this.addToUpdates = true;
			UiManager.toUpdates.Add(this);
			this.softGameObject = base.gameObject;
		}

		protected void RemoveFromUpdates()
		{
			UiManager.toUpdates.Remove(this);
		}

		private void OnDestroy()
		{
			if (this.addToUpdates)
			{
				UiManager.willRemoveFromToUpdates.Add(this);
			}
		}

		[NonSerialized]
		public bool addToUpdates;

		[NonSerialized]
		public GameObject softGameObject;
	}
}
