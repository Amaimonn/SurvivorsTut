using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace TMG.Survivors
{
    [BurstCompile]
    public partial struct PlasmaBlastAttackJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<PlasmaBlastData> PlasmaBlastDataLookup;
        [ReadOnly] public ComponentLookup<EnemyTag> EnemyTagLookup;
        public ComponentLookup<DestroyEntityFlag> DestroyEntityFlagLookup;
        public BufferLookup<DamageThisFrame> DamageBufferLookup;

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity blastEntity;
            Entity enemyEntity;
            if (PlasmaBlastDataLookup.HasComponent(triggerEvent.EntityA) && EnemyTagLookup.HasComponent(triggerEvent.EntityB))
            {
                blastEntity = triggerEvent.EntityA;
                enemyEntity = triggerEvent.EntityB;
            }
            else if (PlasmaBlastDataLookup.HasComponent(triggerEvent.EntityB) && EnemyTagLookup.HasComponent(triggerEvent.EntityA))
            {
                blastEntity = triggerEvent.EntityB;
                enemyEntity = triggerEvent.EntityA;
            }
            else
            {
                return;
            }

            var blastData = PlasmaBlastDataLookup[blastEntity];
            var damageBuffer = DamageBufferLookup[enemyEntity];
            damageBuffer.Add(new DamageThisFrame { Value = blastData.Damage });

            DestroyEntityFlagLookup.SetComponentEnabled(blastEntity, true);
        }
    }
}