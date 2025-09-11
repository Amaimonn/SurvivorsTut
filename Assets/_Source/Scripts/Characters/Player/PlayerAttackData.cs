using Unity.Entities;

namespace TMG.Survivors
{
    public struct PlayerAttackData : IComponentData
    {
        public Entity AttackPrefab;
        public float Cooldown;
    }
}