using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct Direction : IComponentData
{
    public float3 Value;
}
