Shader "Custom/TopFaceRed"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (1, 0, 0, 1)   // Red
        _SideColor ("Side Color", Color) = (0, 0, 1, 1) // Blue
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD0;
            };
            
            fixed4 _TopColor;
            fixed4 _SideColor;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // Check if the face is the top face (Y-axis pointing up)
                if (i.normal.y > 0.9) 
                    return _TopColor;
                else
                    return _SideColor;
            }
            ENDCG
        }
    }
}
