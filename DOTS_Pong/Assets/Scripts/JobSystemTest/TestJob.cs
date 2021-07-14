using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct TestJob : IJob
{
    [ReadOnly] public float a;
    [ReadOnly] public float b;
    [WriteOnly] public NativeArray<float> result;

    public void Execute()
    {
        result[0] = a + b;
    }
}
