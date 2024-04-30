
using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public sealed class MouseMovToRotConvertersSelector:ModuleSelector<IMouseMovingToRotationConverter>,
        IMouseMovingToRotationConverter
    {
        protected override void SubscribeOnModulesEvents(IMouseMovingToRotationConverter module)
        { }
        protected override void UnsubscribeFromModuleEvents(IMouseMovingToRotationConverter module)
        { }

        public Vector2 GetRotation(Vector2 input) =>
            CurrentModule_ != null ? CurrentModule_.GetRotation(input) : Vector2.zero;
    }
}
