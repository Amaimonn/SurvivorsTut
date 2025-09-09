using Unity.Entities;

namespace TMG.Survivors
{
    public struct EnemyAttackData : IComponentData
    {
        public int Damage;
        public float Cooldown;
    }
}