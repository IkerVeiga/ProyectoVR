Shader "Custom/Read" {
    Properties {
        _Color ("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _Smoothness ("Smoothness", Range(0, 1)) = 0
        _Metallic ("Metalness", Range(0, 1)) = 0
        [HDR] _Emission ("Emission", color) = (0,0,0)

        [IntRange(0, 255)] _StencilRef ("Stencil Reference Value", Range(0,255)) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

        //stencil operation
        Stencil {
            Ref [_StencilRef]
            Comp Equal
        }

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;

            half _Smoothness;
            half _Metallic;
            half3 _Emission;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _Color;
                col.rgb *= _Metallic;
                col.a *= _Smoothness;
                col.rgb += _Emission;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Standard"
}
