using System;
using UnityEngine;

namespace SA.IOSNative.StoreKit
{
	[Serializable]
	public class Product
	{
		public void UpdatePriceByTier()
		{
			this._Price = SK_Util.GetPriceByTier(this._PriceTier);
		}

		public string Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		public string DisplayName
		{
			get
			{
				return this._DisplayName;
			}
			set
			{
				this._DisplayName = value;
			}
		}

		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}

		public ProductType Type
		{
			get
			{
				return this._ProductType;
			}
			set
			{
				this._ProductType = value;
			}
		}

		public float Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				this._Price = value;
			}
		}

		public long PriceInMicros
		{
			get
			{
				return Convert.ToInt64(this._Price * 1000000f);
			}
		}

		public string LocalizedPrice
		{
			get
			{
				if (this._LocalizedPrice.Equals(string.Empty))
				{
					return this.Price + " " + this._CurrencySymbol;
				}
				return this._LocalizedPrice;
			}
			set
			{
				this._LocalizedPrice = value;
			}
		}

		public string CurrencySymbol
		{
			get
			{
				return this._CurrencySymbol;
			}
			set
			{
				this._CurrencySymbol = value;
			}
		}

		public string CurrencyCode
		{
			get
			{
				return this._CurrencyCode;
			}
			set
			{
				this._CurrencyCode = value;
			}
		}

		public Texture2D Texture
		{
			get
			{
				return this._Texture;
			}
			set
			{
				this._Texture = value;
			}
		}

		public PriceTier PriceTier
		{
			get
			{
				return this._PriceTier;
			}
			set
			{
				this._PriceTier = value;
			}
		}

		public bool IsAvailable
		{
			get
			{
				return this._IsAvailable;
			}
			set
			{
				this._IsAvailable = value;
			}
		}

		public bool IsOpen = true;

		[SerializeField]
		private bool _IsAvailable;

		[SerializeField]
		private string _Id = string.Empty;

		[SerializeField]
		private string _DisplayName = "New Product";

		[SerializeField]
		private string _Description;

		[SerializeField]
		private float _Price = 0.99f;

		[SerializeField]
		private string _LocalizedPrice = string.Empty;

		[SerializeField]
		private string _CurrencySymbol = "$";

		[SerializeField]
		private string _CurrencyCode = "USD";

		[SerializeField]
		private Texture2D _Texture;

		[SerializeField]
		private ProductType _ProductType;

		[SerializeField]
		private PriceTier _PriceTier;
	}
}
