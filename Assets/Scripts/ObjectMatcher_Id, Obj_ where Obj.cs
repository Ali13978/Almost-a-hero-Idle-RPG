using System;
using System.Collections.Generic;

public class ObjectMatcher<Id, Obj> where Obj : class, IMatchRemovable
{
	public ObjectMatcher(Func<Obj> creator, Action<Obj, bool> disposer)
	{
		this.pairs = new Dictionary<Id, Obj>();
		this.objs = new List<Obj>();
		this.ids = new List<Id>();
		this.creatorFunction = creator;
		this.objectDisposer = disposer;
	}

	public Obj GetObjectFor(Id id)
	{
		Obj obj = (Obj)((object)null);
		if (this.pairs.TryGetValue(id, out obj))
		{
			return obj;
		}
		obj = this.creatorFunction();
		this.objs.Add(obj);
		this.ids.Add(id);
		this.pairs.Add(id, obj);
		return obj;
	}

	public void RemoveDisposibles(bool challengeActive)
	{
		for (int i = this.objs.Count - 1; i >= 0; i--)
		{
			Obj arg = this.objs[i];
			if (arg.willDispose)
			{
				this.objs.RemoveAt(i);
				Id key = this.ids[i];
				this.ids.RemoveAt(i);
				this.pairs.Remove(key);
				this.objectDisposer(arg, challengeActive);
			}
			else
			{
				arg.willDispose = true;
			}
		}
	}

	public Dictionary<Id, Obj> pairs;

	public List<Obj> objs;

	public List<Id> ids;

	public Func<Obj> creatorFunction;

	public Action<Obj, bool> objectDisposer;
}
