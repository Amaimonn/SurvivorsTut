using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    public class PlasmaBlastAuthoring : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _damage;

        private class Baker : Baker<PlasmaBlastAuthoring>
        {
            public override void Bake(PlasmaBlastAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlasmaBlastData() { MoveSpeed = authoring._moveSpeed, Damage = authoring._damage });
                AddComponent<DestroyEntityFlag>(entity);
                SetComponentEnabled<DestroyEntityFlag>(entity, false);
            }
        }
    }
}