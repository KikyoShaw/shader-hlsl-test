sampler2D input : register(S0);
float amount : register(C0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(input, uv);
    float gray = dot(color.rgb, float3(0.299, 0.587, 0.114));
    color.rgb = lerp(color.rgb, gray.xxx, amount);
    return color;
}