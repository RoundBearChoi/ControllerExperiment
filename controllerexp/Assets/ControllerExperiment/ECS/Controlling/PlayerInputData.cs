using Unity.Entities;
using Unity.Mathematics;

namespace ControllerExperiment
{
    public struct PlayerInputData : IComponentData
    {
        public quaternion TargetRotationDelta;
        public bool W;
        public bool A;
        public bool S;
        public bool D;
    }
}
