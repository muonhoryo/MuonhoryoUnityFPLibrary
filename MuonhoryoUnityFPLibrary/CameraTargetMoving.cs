
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
        [SerializeField] private bool IsActiveOnAwake = false;

        [SerializeField] private MonoBehaviour LocalOffsetProvider;
        [SerializeField] private MonoBehaviour GlobalOffsetProvider;

        private IConstProvider<Vector3> ParsedLocalOffsetProvider;
        private IConstProvider<Vector3> ParsedGlobalOffsetProvider;
        public Vector3 LocalOffset_ => ParsedLocalOffsetProvider.GetValue();
        public Vector3 GlobalOffset_ => ParsedGlobalOffsetProvider.GetValue();

        private bool IsActive = false;
        private bool IsInitialized = false;

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

        private void OnEnable()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                IsActive_ = true;
            }

            if (!IsActive_)
                enabled = false;
        }
        private void OnDisable()
        {
            if (IsActive_)
                enabled = true;
        }
        private void Awake()
        {
            ParsedLocalOffsetProvider = LocalOffsetProvider as IConstProvider<Vector3>;
            if (ParsedLocalOffsetProvider == null)
                throw new NullReferenceException("Missing LocalOffsetProvider.");

            ParsedGlobalOffsetProvider = GlobalOffsetProvider as IConstProvider<Vector3>;
            if (ParsedGlobalOffsetProvider == null)
                throw new NullReferenceException("Missing GlobalOffsetProvider.");

            if (!IsActiveOnAwake || Target == null)
            {
                IsInitialized = true;
                enabled = false;
            }
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
