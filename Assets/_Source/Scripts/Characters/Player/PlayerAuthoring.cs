using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

namespace TMG.Survivors
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] GameObject _attackPrefab;
        [SerializeField] float _attackCooldown;
        [SerializeField] float _detectionSize;

        private class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PlayerTag>(entity);
                AddComponent<InitCameraTargetTag>(entity);
                AddComponent<CameraTarget>(entity);
                AddComponent<AnimationIndexOverride>(entity);

                var enemyLayer = LayerMask.NameToLayer("Enemy");
                var enemyLayerMask = (uint)math.pow(2, enemyLayer);
                var attackCollisionFilter = new CollisionFilter()
                {
                    BelongsTo = uint.MaxValue,
                    CollidesWith = enemyLayerMask,
                };

                AddComponent(entity, new PlayerAttackData()
                {
                    AttackPrefab = GetEntity(authoring._attackPrefab, TransformUsageFlags.Dynamic),
                    Cooldown = authoring._attackCooldown,
                    DetectionSize = new float3(authoring._detectionSize),
                    AttackFilter = attackCollisionFilter,
                });
                AddComponent<PlayerCooldownExpirationTimestamp>(entity);
            }
        }
    }
}
