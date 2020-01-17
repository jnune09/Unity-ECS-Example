using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
[InternalBufferCapacity(8)]
public struct Inventory : IBufferElementData
{
    public int Id;
    public int Count;
}
