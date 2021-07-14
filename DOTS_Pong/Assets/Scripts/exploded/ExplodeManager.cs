using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UI;

public class ExplodeManager : MonoBehaviour
{
    private EntityManager manager;
    //缓存类
    private BlobAssetStore blobAssetStore;
    private GameObjectConversionSettings settings;
    private System.Random rd;
    private float3 velocity;
    private float3 pos;

    public int amount = 100;
    public float cellSize = 0.1f;
    public int lifeTime = 5;
    public GameObject spwanObj;
    public GameObject cube;
    public Button button;

    public static ExplodeManager Inst;

    // Start is called before the first frame update
    void Start()
    {
        ExplodeManager.Inst = this;
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        blobAssetStore = new BlobAssetStore();
        settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
        rd = new System.Random();
        button.onClick.AddListener(Onclick); 
    }

    public void Onclick()
    {
        ExplodeManager.Inst.Explode(cube);
    }

    public void Explode(GameObject target)
    {
       

      
        var targetECS = GameObjectConversionUtility.ConvertGameObjectHierarchy(target, settings);

        //创建数组
        NativeArray<Entity> many = new NativeArray<Entity>(amount, Allocator.Temp);
        manager.AddComponentData(targetECS, new LifeTimeToDestroyData { restTime = lifeTime });
        manager.Instantiate(targetECS, many);
        for(int i = 0;i<many.Length;i++)
        {
            pos.x = spwanObj.transform.position.x + rd.Next(-1, 1);
            pos.y = spwanObj.transform.position.y + rd.Next(-1, 1);
            pos.z = spwanObj.transform.position.z + rd.Next(-1, 1);
            velocity.x = rd.Next(-10, 10);
            velocity.y = rd.Next(0, 10);
            velocity.z = rd.Next(-10, 10);
            manager.SetComponentData(many[i], new Translation { Value = pos });
            manager.AddComponentData(many[i], new PhysicsVelocity { Linear = velocity });
        }
        manager.DestroyEntity(targetECS);
        ////销毁数组
        many.Dispose();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        blobAssetStore.Dispose();
    }
}
