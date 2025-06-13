struct GrassData
{
    float3 position;
    float3 normal;
    float random;
};

GrassData GetComputeData(float3 positionWS)
{
    GrassData data;
    data.position = positionWS;
    data.normal = float3(0, 1, 0);
    data.random = frac(sin(dot(positionWS.xy, float2(12.9898,78.233))) * 43758.5453);
    return data;
}
