using System;

namespace SA.Common.Models
{
	public class Result
	{
		public Result()
		{
		}

		public Result(Error error)
		{
			this._Error = error;
		}

		public Error Error
		{
			get
			{
				return this._Error;
			}
		}

		public bool HasError
		{
			get
			{
				return this._Error != null;
			}
		}

		public bool IsSucceeded
		{
			get
			{
				return !this.HasError;
			}
		}

		public bool IsFailed
		{
			get
			{
				return this.HasError;
			}
		}

		protected Error _Error;
	}
}
