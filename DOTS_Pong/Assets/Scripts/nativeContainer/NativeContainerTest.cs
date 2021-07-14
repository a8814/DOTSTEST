using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Profiling;

public class NativeContainerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void testNativeContainer()
    {
        int length = 1000;
        Profiler.BeginSample("normal Array");
        int[] array = new int[length];
        for(int i = 0;i<length;i++)
        {
            array[i] = i;
        }
        Profiler.EndSample();

        Profiler.BeginSample("native Array");
        NativeArray<int> nativeArray = new NativeArray<int>(length, Allocator.Persistent);
        for (int i = 0; i < length; i++)
        {
            nativeArray[i] = i;
        }
        Profiler.EndSample();
        nativeArray.Dispose();
    }    

    // Update is called once per frame
    void Update()
    {
        testNativeContainer();
    }
}
