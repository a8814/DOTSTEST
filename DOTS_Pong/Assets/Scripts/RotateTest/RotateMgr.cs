using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class RotateMgr : MonoBehaviour
{
    public GameObject cube;
    public GameObject cube2;
    private EntityManager manager;
    public int Amount = 3000;
    public bool uesECS = true;
    //缓存类
    private BlobAssetStore blobAssetStore;
    private GameObjectConversionSettings settings;
    void Start()
    {
        if(uesECS)
        {
            float3 pos = new float3(0, 0, 0);
            ///EntityManager管理在一个World中所有的entities
            manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            ///一个提供缓存的类,缓存能让你对象创建时更快
            blobAssetStore = new BlobAssetStore();
            settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
            
            NativeArray<Entity> array = new NativeArray<Entity>(Amount, Allocator.Temp);
            ///将prefab对象转成entity
            var targetECS = GameObjectConversionUtility.ConvertGameObjectHierarchy(cube, settings);
            
            manager.Instantiate(targetECS, array);
            
            for (int i = 0; i < array.Length; i++)
            {
                pos.y = i / 100;
                pos.x = i % 100;
                manager.AddComponentData(array[i], new Rotate { ratiansPerSecond = math.radians(30) });
                manager.AddComponentData(array[i], new RotationEulerXYZ());
                manager.AddComponentData(array[i], new Translation { Value = pos });
                manager.AddComponentData(array[i], new SinMove { startY = pos.y,angel = pos.x * 10, moveSpeed = 100 });
            }

            manager.DestroyEntity(targetECS);
            array.Dispose();
        }
        else
        {
            for (int i = 0; i < Amount; i++)
            {
                var obj = GameObject.Instantiate(cube2);
                obj.transform.position = new Vector3(i % 100, i / 100, 0);
            }
        }
    }

    private void OnDestroy()
    {
        if(uesECS)
            blobAssetStore.Dispose();
    }
}
