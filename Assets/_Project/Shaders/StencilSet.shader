Shader "Custom/StencilSet"
{
    Properties
    {
        [IntRange] _StencilID ("Stencil ID", Range(0, 255)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Blend Zero One
            ZWrite Off

            // Stencil block here.
            Stencil
            {
                Ref [_StencilID]
                Comp Always
                Pass Replace
                Fail Keep
            }
        }
    }
}
