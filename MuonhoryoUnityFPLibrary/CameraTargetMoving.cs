
using System;
using UnityEngine;
using MuonhoryoLibrary.Unity.COM;

namespace MuonhoryoLibrary.Unity
{
    public sealed class CameraTargetMoving : MonoBehaviour, IActiveModule
    {
        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };

        [SerializeField] private Transform Target;

        [SerializeField] private Vector3 DefaultLocalOffset;
        [SerializeField] private Vector3 DefaultGlobalOffset;

        public CompositeVector3 LocalOffset_ { get; private set; }
        public CompositeVector3 GlobalOffset_ { get; private set; }

        [SerializeField] private bool IsActive = false;

        public bool IsActive_
        {
            get => IsActive;
            set
            {
                if (IsActive != value)
                {
                    IsActive = value;
                    enabled = value;
                    if (IsActive)
                        ActivateModuleEvent.Invoke();
                    else
                        DeactivateModuleEvent.Invoke();
                }
            }
        }

        private void Awake()
        {
            LocalOffset_ = new CompositeVector3(DefaultLocalOffset);
            GlobalOffset_ = new CompositeVector3(DefaultGlobalOffset);

            if (IsActive && Target != null)
                IsActive_ = true;
            else
                IsActive_ = false;
        }

        private void LateUpdate()
        {
            if (Target == null)
                IsActive_ = false;

            Vector3 offset = (Vector3)LocalOffset_;

            transform.position = Target.position + (Vector3)GlobalOffset_ +
                transform.right * offset.x +
                transform.up * offset.y +
                transform.forward * offset.z;
        }

        bool IActiveModule.IsActive { get => IsActive_; set => IsActive_ = value; }
    }
}
