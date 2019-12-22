using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public class PlayerInput : IComponentData
{
    public bool moveUp;
    public bool moveDown;
    public bool moveLeft;
    public bool moveRight;
}
