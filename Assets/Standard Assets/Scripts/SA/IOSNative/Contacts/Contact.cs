using System;
using System.Collections.Generic;

namespace SA.IOSNative.Contacts
{
	public class Contact
	{
		public string GivenName = string.Empty;

		public string FamilyName = string.Empty;

		public List<string> Emails = new List<string>();

		public List<PhoneNumber> PhoneNumbers = new List<PhoneNumber>();
	}
}
