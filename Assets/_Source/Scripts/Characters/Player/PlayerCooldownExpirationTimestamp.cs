using Unity.Entities;

namespace TMG.Survivors
{
    public struct PlayerCooldownExpirationTimestamp : IComponentData
    {
        public double Value;
    }
}