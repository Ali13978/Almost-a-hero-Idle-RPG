Shader "Spine/Special/HiddenPass"
{
  Properties
  {
  }
  SubShader
  {
    Tags
    { 
      "QUEUE" = "Geometry-1"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "QUEUE" = "Geometry-1"
      }
      ZWrite Off
      Fog
      { 
        Mode  Off
      } 
      ColorMask 0
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      struct appdata_t
      {
          float3 vertex :POSITION0;
          float4 color :COLOR0;
      };
      
      struct OUT_Data_Vert
      {
          float4 color :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 color :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          out_v.color = in_v.color;
          out_v.color = clamp(out_v.color, 0, 1);
          out_v.vertex = UnityObjectToClipPos(float4(in_v.vertex, 0));
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          out_f.color = in_f.color;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
