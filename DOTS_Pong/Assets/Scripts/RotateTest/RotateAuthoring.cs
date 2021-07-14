using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class RotateAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
   [SerializeField]
   private float degresPerSecond;

   public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
   {
       dstManager.AddComponentData(entity, new Rotate { ratiansPerSecond = math.radians(degresPerSecond) });
       dstManager.AddComponentData(entity, new RotationEulerXYZ());
   }
}