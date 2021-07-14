using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Burst;

//public class RotateSystem : JobComponentSystem
//{
//    [BurstCompile]
//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        var job = new RotateJob { deltaTime = Time.DeltaTime };
//        return job.Schedule(this, inputDeps);
//    }

//    private struct RotateJob : IJobForEach<RotationEulerXYZ, Rotate>
//    {
//        public float deltaTime;
//        public void Execute(ref RotationEulerXYZ euler, ref Rotate rotate)
//        {
//            euler.Value.y += rotate.ratiansPerSecond * deltaTime;
//            euler.Value.x += rotate.ratiansPerSecond * deltaTime;
//        }
//    }
//}

public class RotateSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Rotate rotate, ref RotationEulerXYZ euler) =>
        {
            euler.Value.y += rotate.ratiansPerSecond * Time.DeltaTime;
        });
    }
}