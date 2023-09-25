using System;

namespace SA.IOSNative.StoreKit
{
	public enum TransactionErrorCode
	{
		SKErrorUnknown,
		SKErrorClientInvalid,
		SKErrorPaymentCanceled,
		SKErrorPaymentInvalid,
		SKErrorPaymentNotAllowed,
		SKErrorStoreProductNotAvailable,
		SKErrorPaymentNoPurchasesToRestore,
		SKErrorPaymentServiceNotInitialized,
		SKErrorNone
	}
}
