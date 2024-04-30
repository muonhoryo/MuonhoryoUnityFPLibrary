
using UnityEngine;
using MuonhoryoLibrary.Unity.COM;
using System;

namespace MuonhoryoLibrary.Unity
{
    public sealed class MouseInputHandler : MonoBehaviour, IActiveModule
    {
        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };

        [SerializeField] private MonoBehaviour CameraViewModule;
        [SerializeField] private MonoBehaviour MouseInputConverter;

        private ICameraViewRotationModule ParsedCameraViewModule;
        private IMouseMovingToRotationConverter ParsedMouseInputConverter;

        [SerializeField] private string InputName_VerticalAxis;
        [SerializeField] private string InputName_HorizontalAxis;

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

        private void OnEnable()
        {
            if (!IsActive)
                enabled = false;
        }
        private void OnDisable()
        {
            if (IsActive)
                enabled = true;
        }
        private void Awake()
        {
            ParsedCameraViewModule = CameraViewModule as ICameraViewRotationModule;
            if (ParsedCameraViewModule == null)
                throw new NullReferenceException("Missing CameraViewModule.");
            ParsedMouseInputConverter = MouseInputConverter as IMouseMovingToRotationConverter;
            if (ParsedMouseInputConverter == null)
                throw new NullReferenceException("Missing MouseInputConverter.");

            if (string.IsNullOrEmpty(InputName_HorizontalAxis))
                throw new NullReferenceException("Missing InputName - Horizontal axis.");
            if (string.IsNullOrEmpty(InputName_VerticalAxis))
                throw new NullReferenceException("Missing InputName - Vertical axis.");

            enabled = IsActive;
        }
        private void LateUpdate()
        {
            Vector2 input = new Vector2(
                Input.GetAxisRaw(InputName_HorizontalAxis),
                Input.GetAxisRaw(InputName_VerticalAxis));
            if (input != Vector2.zero)
            {
                Vector2 rotation = ParsedMouseInputConverter.GetRotation(input);
                if (rotation != Vector2.zero)
                {
                    ParsedCameraViewModule.Rotate(rotation);
                }
            }
        }

        bool IActiveModule.IsActive { get => IsActive_; set => IsActive_ = value; }
    }
}
