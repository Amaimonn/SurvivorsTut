using Unity.Burst;
using Unity.Entities;

namespace TMG.Survivors
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct DestroyEntitySystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var endEcbSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var endEcbBuffer = endEcbSystem.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (_, entity) in SystemAPI.Query<EnabledRefRO<DestroyEntityFlag>>().WithEntityAccess())
            {
                if (SystemAPI.HasComponent<PlayerTag>(entity))
                    GameUIController.Instance.ShowGameOverUI();
                    
                endEcbBuffer.DestroyEntity(entity);
            }

        }
    }
}