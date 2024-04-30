
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

        [SerializeField] private MonoBehaviour LocalOffsetProvider;
        [SerializeField] private MonoBehaviour GlobalOffsetProvider;

        private IConstProvider<Vector3> ParsedLocalOffsetProvider;
        private IConstProvider<Vector3> ParsedGlobalOffsetProvider;
        public Vector3 LocalOffset_ => ParsedLocalOffsetProvider.GetValue();
        public Vector3 GlobalOffset_ => ParsedGlobalOffsetProvider.GetValue();

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
            ParsedLocalOffsetProvider = LocalOffsetProvider as IConstProvider<Vector3>;
            if (ParsedLocalOffsetProvider == null)
                throw new NullReferenceException("Missing LocalOffsetProvider.");

            ParsedGlobalOffsetProvider = GlobalOffsetProvider as IConstProvider<Vector3>;
            if (ParsedGlobalOffsetProvider == null)
                throw new NullReferenceException("Missing GlobalOffsetProvider.");

            if (IsActive && Target != null)
                IsActive_ = true;
            else
                IsActive_ = false;
        }

        private void LateUpdate()
        {
            if (Target == null)
                IsActive_ = false;

            Vector3 offset = LocalOffset_;

            transform.position = Target.position + GlobalOffset_ +
                transform.right * offset.x +
                transform.up * offset.y +
                transform.forward * offset.z;
        }

        bool IActiveModule.IsActive { get => IsActive_; set => IsActive_ = value; }
    }
}
