using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace TMG.Survivors
{
    public struct PlayerAttackData : IComponentData
    {
        public Entity AttackPrefab;
        public float Cooldown;
        public float3 DetectionSize;
        public CollisionFilter AttackFilter;
    }
}