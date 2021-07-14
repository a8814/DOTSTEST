using Unity.Entities;

[GenerateAuthoringComponent]
public struct SinMove : IComponentData
{
    public float angel;
    public float startY;
    public float moveSpeed;
}
