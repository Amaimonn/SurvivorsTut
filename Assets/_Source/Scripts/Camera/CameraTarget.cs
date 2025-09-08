using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    public struct CameraTarget : IComponentData
    {
        public UnityObjectRef<Transform> TargetTransform;
    }
}