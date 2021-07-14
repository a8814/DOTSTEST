using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheTest : MonoBehaviour
{
    int[,] array = new int[2000,1000];
    float time;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 2000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                array[i, j] = 10;
            }
        }
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        FuncA();
        sw.Stop();
        Debug.Log(string.Format("FuncA执行了:{0} ms", sw.ElapsedMilliseconds));

        sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        FuncB();
        sw.Stop();
        Debug.Log(string.Format("FuncB执行了:{0} ms", sw.ElapsedMilliseconds));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int FuncA()
    {
        int sum = 0;
        for(int i = 0;i<2000;i++)
        {
            for(int j = 0;j<1000;j++)
            {
                sum += array[i, j];
            }
        }
        return sum;
    }

    int FuncB()
    {
        int sum = 0;
        for (int j = 0; j < 1000; j++)
        {
            for (int i = 0; i < 2000; i++)
            {
                sum += array[i, j];
            }
        }
        return sum;
    }
}
