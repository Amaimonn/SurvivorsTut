using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    public partial struct ProcessDamageThisFrameSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (characterHitPoints, damageBuffer) in SystemAPI.Query<RefRW<CharacterCurrentHitPoints>,
                DynamicBuffer<DamageThisFrame>>())
            {
                if (damageBuffer.IsEmpty)
                    continue;

                foreach (var damage in damageBuffer)
                    characterHitPoints.ValueRW.Value -= damage.Value;

                damageBuffer.Clear();
            }
        }
    }
}