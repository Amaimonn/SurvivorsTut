using Unity.Entities;
using Unity.Transforms;

namespace TMG.Survivors
{
    public partial struct EnemyMoveToPlayerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var player = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerPosition = SystemAPI.GetComponent<LocalTransform>(player).Position.xy;
            var moveToPlaterJob = new EnemyMoveToPlayerJob { PlayerPosition = playerPosition };

            state.Dependency = moveToPlaterJob.ScheduleParallel(state.Dependency);
        }
    }
}