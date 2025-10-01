using Unity.Entities;

namespace TMG.Survivors
{
    public struct LifetimeExpirationData : IComponentData, IEnableableComponent
    {
        public double ExpirationTime;
    }
}