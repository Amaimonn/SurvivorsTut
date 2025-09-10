using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace TMG.Survivors
{
    [BurstCompile]
    public struct EnemyAttackJob : ICollisionEventsJob
    {
        [ReadOnly] public ComponentLookup<PlayerTag> PlayerLookup;
        [ReadOnly] public ComponentLookup<EnemyAttackData> EnemyAttackLookup;
        public ComponentLookup<EnemyCooldownExpirationTimestep> EnemyCooldownLookup;
        public BufferLookup<DamageThisFrame> DamageThisFrameLookup;
        public double ElapsedTime;

        public void Execute(CollisionEvent collisionEvent)
        {
            Entity playerEntity;
            Entity enemyEntity;

            if (PlayerLookup.HasComponent(collisionEvent.EntityA) && EnemyAttackLookup.HasComponent(collisionEvent.EntityB))
            {
                playerEntity = collisionEvent.EntityA;
                enemyEntity = collisionEvent.EntityB;
            }
            else if (PlayerLookup.HasComponent(collisionEvent.EntityB) && EnemyAttackLookup.HasComponent(collisionEvent.EntityA))
            {
                playerEntity = collisionEvent.EntityB;
                enemyEntity = collisionEvent.EntityA;
            }
            else
            {
                return;
            }

            if (EnemyCooldownLookup.IsComponentEnabled(enemyEntity))
                return;

            var attackData = EnemyAttackLookup[enemyEntity];
            EnemyCooldownLookup[enemyEntity] = new EnemyCooldownExpirationTimestep() { Value = ElapsedTime + attackData.Cooldown };
            EnemyCooldownLookup.SetComponentEnabled(enemyEntity, true);

            var playerDamageBuffer = DamageThisFrameLookup[playerEntity];
            playerDamageBuffer.Add(new DamageThisFrame() { Value = attackData.Damage });
        }
    }
}