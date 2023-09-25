Shader "Spine/Skeleton Lit"
{
  Properties
  {
    _Cutoff ("Shadow alpha cutoff", Range(0, 1)) = 0.1
    [NoScaleOffset] _MainTex ("Main Texture", 2D) = "black" {}
    [Toggle(_STRAIGHT_ALPHA_INPUT)] _StraightAlphaInput ("Straight Alpha Texture", float) = 0
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    LOD 100
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "Vertex"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      LOD 100
      ZWrite Off
      Cull Off
      Blend One OneMinusSrcAlpha
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 unity_LightColor[8];
      //uniform float4 unity_LightPosition[8];
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 glstate_lightmodel_ambient;
      //uniform float4x4 unity_MatrixInvV;
      //uniform float4x4 unity_MatrixVP;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float3 vertex :POSITION0;
          float4 color :COLOR0;
          float3 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float4 color :COLOR0;
          float2 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 color :COLOR0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float3 u_xlat16_1;
      float3 u_xlat16_2;
      float3 u_xlat16_3;
      float u_xlat12;
      float u_xlat16_13;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0.x = (conv_mxt4x4_1(unity_WorldToObject).z * conv_mxt4x4_0(unity_MatrixInvV).y).x;
          u_xlat0.x = ((conv_mxt4x4_0(unity_WorldToObject).z * conv_mxt4x4_0(unity_MatrixInvV).x) + u_xlat0.x).x;
          u_xlat0.x = ((conv_mxt4x4_2(unity_WorldToObject).z * conv_mxt4x4_0(unity_MatrixInvV).z) + u_xlat0.x).x;
          u_xlat0.x = ((conv_mxt4x4_3(unity_WorldToObject).z * conv_mxt4x4_0(unity_MatrixInvV).w) + u_xlat0.x).x;
          u_xlat0.w = mul(unity_MatrixInvV, unity_WorldToObject[0]).x;
          u_xlat0.xy = float2((-u_xlat0.xw));
          u_xlat12 = (conv_mxt4x4_1(unity_WorldToObject).z * conv_mxt4x4_2(unity_MatrixInvV).y).x;
          u_xlat12 = ((conv_mxt4x4_0(unity_WorldToObject).z * conv_mxt4x4_2(unity_MatrixInvV).x) + u_xlat12).x;
          u_xlat12 = ((conv_mxt4x4_2(unity_WorldToObject).z * conv_mxt4x4_2(unity_MatrixInvV).z) + u_xlat12).x;
          u_xlat12 = ((conv_mxt4x4_3(unity_WorldToObject).z * conv_mxt4x4_2(unity_MatrixInvV).w) + u_xlat12).x;
          u_xlat0.z = (-u_xlat12);
          u_xlat0.xyz = float3(normalize(u_xlat0.xyz));
          u_xlat16_1.x = dot(u_xlat0.xyz, unity_LightPosition[0].xyz);
          u_xlat16_1.x = max(u_xlat16_1.x, 0);
          u_xlat16_1.xyz = float3((u_xlat16_1.xxx * in_v.color.xyz));
          u_xlat16_1.xyz = float3((u_xlat16_1.xyz * unity_LightColor[0].xyz));
          u_xlat16_1.xyz = float3((u_xlat16_1.xyz * float3(0.5, 0.5, 0.5)));
          u_xlat16_1.xyz = float3(min(u_xlat16_1.xyz, float3(1, 1, 1)));
          u_xlat16_1.xyz = float3(((in_v.color.xyz * glstate_lightmodel_ambient.xyz) + u_xlat16_1.xyz));
          u_xlat16_13 = dot(u_xlat0.xyz, unity_LightPosition[1].xyz);
          u_xlat16_13 = max(u_xlat16_13, 0);
          u_xlat16_2.xyz = float3((float3(u_xlat16_13, u_xlat16_13, u_xlat16_13) * in_v.color.xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * unity_LightColor[1].xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * float3(0.5, 0.5, 0.5)));
          u_xlat16_2.xyz = float3(min(u_xlat16_2.xyz, float3(1, 1, 1)));
          u_xlat16_1.xyz = float3((u_xlat16_1.xyz + u_xlat16_2.xyz));
          u_xlat16_13 = dot(u_xlat0.xyz, unity_LightPosition[2].xyz);
          u_xlat16_13 = max(u_xlat16_13, 0);
          u_xlat16_2.xyz = float3((float3(u_xlat16_13, u_xlat16_13, u_xlat16_13) * in_v.color.xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * unity_LightColor[2].xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * float3(0.5, 0.5, 0.5)));
          u_xlat16_2.xyz = float3(min(u_xlat16_2.xyz, float3(1, 1, 1)));
          u_xlat16_1.xyz = float3((u_xlat16_1.xyz + u_xlat16_2.xyz));
          u_xlat16_13 = dot(u_xlat0.xyz, unity_LightPosition[3].xyz);
          u_xlat16_13 = max(u_xlat16_13, 0);
          u_xlat16_2.xyz = float3((float3(u_xlat16_13, u_xlat16_13, u_xlat16_13) * in_v.color.xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * unity_LightColor[3].xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * float3(0.5, 0.5, 0.5)));
          u_xlat16_2.xyz = float3(min(u_xlat16_2.xyz, float3(1, 1, 1)));
          u_xlat16_1.xyz = float3((u_xlat16_1.xyz + u_xlat16_2.xyz));
          u_xlat16_13 = dot(u_xlat0.xyz, unity_LightPosition[4].xyz);
          u_xlat16_13 = max(u_xlat16_13, 0);
          u_xlat16_2.xyz = float3((float3(u_xlat16_13, u_xlat16_13, u_xlat16_13) * in_v.color.xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * unity_LightColor[4].xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * float3(0.5, 0.5, 0.5)));
          u_xlat16_2.xyz = float3(min(u_xlat16_2.xyz, float3(1, 1, 1)));
          u_xlat16_1.xyz = float3((u_xlat16_1.xyz + u_xlat16_2.xyz));
          u_xlat16_13 = dot(u_xlat0.xyz, unity_LightPosition[5].xyz);
          u_xlat16_13 = max(u_xlat16_13, 0);
          u_xlat16_2.xyz = float3((float3(u_xlat16_13, u_xlat16_13, u_xlat16_13) * in_v.color.xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * unity_LightColor[5].xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * float3(0.5, 0.5, 0.5)));
          u_xlat16_2.xyz = float3(min(u_xlat16_2.xyz, float3(1, 1, 1)));
          u_xlat16_1.xyz = float3((u_xlat16_1.xyz + u_xlat16_2.xyz));
          u_xlat16_13 = dot(u_xlat0.xyz, unity_LightPosition[6].xyz);
          u_xlat16_2.x = dot(u_xlat0.xyz, unity_LightPosition[7].xyz);
          u_xlat16_2.x = max(u_xlat16_2.x, 0);
          u_xlat16_2.xyz = float3((u_xlat16_2.xxx * in_v.color.xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * unity_LightColor[7].xyz));
          u_xlat16_2.xyz = float3((u_xlat16_2.xyz * float3(0.5, 0.5, 0.5)));
          u_xlat16_2.xyz = float3(min(u_xlat16_2.xyz, float3(1, 1, 1)));
          u_xlat16_13 = max(u_xlat16_13, 0);
          u_xlat16_3.xyz = float3((float3(u_xlat16_13, u_xlat16_13, u_xlat16_13) * in_v.color.xyz));
          u_xlat16_3.xyz = float3((u_xlat16_3.xyz * unity_LightColor[6].xyz));
          u_xlat16_3.xyz = float3((u_xlat16_3.xyz * float3(0.5, 0.5, 0.5)));
          u_xlat16_3.xyz = float3(min(u_xlat16_3.xyz, float3(1, 1, 1)));
          u_xlat16_1.xyz = float3((u_xlat16_1.xyz + u_xlat16_3.xyz));
          out_v.color.xyz = float3((u_xlat16_2.xyz + u_xlat16_1.xyz));
          out_v.color.xyz = float3(clamp(out_v.color.xyz, 0, 1));
          out_v.color.w = in_v.color.w;
          out_v.color.w = clamp(out_v.color.w, 0, 1);
          out_v.texcoord.xy = float2(in_v.texcoord.xy);
          out_v.vertex = UnityObjectToClipPos(float4(in_v.vertex, 0));
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat10_0;
      float3 u_xlat16_1_d;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0 = tex2D(_MainTex, in_f.texcoord.xy);
          u_xlat16_1_d.xyz = float3((u_xlat10_0.xyz * in_f.color.xyz));
          out_f.color.w = (u_xlat10_0.w * in_f.color.w);
          out_f.color.xyz = float3((u_xlat16_1_d.xyz + u_xlat16_1_d.xyz));
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: Caster
    {
      Name "Caster"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "SHADOWCASTER"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
        "SHADOWSUPPORT" = "true"
      }
      LOD 100
      Cull Off
      Offset 1, 1
      Fog
      { 
        Mode  Off
      } 
      Blend One OneMinusSrcAlpha
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile SHADOWS_DEPTH
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 unity_LightShadowBias;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      uniform float _Cutoff;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float u_xlat4;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0 = UnityObjectToClipPos(in_v.vertex);
          u_xlat1.x = (unity_LightShadowBias.x / u_xlat0.w);
          u_xlat1.x = clamp(u_xlat1.x, 0, 1);
          u_xlat4 = (u_xlat0.z + u_xlat1.x);
          u_xlat1.x = max((-u_xlat0.w), u_xlat4);
          out_v.vertex.xyw = u_xlat0.xyw;
          u_xlat0.x = ((-u_xlat4) + u_xlat1.x);
          out_v.vertex.z = ((unity_LightShadowBias.y * u_xlat0.x) + u_xlat4);
          out_v.texcoord1.xy = float2(TRANSFORM_TEX(in_v.texcoord.xy, _MainTex));
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float u_xlat10_0;
      int u_xlatb0;
      float u_xlat16_1;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0 = tex2D(_MainTex, in_f.texcoord1.xy).w.x;
          u_xlat16_1 = (u_xlat10_0 + (-_Cutoff));
          if((u_xlat16_1<0))
          {
              u_xlatb0 = 1;
          }
          else
          {
              u_xlatb0 = 0;
          }
          if(((int(u_xlatb0) * int(65535))!=0))
          {
              discard;
          }
          out_f.color = float4(0, 0, 0, 0);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
