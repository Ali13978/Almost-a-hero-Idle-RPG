using System;

namespace Render
{
	public interface IRenderPool
	{
		void OnPreFrame();

		void OnPostFrame();
	}
}
