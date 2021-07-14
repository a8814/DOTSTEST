using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using Unity.Collections;

public class JobRun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NativeArray<float> result = new NativeArray<float>(10, Allocator.TempJob);
        TestJob jobData = new TestJob();
        jobData.a = 10;
        jobData.b = 10;
        jobData.result = result;

        JobHandle handle = jobData.Schedule();

        handle.Complete();

        float aPlusB = result[0];
        Debug.Log(aPlusB);
        result.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
