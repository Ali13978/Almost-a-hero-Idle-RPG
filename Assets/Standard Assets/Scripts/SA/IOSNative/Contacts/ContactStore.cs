using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Data;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.IOSNative.Contacts
{
	public class ContactStore : Singleton<ContactStore>
	{
		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event Action<ContactsResult> ContactsLoadResult;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event Action<ContactsResult> ContactsPickResult;



		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void ShowContactsPickerUI(Action<ContactsResult> callback)
		{
			this.ContactsPickResult += callback;
		}

		public void RetrievePhoneContacts(Action<ContactsResult> callback)
		{
			this.ContactsLoadResult += callback;
		}

		private Contact ParseContactData(string data)
		{
			string[] array = data.Split(new char[]
			{
				'|'
			});
			Contact contact = new Contact();
			contact.GivenName = array[0];
			contact.FamilyName = array[1];
			string[] collection = Converter.ParseArray(array[2], "%%%");
			contact.Emails.AddRange(collection);
			string[] array2 = Converter.ParseArray(array[3], "%%%");
			string[] array3 = Converter.ParseArray(array[4], "%%%");
			for (int i = 0; i < array2.Length; i++)
			{
				PhoneNumber phoneNumber = new PhoneNumber();
				phoneNumber.CountryCode = array2[i];
				phoneNumber.Digits = array3[i];
				contact.PhoneNumbers.Add(phoneNumber);
			}
			return contact;
		}

		private List<Contact> ParseContactArray(string data)
		{
			string[] array = data.Split(new string[]
			{
				"|%|"
			}, StringSplitOptions.None);
			List<Contact> list = new List<Contact>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "endofline")
				{
					break;
				}
				Contact item = this.ParseContactData(array[i]);
				list.Add(item);
			}
			return list;
		}

		private void OnContactPickerDidCancel(string errorData)
		{
			Error error = new Error(0, "User Canceled");
			ContactsResult obj = new ContactsResult(error);
			this.ContactsPickResult(obj);
			this.ContactsPickResult = delegate(ContactsResult A_0)
			{
			};
		}

		private void OnPickerDidSelectContacts(string data)
		{
			ISN_Logger.Log("[ContactStore] OnPickerDidSelectContacts", LogType.Log);
			List<Contact> list = this.ParseContactArray(data);
			ISN_Logger.Log("[ContactStore] Picked " + list.Count + " contacts", LogType.Log);
			ContactsResult obj = new ContactsResult(list);
			this.ContactsPickResult(obj);
			this.ContactsPickResult = delegate(ContactsResult A_0)
			{
			};
		}

		private void OnContactsRetrieveFailed(string errorData)
		{
			ISN_Logger.Log("[ContactStore] OnContactsRetrieveFailed", LogType.Log);
			Error error = new Error(errorData);
			ContactsResult obj = new ContactsResult(error);
			this.ContactsLoadResult(obj);
			this.ContactsLoadResult = delegate(ContactsResult A_0)
			{
			};
		}

		private void OnContactsRetrieveFinished(string data)
		{
			ISN_Logger.Log("[ContactStore] OnContactsRetrieveFinished", LogType.Log);
			List<Contact> list = this.ParseContactArray(data);
			ISN_Logger.Log("[ContactStore] Loaded " + list.Count + " contacts", LogType.Log);
			ContactsResult obj = new ContactsResult(list);
			this.ContactsLoadResult(obj);
			this.ContactsLoadResult = delegate(ContactsResult A_0)
			{
			};
		}
	}
}
