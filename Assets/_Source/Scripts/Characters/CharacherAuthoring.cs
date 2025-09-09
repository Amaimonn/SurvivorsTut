using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    public class CharacherAuthoring : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField, Min(0)] private int _hitPoints;

        private class Baker : Baker<CharacherAuthoring>
        {
            public override void Bake(CharacherAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<InitCharacterFlag>(entity);
                AddComponent<CharacterMoveDirection>(entity);
                AddComponent(entity, new CharacterMoveSpeed { Value = authoring._moveSpeed });
                AddComponent(entity, new FacingDirectionOverride { Value = 1 });
                AddComponent(entity, new CharacterMaxHitPoints { Value = authoring._hitPoints });
                AddComponent(entity, new CharacterCurrentHitPoints { Value = authoring._hitPoints });
                AddBuffer<DamageThisFrame>(entity);
            }
        }
    }
}
