using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    [RequireComponent(typeof(CharacherAuthoring))]
    public class EnemyAuthoring : MonoBehaviour
    {
        private class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<EnemyTag>(entity);
                // AddComponent<AnimationIndexOverride>(entity);
            }
        }
    }
}