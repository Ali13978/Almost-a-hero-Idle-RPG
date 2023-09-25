using System;
using System.Collections.Generic;
using SA.Common.Models;

namespace SA.IOSNative.Contacts
{
	public class ContactsResult : Result
	{
		public ContactsResult(List<Contact> contacts)
		{
			this._Contacts = contacts;
		}

		public ContactsResult(Error error) : base(error)
		{
		}

		public List<Contact> Contacts
		{
			get
			{
				return this._Contacts;
			}
		}

		private List<Contact> _Contacts = new List<Contact>();
	}
}
