


using System;
using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public sealed class CameraRotationModulesSelector:ModuleSelector<ICameraViewRotationModule>,
        ICameraViewRotationModule
    {
        public event Action<Vector3, float> ChangeViewRotationEvent = delegate { };
        public event Action<Vector3, Vector2> ChangeViewDirectionEvent = delegate { };

        protected override void SubscribeOnModulesEvents(ICameraViewRotationModule module)
        {
            module.ChangeViewRotationEvent += ExecuteEvent_ChangeViewRotation;
            module.ChangeViewDirectionEvent += ExecuteEvent_ChangeViewDirectionEvent;
        }
        protected override void UnsubscribeFromModuleEvents(ICameraViewRotationModule module)
        {
            module.ChangeViewRotationEvent -= ExecuteEvent_ChangeViewRotation;
            module.ChangeViewDirectionEvent -= ExecuteEvent_ChangeViewDirectionEvent;
        }

        private void ExecuteEvent_ChangeViewRotation(Vector3 i,float j)
        {
            ChangeViewRotationEvent(i,j);
        }
        private void ExecuteEvent_ChangeViewDirectionEvent(Vector3 i,Vector2 j)
        {
            ChangeViewDirectionEvent(i, j);
        }

        public void Rotate(Vector3 rotation) => CurrentModule_.Rotate(rotation);
        public void SetRotation(Vector3 rotation) => CurrentModule_.SetRotation(rotation);
        public void SetXRotation(float rotation)=>CurrentModule_.SetXRotation(rotation);
        public void SetYRotation(float rotation) => CurrentModule_.SetYRotation(rotation);
        public void SetZRotation(float rotation) => CurrentModule_.SetZRotation(rotation);

        public Vector3 CurrentViewDirection_ =>
            CurrentModule_ != null ? CurrentModule_.CurrentViewDirection_ : Vector3.zero;
        public Vector2 CurrentHorizontalViewDirection_ =>
            CurrentModule_!=null?CurrentModule_.CurrentHorizontalViewDirection_ : Vector2.zero;
    }
}
