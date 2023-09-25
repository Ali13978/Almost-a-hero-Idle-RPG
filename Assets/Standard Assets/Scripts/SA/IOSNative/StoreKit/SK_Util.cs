using System;

namespace SA.IOSNative.StoreKit
{
	public static class SK_Util
	{
		public static float GetPriceByTier(PriceTier priceTier)
		{
			int num = (int)(priceTier + 1);
			float result = 0f;
			float num2 = (float)num;
			if (num2 < 51f)
			{
				result = num2 - 0.01f;
			}
			else if (num2 < 61f)
			{
				float num3 = num2 - 50f;
				result = 50f + num3 * 5f - 0.01f;
			}
			else
			{
				switch (num)
				{
				case 61:
					result = 109.99f;
					break;
				case 62:
					result = 119.99f;
					break;
				case 63:
					result = 124.99f;
					break;
				case 64:
					result = 129.99f;
					break;
				case 65:
					result = 139.99f;
					break;
				case 66:
					result = 149.99f;
					break;
				case 67:
					result = 159.99f;
					break;
				case 68:
					result = 169.99f;
					break;
				case 69:
					result = 174.99f;
					break;
				case 70:
					result = 179.99f;
					break;
				case 72:
					result = 199.99f;
					break;
				case 73:
					result = 209.99f;
					break;
				case 74:
					result = 219.99f;
					break;
				case 75:
					result = 229.99f;
					break;
				case 76:
					result = 239.99f;
					break;
				case 77:
					result = 249.99f;
					break;
				case 78:
					result = 299.99f;
					break;
				case 79:
					result = 349.99f;
					break;
				case 80:
					result = 399.99f;
					break;
				case 81:
					result = 449.99f;
					break;
				case 82:
					result = 499.99f;
					break;
				case 83:
					result = 599.99f;
					break;
				case 84:
					result = 699.99f;
					break;
				case 85:
					result = 799.99f;
					break;
				case 86:
					result = 899.99f;
					break;
				case 87:
					result = 999.99f;
					break;
				}
			}
			return result;
		}
	}
}
