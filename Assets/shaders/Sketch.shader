Shader "Custom/Sketch"
{
    Properties
    {
        _MainTex("Base Texture", 2D) = "white" {}
        _PaperTex("Paper Texture", 2D) = "white" {}
        _PaperSpeed("Paper Speed", Float) = 1.0
        _PaperIntensity("Paper Intensity", Range(0, 1)) = 0.1
        _StrokeIntensity("Stroke Intensity", Range(0, 1)) = 0.5
        _ShadeIntensity("Shade Intensity", Range(0, 1)) = 0.5
        _PaperSpeedFrequency("Paper Speed Frequency", Float) = 1.0
        _PaperSpeedAmplitude("Paper Speed Amplitude", Float) = 0.5
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows

            sampler2D _MainTex;
            sampler2D _PaperTex;
            float _PaperSpeed;
            float _PaperIntensity;
            float _StrokeIntensity;
            float _ShadeIntensity;
            float _PaperSpeedFrequency;
            float _PaperSpeedAmplitude;

            struct Input
            {
                float2 uv_MainTex;
                float time; // Add time variable
            };

            // Pseudo-random number generator function
            float PseudoRandom(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898,78.233))) * 43758.5453);
            }

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                // Calculate paper speed using a sine wave
                float paperSpeed = _PaperSpeed + sin(IN.time * _PaperSpeedFrequency) * _PaperSpeedAmplitude;

                // Sample the base texture
                fixed4 baseColor = tex2D(_MainTex, IN.uv_MainTex);

                // Sample the paper texture
                float2 paperUV = IN.uv_MainTex + float2(sin(IN.time * paperSpeed), cos(IN.time * paperSpeed)) * _PaperIntensity;
                fixed4 paperColor = tex2D(_PaperTex, paperUV);

                // Apply stroke effect by adding a noise texture
                float stroke = smoothstep(0.5 - _StrokeIntensity, 0.5 + _StrokeIntensity, PseudoRandom(IN.uv_MainTex));
                baseColor.rgb += stroke * 0.1; // Adjust stroke intensity as needed

                // Apply shade effect by darkening the color
                baseColor.rgb *= 1.0 - _ShadeIntensity;

                // Combine base color with paper texture for a textured appearance
                baseColor = lerp(baseColor, paperColor, paperColor.a);

                o.Albedo = baseColor.rgb;
                o.Alpha = baseColor.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
