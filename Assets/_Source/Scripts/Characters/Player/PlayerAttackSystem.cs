using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace TMG.Survivors
{
    [BurstCompile]
    public partial struct PlayerAttackSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PhysicsWorldSingleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            var ecbBeginInitializationSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecbBeginSimulation = ecbBeginInitializationSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            var physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

            foreach (var (expirationTimestamp, attackData, transform) in SystemAPI.Query<RefRW<PlayerCooldownExpirationTimestamp>,
                RefRO<PlayerAttackData>, RefRO<LocalTransform>>())
            {
                if (expirationTimestamp.ValueRO.Value > elapsedTime)
                    continue;

                var spawnPosition = transform.ValueRO.Position;
                var minDistance = spawnPosition - attackData.ValueRO.DetectionSize;
                var maxDistance = spawnPosition + attackData.ValueRO.DetectionSize;
                var aaBbInput = new OverlapAabbInput
                {
                    Aabb = new Aabb()
                    {
                        Min = minDistance,
                        Max = maxDistance
                    },
                    Filter = attackData.ValueRO.AttackFilter
                };

                var overlapHits = new NativeList<int>(state.WorldUpdateAllocator);
                if (!physicsWorldSingleton.OverlapAabb(aaBbInput, ref overlapHits))
                {
                    continue;
                }

                var minDistanceToPlayer = float.MaxValue;
                var closestEnemyPosition = float3.zero;

                foreach (var hit in overlapHits)
                {
                    var enemyPosition = physicsWorldSingleton.Bodies[hit].WorldFromBody.pos;
                    var currentDistanceToPlayer = math.distancesq(spawnPosition.xy, enemyPosition.xy);
                    if (currentDistanceToPlayer < minDistanceToPlayer)
                    {
                        minDistanceToPlayer = currentDistanceToPlayer;
                        closestEnemyPosition = enemyPosition;
                    }
                }

                var vectorToEnemy = closestEnemyPosition - spawnPosition;
                var angleToEnemy = math.atan2(vectorToEnemy.y, vectorToEnemy.x);
                var attackOrientation = quaternion.Euler(0, 0, angleToEnemy);

                var attackEntity = ecbBeginSimulation.Instantiate(attackData.ValueRO.AttackPrefab);

                ecbBeginSimulation.SetComponent(attackEntity, LocalTransform.FromPositionRotation(spawnPosition, attackOrientation));

                expirationTimestamp.ValueRW.Value = elapsedTime + attackData.ValueRO.Cooldown;
            }
        }
    }
}