using Unity.Burst;
using Unity.Entities;

namespace TMG.Survivors
{
    public partial struct ProcessDamageThisFrameSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (characterHitPoints, damageBuffer, entity) in SystemAPI.Query<RefRW<CharacterCurrentHitPoints>,
                DynamicBuffer<DamageThisFrame>>().WithPresent<DestroyEntityFlag>().WithEntityAccess())
            {
                if (damageBuffer.IsEmpty)
                    continue;

                foreach (var damage in damageBuffer)
                    characterHitPoints.ValueRW.Value -= damage.Value;

                damageBuffer.Clear();

                if (characterHitPoints.ValueRO.Value <= 0)
                    SystemAPI.SetComponentEnabled<DestroyEntityFlag>(entity, true);
            }
        }
    }
}