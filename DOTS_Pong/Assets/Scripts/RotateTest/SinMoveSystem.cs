using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Burst;
using UnityEngine;
using Unity.Mathematics;

public class SinMoveSystem : JobComponentSystem
{
    [BurstCompile]
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new SinMoveJob { deltaTime = Time.DeltaTime };
        return job.Schedule(this, inputDeps);
    }

    private struct SinMoveJob : IJobForEach<SinMove, Translation>
    {
        public float deltaTime;
        public void Execute(ref SinMove sinMove, ref Translation translation)
        {
            sinMove.angel = sinMove.angel + deltaTime * sinMove.moveSpeed;
            translation.Value.y = sinMove.startY + Mathf.Sin(Mathf.Deg2Rad * sinMove.angel) * 10;
        }
    }
}

//public class SinMoveSystem : ComponentSystem
//{
//    protected override void OnUpdate()
//    {
//        Entities.ForEach((ref SinMove sinMove, ref Translation translation) =>
//        {
//            sinMove.angel = sinMove.angel + Time.DeltaTime * sinMove.moveSpeed;
//            translation.Value.y = sinMove.startY + Mathf.Sin(Mathf.Deg2Rad * sinMove.angel) * 10;
//        });
//    }
//}`