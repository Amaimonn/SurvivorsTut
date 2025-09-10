using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    [RequireComponent(typeof(CharacherAuthoring))]
    public class EnemyAuthoring : MonoBehaviour
    {
        [SerializeField] private int _attackDamage;
        [SerializeField] private float _attackCooldown;

        private class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<EnemyTag>(entity);
                AddComponent(entity, new EnemyAttackData
                {
                    Damage = authoring._attackDamage,
                    Cooldown = authoring._attackCooldown
                });
                AddComponent<EnemyCooldownExpirationTimestep>(entity);
                SetComponentEnabled<EnemyCooldownExpirationTimestep>(entity, false);
                // AddComponent<AnimationIndexOverride>(entity);
            }
        }
    }
}