Shader "Custom/InvertcolorSprite"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {} 
        _Color ("Tint Color", Color) = (1,1,1,1)
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        // İlk pass; renderlama yapmıyor, ama blend modunun çalışması için buffer hazırlar.
        Pass
        {
            ZWrite Off
            ColorMask 0
        }
        
        // İkinci pass; sprite alanında invert efekti uyguluyor.
        Pass
        {
            ZWrite Off
            Blend OneMinusDstColor OneMinusSrcAlpha
            BlendOp Add

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float4 _Color;
            
            struct vertexInput
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
                // SpriteRenderer üzerinden gelen vertex renk bilgisi (alfa için önemli)
                float4 color  : COLOR;
            };
            
            struct fragmentInput
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
                float4 color : COLOR0;
            };
            
            fragmentInput vert(vertexInput i)
            {
                fragmentInput o;
                o.pos = UnityObjectToClipPos(i.vertex);
                o.uv = i.uv;
                // Orijinal vertex rengini _Color ile çarparak (varsayılan 1,1,1,1 olsa da) kullanıyoruz.
                o.color = i.color * _Color;
                return o;
            }
            
            half4 frag(fragmentInput i) : SV_Target
            {
                half4 texCol = tex2D(_MainTex, i.uv);
                // Dokunun alfa değeriyle sprite’nın maskesini elde ediyoruz.
                half finalAlpha = texCol.a * i.color.a;
                // Renk olarak _Color'nun (vertex çarpımı sonucu) RGB'sini, alfa olarak da maskeyi döndürüyoruz.
                // Böylece sadece sprite alanı invert blend ile etkilenecek.
                return half4(i.color.rgb, finalAlpha);
            }
            ENDCG
        }
    }
    FallBack "Sprites/Default"
}
