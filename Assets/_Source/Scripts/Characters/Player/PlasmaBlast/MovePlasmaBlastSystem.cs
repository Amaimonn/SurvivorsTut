using Unity.Entities;
using Unity.Transforms;

namespace TMG.Survivors
{
    public partial struct MovePlasmaBlastSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (blastTransform, blastData) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<PlasmaBlastData>>())
            {
                blastTransform.ValueRW.Position += blastData.ValueRO.MoveSpeed * deltaTime * blastTransform.ValueRO.Right();
            }
        }
    }
}