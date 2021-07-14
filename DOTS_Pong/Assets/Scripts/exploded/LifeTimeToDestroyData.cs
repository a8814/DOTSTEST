using Unity.Entities;

[GenerateAuthoringComponent]
public struct LifeTimeToDestroyData : IComponentData
{
    public float restTime;
}