


using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public sealed class RotationLimitersSelector:ModuleSelector<IRotationLimiter>,IRotationLimiter
    {
        protected override void SubscribeOnModulesEvents(IRotationLimiter module) { }
        protected override void UnsubscribeFromModuleEvents(IRotationLimiter module) { }

        public Vector3 GetLimitedRotation(Vector3 rotation) =>
            CurrentModule_ != null ? CurrentModule_.GetLimitedRotation(rotation) : rotation;
    }
}
