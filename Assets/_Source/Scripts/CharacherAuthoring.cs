using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    public class CharacherAuthoring : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        private class Baker : Baker<CharacherAuthoring>
        {
            public override void Bake(CharacherAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<InitCharacterFlag>(entity);
                AddComponent<CharacterMoveDirection>(entity);
                AddComponent(entity, new CharacterMoveSpeed { Value = authoring._moveSpeed });
            }
        }
    }
}
