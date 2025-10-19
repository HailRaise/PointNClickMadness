Shader "Custom/CameraCornerMask"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Radius  ("Corner Radius", Range(0, 0.5)) = 0.1
        _Smooth  ("Edge Smoothness", Range(0, 0.5)) = 0.02
        _Color   ("Background Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Radius;
            float _Smooth;
            float4 _Color;

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;
                float2 dist = min(uv, 1 - uv);
                float d = min(dist.x, dist.y);
                float mask = smoothstep(_Radius, _Radius + _Smooth, d);
                fixed4 col = tex2D(_MainTex, uv);
                return lerp(_Color, col, mask);
            }
            ENDCG
        }
    }
}
