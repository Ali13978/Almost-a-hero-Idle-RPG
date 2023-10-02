#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>



struct String_t;



IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_tD1F0B9705D5594A42609E77459719A6C0DB9DD53 
{
};
struct AnalyticsCommon_t9A0DD5EB3A8E861A4481AA17C2105396E9C4E34C  : public RuntimeObject
{
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
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
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
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif



IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnalyticsCommon_set_ugsAnalyticsEnabledInternal_m4264C600AC21370AB14F1A2D811CF8976C63E864 (bool ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnalyticsCommon_set_ugsAnalyticsEnabled_m998A626F754515EF4257B9FC3BD0850DDF4370F3 (bool ___0_value, const RuntimeMethod* method) ;
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
// Method Definition Index: 75628
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnalyticsCommon_set_ugsAnalyticsEnabledInternal_m4264C600AC21370AB14F1A2D811CF8976C63E864 (bool ___0_value, const RuntimeMethod* method) 
{
	typedef void (*AnalyticsCommon_set_ugsAnalyticsEnabledInternal_m4264C600AC21370AB14F1A2D811CF8976C63E864_ftn) (bool);
	static AnalyticsCommon_set_ugsAnalyticsEnabledInternal_m4264C600AC21370AB14F1A2D811CF8976C63E864_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AnalyticsCommon_set_ugsAnalyticsEnabledInternal_m4264C600AC21370AB14F1A2D811CF8976C63E864_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Analytics.AnalyticsCommon::set_ugsAnalyticsEnabledInternal(System.Boolean)");
	_il2cpp_icall_func(___0_value);
}
// Method Definition Index: 75629
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnalyticsCommon_set_ugsAnalyticsEnabled_m998A626F754515EF4257B9FC3BD0850DDF4370F3 (bool ___0_value, const RuntimeMethod* method) 
{
	{
		bool L_0 = ___0_value;
		AnalyticsCommon_set_ugsAnalyticsEnabledInternal_m4264C600AC21370AB14F1A2D811CF8976C63E864(L_0, NULL);
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
// Method Definition Index: 75630
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UGSAnalyticsInternalTools_SetPrivacyStatus_mF4F04D971EBA3A54A9AD22DC4E8D95711334516A (bool ___0_status, const RuntimeMethod* method) 
{
	{
		bool L_0 = ___0_status;
		AnalyticsCommon_set_ugsAnalyticsEnabled_m998A626F754515EF4257B9FC3BD0850DDF4370F3(L_0, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
