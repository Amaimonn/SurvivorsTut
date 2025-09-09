using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    public partial struct EnemyAttackSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            foreach (var (currentCooldown, isOnCooldown) in SystemAPI.Query<RefRW<EnemyCooldownExpirationTimestep>,
                EnabledRefRW<EnemyCooldownExpirationTimestep>>())
            {
                if (elapsedTime < currentCooldown.ValueRO.Value)
                    continue;

                isOnCooldown.ValueRW = false;
            }

            foreach (var ct in SystemAPI.Query<RefRO<EnemyCooldownExpirationTimestep>>()
                .WithDisabled<EnemyCooldownExpirationTimestep>())
            {

            }
        }
    }
}