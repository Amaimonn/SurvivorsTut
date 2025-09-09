using Unity.Entities;

namespace TMG.Survivors
{
    public struct EnemyCooldownExpirationTimestep : IComponentData, IEnableableComponent
    {
        public double Value;
    }
}