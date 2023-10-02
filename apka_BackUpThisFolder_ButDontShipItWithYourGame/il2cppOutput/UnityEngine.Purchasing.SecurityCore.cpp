#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>



struct AppleInAppPurchaseReceiptU5BU5D_t8A8951A16B47F87B92AC3879619FB94166150C8A;
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6;
struct AppleReceipt_t16E9FEBF193F54B9B4E5D3323C48E487DCF3414C;
struct Exception_t;
struct IAPSecurityException_t0CF168A490D20D9F3A643C75A77826B27ABDEA9B;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct String_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* Exception_t_il2cpp_TypeInfo_var;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_tACFCF0B6D156A8ED51661BAE73BC8AEF5B982660 
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	uint8_t ___m_value;
};
struct DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D 
{
	uint64_t ____dateData;
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6  : public RuntimeObject
{
	int32_t ___U3CquantityU3Ek__BackingField;
	String_t* ___U3CproductIDU3Ek__BackingField;
	String_t* ___U3CtransactionIDU3Ek__BackingField;
	String_t* ___U3CoriginalTransactionIdentifierU3Ek__BackingField;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___U3CpurchaseDateU3Ek__BackingField;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___U3CoriginalPurchaseDateU3Ek__BackingField;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___U3CsubscriptionExpirationDateU3Ek__BackingField;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___U3CcancellationDateU3Ek__BackingField;
	int32_t ___U3CisFreeTrialU3Ek__BackingField;
	int32_t ___U3CproductTypeU3Ek__BackingField;
	int32_t ___U3CisIntroductoryPricePeriodU3Ek__BackingField;
};
struct AppleReceipt_t16E9FEBF193F54B9B4E5D3323C48E487DCF3414C  : public RuntimeObject
{
	String_t* ___U3CbundleIDU3Ek__BackingField;
	String_t* ___U3CappVersionU3Ek__BackingField;
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___U3CopaqueU3Ek__BackingField;
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___U3ChashU3Ek__BackingField;
	String_t* ___U3CoriginalApplicationVersionU3Ek__BackingField;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___U3CreceiptCreationDateU3Ek__BackingField;
	AppleInAppPurchaseReceiptU5BU5D_t8A8951A16B47F87B92AC3879619FB94166150C8A* ___inAppPurchaseReceipts;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct IAPSecurityException_t0CF168A490D20D9F3A643C75A77826B27ABDEA9B  : public Exception_t
{
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_StaticFields
{
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ___s_daysToMonth365;
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ___s_daysToMonth366;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___MinValue;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___MaxValue;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___UnixEpoch;
};
struct Exception_t_StaticFields
{
	RuntimeObject* ___s_EDILock;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint8_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint8_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint8_t value)
	{
		m_Items[index] = value;
	}
};



IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Exception__ctor_m203319D1EA1274689B380A947B4ADC8445662B8F (Exception_t* __this, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 75685
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleReceipt_set_bundleID_mA1F88F87405C2A5979ACBB58D5DC010716EFB877 (AppleReceipt_t16E9FEBF193F54B9B4E5D3323C48E487DCF3414C* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:14>
		String_t* L_0 = ___0_value;
		__this->___U3CbundleIDU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CbundleIDU3Ek__BackingField), (void*)L_0);
		return;
	}
}
// Method Definition Index: 75686
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleReceipt_set_appVersion_m59F5F0B3DF859FC014F55CB92DFAB42C8367E76D (AppleReceipt_t16E9FEBF193F54B9B4E5D3323C48E487DCF3414C* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:19>
		String_t* L_0 = ___0_value;
		__this->___U3CappVersionU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CappVersionU3Ek__BackingField), (void*)L_0);
		return;
	}
}
// Method Definition Index: 75687
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleReceipt_set_opaque_m4FDDBCA4DB089D281154D7F537533B764875276C (AppleReceipt_t16E9FEBF193F54B9B4E5D3323C48E487DCF3414C* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:29>
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = ___0_value;
		__this->___U3CopaqueU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CopaqueU3Ek__BackingField), (void*)L_0);
		return;
	}
}
// Method Definition Index: 75688
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleReceipt_set_hash_mBDA78FE22F99E68A4A9BAAA8D824ABF2B02F7921 (AppleReceipt_t16E9FEBF193F54B9B4E5D3323C48E487DCF3414C* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:34>
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = ___0_value;
		__this->___U3ChashU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3ChashU3Ek__BackingField), (void*)L_0);
		return;
	}
}
// Method Definition Index: 75689
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleReceipt_set_originalApplicationVersion_mF68D4C59B12264FD840D3860AD5CA45937421DC8 (AppleReceipt_t16E9FEBF193F54B9B4E5D3323C48E487DCF3414C* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:39>
		String_t* L_0 = ___0_value;
		__this->___U3CoriginalApplicationVersionU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CoriginalApplicationVersionU3Ek__BackingField), (void*)L_0);
		return;
	}
}
// Method Definition Index: 75690
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleReceipt_set_receiptCreationDate_mE84412087D877A38E66984C4ACE982598F7229B0 (AppleReceipt_t16E9FEBF193F54B9B4E5D3323C48E487DCF3414C* __this, DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:44>
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_0 = ___0_value;
		__this->___U3CreceiptCreationDateU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 75691
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleReceipt__ctor_mBC2D8186C1DD8B5601E6337907E32A7070F1032A (AppleReceipt_t16E9FEBF193F54B9B4E5D3323C48E487DCF3414C* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 75692
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt_set_quantity_mE9F9F54A27DAE0A441147CCB2995FC37BAA47F14 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:60>
		int32_t L_0 = ___0_value;
		__this->___U3CquantityU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 75693
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AppleInAppPurchaseReceipt_get_productID_m084EF4FFEBF308CE58401DBA48416D05CD01AC6A (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:65>
		String_t* L_0 = __this->___U3CproductIDU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 75694
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt_set_productID_m4A6851EB19B52428981949AA404E29FDF0574573 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:65>
		String_t* L_0 = ___0_value;
		__this->___U3CproductIDU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CproductIDU3Ek__BackingField), (void*)L_0);
		return;
	}
}
// Method Definition Index: 75695
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AppleInAppPurchaseReceipt_get_transactionID_mDFD3CCA8171F1F2648DA74BB29560D5A1D3AC9AF (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:70>
		String_t* L_0 = __this->___U3CtransactionIDU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 75696
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt_set_transactionID_m4697780649988191098121B31BAF8AE6045180C7 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:70>
		String_t* L_0 = ___0_value;
		__this->___U3CtransactionIDU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CtransactionIDU3Ek__BackingField), (void*)L_0);
		return;
	}
}
// Method Definition Index: 75697
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AppleInAppPurchaseReceipt_get_originalTransactionIdentifier_m4CCC48A28E54C696DB545948158A86906EA4B052 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:75>
		String_t* L_0 = __this->___U3CoriginalTransactionIdentifierU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 75698
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt_set_originalTransactionIdentifier_m987D93700377FB81897D206F65D08CC908BDFB96 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:75>
		String_t* L_0 = ___0_value;
		__this->___U3CoriginalTransactionIdentifierU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CoriginalTransactionIdentifierU3Ek__BackingField), (void*)L_0);
		return;
	}
}
// Method Definition Index: 75699
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D AppleInAppPurchaseReceipt_get_purchaseDate_mD0C3B7344A76EE7BFEC69AB815F02B52F98145B9 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:80>
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_0 = __this->___U3CpurchaseDateU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 75700
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt_set_purchaseDate_m79DB71B8BB914A129B3594C9B8770E23184E4B87 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:80>
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_0 = ___0_value;
		__this->___U3CpurchaseDateU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 75701
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt_set_originalPurchaseDate_m95399776CB56AA18A87A006DD0103F3FBC21D858 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:85>
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_0 = ___0_value;
		__this->___U3CoriginalPurchaseDateU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 75702
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D AppleInAppPurchaseReceipt_get_subscriptionExpirationDate_m643E75D4AFD8C1D478C51377E72D5809B383648F (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:90>
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_0 = __this->___U3CsubscriptionExpirationDateU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 75703
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt_set_subscriptionExpirationDate_mB09CE098283C52BE66DCD8017D2D77E5EB38AE39 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:90>
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_0 = ___0_value;
		__this->___U3CsubscriptionExpirationDateU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 75704
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D AppleInAppPurchaseReceipt_get_cancellationDate_mDF2DD93796406B22172FBB7670D94A28F9AB0DC7 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:96>
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_0 = __this->___U3CcancellationDateU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 75705
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt_set_cancellationDate_m9D578D8213D0340DF3EB0BE6A6D296B2343A5802 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:96>
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_0 = ___0_value;
		__this->___U3CcancellationDateU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 75706
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AppleInAppPurchaseReceipt_get_isFreeTrial_m759D2E45FFC7CEDF367DCBCFB621DB339BDDB5EC (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:101>
		int32_t L_0 = __this->___U3CisFreeTrialU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 75707
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt_set_isFreeTrial_m5CDF1AB9619EDC68876559BE1A16CC9906CD9BDF (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:101>
		int32_t L_0 = ___0_value;
		__this->___U3CisFreeTrialU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 75708
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AppleInAppPurchaseReceipt_get_productType_mA5E7583E60B52D6D959FA7123FC5E9BF1D8774B1 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:106>
		int32_t L_0 = __this->___U3CproductTypeU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 75709
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt_set_productType_mB62D71D6EB76E643F4F72AC0F12A59FC41D16317 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:106>
		int32_t L_0 = ___0_value;
		__this->___U3CproductTypeU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 75710
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AppleInAppPurchaseReceipt_get_isIntroductoryPricePeriod_m562487C7A6F83DA38820E1F47D21D3E481C790D5 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:111>
		int32_t L_0 = __this->___U3CisIntroductoryPricePeriodU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 75711
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt_set_isIntroductoryPricePeriod_m068C04597DEF74873326A24950D8C21FBE1CDDC0 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/AppleReceipt.cs:111>
		int32_t L_0 = ___0_value;
		__this->___U3CisIntroductoryPricePeriodU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 75712
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AppleInAppPurchaseReceipt__ctor_m7A1AA91A843F1F6710D5B130ECEFA43AC6152FA5 (AppleInAppPurchaseReceipt_t45E81B173E0600832D2E259DA4A6CFA9C54A7CD6* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 75713
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IAPSecurityException__ctor_m6A57B7B67543FE72E27DF1424812F14667CC38D0 (IAPSecurityException_t0CF168A490D20D9F3A643C75A77826B27ABDEA9B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/IAPSecurityException.cs:13>
		il2cpp_codegen_runtime_class_init_inline(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m203319D1EA1274689B380A947B4ADC8445662B8F(__this, NULL);
		//<source_info:./Library/PackageCache/com.unity.purchasing@4.9.4/Runtime/SecurityCore/IAPSecurityException.cs:13>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
