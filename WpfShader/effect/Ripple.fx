sampler2D Input : register(S0);
float2 MousePosition : register(C0);
float Decay : register(C1);
float Damping : register(C2);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float2 delta = uv - MousePosition;
    float distance = length(delta);
    float time = distance * Decay;

    if (time == 0)
        time = 0.001;

    float2 ripple = delta / distance * sin(time * 20) * exp(-time * 4) * Damping;

    float4 color = tex2D(Input, uv + ripple * 0.03);
    return color;
}