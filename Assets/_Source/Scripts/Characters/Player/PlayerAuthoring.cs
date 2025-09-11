using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] GameObject _attackPrefab;
        [SerializeField] float _attackCooldown;

        private class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PlayerTag>(entity);
                AddComponent<InitCameraTargetTag>(entity);
                AddComponent<CameraTarget>(entity);
                AddComponent<AnimationIndexOverride>(entity);
                AddComponent(entity, new PlayerAttackData()
                {
                    AttackPrefab = GetEntity(authoring._attackPrefab, TransformUsageFlags.Dynamic),
                    Cooldown = authoring._attackCooldown
                });
                AddComponent<PlayerCooldownExpirationTimestamp>(entity);
            }
        }
    }
}
