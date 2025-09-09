using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace TMG.Survivors
{
    public struct EnemyAttackJob : ICollisionEventsJob
    {
        [ReadOnly] public ComponentLookup<PlayerTag> PlayerLookup;
        [ReadOnly] public ComponentLookup<EnemyAttackData> EnemyAttackLookup;

        public void Execute(CollisionEvent collisionEvent)
        {
        }
    }
}