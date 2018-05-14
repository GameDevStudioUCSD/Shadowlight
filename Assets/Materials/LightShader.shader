Shader "Custom/LightShader"
{
	SubShader
	{
		Tags {
			"RenderType" = "Opaque"
			"Queue" = "Geometry"
		}
		LOD 200

        // Do not output any color.
		ColorMask 0
		// Do not write z buffer.
		ZWrite Off

		// Always write value of 255 to stencil buffer.
		Stencil {
			Ref 255
			Comp always
			Pass replace
		}

		// Simple shader that just transforms the position and colors it white.
		Pass
		{
			CGPROGRAM
			// Indicate function `vert` is the vertex shader.
			#pragma vertex vert
			// Indicate that function `frag` is the fragment shader.
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			// Takes in position as parameter and outputs the screen space position.
			float4 vert(float4 v : POSITION) : SV_POSITION {
				return UnityObjectToClipPos(v);
			}
			
			// Just output color of white.
			float4 frag() : SV_Target {
				return float4(1.0f, 1.0f, 1.0f, 1.0f);
			}
			ENDCG
		}
	}
}
