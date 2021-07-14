using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EntityPool 
{
    public static EntityPool Inst
    {
        get { 
            if (_inst == null){
                _inst = new EntityPool();
                _inst.Init();
            }
            return _inst;
        }
    }
    private static EntityPool _inst;
    private Dictionary<GameObject,List<Entity>> dic;

    private EntityManager manager;
    //缓存类
    private BlobAssetStore blobAssetStore;
    private GameObjectConversionSettings settings;

    private void Init()
    {
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        blobAssetStore = new BlobAssetStore();
        settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
        dic = new Dictionary<GameObject, List<Entity>>();
    }


    public Entity Get(GameObject prefab)
    {
        List<Entity> list;
        dic.TryGetValue(prefab, out list);
        if (list == null)
            list = new List<Entity>();
        Entity target;
        if(list.Count > 0)
        {
            target = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
        }else
        {
            var targetECS = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, settings);
            target = manager.Instantiate(targetECS);
        }
        return target;
    }

    public void Restore(Entity target,GameObject prefab)
    {
        List<Entity> list;
        dic.TryGetValue(prefab, out list);
        if (list == null)
            list = new List<Entity>();
        list.Add(target);
    }
        
}
