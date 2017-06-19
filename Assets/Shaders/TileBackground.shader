Shader "TileBackground" {
    Properties {
        _MainTex ("Base (RGB), Alpha (angel)", 2D) = "white" {}
    }
    
    SubShader {
        Tags {"Queue"="Transparent"}
        ZWrite Off
        Blend SrcAlpha One
        Cull Off
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            struct appdata_t {
                float4 vertex : POSITION;
                half4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };
            
            struct v2f {
                float4 position : SV_POSITION;
                float2 texcoord : TEXCOORD;
                half4 color : COLOR;
            };
            
            v2f vert (appdata_t v) {
                v2f o;
                o.position = v.vertex;
                o.texcoord = v.texcoord;
                o.color = v.color;
                return o;
            }
            
            uniform sampler2D _MainTex;

            fixed4 frag (v2f IN) : COLOR {
                fixed4 mainTex = tex2D(_MainTex, IN.texcoord);
                return mainTex * IN.color;
            }
            ENDCG
        }
    }
}